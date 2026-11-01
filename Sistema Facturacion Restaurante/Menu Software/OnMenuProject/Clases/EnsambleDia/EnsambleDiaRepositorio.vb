Option Strict On
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

'=====================================================================================================
'  ENSAMBLE DEL DIA - REPOSITORIO
'  Todo el acceso a SQL Server del ensamble diario (consultas parametrizadas). Las rutinas que escriben
'  reciben la conexion y la transaccion del servicio para que el proceso sea atomico.
'=====================================================================================================
Public Class EnsambleDiaRepositorio

    ' Unidades facturadas en el dia de productos Ensamble, menos lo ya ensamblado en procesos anteriores
    Private Const SQL_VENTAS As String =
        "SELECT P.Cod_Productos AS Codigo, P.Descripcion_Producto AS Descripcion, ISNULL(P.CodComponente, 0) AS CodComponente, " &
        "       SUM(ISNULL(DF.Cantidad, 0)) AS Vendida, " &
        "       ISNULL((SELECT SUM(EP.Cantidad) FROM Ensamble_Dia_Producto EP " &
        "               INNER JOIN Ensamble_Dia E ON E.Id_EnsambleDia = EP.Id_EnsambleDia " &
        "               WHERE E.Fecha_Ensamble = @fecha AND E.Estado = 'Procesado' AND EP.Cod_Producto = P.Cod_Productos), 0) AS Procesada " &
        "FROM Detalle_Facturas DF " &
        "INNER JOIN Facturas F ON DF.Numero_Factura = F.Numero_Factura AND DF.Fecha_Factura = F.Fecha_Factura AND DF.Tipo_Factura = F.Tipo_Factura " &
        "INNER JOIN Productos P ON P.Cod_Productos = DF.Cod_Producto " &
        "WHERE F.Tipo_Factura = 'Factura' AND F.Fecha_Factura >= @fecha AND F.Fecha_Factura < DATEADD(DAY, 1, @fecha) " &
        "  AND ISNULL(F.Cancelado, 0) = 0 AND P.Tipo_Producto = 'Ensamble' " &
        "GROUP BY P.Cod_Productos, P.Descripcion_Producto, P.CodComponente " &
        "ORDER BY P.Descripcion_Producto"

    ' Mismo calculo de existencia por bodega que usa el sistema de facturacion (BuscaExistenciaBodega)
    Private Const SQL_EXISTENCIA As String =
        "SELECT ISNULL(MR.Cantidad,0) + ISNULL(TR.Cantidad,0) + ISNULL(DV.Cantidad,0) - (ISNULL(Fact.Cantidad,0) + ISNULL(SB.Cantidad,0) + ISNULL(TE.Cantidad,0) + ISNULL(DC.Cantidad,0)) AS Existencia " &
        "FROM (SELECT Cod_Producto FROM Detalle_Compras WHERE Fecha_Compra <= @FechaCorte UNION SELECT Cod_Producto FROM Detalle_Facturas WHERE Fecha_Factura <= @FechaCorte) P " &
        "INNER JOIN Productos Prod ON P.Cod_Producto = Prod.Cod_Productos AND Prod.Cod_Productos = @CodigoProducto " &
        "LEFT JOIN (SELECT DC.Cod_Producto, SUM(DC.Cantidad) AS Cantidad FROM Detalle_Compras DC INNER JOIN Compras C ON DC.Numero_Compra = C.Numero_Compra AND DC.Fecha_Compra = C.Fecha_Compra AND DC.Tipo_Compra = C.Tipo_Compra WHERE C.Fecha_Compra <= @FechaCorte AND C.Tipo_Compra = 'Mercancia Recibida' AND C.Cod_Bodega = @CodigoBodega GROUP BY DC.Cod_Producto) MR ON P.Cod_Producto = MR.Cod_Producto " &
        "LEFT JOIN (SELECT DC.Cod_Producto, SUM(DC.Cantidad) AS Cantidad FROM Detalle_Compras DC INNER JOIN Compras C ON DC.Numero_Compra = C.Numero_Compra AND DC.Fecha_Compra = C.Fecha_Compra AND DC.Tipo_Compra = C.Tipo_Compra WHERE C.Fecha_Compra <= @FechaCorte AND C.Tipo_Compra = 'Transferencia Recibida' AND C.Cod_Bodega = @CodigoBodega GROUP BY DC.Cod_Producto) TR ON P.Cod_Producto = TR.Cod_Producto " &
        "LEFT JOIN (SELECT DF.Cod_Producto, SUM(DF.Cantidad) AS Cantidad FROM Detalle_Facturas DF INNER JOIN Facturas F ON DF.Numero_Factura = F.Numero_Factura AND DF.Fecha_Factura = F.Fecha_Factura AND DF.Tipo_Factura = F.Tipo_Factura WHERE F.Fecha_Factura <= @FechaCorte AND F.Tipo_Factura = 'Devolucion de Venta' AND F.Cod_Bodega = @CodigoBodega GROUP BY DF.Cod_Producto) DV ON P.Cod_Producto = DV.Cod_Producto " &
        "LEFT JOIN (SELECT DF.Cod_Producto, SUM(DF.Cantidad) AS Cantidad FROM Detalle_Facturas DF INNER JOIN Facturas F ON DF.Numero_Factura = F.Numero_Factura AND DF.Fecha_Factura = F.Fecha_Factura AND DF.Tipo_Factura = F.Tipo_Factura WHERE F.Fecha_Factura <= @FechaCorte AND F.Tipo_Factura = 'Factura' AND F.Cod_Bodega = @CodigoBodega GROUP BY DF.Cod_Producto) Fact ON P.Cod_Producto = Fact.Cod_Producto " &
        "LEFT JOIN (SELECT DF.Cod_Producto, SUM(DF.Cantidad) AS Cantidad FROM Detalle_Facturas DF INNER JOIN Facturas F ON DF.Numero_Factura = F.Numero_Factura AND DF.Fecha_Factura = F.Fecha_Factura AND DF.Tipo_Factura = F.Tipo_Factura WHERE F.Fecha_Factura <= @FechaCorte AND F.Tipo_Factura = 'Salida Bodega' AND F.Cod_Bodega = @CodigoBodega GROUP BY DF.Cod_Producto) SB ON P.Cod_Producto = SB.Cod_Producto " &
        "LEFT JOIN (SELECT DF.Cod_Producto, SUM(DF.Cantidad) AS Cantidad FROM Detalle_Facturas DF INNER JOIN Facturas F ON DF.Numero_Factura = F.Numero_Factura AND DF.Fecha_Factura = F.Fecha_Factura AND DF.Tipo_Factura = F.Tipo_Factura WHERE F.Fecha_Factura <= @FechaCorte AND F.Tipo_Factura = 'Transferencia Enviada' AND F.Cod_Bodega = @CodigoBodega GROUP BY DF.Cod_Producto) TE ON P.Cod_Producto = TE.Cod_Producto " &
        "LEFT JOIN (SELECT DC.Cod_Producto, SUM(DC.Cantidad) AS Cantidad FROM Detalle_Compras DC INNER JOIN Compras C ON DC.Numero_Compra = C.Numero_Compra AND DC.Fecha_Compra = C.Fecha_Compra AND DC.Tipo_Compra = C.Tipo_Compra WHERE C.Fecha_Compra <= @FechaCorte AND C.Tipo_Compra = 'Devolucion de Compra' AND C.Cod_Bodega = @CodigoBodega GROUP BY DC.Cod_Producto) DC ON P.Cod_Producto = DC.Cod_Producto"

    ' Costo promedio en cordobas del kardex a la fecha (misma logica de CostoPromedioKardex):
    ' entradas = compras recibidas, transferencias recibidas y devoluciones de venta; salidas = ventas, salidas de bodega,
    ' transferencias enviadas y devoluciones de compra.
    Private Const SQL_COSTO_PROMEDIO As String =
        "SELECT ISNULL(SUM(Cant * Signo), 0) AS Cantidad, ISNULL(SUM(Importe * Signo), 0) AS Importe FROM ( " &
        "  SELECT DC.Cantidad AS Cant, " &
        "         DC.Precio_Neto * DC.Cantidad * CASE WHEN C.MonedaCompra = 'Dolares' THEN ISNULL(T.MontoTasa, 1) ELSE 1 END AS Importe, " &
        "         CASE WHEN C.Tipo_Compra = 'Devolucion de Compra' THEN -1 ELSE 1 END AS Signo " &
        "  FROM Detalle_Compras DC " &
        "  INNER JOIN Compras C ON DC.Numero_Compra = C.Numero_Compra AND DC.Fecha_Compra = C.Fecha_Compra AND DC.Tipo_Compra = C.Tipo_Compra " &
        "  LEFT JOIN TasaCambio T ON T.FechaTasa = C.Fecha_Compra " &
        "  WHERE DC.Cod_Producto = @producto AND DC.Fecha_Compra <= @fecha " &
        "    AND C.Tipo_Compra IN ('Mercancia Recibida', 'Transferencia Recibida', 'Devolucion de Compra') " &
        "  UNION ALL " &
        "  SELECT CASE WHEN DF.Costo_Unitario = 0 THEN 0 ELSE DF.Cantidad END, DF.Cantidad * DF.Costo_Unitario, " &
        "         CASE WHEN DF.Tipo_Factura = 'Devolucion de Venta' THEN 1 ELSE -1 END " &
        "  FROM Detalle_Facturas DF " &
        "  WHERE DF.Cod_Producto = @producto AND DF.Fecha_Factura <= @fecha " &
        "    AND DF.Tipo_Factura IN ('Factura', 'Salida Bodega', 'Transferencia Enviada', 'Devolucion de Venta') " &
        "    AND DF.Cantidad * DF.Costo_Unitario <> 0 " &
        ") X"

    Private Const SQL_ULTIMO_COSTO As String =
        "SELECT TOP 1 Costo_Unitario FROM Detalle_Facturas " &
        "WHERE Cod_Producto = @producto AND Costo_Unitario <> 0 AND Fecha_Factura <= @fecha " &
        "ORDER BY Fecha_Factura DESC, id_Detalle_Factura DESC"

    Private ReadOnly _conexion As String

    Public Sub New(ByVal conexion As String)
        If String.IsNullOrWhiteSpace(conexion) Then
            Throw New ArgumentException("La cadena de conexión es requerida.")
        End If
        _conexion = conexion
    End Sub

#Region "Consultas"

    Public Function ObtenerBodegas() As DataTable
        Return LeerTabla("SELECT Cod_Bodega, Nombre_Bodega FROM Bodegas ORDER BY Cod_Bodega")
    End Function

    ''' <summary>Productos Ensamble facturados en el dia con lo vendido y lo ya procesado.</summary>
    Public Function ObtenerVentasEnsamble(ByVal fecha As Date) As DataTable
        Return LeerTabla(SQL_VENTAS, Fecha_("@fecha", fecha))
    End Function

    ''' <summary>Componentes de la receta de un producto (cantidad por unidad).</summary>
    Public Function ObtenerComponentes(ByVal codComponente As Double) As DataTable
        'Cod_Componente es texto en la base: se compara como texto para no depender de que todos los valores sean numericos
        Return LeerTabla(
            "SELECT C.Cod_Producto, ISNULL(P.Descripcion_Producto, C.Descripcion_Producto) AS Descripcion, " &
            "       ISNULL(C.Requerido, 0) AS Requerido, ISNULL(P.Tipo_Producto, '') AS Tipo_Producto " &
            "FROM Componentes C LEFT JOIN Productos P ON P.Cod_Productos = C.Cod_Producto " &
            "WHERE C.Cod_Componente = @componente ORDER BY C.Cod_Producto",
            Texto("@componente", codComponente.ToString("0.##########", CultureInfo.InvariantCulture)))
    End Function

    Public Function ObtenerDatosEmpresa() As DataTable
        Return LeerTabla("SELECT TOP 1 Nombre_Empresa, Direccion_Empresa, Numero_Ruc FROM DatosEmpresa")
    End Function

    ''' <summary>Procesos ya realizados para el dia (para mostrar los documentos generados).</summary>
    Public Function ObtenerProcesosDelDia(ByVal fecha As Date) As DataTable
        Return LeerTabla(
            "SELECT Id_EnsambleDia, Fecha_Proceso, Numero_Salida, Numero_Compra, Total_Costo, Usuario " &
            "FROM Ensamble_Dia WHERE Fecha_Ensamble = @fecha AND Estado = 'Procesado' ORDER BY Id_EnsambleDia",
            Fecha_("@fecha", fecha))
    End Function

    ''' <summary>Cuenta de cliente marcada como inventario fisico (Nothing si no hay).</summary>
    Public Function ObtenerClienteInventario() As DataRow
        Dim tabla As DataTable = LeerTabla("SELECT TOP 1 Cod_Cliente, Nombre_Cliente FROM Clientes WHERE InventarioFisico = 1")
        If tabla.Rows.Count = 0 Then Return Nothing
        Return tabla.Rows(0)
    End Function

    ''' <summary>Cuenta de proveedor marcada como inventario fisico (Nothing si no hay).</summary>
    Public Function ObtenerProveedorInventario() As DataRow
        Dim tabla As DataTable = LeerTabla("SELECT TOP 1 Cod_Proveedor, Nombre_Proveedor FROM Proveedor WHERE InventarioFisico = 1")
        If tabla.Rows.Count = 0 Then Return Nothing
        Return tabla.Rows(0)
    End Function

#End Region

#Region "Kardex (dentro de la transaccion)"

    ''' <summary>Siguiente numero del consecutivo (columna de la tabla Consecutivos), ya reservado.</summary>
    Public Function SiguienteConsecutivo(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal campo As String) As Double
        If campo <> "SalidaBodega" AndAlso campo <> "Compra" Then
            Throw New ArgumentException("Consecutivo no válido: " & campo)
        End If

        Using cmd As New SqlCommand(
            "UPDATE Consecutivos SET [" & campo & "] = ISNULL([" & campo & "], 0) + 1 OUTPUT inserted.[" & campo & "]", cn, tx)
            Dim valor As Object = cmd.ExecuteScalar()
            If valor Is Nothing OrElse IsDBNull(valor) Then
                Throw New EnsambleDiaNegocioException("No se encontró la tabla de consecutivos del sistema.")
            End If
            Return Convert.ToDouble(valor)
        End Using
    End Function

    ''' <summary>
    ''' Costo promedio en cordobas a la fecha. Si no hay costo promedio se toma el ultimo costo facturado.
    ''' </summary>
    Public Function CostoPromedio(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal codProducto As String, ByVal fecha As Date) As Double
        Dim cantidad As Double = 0, importe As Double = 0

        Using cmd As New SqlCommand(SQL_COSTO_PROMEDIO, cn, tx)
            cmd.Parameters.Add(Texto("@producto", codProducto))
            cmd.Parameters.Add(Fecha_("@fecha", fecha))
            Using rd As SqlDataReader = cmd.ExecuteReader()
                If rd.Read() Then
                    cantidad = Convert.ToDouble(rd(0))
                    importe = Convert.ToDouble(rd(1))
                End If
            End Using
        End Using

        Dim costo As Double = 0
        If cantidad <> 0 Then costo = importe / cantidad
        If costo > 0 Then Return costo

        Using cmd As New SqlCommand(SQL_ULTIMO_COSTO, cn, tx)
            cmd.Parameters.Add(Texto("@producto", codProducto))
            cmd.Parameters.Add(Fecha_("@fecha", fecha))
            Dim ultimo As Object = cmd.ExecuteScalar()
            If ultimo IsNot Nothing AndAlso Not IsDBNull(ultimo) Then Return Convert.ToDouble(ultimo)
        End Using
        Return 0
    End Function

    Public Function Existencia(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal codProducto As String, _
                               ByVal codBodega As String, ByVal fechaCorte As Date) As Double
        Using cmd As New SqlCommand(SQL_EXISTENCIA, cn, tx)
            cmd.Parameters.Add(Texto("@CodigoBodega", codBodega))
            cmd.Parameters.Add(Texto("@CodigoProducto", codProducto))
            cmd.Parameters.Add(Fecha_("@FechaCorte", fechaCorte))
            Dim valor As Object = cmd.ExecuteScalar()
            If valor Is Nothing OrElse IsDBNull(valor) Then Return 0
            Return Convert.ToDouble(valor)
        End Using
    End Function

    ''' <summary>Recalcula DetalleBodegas.Existencia del producto en cada una de sus bodegas.</summary>
    Public Sub ActualizarExistencia(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal codProducto As String, ByVal fechaCorte As Date)
        Dim bodegas As New DataTable
        Using cmd As New SqlCommand("SELECT Cod_Bodegas FROM DetalleBodegas WHERE Cod_Productos = @producto", cn, tx)
            cmd.Parameters.Add(Texto("@producto", codProducto))
            Using da As New SqlDataAdapter(cmd)
                da.Fill(bodegas)
            End Using
        End Using

        For Each fila As DataRow In bodegas.Rows
            Dim codBodega As String = Convert.ToString(fila("Cod_Bodegas"))
            Dim cantidad As Double = Existencia(cn, tx, codProducto, codBodega, fechaCorte)

            Using cmd As New SqlCommand("UPDATE DetalleBodegas SET Existencia = @existencia WHERE Cod_Productos = @producto AND Cod_Bodegas = @bodega", cn, tx)
                cmd.Parameters.Add(Numero_("@existencia", cantidad))
                cmd.Parameters.Add(Texto("@producto", codProducto))
                cmd.Parameters.Add(Texto("@bodega", codBodega))
                cmd.ExecuteNonQuery()
            End Using
        Next
    End Sub

#End Region

#Region "Documentos de inventario (dentro de la transaccion)"

    Public Sub InsertarSalidaBodega(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal numero As String, ByVal fecha As Date, _
                                    ByVal codCliente As String, ByVal nombreCliente As String, ByVal codBodega As String, _
                                    ByVal total As Double, ByVal observaciones As String)
        Using cmd As New SqlCommand(
            "INSERT INTO Facturas (Numero_Factura, Fecha_Factura, Tipo_Factura, Cod_Cliente, Cod_Bodega, Nombre_Cliente, Apellido_Cliente, " &
            "                      Fecha_Vencimiento, Observaciones, SubTotal, IVA, Pagado, NetoPagar, MontoCredito, MonedaFactura, FechaHora, Activo, Exonerado) " &
            "VALUES (@numero, @fecha, 'Salida Bodega', @cliente, @bodega, @nombre, @nombre, @fecha, @observaciones, @total, 0, 0, @total, 0, 'Cordobas', @fechaHora, 0, 1)", cn, tx)
            cmd.Parameters.Add(Texto("@numero", numero))
            cmd.Parameters.Add(Fecha_("@fecha", fecha))
            cmd.Parameters.Add(Texto("@cliente", codCliente))
            cmd.Parameters.Add(Texto("@bodega", codBodega))
            cmd.Parameters.Add(Texto("@nombre", nombreCliente))
            cmd.Parameters.Add(Texto("@observaciones", observaciones))
            cmd.Parameters.Add(Numero_("@total", total))
            cmd.Parameters.Add(FechaHora_("@fechaHora", fecha))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub InsertarDetalleSalida(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal numero As String, ByVal fecha As Date, _
                                     ByVal codProducto As String, ByVal descripcion As String, ByVal cantidad As Double, ByVal costo As Double)
        Using cmd As New SqlCommand(
            "INSERT INTO Detalle_Facturas (Numero_Factura, Fecha_Factura, Tipo_Factura, Cod_Producto, Descripcion_Producto, Cantidad, " &
            "                              Precio_Unitario, Descuento, Precio_Neto, Importe, TasaCambio, Costo_Unitario) " &
            "VALUES (@numero, @fecha, 'Salida Bodega', @producto, @descripcion, @cantidad, @costo, '0', @costo, @importe, 1, @costo)", cn, tx)
            cmd.Parameters.Add(Texto("@numero", numero))
            cmd.Parameters.Add(Fecha_("@fecha", fecha))
            cmd.Parameters.Add(Texto("@producto", codProducto))
            cmd.Parameters.Add(Texto("@descripcion", descripcion))
            cmd.Parameters.Add(Numero_("@cantidad", cantidad))
            cmd.Parameters.Add(Numero_("@costo", costo))
            cmd.Parameters.Add(Numero_("@importe", cantidad * costo))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub InsertarMercanciaRecibida(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal numero As String, ByVal fecha As Date, _
                                         ByVal codProveedor As String, ByVal nombreProveedor As String, ByVal codBodega As String, _
                                         ByVal total As Double, ByVal observaciones As String)
        Using cmd As New SqlCommand(
            "INSERT INTO Compras (Numero_Compra, Fecha_Compra, Tipo_Compra, Cod_Proveedor, Cod_Bodega, Nombre_Proveedor, Apellido_Proveedor, " &
            "                     Fecha_Vencimiento, Observaciones, SubTotal, IVA, Pagado, NetoPagar, MontoCredito, MonedaCompra, FechaHora, Activo, Exonerado) " &
            "VALUES (@numero, @fecha, 'Mercancia Recibida', @proveedor, @bodega, @nombre, @nombre, @fecha, @observaciones, @total, 0, 0, @total, 0, 'Cordobas', @fechaHora, 0, 1)", cn, tx)
            cmd.Parameters.Add(Texto("@numero", numero))
            cmd.Parameters.Add(Fecha_("@fecha", fecha))
            cmd.Parameters.Add(Texto("@proveedor", codProveedor))
            cmd.Parameters.Add(Texto("@bodega", codBodega))
            cmd.Parameters.Add(Texto("@nombre", nombreProveedor))
            cmd.Parameters.Add(Texto("@observaciones", observaciones))
            cmd.Parameters.Add(Numero_("@total", total))
            cmd.Parameters.Add(FechaHora_("@fechaHora", fecha))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub InsertarDetalleCompra(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal numero As String, ByVal fecha As Date, _
                                     ByVal codProducto As String, ByVal descripcion As String, ByVal cantidad As Double, ByVal costo As Double)
        Using cmd As New SqlCommand(
            "INSERT INTO Detalle_Compras (Numero_Compra, Fecha_Compra, Tipo_Compra, Cod_Producto, Descripcion_Producto, Cantidad, " &
            "                             Precio_Unitario, Descuento, Precio_Neto, Importe, TasaCambio, Numero_Lote, Fecha_Vence) " &
            "VALUES (@numero, @fecha, 'Mercancia Recibida', @producto, @descripcion, @cantidad, @costo, 0, @costo, @importe, 1, '0000', '19000101')", cn, tx)
            cmd.Parameters.Add(Texto("@numero", numero))
            cmd.Parameters.Add(Fecha_("@fecha", fecha))
            cmd.Parameters.Add(Texto("@producto", codProducto))
            cmd.Parameters.Add(Texto("@descripcion", descripcion))
            cmd.Parameters.Add(Numero_("@cantidad", cantidad))
            cmd.Parameters.Add(Numero_("@costo", costo))
            cmd.Parameters.Add(Numero_("@importe", cantidad * costo))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

#End Region

#Region "Trazabilidad (dentro de la transaccion)"

    ''' <summary>Crea el encabezado del proceso y devuelve su Id.</summary>
    Public Function InsertarProceso(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal solicitud As EnsambleDiaSolicitud, _
                                    ByVal numeroSalida As String, ByVal numeroCompra As String, ByVal totalCosto As Double) As Integer
        Using cmd As New SqlCommand(
            "INSERT INTO Ensamble_Dia (Fecha_Ensamble, Cod_Bodega_Origen, Cod_Bodega_Destino, Numero_Salida, Numero_Compra, Total_Costo, Usuario, Equipo) " &
            "VALUES (@fecha, @origen, @destino, @salida, @compra, @total, @usuario, @equipo); SELECT CAST(SCOPE_IDENTITY() AS INT)", cn, tx)
            cmd.Parameters.Add(Fecha_("@fecha", solicitud.Fecha))
            cmd.Parameters.Add(Texto("@origen", solicitud.BodegaOrigen))
            cmd.Parameters.Add(Texto("@destino", solicitud.BodegaDestino))
            cmd.Parameters.Add(Texto("@salida", numeroSalida))
            cmd.Parameters.Add(Texto("@compra", numeroCompra))
            cmd.Parameters.Add(Numero_("@total", totalCosto))
            cmd.Parameters.Add(Texto("@usuario", solicitud.Usuario))
            cmd.Parameters.Add(Texto("@equipo", Environment.MachineName))
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function

    Public Sub InsertarProcesoProducto(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal idProceso As Integer, _
                                       ByVal producto As EnsambleDiaProducto, ByVal costoUnitario As Double)
        Using cmd As New SqlCommand(
            "INSERT INTO Ensamble_Dia_Producto (Id_EnsambleDia, Cod_Producto, Descripcion_Producto, Cantidad_Vendida, Cantidad, Costo_Unitario, Costo_Total) " &
            "VALUES (@id, @producto, @descripcion, @vendida, @cantidad, @costo, @total)", cn, tx)
            cmd.Parameters.Add(Entero_("@id", idProceso))
            cmd.Parameters.Add(Texto("@producto", producto.Codigo))
            cmd.Parameters.Add(Texto("@descripcion", producto.Descripcion))
            cmd.Parameters.Add(Numero_("@vendida", producto.Vendida))
            cmd.Parameters.Add(Numero_("@cantidad", producto.Pendiente))
            cmd.Parameters.Add(Numero_("@costo", costoUnitario))
            cmd.Parameters.Add(Numero_("@total", costoUnitario * producto.Pendiente))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub InsertarProcesoComponente(ByVal cn As SqlConnection, ByVal tx As SqlTransaction, ByVal idProceso As Integer, _
                                         ByVal codProducto As String, ByVal componente As EnsambleDiaComponente)
        Using cmd As New SqlCommand(
            "INSERT INTO Ensamble_Dia_Componente (Id_EnsambleDia, Cod_Producto, Cod_Componente, Descripcion_Componente, Cantidad_Unidad, Cantidad_Total, Costo_Unitario, Costo_Total) " &
            "VALUES (@id, @producto, @componente, @descripcion, @unidad, @cantidad, @costo, @total)", cn, tx)
            cmd.Parameters.Add(Entero_("@id", idProceso))
            cmd.Parameters.Add(Texto("@producto", codProducto))
            cmd.Parameters.Add(Texto("@componente", componente.Codigo))
            cmd.Parameters.Add(Texto("@descripcion", componente.Descripcion))
            cmd.Parameters.Add(Numero_("@unidad", componente.CantidadUnidad))
            cmd.Parameters.Add(Numero_("@cantidad", componente.CantidadTotal))
            cmd.Parameters.Add(Numero_("@costo", componente.CostoUnitario))
            cmd.Parameters.Add(Numero_("@total", componente.CostoTotal))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

#End Region

#Region "Utilidades"

    Private Function LeerTabla(ByVal sql As String, ByVal ParamArray parametros() As SqlParameter) As DataTable
        Dim tabla As New DataTable
        Using cn As New SqlConnection(_conexion)
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddRange(parametros)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(tabla)
                End Using
            End Using
        End Using
        Return tabla
    End Function

    Private Shared Function Texto(ByVal nombre As String, ByVal valor As String) As SqlParameter
        Dim p As New SqlParameter(nombre, SqlDbType.NVarChar)
        p.Value = If(valor, "")
        Return p
    End Function

    Private Shared Function Numero_(ByVal nombre As String, ByVal valor As Double) As SqlParameter
        Dim p As New SqlParameter(nombre, SqlDbType.Float)
        p.Value = valor
        Return p
    End Function

    Private Shared Function Entero_(ByVal nombre As String, ByVal valor As Integer) As SqlParameter
        Dim p As New SqlParameter(nombre, SqlDbType.Int)
        p.Value = valor
        Return p
    End Function

    'Las fechas de los documentos son smalldatetime: se guardan siempre sin hora
    Private Shared Function Fecha_(ByVal nombre As String, ByVal valor As Date) As SqlParameter
        Dim p As New SqlParameter(nombre, SqlDbType.DateTime)
        p.Value = valor.Date
        Return p
    End Function

    'FechaHora del documento: el dia del proceso con la hora actual (como el resto del sistema)
    Private Shared Function FechaHora_(ByVal nombre As String, ByVal fecha As Date) As SqlParameter
        Dim p As New SqlParameter(nombre, SqlDbType.DateTime)
        p.Value = fecha.Date.Add(Date.Now.TimeOfDay)
        Return p
    End Function

#End Region

End Class
