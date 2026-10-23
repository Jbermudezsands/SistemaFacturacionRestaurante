# CLAUDE.md

Sistema de facturación para restaurante (punto de venta con mesas, meseros, comandas y facturas). Aplicación de escritorio **Windows Forms en VB.NET, .NET Framework 4.0**, con base de datos **SQL Server**.

## Estructura del repositorio

La raíz tiene una carpeta anidada con el mismo nombre; el código vive en:

```
Sistema Facturacion Restaurante/Menu Software/
  OnMenuProject.sln
  OnMenuProject/        <- proyecto activo (OnMenuProject.vbproj)
  Backup/               <- copia antigua del proyecto; NO editar
  SetupFact/            <- instalador (setup.exe / .msi)
  Menu.txt              <- notas de diseño (Mesas = Clientes con límite de crédito 0, Meseros = Vendedores)
Sistema Facturacion Restaurante/
  ScriptBaseDatos 2014.sql, ScriptBaseDatos 2019.sql, DatosBaseDatosISAKAYA.sql  <- scripts de BD
  SFacturacionRestauranteDemo.bak.bak                                            <- respaldo de BD demo
```

`bin/`, `obj/` y `Backup/` son artefactos o copias: no los modifiques ni los uses como referencia del código vigente.

## Compilación

- Abrir `Menu Software/OnMenuProject.sln` en Visual Studio y compilar (Debug/Release). Plataforma **x86** (`PlatformTarget=x86`).
- Dependencias de terceros referenciadas como DLL: **ComponentOne** (`C1.Win.C1Command/C1Input/C1List/C1TrueDBGrid`) y **ActiveReports 3** (`ActiveReports3`, `ActiveReports.Viewer3`). Los controles C1 usan licencias (`My Project/licenses.licx`).
- No hay pruebas automatizadas.

## Variable global `Conexion`

Definida en `OnMenuProject/Globales.vb` (módulo `Globales`, accesible desde todo el proyecto sin calificar):

| Variable | Tipo | Uso |
|---|---|---|
| `Conexion` | `String` | **Cadena de conexión** a SQL Server. Es la fuente única de verdad. |
| `MiConexion` | `SqlConnection` | Conexión global creada a partir de `Conexion`. |
| `TempConn` | `SqlConnection` | Segunda conexión global creada a partir de `Conexion` (usada en consultas de lectura rápidas). |
| `ConexionAccess` / `RutaBD` | `String` | Cadena OLEDB hacia `CntFacturacion.dll` (un `.mdb` de Access que guarda la tabla `Servidor`). |

Cómo se inicializa (`FormLog.vb`, `FormLog_Load`, el formulario de inicio):

1. Lee la tabla `Servidor` del Access `CntFacturacion.dll` (junto al ejecutable).
2. `Conexion = <fila 0>("Servidor")`
3. `MiConexion = New SqlClient.SqlConnection(Conexion)` y `TempConn = New SqlClient.SqlConnection(Conexion)`

Reglas al trabajar con ella:

- **Usa siempre `Conexion` / `MiConexion` / `TempConn`.** No escribas cadenas de conexión nuevas ni crees conexiones con credenciales hardcodeadas.
- No reasignes `Conexion` fuera de `FormLog_Load`.
- Antes de usar `MiConexion` verifica el estado, como ya se hace en `FrmPago.vb`: `If MiConexion.State <> ConnectionState.Open Then MiConexion.Open()` y ciérrala al terminar (`Finally`).
- Para comandos nuevos prefiere `Using ... SqlCommand(sql, MiConexion)` con **parámetros** (`cmd.Parameters.AddWithValue`) en lugar de concatenar strings.

### Trampas conocidas (no replicar)

- `Globales.vb`: `TempConn` tiene un valor inicial con servidor y credenciales `sa` hardcodeados (`Data Source=DESKTOP-E2SQPU1\SQL2005`). Se sobrescribe en `FormLog_Load`; no depender de él.
- `FormImportar.vb` declara un `Dim Conexion As New SqlConnection(...)` **local de módulo/clase** que oculta la variable global, apuntando a otra BD (`Firefact`). Dentro de ese formulario `Conexion` NO es la global.
- `FrmConsecutivos.vb` declara `Public MiConexion As New SqlClient.SqlConnection(Conexion)` que oculta la global y se evalúa al instanciar el formulario; funciona solo porque `Conexion` ya fue asignada por `FormLog`.
- Muchas consultas concatenan valores en el SQL (riesgo de inyección SQL). No lo repitas en código nuevo.

## Arquitectura

- Formulario de inicio: `FormLog` (login de mesero con contraseña, `My Project/Application.myapp`). Luego `Form1` (pantalla principal de mesas/pedidos).
- `Globales.vb`: variables globales (`Conexion`, `PUsuario`, `PBodega`, `PCliente`, `PVendedor`, `PCajero`, `LogoEmpresa`, `Version`).
- `Funciones.vb`: funciones compartidas (impresoras, consecutivos `BuscaConsecutivo*`, `GenerarNumeroFactura`, `Monto_Pendiente`, `BuscaTasaCambio`, teclado en pantalla `NumeroTecla`/`LetraTecla`, etc.).
- Formularios: `Form1` (principal), `Form2`, `Form3`, `FormCuentas`, `FrmPago` (cobro), `frmConsulta`, `FrmConsecutivos`, `FormImportar`, `FrmTeclado`/`FrmTecladoNumerico` (teclados táctiles), `FrmViewer` (visor de reportes).
- Reportes ActiveReports en `Reportes/` (`ArepComanda`, `ArepFacturasTiras2`).
- Convenciones del dominio: **Mesas** = registros de `Clientes`; **Meseros** = `Vendedores`; las facturas se graban directamente (no como cotización).

## Convenciones de código

- Idioma: nombres y comentarios en **español** (`Monto_Pendiente`, `BuscaConsecutivo`, `Cod_Cliente`); sigue ese estilo.
- Acceso a datos: ADO.NET clásico (`SqlDataAdapter`, `SqlCommand`, `DataTable/DataSet`), sin ORM. Tablas principales vistas: `Facturas`, `vendedores`, `DatosEmpresa`, `Bodegas`, `ConsecutivoSucursales`, `Impuestos`, `Lineas`.
- Archivos de formulario: cada `.vb` tiene su `.Designer.vb` y `.resx`; no edites los `.Designer.vb` a mano salvo necesidad.
- **Codificación**: varios `.vb` (p. ej. `FormLog.vb`) están en Windows-1252, no UTF-8, y contienen caracteres como `ñ`. Si los editas, conserva la codificación (no guardes como UTF-8 sin verificar).
- Al agregar un archivo nuevo, hay que incluirlo en `OnMenuProject.vbproj` (`<Compile Include=...>`), ya que es un proyecto de formato antiguo con lista explícita de archivos.
- Compatibilidad: solo sintaxis y APIs disponibles en **.NET Framework 4.0** (sin `async/await` nativo, sin `String interpolation $""`, sin `?.`).
- Seguridad: no commitear contraseñas ni cadenas de conexión reales de clientes.
