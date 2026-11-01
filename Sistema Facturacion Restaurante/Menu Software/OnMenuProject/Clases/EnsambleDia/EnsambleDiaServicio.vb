Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text

'=====================================================================================================
'  ENSAMBLE DEL DIA - SERVICIO
'  Arma el plan del dia (productos Ensamble facturados y sus componentes) y lo procesa:
'    1. Salida Bodega con los componentes totalizados (descarga de la bodega origen).
'    2. Mercancia Recibida con los productos ensamblados (ingreso a la bodega destino).
'    3. Trazabilidad en Ensamble_Dia / Ensamble_Dia_Producto / Ensamble_Dia_Componente.
'  Todo ocurre en una sola transaccion: o se graba completo o no se graba nada.
'
'  Los costos siguen la misma regla que FrmEnsamble del sistema de facturacion: cada componente sale al
'  costo promedio del kardex a la fecha y el producto ensamblado entra al costo de sus componentes.
'=====================================================================================================
Public Class EnsambleDiaServicio

    Private Const TOLERANCIA As Double = 0.00001

    Private ReadOnly _repositorio As EnsambleDiaRepositorio

    Public Sub New(ByVal conexion As String)
        _conexion = conexion
        _repositorio = New EnsambleDiaRepositorio(conexion)
    End Sub

    Private ReadOnly _conexion As String

#Region "Consultas"

    Public Function ObtenerBodegas() As DataTable
        Return _repositorio.ObtenerBodegas()
    End Function

    Public Function ObtenerProcesosDelDia(ByVal fecha As Date) As DataTable
        Return _repositorio.ObtenerProcesosDelDia(fecha)
    End Function

    Public Function ObtenerDatosEmpresa() As DataTable
        Return _repositorio.ObtenerDatosEmpresa()
    End Function

    ''' <summary>
    ''' Productos Ensamble facturados en la fecha con su receta. La cantidad a ensamblar es lo facturado
    ''' menos lo que ya se proceso ese dia.
    ''' </summary>
    Public Function ConstruirPlan(ByVal fecha As Date) As List(Of EnsambleDiaProducto)
        Dim plan As New List(Of EnsambleDiaProducto)
        Dim ventas As DataTable = _repositorio.ObtenerVentasEnsamble(fecha)

        For Each fila As DataRow In ventas.Rows
            Dim producto As New EnsambleDiaProducto
            producto.Codigo = Convert.ToString(fila("Codigo"))
            producto.Descripcion = Convert.ToString(fila("Descripcion"))
            producto.CodComponente = Numero(fila("CodComponente"))
            producto.Vendida = Numero(fila("Vendida"))
            producto.Procesada = Numero(fila("Procesada"))

            If producto.CodComponente <> 0 Then
                Dim receta As DataTable = _repositorio.ObtenerComponentes(producto.CodComponente)
                For Each filaReceta As DataRow In receta.Rows
                    Dim componente As New EnsambleDiaComponente
                    componente.Codigo = Convert.ToString(filaReceta("Cod_Producto"))
                    componente.Descripcion = Convert.ToString(filaReceta("Descripcion"))
                    componente.TipoProducto = Convert.ToString(filaReceta("Tipo_Producto"))
                    componente.CantidadUnidad = Numero(filaReceta("Requerido"))
                    componente.CantidadTotal = componente.CantidadUnidad * producto.Pendiente
                    producto.Componentes.Add(componente)
                Next
            End If

            plan.Add(producto)
        Next
        Return plan
    End Function

    ''' <summary>
    ''' Tabla plana (un renglon por componente) que alimenta el reporte. Solo incluye lo pendiente de ensamblar;
    ''' un producto sin receta sale con un renglon vacio para que se note.
    ''' </summary>
    Public Function ConstruirTablaReporte(ByVal plan As List(Of EnsambleDiaProducto)) As DataTable
        Dim tabla As New DataTable("EnsambleDia")
        tabla.Columns.Add("Cod_Producto", GetType(String))
        tabla.Columns.Add("Descripcion_Producto", GetType(String))
        tabla.Columns.Add("Vendida", GetType(Double))
        tabla.Columns.Add("Cantidad_Ensamblar", GetType(Double))
        tabla.Columns.Add("Cod_Componente", GetType(String))
        tabla.Columns.Add("Descripcion_Componente", GetType(String))
        tabla.Columns.Add("Cantidad_Unidad", GetType(Double))
        tabla.Columns.Add("Cantidad_Usar", GetType(Double))

        For Each producto As EnsambleDiaProducto In plan
            If producto.Pendiente <= 0 Then Continue For

            If producto.SinReceta Then
                tabla.Rows.Add(producto.Codigo, producto.Descripcion, producto.Vendida, producto.Pendiente, "", "(Sin componentes)", 0.0, 0.0)
                Continue For
            End If

            For Each componente As EnsambleDiaComponente In producto.Componentes
                tabla.Rows.Add(producto.Codigo, producto.Descripcion, producto.Vendida, producto.Pendiente, _
                               componente.Codigo, componente.Descripcion, componente.CantidadUnidad, componente.CantidadTotal)
            Next
        Next
        Return tabla
    End Function

#End Region

#Region "Procesar"

    Public Function Procesar(ByVal solicitud As EnsambleDiaSolicitud) As EnsambleDiaResultado
        If solicitud Is Nothing Then Throw New ArgumentNullException("solicitud")
        If String.IsNullOrEmpty(solicitud.BodegaOrigen) Then
            Throw New EnsambleDiaNegocioException("Seleccione la bodega de la que salen los componentes.")
        End If
        If String.IsNullOrEmpty(solicitud.BodegaDestino) Then
            Throw New EnsambleDiaNegocioException("Seleccione la bodega que recibe los productos ensamblados.")
        End If

        'El plan se vuelve a leer: lo que se procesa es lo que hay en la base en este momento
        Dim plan As List(Of EnsambleDiaProducto) = ConstruirPlan(solicitud.Fecha)
        Dim aProcesar As New List(Of EnsambleDiaProducto)
        Dim resultado As New EnsambleDiaResultado

        For Each producto As EnsambleDiaProducto In plan
            If producto.Pendiente <= 0 Then Continue For
            If producto.SinReceta Then
                resultado.SinReceta.Add(producto.Codigo & " " & producto.Descripcion)
            Else
                aProcesar.Add(producto)
            End If
        Next

        If aProcesar.Count = 0 Then
            Throw New EnsambleDiaNegocioException("No hay productos pendientes de ensamblar para la fecha seleccionada.")
        End If

        Dim cliente As DataRow = _repositorio.ObtenerClienteInventario()
        If cliente Is Nothing Then
            Throw New EnsambleDiaNegocioException("Seleccione una cuenta de cliente para ajustes de inventario.")
        End If
        Dim proveedor As DataRow = _repositorio.ObtenerProveedorInventario()
        If proveedor Is Nothing Then
            Throw New EnsambleDiaNegocioException("Seleccione una cuenta de proveedor para ajustes de inventario.")
        End If

        Dim fecha As Date = solicitud.Fecha.Date
        Dim observaciones As String = "Procesado por Ensamble del Dia " & fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

        Using cn As New SqlConnection(_conexion)
            cn.Open()
            Using tx As SqlTransaction = cn.BeginTransaction()
                Try
                    Dim totales As Dictionary(Of String, EnsambleDiaComponente) = CostearYVerificar(cn, tx, aProcesar, solicitud)

                    '--- Salida de bodega: un renglon por componente con lo que necesitan todos los productos
                    Dim numeroSalida As String = Format(_repositorio.SiguienteConsecutivo(cn, tx, "SalidaBodega"), "0000#")
                    Dim totalSalida As Double = 0
                    For Each componente As EnsambleDiaComponente In totales.Values
                        totalSalida += componente.CostoTotal
                    Next

                    _repositorio.InsertarSalidaBodega(cn, tx, numeroSalida, fecha, Convert.ToString(cliente("Cod_Cliente")), _
                                                      Convert.ToString(cliente("Nombre_Cliente")), solicitud.BodegaOrigen, totalSalida, observaciones)
                    For Each componente As EnsambleDiaComponente In totales.Values
                        _repositorio.InsertarDetalleSalida(cn, tx, numeroSalida, fecha, componente.Codigo, componente.Descripcion, _
                                                           componente.CantidadTotal, componente.CostoUnitario)
                    Next

                    '--- Mercancia recibida: cada producto entra al costo de sus componentes
                    Dim costos As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
                    Dim totalCompra As Double = 0
                    For Each producto As EnsambleDiaProducto In aProcesar
                        Dim costoUnitario As Double = 0
                        For Each componente As EnsambleDiaComponente In producto.Componentes
                            If Not componente.EsServicio Then costoUnitario += componente.CantidadUnidad * componente.CostoUnitario
                        Next
                        costos(producto.Codigo) = costoUnitario
                        totalCompra += costoUnitario * producto.Pendiente
                    Next

                    Dim numeroCompra As String = Format(_repositorio.SiguienteConsecutivo(cn, tx, "Compra"), "0000#")
                    _repositorio.InsertarMercanciaRecibida(cn, tx, numeroCompra, fecha, Convert.ToString(proveedor("Cod_Proveedor")), _
                                                           Convert.ToString(proveedor("Nombre_Proveedor")), solicitud.BodegaDestino, totalCompra, observaciones)
                    For Each producto As EnsambleDiaProducto In aProcesar
                        _repositorio.InsertarDetalleCompra(cn, tx, numeroCompra, fecha, producto.Codigo, producto.Descripcion, producto.Pendiente, costos(producto.Codigo))
                    Next

                    '--- Trazabilidad
                    Dim idProceso As Integer = _repositorio.InsertarProceso(cn, tx, solicitud, numeroSalida, numeroCompra, totalCompra)
                    For Each producto As EnsambleDiaProducto In aProcesar
                        _repositorio.InsertarProcesoProducto(cn, tx, idProceso, producto, costos(producto.Codigo))
                        For Each componente As EnsambleDiaComponente In producto.Componentes
                            _repositorio.InsertarProcesoComponente(cn, tx, idProceso, producto.Codigo, componente)
                        Next
                    Next

                    '--- Existencias por bodega de todo lo que se movio
                    For Each componente As EnsambleDiaComponente In totales.Values
                        _repositorio.ActualizarExistencia(cn, tx, componente.Codigo, Date.Now)
                    Next
                    For Each producto As EnsambleDiaProducto In aProcesar
                        _repositorio.ActualizarExistencia(cn, tx, producto.Codigo, Date.Now)
                    Next

                    tx.Commit()

                    resultado.IdEnsambleDia = idProceso
                    resultado.NumeroSalida = numeroSalida
                    resultado.NumeroCompra = numeroCompra
                    resultado.TotalCosto = totalCompra
                    resultado.ProductosProcesados = aProcesar.Count
                    Return resultado
                Catch
                    tx.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Asigna el costo promedio a cada componente, totaliza lo que sale de la bodega por componente
    ''' y valida costo y existencia. Reune todos los problemas antes de avisar.
    ''' </summary>
    Private Function CostearYVerificar(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, _
                                       ByVal productos As List(Of EnsambleDiaProducto), _
                                       ByVal solicitud As EnsambleDiaSolicitud) As Dictionary(Of String, EnsambleDiaComponente)
        Dim totales As New Dictionary(Of String, EnsambleDiaComponente)(StringComparer.OrdinalIgnoreCase)
        Dim costos As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
        Dim problemas As New StringBuilder

        For Each producto As EnsambleDiaProducto In productos
            For Each componente As EnsambleDiaComponente In producto.Componentes
                If componente.EsServicio Then Continue For

                If Not costos.ContainsKey(componente.Codigo) Then
                    costos(componente.Codigo) = _repositorio.CostoPromedio(cn, tx, componente.Codigo, solicitud.Fecha)
                    If costos(componente.Codigo) <= 0 Then
                        problemas.AppendLine("No existe costo para el producto " & componente.Codigo & " " & componente.Descripcion)
                    End If
                End If
                componente.CostoUnitario = costos(componente.Codigo)

                Dim total As EnsambleDiaComponente = Nothing
                If Not totales.TryGetValue(componente.Codigo, total) Then
                    total = New EnsambleDiaComponente
                    total.Codigo = componente.Codigo
                    total.Descripcion = componente.Descripcion
                    total.TipoProducto = componente.TipoProducto
                    total.CostoUnitario = componente.CostoUnitario
                    totales.Add(componente.Codigo, total)
                End If
                total.CantidadTotal += componente.CantidadTotal
            Next
        Next

        For Each total As EnsambleDiaComponente In totales.Values
            Dim existencia As Double = _repositorio.Existencia(cn, tx, total.Codigo, solicitud.BodegaOrigen, Date.Now)
            If total.CantidadTotal > existencia + TOLERANCIA Then
                problemas.AppendLine("No existe inventario suficiente de " & total.Codigo & " " & total.Descripcion & _
                                     " (necesita " & total.CantidadTotal.ToString("#,##0.####") & ", hay " & existencia.ToString("#,##0.####") & ")")
            End If
        Next

        If problemas.Length > 0 Then
            Throw New EnsambleDiaNegocioException("No se puede procesar el ensamble:" & Environment.NewLine & problemas.ToString().TrimEnd())
        End If
        Return totales
    End Function

#End Region

    Private Shared Function Numero(ByVal valor As Object) As Double
        If valor Is Nothing OrElse IsDBNull(valor) Then Return 0
        Return Convert.ToDouble(valor)
    End Function

End Class
