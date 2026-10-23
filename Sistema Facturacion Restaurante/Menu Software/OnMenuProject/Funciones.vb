Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

Module Funciones

    <DllImport("winspool.drv", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Function SetDefaultPrinter(
    ByVal pszPrinter As String
) As Boolean
    End Function

    Public Sub ActualizarMonedaPorMetodoPago(
    ByVal CboMetodoPago As C1.Win.C1List.C1Combo,
    ByVal CboMoneda As System.Windows.Forms.ComboBox)

        If CboMetodoPago.SelectedIndex < 0 Then Exit Sub

        Dim metodo As String = CboMetodoPago.Text.Trim()

        ' Determinar la moneda seg˙n el mÈtodo seleccionado.
        If metodo.EndsWith("$") Then

            CboMoneda.Text = "Dolares"

        Else

            CboMoneda.Text = "Cordobas"

        End If

    End Sub

    Public Sub ActualizarMetodoPagoPorMoneda(
    ByVal CboMetodoPago As C1.Win.C1List.C1Combo,
    ByVal CboMoneda As System.Windows.Forms.ComboBox)

        If CboMetodoPago.SelectedIndex < 0 Then Exit Sub
        If CboMoneda.SelectedIndex < 0 Then Exit Sub

        Dim metodoActual As String = CboMetodoPago.Text.Trim()
        Dim monedaActual As String = CboMoneda.Text.Trim()

        ' Obtener el nombre base del mÈtodo.
        If metodoActual.EndsWith("$") Then
            metodoActual = metodoActual.Substring(
            0, metodoActual.Length - 1).Trim()
        End If

        Dim nuevoMetodo As String

        If monedaActual.Equals(
        "Dolares", StringComparison.OrdinalIgnoreCase) OrElse
       monedaActual.Equals(
        "DÛlares", StringComparison.OrdinalIgnoreCase) Then

            nuevoMetodo = metodoActual & " $"

        ElseIf monedaActual.Equals(
        "Cordobas", StringComparison.OrdinalIgnoreCase) OrElse
       monedaActual.Equals(
        "CÛrdobas", StringComparison.OrdinalIgnoreCase) Then

            nuevoMetodo = metodoActual

        Else
            Exit Sub
        End If

        ' Buscar el mÈtodo correspondiente.
        Dim indice As Integer =
        CboMetodoPago.FindStringExact(nuevoMetodo)

        If indice >= 0 Then
            CboMetodoPago.SelectedIndex = indice
        End If

    End Sub
    Public Function ObtenerMontoPagadoCordobas(
    ByVal NumeroFactura As String,
    ByVal FechaFactura As Date,
    ByVal TipoFactura As String) As Decimal

        Dim sql As String =
        "SELECT " &
        "ISNULL(SUM(" &
        "    CASE " &
        "        WHEN MP.Moneda = 'Cordobas' " &
        "            THEN ISNULL(DMF.Monto, 0) " &
        "        WHEN MP.Moneda = 'Dolares' " &
        "            THEN DMF.Monto * TC.MontoTasa " &
        "        ELSE 0 " &
        "    END), 0) AS MontoPagado " &
        "FROM dbo.Detalle_MetodoFacturas DMF " &
        "INNER JOIN dbo.MetodoPago MP " &
        "    ON MP.NombrePago = DMF.NombrePago " &
        "LEFT JOIN dbo.TasaCambio TC " &
        "    ON TC.FechaTasa = DMF.Fecha_Factura " &
        "WHERE DMF.Numero_Factura = @NumeroFactura " &
        "AND DMF.Fecha_Factura = @FechaFactura " &
        "AND DMF.Tipo_Factura = @TipoFactura " &
        "AND NOT EXISTS (" &
        "    SELECT 1 " &
        "    FROM dbo.Detalle_MetodoFacturas D2 " &
        "    INNER JOIN dbo.MetodoPago M2 " &
        "        ON M2.NombrePago = D2.NombrePago " &
        "    LEFT JOIN dbo.TasaCambio T2 " &
        "        ON T2.FechaTasa = D2.Fecha_Factura " &
        "    WHERE D2.Numero_Factura = @NumeroFactura " &
        "    AND D2.Fecha_Factura = @FechaFactura " &
        "    AND D2.Tipo_Factura = @TipoFactura " &
        "    AND M2.Moneda = 'Dolares' " &
        "    AND (T2.MontoTasa IS NULL OR T2.MontoTasa <= 0)" &
        ")"

        Using cn As New SqlClient.SqlConnection(Conexion)

            Using cmd As New SqlClient.SqlCommand(sql, cn)

                cmd.Parameters.Add("@NumeroFactura",
                SqlDbType.NVarChar, 50).Value = NumeroFactura

                cmd.Parameters.Add("@FechaFactura",
                SqlDbType.SmallDateTime).Value = FechaFactura

                cmd.Parameters.Add("@TipoFactura",
                SqlDbType.NVarChar, 50).Value = TipoFactura

                cn.Open()

                Dim resultado As Object = cmd.ExecuteScalar()

                If resultado Is Nothing OrElse IsDBNull(resultado) Then
                    Return 0D
                End If

                ' Si existe un pago en dÛlares sin tasa v·lida,
                ' la consulta no devuelve el monto calculado.
                Return Convert.ToDecimal(resultado)

            End Using

        End Using

    End Function



    Public Function BuscaImpresora(ByVal Modulo As String) As String

        Dim RutaBD As String =
        My.Application.Info.DirectoryPath & "\Impresoras.dll"

        Dim ConexionImpresora As String =
        "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & RutaBD & ";"

        Dim SqlString As String =
        "SELECT Impresora FROM Impresoras WHERE TipoImpresora = ?"

        Try
            Using MiConexionImpresora As New OleDb.OleDbConnection(ConexionImpresora)

                Using Comando As New OleDb.OleDbCommand(SqlString, MiConexionImpresora)

                    Comando.Parameters.AddWithValue("@Modulo", Modulo)

                    MiConexionImpresora.Open()

                    Dim Resultado As Object = Comando.ExecuteScalar()

                    If Resultado IsNot Nothing AndAlso
                   Resultado IsNot DBNull.Value Then

                        Return Resultado.ToString().Trim()

                    End If

                End Using

            End Using

            Return "Ninguno"

        Catch ex As Exception

            MessageBox.Show(
            "Error al buscar la impresora del mÛdulo " & Modulo &
            "." & Environment.NewLine & ex.Message,
            "Zeus FacturaciÛn",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Return "Ninguno"

        End Try

    End Function



    Public Function Establecer_Impresora(
    ByVal NamePrinter As String
) As Boolean

        Try

            If String.IsNullOrWhiteSpace(NamePrinter) OrElse
           NamePrinter.Trim().Equals(
               "Ninguno",
               StringComparison.OrdinalIgnoreCase) Then

                Return False

            End If

            NamePrinter = NamePrinter.Trim()

            ' Verificar que la impresora estÈ instalada.
            Dim ImpresoraEncontrada As Boolean = False

            For Each Impresora As String In PrinterSettings.InstalledPrinters

                If Impresora.Equals(
                NamePrinter,
                StringComparison.OrdinalIgnoreCase) Then

                    ImpresoraEncontrada = True
                    Exit For

                End If

            Next

            If Not ImpresoraEncontrada Then

                MessageBox.Show(
                "La impresora no est· instalada en Windows:" &
                Environment.NewLine & NamePrinter,
                "Zeus FacturaciÛn",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

                Return False

            End If

            ' Cambiar la impresora predeterminada de Windows.
            If Not SetDefaultPrinter(NamePrinter) Then

                Dim CodigoError As Integer =
                Marshal.GetLastWin32Error()

                MessageBox.Show(
                "Windows no pudo establecer la impresora predeterminada." &
                Environment.NewLine &
                "Impresora: " & NamePrinter &
                Environment.NewLine &
                "CÛdigo de error: " & CodigoError,
                "Zeus FacturaciÛn",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

                Return False

            End If

            ' Confirmar el resultado.
            Using pd As New PrintDocument()

                If Not pd.PrinterSettings.PrinterName.Equals(
                NamePrinter,
                StringComparison.OrdinalIgnoreCase) Then

                    MessageBox.Show(
                    "No se pudo confirmar la impresora predeterminada.",
                    "Zeus FacturaciÛn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)

                    Return False

                End If

            End Using

            Return True

        Catch ex As Exception

            MessageBox.Show(
            "Error al establecer la impresora:" &
            Environment.NewLine & ex.Message,
            "Zeus FacturaciÛn",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Return False

        End Try

    End Function
    Public Function ObtenerImprimirSinPreview() As Boolean

        Try
            Dim SqlDatos As String = "SELECT TOP 1 ImprimirSinPreview FROM DatosEmpresa"

            Using cn As New SqlClient.SqlConnection(Conexion)
                Using cmd As New SqlClient.SqlCommand(SqlDatos, cn)

                    cn.Open()

                    Dim resultado As Object = cmd.ExecuteScalar()

                    If resultado IsNot Nothing AndAlso Not IsDBNull(resultado) Then
                        Return Convert.ToBoolean(resultado)
                    End If

                End Using
            End Using

            Return False

        Catch ex As Exception
            Return False
        End Try

    End Function





    Public Function Monto_Pendiente(Numero_Factura As String) As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlString As String
        Dim MonedaFactura As String = "Cordobas", FechaFactura As Date
        Dim i As Double = 0, Cadena As String = "", Monto As Double = 0

        If Numero_Factura = "" Then
            Exit Function
        End If

        '/////////////////////CONSULTO LOS DATOS DE LA FACTURA ///////////////////////////
        SqlString = "SELECT Facturas.* FROM Facturas WHERE  (Numero_Factura = '" & Numero_Factura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Factura")
        If Not DataSet.Tables("Factura").Rows.Count = 0 Then
            FechaFactura = DataSet.Tables("Factura").Rows(0)("Fecha_Factura")
            MonedaFactura = DataSet.Tables("Factura").Rows(0)("MonedaFactura")
        End If

        '///////////////////////CONSULTO DETALLE DE FACTURA //////////////////////////////
        SqlString = "SELECT * FROM  Detalle_MetodoFacturas INNER JOIN MetodoPago ON Detalle_MetodoFacturas.NombrePago = MetodoPago.NombrePago WHERE  (Detalle_MetodoFacturas.Numero_Factura = '" & Numero_Factura & "') AND (Detalle_MetodoFacturas.Fecha_Factura = CONVERT(DATETIME, '" & Format(FechaFactura, "yyyy-MM-dd") & "', 102)) AND (Detalle_MetodoFacturas.Tipo_Factura = 'Factura')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "MetodoPago")
        i = 0
        Monto = 0
        Do While DataSet.Tables("MetodoPago").Rows.Count > i
            Monto = DataSet.Tables("MetodoPago").Rows(i)("Monto")
            i = i + 1
        Loop

        Return Monto

    End Function

    Public Function BuscaConsecutivo(ByVal NombreCampo As String) As Double

        Dim SqlConsecutivo As String, SQlUpdate As String, CodConsecutivo As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, FacturaBodega As Boolean = False, CompraBodega As Boolean = False

        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO SI TIENE ACTIVADA LA OPCION DE CONSECUTIVO X BODEGA /////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////
        SqlConsecutivo = "SELECT * FROM  DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Configuracion")
        If Not DataSet.Tables("Configuracion").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")) = True Then
                FacturaBodega = True
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")) = True Then
                CompraBodega = True
            End If

        End If


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO EL CONSECUTIVO COMPONENTES/////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////

        SqlConsecutivo = "SELECT  * FROM  Consecutivos"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Consecutivo")
        If Not DataSet.Tables("Consecutivo").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consecutivo").Rows(0)(NombreCampo)) Then
                CodConsecutivo = DataSet.Tables("Consecutivo").Rows(0)(NombreCampo) + 1
            Else
                CodConsecutivo = 1
            End If
            BuscaConsecutivo = CodConsecutivo

            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////ACTUALIZO EL CONSECUTIVO///////////////////////////////////////////////////
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            MiConexion.Close()
            SQlUpdate = "UPDATE [Consecutivos]  SET [" & NombreCampo & "] = " & CodConsecutivo & ""
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SQlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            BuscaConsecutivo = 0
        End If

    End Function

    Public Function BuscaConsecutivoSerie(ByVal NombreCampo As String, ByVal TipoSerie As String) As Double

        Dim SqlConsecutivo As String, SQlUpdate As String, CodConsecutivo As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, FacturaBodega As Boolean = False, CompraBodega As Boolean = False


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO EL CONSECUTIVO COMPONENTES/////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////

        SqlConsecutivo = "SELECT  * FROM ConsecutivoSerie WHERE (Serie = '" & TipoSerie & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Consecutivo")
        If Not DataSet.Tables("Consecutivo").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consecutivo").Rows(0)(NombreCampo)) Then
                CodConsecutivo = DataSet.Tables("Consecutivo").Rows(0)(NombreCampo) + 1
            Else
                CodConsecutivo = 1
            End If
            BuscaConsecutivoSerie = CodConsecutivo

            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////ACTUALIZO EL CONSECUTIVO///////////////////////////////////////////////////
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            MiConexion.Close()
            SQlUpdate = "UPDATE [ConsecutivoSerie]  SET [" & NombreCampo & "] = " & CodConsecutivo & " WHERE (Serie = '" & TipoSerie & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SQlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            BuscaConsecutivoSerie = 0
        End If

    End Function

    Public Function BuscaConsecutivoSerieNoEdita(ByVal NombreCampo As String, ByVal TipoSerie As String) As Double

        Dim SqlConsecutivo As String, SQlUpdate As String, CodConsecutivo As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, FacturaBodega As Boolean = False, CompraBodega As Boolean = False


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO EL CONSECUTIVO COMPONENTES/////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////

        SqlConsecutivo = "SELECT  * FROM ConsecutivoSerie WHERE (Serie = '" & TipoSerie & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Consecutivo")
        If Not DataSet.Tables("Consecutivo").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consecutivo").Rows(0)(NombreCampo)) Then
                CodConsecutivo = DataSet.Tables("Consecutivo").Rows(0)(NombreCampo) + 1
            Else
                CodConsecutivo = 1
            End If
            BuscaConsecutivoSerieNoEdita = CodConsecutivo

        Else
            BuscaConsecutivoSerieNoEdita = 0
        End If

    End Function

    Public Function BuscaConsecutivoNoEdita(ByVal NombreCampo As String) As Double

        Dim SqlConsecutivo As String, CodConsecutivo As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ComandoUpdate As New SqlClient.SqlCommand
        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO EL CONSECUTIVO COMPONENTES/////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////

        SqlConsecutivo = "SELECT  * FROM  Consecutivos"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Consecutivo")
        If Not DataSet.Tables("Consecutivo").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consecutivo").Rows(0)(NombreCampo)) Then
                CodConsecutivo = DataSet.Tables("Consecutivo").Rows(0)(NombreCampo) + 1
            Else
                CodConsecutivo = 1
            End If
            BuscaConsecutivoNoEdita = CodConsecutivo

        Else
            BuscaConsecutivoNoEdita = 0
        End If

    End Function

    Public Function BuscaTasaCambio(ByVal FechaTasa As Date) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SQlTasa As String, TasaCambio As Double, Fecha As String

        Fecha = Format(FechaTasa, "yyyy-MM-dd")
        TasaCambio = 0
        SQlTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlTasa, MiConexion)
        My.Application.DoEvents()
        DataAdapter.Fill(DataSet, "TasaCambio")
        If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
            TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")
        End If

        'If TasaCambio = 0 Then
        '    TasaCambio = 1
        'End If


        BuscaTasaCambio = TasaCambio
    End Function

    Public Function ExisteFactura(Numero_Factura As String, Tipo_Factura As String) As Boolean
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlString As String, Numero As Double = 0
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        SqlString = "SELECT  *  FROM Facturas WHERE (Numero_Factura = '" & Numero_Factura & "') AND (Tipo_Factura = '" & Tipo_Factura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        If DataSet.Tables("Consulta").Rows.Count <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Function GenerarNumeroFactura(ByVal ConsecutivoFacturaManual As Boolean, ByVal TipoFactura As String, Serie As String, Cod_Bodega As String) As String
        Dim ConsecutivoFactura As Double, SqlConsecutivo As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, FacturaBodega As Boolean = False, CompraBodega As Boolean = False
        Dim NumeroFactura As String, FacturaSerie As Boolean = False, SqlString As String, Numero As Double = 0
        Dim CadenaDiv() As String


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO SI TIENE ACTIVADA LA OPCION DE CONSECUTIVO X BODEGA /////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////
        SqlConsecutivo = "SELECT * FROM  DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Configuracion")
        If Not DataSet.Tables("Configuracion").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")) Then
                FacturaBodega = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")) Then
                CompraBodega = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacSerie")) Then
                FacturaSerie = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacSerie")
            End If

        End If

        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////BUSCO EL CONSECUTIVO DE LA COMPRA /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////7

        'If FrmFacturas.TxtNumeroEnsamble.Text = "-----0-----" Then

        Select Case TipoFactura
            Case "Cotizacion"
                If FacturaSerie = False Then
                    ConsecutivoFactura = BuscaConsecutivo("Cotizacion")
                Else
                    ConsecutivoFactura = BuscaConsecutivoSerie("Cotizacion", Serie)
                End If
            Case "Factura"
                If ConsecutivoFacturaManual = False Then
                    If FacturaSerie = False Then
                        ConsecutivoFactura = BuscaConsecutivo("Factura")
                    Else
                        ConsecutivoFactura = BuscaConsecutivoSerie("Factura", Serie)
                    End If
                Else
                    'FrmConsecutivos.
                    '    If FrmConsecutivos.TxtConsecutivo.Text <> "-----0-----" Then
                    '        ConsecutivoFactura = FrmConsecutivos.NumeroFactura
                    '    Else
                    '        ConsecutivoFactura = -1
                    '    End If
                End If
            Case "Devolucion de Venta"
                If FacturaSerie = False Then
                    ConsecutivoFactura = BuscaConsecutivo("DevFactura")
                Else
                    ConsecutivoFactura = BuscaConsecutivoSerie("DevFactura", Serie)
                End If
            Case "Transferencia Enviada"
                If FacturaSerie = False Then
                    ConsecutivoFactura = BuscaConsecutivo("Transferencia_Enviada")
                Else
                    ConsecutivoFactura = BuscaConsecutivoSerie("Transferencia_Enviada", Serie)
                End If
            Case "Salida Bodega"
                If FacturaSerie = False Then
                    ConsecutivoFactura = BuscaConsecutivo("SalidaBodega")
                Else
                    ConsecutivoFactura = BuscaConsecutivoSerie("SalidaBodega", Serie)
                End If

            Case "Orden de Trabajo"
                If FacturaSerie = False Then
                    ConsecutivoFactura = BuscaConsecutivo("OrdenSalida")
                Else
                    ConsecutivoFactura = BuscaConsecutivoSerie("OrdenSalida", Serie)
                End If

        End Select

        If ConsecutivoFactura <> -1 Then
            If FacturaBodega = True Then
                NumeroFactura = Cod_Bodega & "-" & Format(ConsecutivoFactura, "0000#")
            ElseIf FacturaSerie = True Then
                NumeroFactura = Serie & Format(ConsecutivoFactura, "0000#")

            Else
                NumeroFactura = Format(ConsecutivoFactura, "0000#")
            End If

        Else
            NumeroFactura = "0000"
        End If

        '----------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------VERIFICO QUE EL CONSECUTIVO NO EXISTE EN LAS FACTURAS GRABADAS--------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------------
        SqlString = "SELECT  *  FROM Facturas WHERE (Numero_Factura = '" & NumeroFactura & "') AND (Tipo_Factura = '" & TipoFactura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        If DataSet.Tables("Consulta").Rows.Count <> 0 Then
            '-------------------------SI EXISTE ESTA FACTURA GRABADA, ENTONCES RECOMIENDO EL SIGUIENTE SEGUN LA FACTURACION ------------------------------

            If FacturaBodega = True Then
                CadenaDiv = NumeroFactura.Split("-")
                SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') AND (Numero_Factura LIKE '" & CadenaDiv(0) & "%') ORDER BY Numero_Factura DESC"
            ElseIf FacturaSerie = True Then
                SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') AND (Numero_Factura LIKE '" & Serie & "%') ORDER BY Numero_Factura DESC"
            Else
                SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') ORDER BY Numero_Factura DESC"
            End If


            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "Facturas")
            If DataSet.Tables("Facturas").Rows.Count <> 0 Then
                NumeroFactura = DataSet.Tables("Facturas").Rows(0)("Numero_Factura")
                Numero = Mid(NumeroFactura, Len(Serie) + 1, Len(NumeroFactura))
                ConsecutivoFactura = Numero + 1
            End If


            If FacturaBodega = True Then
                NumeroFactura = Cod_Bodega & "-" & Format(ConsecutivoFactura, "0000#")
            ElseIf FacturaSerie = True Then
                NumeroFactura = Serie & Format(ConsecutivoFactura, "0000#")
            Else
                NumeroFactura = Format(ConsecutivoFactura, "0000#")
            End If
        End If

        'Else
        '    NumeroFactura = FrmFacturas.TxtNumeroEnsamble.Text

        'End If
        GenerarNumeroFactura = NumeroFactura


    End Function


    Public Sub NumeroTecla(ByVal Numeros As Boolean)
        If Numeros = True Then
            FrmTeclado.CmdQ.Text = "1"
            FrmTeclado.CmdW.Text = "2"
            FrmTeclado.CmdE.Text = "3"
            FrmTeclado.CmdR.Text = "4"
            FrmTeclado.CmdT.Text = "5"
            FrmTeclado.CmdY.Text = "6"
            FrmTeclado.CmdU.Text = "7"
            FrmTeclado.CmdI.Text = "8"
            FrmTeclado.CmdO.Text = "9"
            FrmTeclado.CmdP.Text = "0"
            FrmTeclado.CmdA.Text = "!"
            FrmTeclado.CmdS.Text = "@"
            FrmTeclado.CmdD.Text = "#"
            FrmTeclado.CmdF.Text = "$"
            FrmTeclado.CmdG.Text = "/"
            FrmTeclado.CmdH.Text = "+"
            FrmTeclado.CmdJ.Text = "&"
            FrmTeclado.CmdK.Text = "*"
            FrmTeclado.CmdL.Text = "("
            FrmTeclado.Cmd—.Text = ")"
            FrmTeclado.CmdZ.Text = "-"
            FrmTeclado.CmdX.Text = "%"
            FrmTeclado.CmdC.Text = "="
            FrmTeclado.CmdV.Text = "?"
            FrmTeclado.CmdB.Text = "ø"
            FrmTeclado.CmdN.Text = ":"
            FrmTeclado.CmdM.Text = ";"
            FrmTeclado.CmdEspacio.Text = "Espacio"
        ElseIf FrmTeclado.CmdMinusculas.Visible = True Then
            FrmTeclado.CmdQ.Text = "q"
            FrmTeclado.CmdW.Text = "w"
            FrmTeclado.CmdE.Text = "e"
            FrmTeclado.CmdR.Text = "r"
            FrmTeclado.CmdT.Text = "t"
            FrmTeclado.CmdY.Text = "y"
            FrmTeclado.CmdU.Text = "u"
            FrmTeclado.CmdI.Text = "i"
            FrmTeclado.CmdO.Text = "o"
            FrmTeclado.CmdP.Text = "p"
            FrmTeclado.CmdA.Text = "a"
            FrmTeclado.CmdS.Text = "s"
            FrmTeclado.CmdD.Text = "d"
            FrmTeclado.CmdF.Text = "f"
            FrmTeclado.CmdG.Text = "g"
            FrmTeclado.CmdH.Text = "h"
            FrmTeclado.CmdJ.Text = "j"
            FrmTeclado.CmdK.Text = "k"
            FrmTeclado.CmdL.Text = "l"
            FrmTeclado.Cmd—.Text = "Ò"
            FrmTeclado.CmdZ.Text = "z"
            FrmTeclado.CmdX.Text = "x"
            FrmTeclado.CmdC.Text = "c"
            FrmTeclado.CmdV.Text = "v"
            FrmTeclado.CmdB.Text = "b"
            FrmTeclado.CmdN.Text = "n"
            FrmTeclado.CmdM.Text = "m"
            FrmTeclado.CmdEspacio.Text = "Espacio"
        ElseIf FrmTeclado.CmdMayusculas.Visible = True Then
            FrmTeclado.CmdQ.Text = "Q"
            FrmTeclado.CmdW.Text = "W"
            FrmTeclado.CmdE.Text = "E"
            FrmTeclado.CmdR.Text = "R"
            FrmTeclado.CmdT.Text = "T"
            FrmTeclado.CmdY.Text = "Y"
            FrmTeclado.CmdU.Text = "U"
            FrmTeclado.CmdI.Text = "I"
            FrmTeclado.CmdO.Text = "O"
            FrmTeclado.CmdP.Text = "P"
            FrmTeclado.CmdA.Text = "A"
            FrmTeclado.CmdS.Text = "S"
            FrmTeclado.CmdD.Text = "D"
            FrmTeclado.CmdF.Text = "F"
            FrmTeclado.CmdG.Text = "G"
            FrmTeclado.CmdH.Text = "H"
            FrmTeclado.CmdJ.Text = "J"
            FrmTeclado.CmdK.Text = "K"
            FrmTeclado.CmdL.Text = "L"
            FrmTeclado.Cmd—.Text = "—"
            FrmTeclado.CmdZ.Text = "Z"
            FrmTeclado.CmdX.Text = "X"
            FrmTeclado.CmdC.Text = "C"
            FrmTeclado.CmdV.Text = "V"
            FrmTeclado.CmdB.Text = "B"
            FrmTeclado.CmdN.Text = "N"
            FrmTeclado.CmdM.Text = "M"
            FrmTeclado.CmdEspacio.Text = "ESPACIO"

        End If




    End Sub
    Public Sub LetraTecla(ByVal Minusculas As Boolean)
        If Minusculas = True Then
            FrmTeclado.CmdQ.Text = "q"
            FrmTeclado.CmdW.Text = "w"
            FrmTeclado.CmdE.Text = "e"
            FrmTeclado.CmdR.Text = "r"
            FrmTeclado.CmdT.Text = "t"
            FrmTeclado.CmdY.Text = "y"
            FrmTeclado.CmdU.Text = "u"
            FrmTeclado.CmdI.Text = "i"
            FrmTeclado.CmdO.Text = "o"
            FrmTeclado.CmdP.Text = "p"
            FrmTeclado.CmdA.Text = "a"
            FrmTeclado.CmdS.Text = "s"
            FrmTeclado.CmdD.Text = "d"
            FrmTeclado.CmdF.Text = "f"
            FrmTeclado.CmdG.Text = "g"
            FrmTeclado.CmdH.Text = "h"
            FrmTeclado.CmdJ.Text = "j"
            FrmTeclado.CmdK.Text = "k"
            FrmTeclado.CmdL.Text = "l"
            FrmTeclado.Cmd—.Text = "Ò"
            FrmTeclado.CmdZ.Text = "z"
            FrmTeclado.CmdX.Text = "x"
            FrmTeclado.CmdC.Text = "c"
            FrmTeclado.CmdV.Text = "v"
            FrmTeclado.CmdB.Text = "b"
            FrmTeclado.CmdN.Text = "n"
            FrmTeclado.CmdM.Text = "m"
            FrmTeclado.CmdEspacio.Text = "Espacio"
        Else
            FrmTeclado.CmdQ.Text = "Q"
            FrmTeclado.CmdW.Text = "W"
            FrmTeclado.CmdE.Text = "E"
            FrmTeclado.CmdR.Text = "R"
            FrmTeclado.CmdT.Text = "T"
            FrmTeclado.CmdY.Text = "Y"
            FrmTeclado.CmdU.Text = "U"
            FrmTeclado.CmdI.Text = "I"
            FrmTeclado.CmdO.Text = "O"
            FrmTeclado.CmdP.Text = "P"
            FrmTeclado.CmdA.Text = "A"
            FrmTeclado.CmdS.Text = "S"
            FrmTeclado.CmdD.Text = "D"
            FrmTeclado.CmdF.Text = "F"
            FrmTeclado.CmdG.Text = "G"
            FrmTeclado.CmdH.Text = "H"
            FrmTeclado.CmdJ.Text = "J"
            FrmTeclado.CmdK.Text = "K"
            FrmTeclado.CmdL.Text = "L"
            FrmTeclado.Cmd—.Text = "—"
            FrmTeclado.CmdZ.Text = "Z"
            FrmTeclado.CmdX.Text = "X"
            FrmTeclado.CmdC.Text = "C"
            FrmTeclado.CmdV.Text = "V"
            FrmTeclado.CmdB.Text = "B"
            FrmTeclado.CmdN.Text = "N"
            FrmTeclado.CmdM.Text = "M"
            FrmTeclado.CmdEspacio.Text = "ESPACIO"

        End If




    End Sub



End Module
