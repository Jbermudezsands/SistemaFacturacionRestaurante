# Genera Archivos\ScriptBaseDatos.sql (esquema + datos de catalogo, sin facturas ni movimientos)
# Ejecutar con Windows PowerShell 5.1:  powershell -ExecutionPolicy Bypass -File GenerarScriptBD.ps1 -Servidor "SERVIDOR\INSTANCIA" -Usuario sa -Clave "..."
param(
    [Parameter(Mandatory = $true)][string]$Servidor,
    [string]$BaseDatos = "SistemaRestauranteISAKAYA",
    [Parameter(Mandatory = $true)][string]$Usuario,
    [Parameter(Mandatory = $true)][string]$Clave,
    [string]$Salida = ""
)

if (-not $Salida) { $Salida = Join-Path (Split-Path -Parent $MyInvocation.MyCommand.Path) "Archivos\ScriptBaseDatos.sql" }
$dir = "C:\Program Files\Microsoft SQL Server\160\COM"
# SMO pide versiones exactas de sus dependencias; se resuelven por nombre desde la misma carpeta
[AppDomain]::CurrentDomain.add_AssemblyResolve({
    param($sender, $e)
    foreach ($carpeta in $dir, "C:\Program Files\Microsoft SQL Server\160\Setup Bootstrap\SQL2022\x64") {
        $ruta = Join-Path $carpeta (($e.Name -split ',')[0] + ".dll")
        if (Test-Path $ruta) { return [Reflection.Assembly]::LoadFrom($ruta) }
    }
    return $null
})
foreach ($dll in "Microsoft.SqlServer.ConnectionInfo", "Microsoft.SqlServer.Management.Sdk.Sfc", "Microsoft.SqlServer.Smo", "Microsoft.SqlServer.SmoExtended", "Microsoft.SqlServer.SqlEnum") {
    [void][Reflection.Assembly]::LoadFrom((Join-Path $dir "$dll.dll"))
}

# Tablas SIN datos: facturas, comandas, caja, compras, notas, recibos y bitacoras
$SinDatos = @(
    'Facturas', 'Detalle_Facturas', 'Detalle_FacturasSeries', 'Detalle_MetodoFacturas', 'Historico_Comandas',
    'Arqueo', 'Detalle_Arqueo', 'Detalle_ArqueoCheque',
    'Compras', 'Detalle_Compras', 'Detalle_ComprasSeries', 'Detalle_MetodoCompras', 'Detalle_MetodoPagoProveedores',
    'IndiceNota', 'Detalle_Nota', 'NotaDebito',
    'Recibo', 'DetalleRecibo', 'DetalleReciboPago', 'Detalle_MetodoRecibo', 'ReciboPago',
    'Liquidacion', 'Detalle_Liquidacion', 'ImpuestosLiquidacion',
    'Bitacora', 'Auditoria_LotesNegativos', 'HistoricoCostos', 'Detalle_Lote', 'Lote',
    'Ensamble', 'Detalle_Ensamble', 'Transformacion', 'Detalle_Transformacion', 'Transferencia',
    'Detalle_Pesadas', 'Detalle_Recepcion', 'Recepcion', 'Detalle_Solicitud', 'Solicitud_Compra',
    'Registro_Transporte', 'Registro_Transporte_Detalle', 'InventarioFisico', 'Endoso',
    'sysdiagrams', 'Table_1', 'Table_2', 'Table_3'
)

$srv = New-Object Microsoft.SqlServer.Management.Smo.Server($Servidor)
$srv.ConnectionContext.TrustServerCertificate = $true
$srv.ConnectionContext.LoginSecure = $false
$srv.ConnectionContext.Login = $Usuario
$srv.ConnectionContext.Password = $Clave
$db = $srv.Databases[$BaseDatos]
if ($null -eq $db) { throw "No existe la base de datos $BaseDatos" }

$tablas = @($db.Tables | Where-Object { -not $_.IsSystemObject })
$sb = New-Object Text.StringBuilder
function Add-Script($lineas) { foreach ($l in $lineas) { [void]$sb.AppendLine($l) } }

[void]$sb.AppendLine("/* Script de base de datos - Zeus Restaurante. Esquema + datos de catalogo (sin facturas ni movimientos). */")
[void]$sb.AppendLine("IF DB_ID(N'$BaseDatos') IS NULL CREATE DATABASE [$BaseDatos];")
[void]$sb.AppendLine("GO")
[void]$sb.AppendLine("USE [$BaseDatos];")
[void]$sb.AppendLine("GO")

# 1) Tablas, llaves primarias, unicas, indices, defaults y checks (sin FK)
$esq = New-Object Microsoft.SqlServer.Management.Smo.Scripter($srv)
$esq.Options.ScriptSchema = $true; $esq.Options.ScriptData = $false
$esq.Options.DriPrimaryKey = $true; $esq.Options.DriUniqueKeys = $true; $esq.Options.DriChecks = $true
$esq.Options.DriDefaults = $true; $esq.Options.DriClustered = $true; $esq.Options.DriNonClustered = $true
$esq.Options.DriForeignKeys = $false; $esq.Options.Indexes = $true; $esq.Options.NonClusteredIndexes = $true
$esq.Options.ClusteredIndexes = $true; $esq.Options.IncludeIfNotExists = $true
$esq.Options.NoCollation = $true; $esq.Options.SchemaQualify = $true
Add-Script ($esq.EnumScript($tablas) | ForEach-Object { $_; "GO" })

# 2) Datos de catalogo
$dat = New-Object Microsoft.SqlServer.Management.Smo.Scripter($srv)
$dat.Options.ScriptSchema = $false; $dat.Options.ScriptData = $true
$dat.Options.NoCommandTerminator = $false; $dat.Options.SchemaQualify = $true
$conDatos = @($tablas | Where-Object { $SinDatos -notcontains $_.Name })
foreach ($t in $conDatos) {
    if ($t.RowCount -gt 0) {
        [void]$sb.AppendLine("-- Datos: $($t.Name)")
        Add-Script ($dat.EnumScript(@($t)) | ForEach-Object { $_; "GO" })
    }
}

# 3) Llaves foraneas
foreach ($t in $tablas) {
    foreach ($fk in $t.ForeignKeys) { Add-Script ($fk.Script() | ForEach-Object { $_; "GO" }) }
}

[IO.File]::WriteAllText($Salida, $sb.ToString(), (New-Object Text.UTF8Encoding $true))
"Tablas: $($tablas.Count)  con datos: $($conDatos.Count)  -> $Salida"
