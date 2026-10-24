Imports System.Math

Public Class FrmPago
    Public MontoAPagar As Double = 0, MontoCambio As Double = 0, Numero_Factura As String
    Private ActualizandoMonedaMetodo As Boolean = False
    Public Sub Cargar_Pagos(NumeroFactura)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlString As String
        Dim MonedaFactura As String = "Cordobas", FechaFactura As Date
        Dim i As Double = 0, Cadena As String = "", Monto As Double = 0

        '/////////////////////CONSULTO LOS DATOS DE LA FACTURA ///////////////////////////
        SqlString = "SELECT Facturas.* FROM Facturas WHERE  (Numero_Factura = '" & Numero_Factura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Factura")
        If Not DataSet.Tables("Factura").Rows.Count = 0 Then
            FechaFactura = DataSet.Tables("Factura").Rows(0)("Fecha_Factura")
            MonedaFactura = DataSet.Tables("Factura").Rows(0)("MonedaFactura")
        End If

        '///////////////////////CONSULTO DETALLE DE FACTURA //////////////////////////////
        'SqlString = "SELECT Numero_Factura, Fecha_Factura, Tipo_Factura, NombrePago, Monto, NumeroTarjeta, FechaVence, Arqueado FROM Detalle_MetodoFacturas WHERE (Numero_Factura = '" & Numero_Factura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Format(FechaFactura, "yyyy-MM-dd") & "', 102)) AND (Tipo_Factura = 'Factura')"
        SqlString = "SELECT * FROM  Detalle_MetodoFacturas INNER JOIN MetodoPago ON Detalle_MetodoFacturas.NombrePago = MetodoPago.NombrePago WHERE  (Detalle_MetodoFacturas.Numero_Factura = '" & Numero_Factura & "') AND (Detalle_MetodoFacturas.Fecha_Factura = CONVERT(DATETIME, '" & Format(FechaFactura, "yyyy-MM-dd") & "', 102)) AND (Detalle_MetodoFacturas.Tipo_Factura = 'Factura')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "MetodoPago")
        i = 0
        Do While DataSet.Tables("MetodoPago").Rows.Count > i
            Monto = DataSet.Tables("MetodoPago").Rows(i)("Monto")
            Cadena = DataSet.Tables("MetodoPago").Rows(i)("NombrePago") & " - " & DataSet.Tables("MetodoPago").Rows(i)("Moneda") & " - " & Monto
            Me.ListBoxMetodos.Items.Add(Cadena)
            i = i + 1
        Loop

        Me.TxtImporte.Text = "0.00"

        Monto = CalcularPago(Me.TxtImporte.Text, Me.CboMoneda.Text)
        Me.txtTotalPagado.Text = Monto

        Leer("C")




    End Sub

    Public Sub AgregarMetodo(TipoPago As String)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Cambio As Double, SqlString As String, Cadena As String, Pago As Double
        Dim MonedaFactura As String = "Cordobas", FechaFactura As Date

        '/////////////////////CONSULTO LOS DATOS DE LA FACTURA ///////////////////////////
        SqlString = "SELECT Facturas.* FROM Facturas WHERE  (Numero_Factura = '" & Numero_Factura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Factura")
        If Not DataSet.Tables("Factura").Rows.Count = 0 Then
            FechaFactura = DataSet.Tables("Factura").Rows(0)("Fecha_Factura")
            MonedaFactura = DataSet.Tables("Factura").Rows(0)("MonedaFactura")
        End If

        If TipoPago = "Cambio" Then
            '///////////////////agrego cambio //////////////////////////////
            If Me.TxtCambio.Text <> "" Then
                Cambio = Me.TxtCambio.Text
                If Cambio <> 0 Then
                    SqlString = "SELECT NombrePago, TipoPago, Cod_Cuenta, Moneda FROM  MetodoPago WHERE (TipoPago LIKE '%" & TipoPago & "%') AND (Moneda = '" & MonedaFactura & "')"
                    DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                    DataAdapter.Fill(DataSet, "MetodoPago")
                    If Not DataSet.Tables("MetodoPago").Rows.Count = 0 Then
                        Cadena = DataSet.Tables("MetodoPago").Rows(0)("NombrePago") & " - " & DataSet.Tables("MetodoPago").Rows(0)("Moneda") & " - " & TxtCambio.Text
                        Me.ListBoxMetodos.Items.Add(Cadena)
                    End If
                End If
            End If

        ElseIf TipoPago = "Efectivo" Then

            If Me.TxtSaldoDeudor.Text <> "" Then
                Pago = Me.TxtSaldoDeudor.Text
                If Pago <> 0 Then
                    SqlString = "SELECT NombrePago, TipoPago, Cod_Cuenta, Moneda FROM  MetodoPago WHERE (TipoPago LIKE '%" & TipoPago & "%') AND (Moneda = '" & MonedaFactura & "')"
                    DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                    DataAdapter.Fill(DataSet, "MetodoPago")
                    If Not DataSet.Tables("MetodoPago").Rows.Count = 0 Then
                        Cadena = DataSet.Tables("MetodoPago").Rows(0)("NombrePago") & " - " & DataSet.Tables("MetodoPago").Rows(0)("Moneda") & " - " & Pago
                        Me.ListBoxMetodos.Items.Add(Cadena)
                    End If
                End If
            End If


        End If


        Leer("C")
    End Sub
    Public Function APlicarPagos() As Boolean

        Dim i As Integer
        Dim Cadena As String = ""

        Dim NombrePago As String = ""
        Dim MonedaPago As String = ""
        Dim MontoDbl As Double
        Dim MontoPago As Decimal

        Dim MonedaFactura As String = "Cordobas"
        Dim FechaFactura As Date
        Dim Cambio As Decimal

        Dim SqlString As String = ""
        Dim StrSqlUpdate As String = ""

        Dim DataSet As New DataSet
        Dim Transaccion As SqlClient.SqlTransaction = Nothing

        'Número de tarjeta y vencimiento de los pagos ya registrados, para no perderlos al reaplicar
        Dim DatosTarjeta As New System.Collections.Generic.Dictionary(Of String, Object())(StringComparer.OrdinalIgnoreCase)
        Dim Datos() As Object = Nothing

        '////////////////////////////////////////////////////////////
        ' CONSULTAR LOS DATOS DE LA FACTURA
        '////////////////////////////////////////////////////////////

        SqlString = "SELECT Fecha_Factura, MonedaFactura " &
                "FROM Facturas " &
                "WHERE Numero_Factura = @NumeroFactura"

        Using ComandoConsulta As New SqlClient.SqlCommand(SqlString, MiConexion)

            ComandoConsulta.Parameters.Add(
            "@NumeroFactura",
            SqlDbType.NVarChar,
            50
        ).Value = Numero_Factura

            Using Adaptador As New SqlClient.SqlDataAdapter(ComandoConsulta)

                Adaptador.Fill(DataSet, "Consulta")

            End Using

        End Using

        If DataSet.Tables("Consulta").Rows.Count = 0 Then
            MessageBox.Show(
            "No se encontró la factura " & Numero_Factura,
            "Aplicación de pagos",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )
            Return False
        End If

        FechaFactura = CDate(
        DataSet.Tables("Consulta").Rows(0)("Fecha_Factura")
    )

        MonedaFactura = CStr(
        DataSet.Tables("Consulta").Rows(0)("MonedaFactura")
    )

        '////////////////////////////////////////////////////////////
        ' AGREGAR EL CAMBIO COMO MÉTODO DE PAGO
        '////////////////////////////////////////////////////////////

        If Me.TxtCambio.Text.Trim() <> "" Then

            If Decimal.TryParse(
            Me.TxtCambio.Text,
            Globalization.NumberStyles.Number,
            Globalization.CultureInfo.CurrentCulture,
            Cambio
        ) AndAlso Cambio <> 0D Then

                SqlString = "SELECT TOP 1 NombrePago " &
                        "FROM MetodoPago " &
                        "WHERE TipoPago LIKE '%Cambio%'"

                Using ComandoCambio As New SqlClient.SqlCommand(
                SqlString, MiConexion
            )

                    If MiConexion.State <> ConnectionState.Open Then
                        MiConexion.Open()
                    End If

                    Try

                        Dim Resultado As Object =
                        ComandoCambio.ExecuteScalar()

                        If Resultado IsNot Nothing AndAlso
                       Resultado IsNot DBNull.Value Then

                            Cadena = CStr(Resultado) & " - " &
                                 CStr(Resultado) & " - " &
                                 Me.TxtCambio.Text

                            Me.ListBoxMetodos.Items.Add(Cadena)

                        End If

                    Finally

                        If MiConexion.State = ConnectionState.Open Then
                            MiConexion.Close()
                        End If

                    End Try

                End Using

            End If

        End If

        '////////////////////////////////////////////////////////////
        ' REEMPLAZAR LOS MÉTODOS DE PAGO (ELIMINAR + INSERTAR EN UNA SOLA TRANSACCIÓN)
        '////////////////////////////////////////////////////////////

        If MiConexion.State <> ConnectionState.Open Then
            MiConexion.Open()
        End If

        Try

            Transaccion = MiConexion.BeginTransaction()

            '--------- Conservar tarjeta y vencimiento de los pagos actuales ---------
            SqlString = "SELECT NombrePago, NumeroTarjeta, FechaVence " &
                    "FROM Detalle_MetodoFacturas " &
                    "WHERE Numero_Factura = @NumeroFactura " &
                    "AND Fecha_Factura = @FechaFactura " &
                    "AND Tipo_Factura = @TipoFactura"

            Using ComandoTarjeta As New SqlClient.SqlCommand(
            SqlString, MiConexion, Transaccion
        )

                ComandoTarjeta.Parameters.Add("@NumeroFactura", SqlDbType.NVarChar, 50).Value = Numero_Factura
                ComandoTarjeta.Parameters.Add("@FechaFactura", SqlDbType.SmallDateTime).Value = FechaFactura
                ComandoTarjeta.Parameters.Add("@TipoFactura", SqlDbType.NVarChar, 50).Value = "Factura"

                Using Lector As SqlClient.SqlDataReader = ComandoTarjeta.ExecuteReader()

                    Do While Lector.Read()
                        DatosTarjeta(CStr(Lector("NombrePago")).Trim()) =
                        New Object() {Lector("NumeroTarjeta"), Lector("FechaVence")}
                    Loop

                End Using

            End Using

            '--------- Eliminar los métodos de pago anteriores ---------
            StrSqlUpdate = "DELETE FROM Detalle_MetodoFacturas " &
                       "WHERE Numero_Factura = @NumeroFactura " &
                       "AND Fecha_Factura = @FechaFactura " &
                       "AND Tipo_Factura = @TipoFactura"

            Using ComandoDelete As New SqlClient.SqlCommand(
            StrSqlUpdate, MiConexion, Transaccion
        )

                ComandoDelete.Parameters.Add("@NumeroFactura", SqlDbType.NVarChar, 50).Value = Numero_Factura
                ComandoDelete.Parameters.Add("@FechaFactura", SqlDbType.SmallDateTime).Value = FechaFactura
                ComandoDelete.Parameters.Add("@TipoFactura", SqlDbType.NVarChar, 50).Value = "Factura"

                ComandoDelete.ExecuteNonQuery()

            End Using

            '--------- Insertar los métodos de pago ---------
            StrSqlUpdate = "INSERT INTO Detalle_MetodoFacturas " &
                       "(Numero_Factura, Fecha_Factura, Tipo_Factura, " &
                       "NombrePago, Monto, NumeroTarjeta, FechaVence, Arqueado) " &
                       "VALUES " &
                       "(@NumeroFactura, @FechaFactura, @TipoFactura, " &
                       "@NombrePago, @Monto, @NumeroTarjeta, @FechaVence, @Arqueado)"

            Using ComandoInsert As New SqlClient.SqlCommand(
            StrSqlUpdate, MiConexion, Transaccion
        )

                ComandoInsert.Parameters.Add("@NumeroFactura", SqlDbType.NVarChar, 50)
                ComandoInsert.Parameters.Add("@FechaFactura", SqlDbType.SmallDateTime)
                ComandoInsert.Parameters.Add("@TipoFactura", SqlDbType.NVarChar, 50)
                ComandoInsert.Parameters.Add("@NombrePago", SqlDbType.NVarChar, 50)
                ComandoInsert.Parameters.Add("@Monto", SqlDbType.Decimal).Precision = 18
                ComandoInsert.Parameters("@Monto").Scale = 2
                ComandoInsert.Parameters.Add("@NumeroTarjeta", SqlDbType.NVarChar, 50)
                ComandoInsert.Parameters.Add("@FechaVence", SqlDbType.SmallDateTime)
                ComandoInsert.Parameters.Add("@Arqueado", SqlDbType.Bit)

                'La llave primaria incluye NombrePago: los elementos repetidos se suman en una sola fila
                Dim Totales As New System.Collections.Generic.Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)
                Dim Orden As New System.Collections.Generic.List(Of String)

                For i = 0 To Me.ListBoxMetodos.Items.Count - 1

                    Cadena = CStr(Me.ListBoxMetodos.Items(i))
                    If Not ParsearMetodo(Cadena, NombrePago, MonedaPago, MontoDbl) Then
                        Throw New Exception(
                        "El método de pago tiene un formato incorrecto: " &
                        Cadena
                    )
                    End If

                    If Totales.ContainsKey(NombrePago) Then
                        Totales(NombrePago) += CDec(MontoDbl)
                    Else
                        Totales.Add(NombrePago, CDec(MontoDbl))
                        Orden.Add(NombrePago)
                    End If

                Next

                For Each NombrePago In Orden

                    MontoPago = Totales(NombrePago)

                    ComandoInsert.Parameters("@NumeroFactura").Value = Numero_Factura
                    ComandoInsert.Parameters("@FechaFactura").Value = FechaFactura
                    ComandoInsert.Parameters("@TipoFactura").Value = "Factura"
                    ComandoInsert.Parameters("@NombrePago").Value = NombrePago
                    ComandoInsert.Parameters("@Monto").Value = MontoPago

                    If DatosTarjeta.TryGetValue(NombrePago, Datos) Then
                        ComandoInsert.Parameters("@NumeroTarjeta").Value = Datos(0)
                        ComandoInsert.Parameters("@FechaVence").Value = Datos(1)
                    Else
                        ComandoInsert.Parameters("@NumeroTarjeta").Value = "0000"
                        ComandoInsert.Parameters("@FechaVence").Value = FechaFactura
                    End If

                    ComandoInsert.Parameters("@Arqueado").Value = False

                    ComandoInsert.ExecuteNonQuery()

                Next NombrePago

            End Using

            Transaccion.Commit()
            Return True

        Catch ex As Exception

            If Transaccion IsNot Nothing Then
                Try
                    Transaccion.Rollback()
                Catch
                    'La transacción ya no está activa; no hay nada que revertir
                End Try
            End If

            MessageBox.Show(
            "No se pudieron aplicar los pagos. No se realizó ningún cambio." &
            vbCrLf & ex.Message,
            "Aplicación de pagos",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )

            Return False

        Finally

            If MiConexion.State = ConnectionState.Open Then
                MiConexion.Close()
            End If

        End Try

    End Function

    'Interpreta un elemento de ListBoxMetodos con formato "Nombre - Moneda - Monto".
    'Se toman la moneda y el monto desde el final para tolerar guiones en el nombre.
    Private Function ParsearMetodo(Cadena As String, ByRef NombrePago As String, ByRef MonedaPago As String, ByRef Monto As Double) As Boolean
        Dim Separador As String = " - "
        Dim PosMonto As Integer = Cadena.LastIndexOf(Separador)
        If PosMonto <= 0 Then Return False

        Dim PosMoneda As Integer = Cadena.LastIndexOf(Separador, PosMonto - 1)
        If PosMoneda <= 0 Then Return False

        NombrePago = Cadena.Substring(0, PosMoneda).Trim()
        MonedaPago = Cadena.Substring(PosMoneda + Separador.Length, PosMonto - PosMoneda - Separador.Length).Trim()

        Return Double.TryParse(Cadena.Substring(PosMonto + Separador.Length).Trim(),
                               Globalization.NumberStyles.Number,
                               Globalization.CultureInfo.CurrentCulture,
                               Monto)
    End Function

    Public Function CalcularPago(Numero_Pantalla As Double, MonedaPantalla As String) As Double
        Dim i As Double, Cadena As String = "", Lista() As String
        Dim MonedaPago As String, NombrePago As String, MontoPago As Double
        Dim Tasacambio As Double = 0, Monto As Double = 0, MonedaFactura As String = "Cordobas"
        Dim SqlString As String, TipoPago As String = "", TotalApagar As Double, TotalPagado As Double, Diferencia As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, FechaFactura As Date = Date.Today

        '/////////////////////CONSULTO LOS DATOS DE LA FACTURA ///////////////////////////
        SqlString = "SELECT Facturas.* FROM Facturas WHERE  (Numero_Factura = @NumeroFactura)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.SelectCommand.Parameters.Add("@NumeroFactura", SqlDbType.NVarChar, 50).Value = Numero_Factura
        DataAdapter.Fill(DataSet, "Factura")
        If Not DataSet.Tables("Factura").Rows.Count = 0 Then
            FechaFactura = DataSet.Tables("Factura").Rows(0)("Fecha_Factura")
            MonedaFactura = DataSet.Tables("Factura").Rows(0)("MonedaFactura")
        End If

        If Trim(MonedaPantalla) <> MonedaFactura Then

            If MonedaPantalla = "Cordobas" Then
                Numero_Pantalla = Numero_Pantalla / BuscaTasaCambio(FechaFactura)
            ElseIf MonedaPantalla = "Dolares" Then
                Numero_Pantalla = Numero_Pantalla * BuscaTasaCambio(FechaFactura)
            End If

        End If

        '////////////////////////////DATOS EN PANTALLA ///////////////////////////
        If Me.CboMetodoPago.Text.Trim() <> "" Then
            SqlString = "SELECT NombrePago, TipoPago, Cod_Cuenta, Moneda FROM MetodoPago WHERE (NombrePago = @NombrePago)"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.SelectCommand.Parameters.Add("@NombrePago", SqlDbType.NVarChar, 50).Value = Me.CboMetodoPago.Text.Trim()
            DataAdapter.Fill(DataSet, "CambioPantalla")
            If Not DataSet.Tables("CambioPantalla").Rows.Count = 0 Then
                TipoPago = DataSet.Tables("CambioPantalla").Rows(0)("TipoPago")
                If TipoPago = "Cambio" Then
                    Numero_Pantalla = Numero_Pantalla * -1
                End If
            End If
        End If


        MontoPago = 0
        For i = 0 To Me.ListBoxMetodos.Items.Count - 1
            Cadena = ListBoxMetodos.Items(i)

            If Not ParsearMetodo(Cadena, NombrePago, MonedaPago, Monto) Then
                Throw New Exception("El método de pago tiene un formato incorrecto: " & Cadena)
            End If

            '/////////////////////CONSULTO SI ES CAMBIO ///////////////////////////
            SqlString = "SELECT NombrePago, TipoPago, Cod_Cuenta, Moneda FROM MetodoPago WHERE  (NombrePago = @NombrePago) AND (TipoPago = 'Cambio')"
            'Fill acumula filas: se descarta la tabla para que cada método se evalúe por separado
            If DataSet.Tables.Contains("MetodoCambio") Then DataSet.Tables.Remove("MetodoCambio")
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.SelectCommand.Parameters.Add("@NombrePago", SqlDbType.NVarChar, 50).Value = NombrePago
            DataAdapter.Fill(DataSet, "MetodoCambio")
            If Not DataSet.Tables("MetodoCambio").Rows.Count = 0 Then
                TipoPago = DataSet.Tables("MetodoCambio").Rows(0)("TipoPago")
                If TipoPago = "Cambio" Then
                    Monto = Monto * -1
                End If

            End If



            '////////////////////////////////////////////////////////////////////////
            '//////////////////Consulto los montos y las monedas /////////////////////
            '/////////////////////////////////////////////////////////////////////////
            If Trim(MonedaPago) = MonedaFactura Then
                MontoPago = Monto + MontoPago
            ElseIf Trim(MonedaPago) = "Cordobas" Then
                '///////Convertir Moneda pago a Dolares //////////////////////////
                Monto = Monto / BuscaTasaCambio(FechaFactura)
                MontoPago = Monto + MontoPago
            ElseIf Trim(MonedaPago) = "Dolares" Then
                '///////Convertir Moneda pago a Cordobas //////////////////////////
                Monto = Monto * BuscaTasaCambio(FechaFactura)
                MontoPago = Monto + MontoPago
            End If

        Next

        TotalApagar = TxtTotalApagar.Text
        TotalPagado = Format(MontoPago + Numero_Pantalla, "##,##0.00")
        Diferencia = Round(TotalApagar - TotalPagado, 2)


        If Diferencia > 0 Then
            TxtSaldoDeudor.Text = Format(Diferencia, "##,##0.00")
            TxtSaldoFavor.Text = "0.00"
            txtDiferencia.Text = Format(Diferencia, "##,##0.00")
            Me.TxtCambio.Text = "0.00"

        ElseIf Diferencia < 0 Then
            TxtSaldoDeudor.Text = "0.00"
            TxtSaldoFavor.Text = Format(Abs(Diferencia), "##,##0.00")
            txtDiferencia.Text = Format(Diferencia, "##,##0.00")
            TxtCambio.Text = Format(Abs(Diferencia), "##,##0.00")
        Else
            TxtSaldoDeudor.Text = "0.00"
            TxtSaldoFavor.Text = "0.00"
            txtDiferencia.Text = "0.00"
            TxtCambio.Text = "0.00"
        End If

        CalcularPago = TotalPagado


    End Function

    Public Sub Borrar(Numero As Double)
        Dim Posicion As Double, i As Double = 0, Longitud As Double = 0, CadenaNueva As String = ""
        Dim PosicionFinal As Double = 0, Cadena As String, Numero_Nuevo As Double = 0, eSdecimal As Boolean = False


        If Numero = 0 Then
            Exit Sub
        End If

        Cadena = Numero
        Longitud = Len(Cadena)
        Posicion = Longitud - 1
        CadenaNueva = Mid(Cadena, 1, Longitud - 1)
        If CadenaNueva <> "" Then
            Numero_Nuevo = CadenaNueva
        Else
            Numero_Nuevo = 0
        End If

        Me.TxtImporte.Text = Format(Numero_Nuevo, "##,##0.00")

        Me.TxtImporte.Focus()
    End Sub
    Public Sub Leer(Numero As String)
        Dim Numero_Pantalla As Double = 0, TotalApagar As Double = 0, Diferencia As Double = 0
        Dim Cambio As Double = 0, UltDigito As String = ""
        Dim SaldoDeudor As Double = 0, MontoPagado As Double = 0


        If IsNumeric(TxtTotalApagar.Text) Then
            TotalApagar = TxtTotalApagar.Text
        Else
            TotalApagar = 0
        End If




        '////////////////////////////VERIFICO SI EL ULTIMO DIGITO ES PUNTO
        If Me.TxtImporte.Text <> "" Then
            UltDigito = Mid(TxtImporte.Text, Len(TxtImporte.Text), Len(TxtImporte.Text))
        End If

        If IsNumeric(Me.TxtImporte.Text) Then
            Numero_Pantalla = Me.TxtImporte.Text
        Else
            TxtImporte.Text = "0.00"
            Numero_Pantalla = 0
        End If

        Select Case Numero
            Case "C"
                TxtImporte.Text = "0.00"
                TxtCambio.Text = "0.00"
                TxtSaldoFavor.Text = "0.00"
                TxtSaldoDeudor.Text = TxtTotalApagar.Text
                txtTotalPagado.Text = "0.00"
                txtDiferencia.Text = "0.00"

            Case "OK"
                TxtTotalApagar.Text = Format(TotalApagar, "##,##0.00")
                TxtImporte.Text = Format(Numero_Pantalla, "##,##0.00")
                Diferencia = TotalApagar - Numero_Pantalla
                TxtCambio.Text = Format(Diferencia, "##,##0.00")

                If Diferencia > 0 Then
                    TxtSaldoDeudor.Text = Format(Diferencia, "##,##0.00")
                    TxtSaldoFavor.Text = "0.00"
                    txtDiferencia.Text = Format(Diferencia, "##,##0.00")
                ElseIf Diferencia < 0 Then
                    TxtSaldoDeudor.Text = "0.00"
                    TxtSaldoFavor.Text = Format(Abs(Diferencia), "##,##0.00")
                    txtDiferencia.Text = Format(Diferencia, "##,##0.00")
                End If

            Case "DEL"
                Borrar(TxtImporte.Text)

            Case "."
                'Se parte de la parte entera (el texto puede ser "12.00") y se ignora si ya hay decimales
                If UltDigito <> "." AndAlso Not CStr(Numero_Pantalla).Contains(".") Then
                    TxtImporte.Text = CStr(Numero_Pantalla) & "."
                End If

        End Select

        If IsNumeric(TxtTotalApagar.Text) Then
            TotalApagar = TxtTotalApagar.Text
        Else
            TotalApagar = 0
        End If

        If IsNumeric(Me.TxtImporte.Text) Then
            If Numero <> "." Then
                Numero_Pantalla = Me.TxtImporte.Text
            End If
        Else
            TxtImporte.Text = "0.00"
            Numero_Pantalla = 0
        End If

        If IsNumeric(Numero) Then
            If UltDigito <> "." Then
                Numero_Pantalla = CStr(Numero_Pantalla) & CStr(Numero)
            Else
                Numero_Pantalla = CStr(Numero_Pantalla) & "." & CStr(Numero)
            End If
        Else
                Numero_Pantalla = CStr(Numero_Pantalla)
        End If

        If Numero <> "." Then
            Me.TxtImporte.Text = Numero_Pantalla
        End If

        MontoPagado = CalcularPago(Numero_Pantalla, Me.CboMoneda.Text)

        If MontoPagado >= MontoAPagar Then
            Cambio = MontoPagado - MontoAPagar
            TxtCambio.Text = Format(Cambio, "##,##0.00")
            TxtSaldoFavor.Text = Format(Cambio, "##,##0.00")
            TxtSaldoDeudor.Text = Format(0, "##,##0.00")
            txtTotalPagado.Text = Format(MontoPagado, "##,##0.00")
            'Diferencia = a pagar - pagado (negativa cuando hay cambio), igual que en CalcularPago
            txtDiferencia.Text = Format(-Cambio, "##,##0.00")



        Else
            Cambio = 0
            TxtCambio.Text = Format(Cambio, "##,##0.00")
            TxtSaldoFavor.Text = Format(Cambio, "##,##0.00")
            txtTotalPagado.Text = Format(MontoPagado, "##,##0.00")
            SaldoDeudor = MontoAPagar - MontoPagado
            TxtSaldoDeudor.Text = Format(SaldoDeudor, "##,##0.00")
            txtDiferencia.Text = Format(SaldoDeudor, "##,##0.00")



        End If



    End Sub

    Private Sub Cmd1_Click(sender As Object, e As EventArgs) Handles Cmd1.Click
        Leer("1")
    End Sub

    Private Sub Cmd2_Click(sender As Object, e As EventArgs) Handles Cmd2.Click
        Leer("2")
    End Sub

    Private Sub Cmd3_Click(sender As Object, e As EventArgs) Handles Cmd3.Click
        Leer("3")
    End Sub

    Private Sub Cmd4_Click(sender As Object, e As EventArgs) Handles Cmd4.Click
        Leer("4")
    End Sub

    Private Sub Cmd5_Click(sender As Object, e As EventArgs) Handles Cmd5.Click
        Leer("5")
    End Sub

    Private Sub Cmd6_Click(sender As Object, e As EventArgs) Handles Cmd6.Click
        Leer("6")
    End Sub

    Private Sub Cmd7_Click(sender As Object, e As EventArgs) Handles Cmd7.Click
        Leer("7")
    End Sub

    Private Sub Cmd8_Click(sender As Object, e As EventArgs) Handles Cmd8.Click
        Leer("8")
    End Sub

    Private Sub Cmd9_Click(sender As Object, e As EventArgs) Handles Cmd9.Click
        Leer("9")
    End Sub

    Private Sub Cmd0_Click(sender As Object, e As EventArgs) Handles Cmd0.Click
        Leer("0")
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Leer(".")
    End Sub

    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles CmdOK.Click
        Leer("OK")
    End Sub

    Private Sub CmdDEL_Click(sender As Object, e As EventArgs) Handles CmdDEL.Click
        Leer("DEL")
    End Sub

    Private Sub CmdC_Click(sender As Object, e As EventArgs) Handles CmdC.Click
        Leer("C")
    End Sub

    Private Sub FrmPago_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DataAdapter As New SqlClient.SqlDataAdapter, DataSet As New DataSet
        Dim SQLInteres As String

        SQLInteres = "SELECT  *  FROM MetodoPago"
        DataAdapter = New SqlClient.SqlDataAdapter(SQLInteres, MiConexion)
        DataAdapter.Fill(DataSet, "MetodoPago")
        Me.CboMetodoPago.DataSource = DataSet.Tables("MetodoPago")
        Me.CboMetodoPago.Text = "Efectivo"



        TxtTotalApagar.Text = Format(MontoAPagar, "##,##0.00")
        Borrar(TxtImporte.Text)

        Cargar_Pagos(Numero_Factura)

    End Sub
    Private Function Moneda(ByVal MetodoPago As String) As String

        Dim sql As String =
        "SELECT Moneda " &
        "FROM dbo.MetodoPago " &
        "WHERE NombrePago = @NombrePago"

        Using cmd As New SqlClient.SqlCommand(sql, MiConexion)

            cmd.Parameters.Add("@NombrePago",
            SqlDbType.NVarChar, 50).Value = MetodoPago

            Dim abrirConexion As Boolean = False

            Try
                If MiConexion.State = ConnectionState.Closed Then
                    MiConexion.Open()
                    abrirConexion = True
                End If

                Dim resultado As Object = cmd.ExecuteScalar()

                If resultado Is Nothing OrElse IsDBNull(resultado) Then
                    Return String.Empty
                End If

                Return resultado.ToString().Trim()

            Finally
                If abrirConexion AndAlso
               MiConexion.State = ConnectionState.Open Then

                    MiConexion.Close()

                End If
            End Try

        End Using

    End Function


    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Me.Close()
    End Sub

    Private Sub CboMetodoPago_TextChanged(sender As Object, e As EventArgs) Handles CboMetodoPago.TextChanged


        If ActualizandoMonedaMetodo Then Exit Sub

        Try
            ActualizandoMonedaMetodo = True

            CboMoneda.Text = Moneda(CboMetodoPago.Text)

            Me.TxtImporte.Text = "0.00"

            Leer("C")

        Finally
            ActualizandoMonedaMetodo = False
        End Try


        'CboMoneda.Text = Moneda(CboMetodoPago.Text)
        'Me.TxtImporte.Text = "0.00"
        'Leer("C")



    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub BtnAplicar_Click(sender As Object, e As EventArgs) Handles BtnAplicar.Click
        If Val(Me.TxtImporte.Text) <> 0 Then
            MsgBox("Agrego el monto digitado antes de aplicar", vbExclamation, "Zeus Facturacion")
            Exit Sub
        End If

        If Val(Me.TxtCambio.Text) <> 0 Then
            MsgBox("Aplique el cambio antes de aplicar", vbExclamation, "Zeus Facturacion")
            Exit Sub
        End If

        If Val(Me.TxtSaldoDeudor.Text) <> 0 Then
            MsgBox("Agrego el saldo pendiente antes de aplicar", vbExclamation, "Zeus Facturacion")
            Exit Sub
        End If


        If Not APlicarPagos() Then Exit Sub

        Form1.C1Button4.Enabled = True

        Me.Close()

    End Sub

    Private Sub TxtSaldoDeudor_TextChanged(sender As Object, e As EventArgs) Handles TxtSaldoDeudor.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub C1Button1_Click(sender As Object, e As EventArgs) Handles C1Button1.Click
        Dim Cadena As String, Monto As Double

        If CboMetodoPago.Text.Trim() = "" OrElse CboMoneda.Text.Trim() = "" Then
            MsgBox("Seleccione el método de pago y la moneda", vbExclamation, "Zeus Facturacion")
            Exit Sub
        End If

        If Val(Replace(Me.TxtImporte.Text, ",", "")) <= 0 Then
            MsgBox("Digite un importe mayor que cero", vbExclamation, "Zeus Facturacion")
            Exit Sub
        End If

        Cadena = CboMetodoPago.Text & " - " & CboMoneda.Text & " - " & TxtImporte.Text
        Me.ListBoxMetodos.Items.Add(Cadena)

        Me.TxtImporte.Text = "0.00"

        Monto = CalcularPago(Me.TxtImporte.Text, Me.CboMoneda.Text)
        Me.txtTotalPagado.Text = Monto
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub C1Button2_Click(sender As Object, e As EventArgs) Handles C1Button2.Click
        Dim Cadena As String = "", Monto As Double = 0

        If Me.ListBoxMetodos.SelectedItem Is Nothing Then
            MsgBox("Seleccione el método de pago que desea quitar", vbExclamation, "Zeus Facturacion")
            Exit Sub
        End If

        Cadena = Me.ListBoxMetodos.SelectedItem
        ListBoxMetodos.Items.Remove(Cadena)

        Me.TxtImporte.Text = "0.00"

        Monto = CalcularPago(Me.TxtImporte.Text, Me.CboMoneda.Text)
        Me.txtTotalPagado.Text = Monto
    End Sub

    Private Sub CboMoneda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboMoneda.SelectedIndexChanged

        If ActualizandoMonedaMetodo Then Exit Sub

        Try
            ActualizandoMonedaMetodo = True

            ActualizarMetodoPagoPorMoneda(
            CboMetodoPago,
            CboMoneda)

        Finally
            ActualizandoMonedaMetodo = False
        End Try

    End Sub

    Private Sub Label4_DoubleClick(sender As Object, e As EventArgs) Handles Label4.DoubleClick
        AgregarMetodo("Cambio")
    End Sub

    Private Sub Label6_DoubleClick(sender As Object, e As EventArgs) Handles Label6.DoubleClick
        AgregarMetodo("Efectivo")
    End Sub
End Class