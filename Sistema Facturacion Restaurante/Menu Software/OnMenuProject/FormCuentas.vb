Imports System.Data.Sqlclient
Public Class FormCuentas
    Dim SelControlCuentas As Integer = -1
    Dim SelControlMesDis As Integer = -1
    Dim SelMesa As String
    Dim ZonaSel As String = ""
    Dim TablaMesas As DataTable
    Dim Ocupadas As New Dictionary(Of String, Boolean)
    Dim MisMesas As New List(Of String)
    Shared CacheImagenes As New Dictionary(Of String, Image)

    '///////////////////////// PALETA (misma del FormLog) //////////////////////
    Dim ColorPrimario As Color = Color.FromArgb(52, 94, 152)
    Dim ColorPrimarioHover As Color = Color.FromArgb(41, 78, 130)
    Dim ColorPrimarioPresionado As Color = Color.FromArgb(30, 62, 110)
    Dim ColorTexto As Color = Color.FromArgb(30, 41, 59)
    Dim ColorTextoSecundario As Color = Color.FromArgb(60, 70, 85)
    Dim ColorFondoSuave As Color = Color.FromArgb(246, 248, 251)
    Dim ColorHover As Color = Color.FromArgb(238, 241, 245)
    Dim ColorPresionado As Color = Color.FromArgb(228, 232, 238)
    Dim ColorBorde As Color = Color.FromArgb(210, 216, 226)
    Dim FuenteTarjeta As New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
    Dim FuenteCuenta As New Font("Segoe UI", 11.0!)

    Private Sub FormCuentas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblSaludo.Text = "Hola, " & StrConv(CStr(Form1.NombreVendedor), VbStrConv.ProperCase)
        CargarDatos()
        CargarMisMesas()
        CrearZonas()
        BuscarMesasDisponiles()
    End Sub

    '///////////////////////// UNA SOLA LECTURA DE MESAS Y FACTURAS ACTIVAS //////////////////////
    Private Sub CargarDatos()
        TablaMesas = New DataTable
        Dim AdapterClientes As New SqlDataAdapter("Select Cod_Cliente, Nombre_Cliente, Departamento, Foto from Clientes Where Limite_Credito = 0 Order by Departamento, Cod_Cliente", TempConn)
        AdapterClientes.Fill(TablaMesas)

        Dim TablaFacturas As New DataTable
        Dim AdapterFacturas As New SqlDataAdapter("Select Cod_Cliente, Cod_Vendedor from Facturas Where Activo = 'True' and Cancelado = 'False'", TempConn)
        AdapterFacturas.Fill(TablaFacturas)

        Ocupadas.Clear()
        MisMesas.Clear()
        Dim CodVendedor As String = CStr(Form1.CodVendedor)
        For Each Fila As DataRow In TablaFacturas.Rows
            Dim Mesa As String = Fila("Cod_Cliente").ToString
            Ocupadas(Mesa) = True
            If Fila("Cod_Vendedor").ToString = CodVendedor AndAlso Not MisMesas.Contains(Mesa) Then
                MisMesas.Add(Mesa)
            End If
        Next
    End Sub

    Private Function BuscarMesa(ByVal CodMesa As String) As DataRow
        For Each Fila As DataRow In TablaMesas.Rows
            If Fila("Cod_Cliente").ToString = CodMesa Then
                Return Fila
            End If
        Next
        Return Nothing
    End Function

    '///////////////////////// IMAGEN DE LA MESA: MINIATURA CON CACHE //////////////////////
    Private Function Miniatura(ByVal Origen As Image) As Image
        Dim Ancho As Integer = 120, Alto As Integer = 75
        Dim Bmp As New Bitmap(Ancho, Alto)
        Using G As Graphics = Graphics.FromImage(Bmp)
            G.Clear(Color.White)
            G.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            Dim Escala As Double = Math.Min(Ancho / Origen.Width, Alto / Origen.Height)
            Dim W As Integer = CInt(Origen.Width * Escala)
            Dim H As Integer = CInt(Origen.Height * Escala)
            G.DrawImage(Origen, (Ancho - W) \ 2, (Alto - H) \ 2, W, H)
        End Using
        Return Bmp
    End Function

    Private Function ImagenMesa(ByVal Fila As DataRow, ByVal Propia As Boolean) As Image
        Dim Clave As String = If(Propia, "#propia", "#libre")
        If Fila IsNot Nothing AndAlso Not IsDBNull(Fila("Foto")) AndAlso Trim(Fila("Foto").ToString) <> "" Then
            Clave = Fila("Foto").ToString
        End If

        Dim Img As Image = Nothing
        If Not CacheImagenes.TryGetValue(Clave, Img) Then
            Try
                If Clave = "#propia" Then
                    Img = Miniatura(My.Resources.Mesa_Roja)
                ElseIf Clave = "#libre" Then
                    Img = Miniatura(My.Resources.MesaRoja3)
                Else
                    Img = Miniatura(BytesToImagen(StringtoByte(Clave)))
                End If
            Catch
                Img = Miniatura(My.Resources.MesaRoja3)
            End Try
            CacheImagenes.Add(Clave, Img)
        End If
        Return Img
    End Function

    Private Function TextoMesa(ByVal Fila As DataRow, ByVal CodMesa As String) As String
        '--- solo el codigo de la mesa (M01, B01, TT01) para mantener la tarjeta compacta en tablets
        Return CodMesa
    End Function

    '///////////////////////// TARJETA DE MESA //////////////////////
    Private Function CrearBotonMesa(ByVal CodMesa As String, ByVal Propia As Boolean, ByVal Manejador As EventHandler) As Button
        Dim Fila As DataRow = BuscarMesa(CodMesa)
        Dim btn As New Button
        btn.Tag = CodMesa
        btn.Text = TextoMesa(Fila, CodMesa)
        btn.Size = New Size(134, 112)
        btn.Margin = New Padding(5)
        btn.Padding = New Padding(0, 8, 0, 4)
        btn.BackColor = Color.White
        btn.ForeColor = ColorTexto
        btn.Font = FuenteTarjeta
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.BorderColor = ColorBorde
        btn.FlatAppearance.MouseOverBackColor = ColorHover
        btn.FlatAppearance.MouseDownBackColor = ColorPresionado
        btn.Image = ImagenMesa(Fila, Propia)
        btn.ImageAlign = ContentAlignment.TopCenter
        btn.TextAlign = ContentAlignment.BottomCenter
        btn.AutoEllipsis = True
        btn.UseVisualStyleBackColor = False
        AddHandler btn.Click, Manejador
        Return btn
    End Function

    Private Sub CargarMisMesas()
        DeletePanelItems(Me.FLPMisMesas)
        For Each CodMesa As String In MisMesas
            Me.FLPMisMesas.Controls.Add(CrearBotonMesa(CodMesa, True, AddressOf BuscarCuentas))
        Next
    End Sub

    Private Sub MarcarMesaSeleccionada()
        For Each Ctl As Control In FLPMisMesas.Controls
            Dim btn As Button = CType(Ctl, Button)
            If CStr(btn.Tag) = SelMesa Then
                btn.FlatAppearance.BorderColor = ColorPrimario
                btn.FlatAppearance.BorderSize = 3
            Else
                btn.FlatAppearance.BorderColor = ColorBorde
                btn.FlatAppearance.BorderSize = 1
            End If
        Next
    End Sub

    '///////////////////////// FILTRO POR ZONA (Clientes.Departamento) //////////////////////
    Private Sub CrearZonas()
        Dim Zonas As New List(Of String)
        For Each Fila As DataRow In TablaMesas.Rows
            If Not IsDBNull(Fila("Departamento")) Then
                Dim Zona As String = Trim(Fila("Departamento").ToString)
                If Zona <> "" AndAlso Not Zonas.Contains(Zona) Then
                    Zonas.Add(Zona)
                End If
            End If
        Next
        If Zonas.Count = 0 Then
            FLPZonas.Visible = False
            Exit Sub
        End If

        AgregarBotonZona("Todas", "")
        For Each Zona As String In Zonas
            AgregarBotonZona(Zona, Zona)
        Next
        MarcarZona()
    End Sub

    Private Sub AgregarBotonZona(ByVal Texto As String, ByVal Zona As String)
        Dim btn As New Button
        btn.Text = Texto
        btn.Tag = Zona
        btn.AutoSize = True
        btn.MinimumSize = New Size(100, 40)
        btn.Margin = New Padding(0, 0, 8, 0)
        btn.Padding = New Padding(10, 0, 10, 0)
        btn.Font = FuenteTarjeta
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 1
        btn.UseVisualStyleBackColor = False
        AddHandler btn.Click, AddressOf CambiarZona
        FLPZonas.Controls.Add(btn)
    End Sub

    Private Sub MarcarZona()
        For Each Ctl As Control In FLPZonas.Controls
            Dim btn As Button = CType(Ctl, Button)
            If CStr(btn.Tag) = ZonaSel Then
                btn.BackColor = ColorPrimario
                btn.ForeColor = Color.White
                btn.FlatAppearance.BorderColor = ColorPrimario
                btn.FlatAppearance.MouseOverBackColor = ColorPrimarioHover
                btn.FlatAppearance.MouseDownBackColor = ColorPrimarioPresionado
            Else
                btn.BackColor = ColorFondoSuave
                btn.ForeColor = ColorTextoSecundario
                btn.FlatAppearance.BorderColor = ColorBorde
                btn.FlatAppearance.MouseOverBackColor = ColorHover
                btn.FlatAppearance.MouseDownBackColor = ColorPresionado
            End If
        Next
    End Sub

    Private Sub CambiarZona(ByVal Sender As Object, ByVal e As EventArgs)
        ZonaSel = CStr(CType(Sender, Button).Tag)
        MarcarZona()
        BuscarMesasDisponiles()
    End Sub

    Public Sub BuscarMesasDisponiles()
        DeletePanelItems(Me.FLPMesasDisponibles)
        SelControlMesDis = -1
        For Each Fila As DataRow In TablaMesas.Rows
            Dim CodMesa As String = Fila("Cod_Cliente").ToString
            Dim Zona As String = ""
            If Not IsDBNull(Fila("Departamento")) Then
                Zona = Trim(Fila("Departamento").ToString)
            End If
            If Not Ocupadas.ContainsKey(CodMesa) AndAlso (ZonaSel = "" OrElse String.Equals(Zona, ZonaSel, StringComparison.OrdinalIgnoreCase)) Then
                Me.FLPMesasDisponibles.Controls.Add(CrearBotonMesa(CodMesa, False, AddressOf CrearCuenta))
            End If
        Next
    End Sub

    Public Sub CrearCuenta(ByVal Sender As Object, ByVal e As EventArgs)
        Form1.Show()
        Dim Btn As Button
        Btn = CType(Sender, Button)
        Form1.cboMesas.SelectedValue = CStr(Btn.Tag)
        Me.Close()
    End Sub

    Public Sub DeletePanelItems(ByVal Pnl As FlowLayoutPanel)
        While Pnl.Controls.Count > 0 'Determine if the flow contains any controls
            Dim controltoremove = Pnl.Controls(0)
            Pnl.Controls.Remove(controltoremove)
            controltoremove.Dispose()
        End While
    End Sub

    Private Function CrearBotonCuenta(ByVal Texto As String, ByVal Numero As String, ByVal Principal As Boolean) As Button
        Dim btn As New Button
        btn.Text = Texto
        btn.Tag = Numero
        btn.Size = New Size(Math.Max(220, FLPMisCuentas.ClientSize.Width - 20), 64)
        btn.Margin = New Padding(0, 0, 0, 8)
        btn.Font = FuenteCuenta
        btn.TextAlign = ContentAlignment.MiddleLeft
        btn.Padding = New Padding(10, 0, 0, 0)
        btn.FlatStyle = FlatStyle.Flat
        btn.UseVisualStyleBackColor = False
        If Principal Then
            btn.BackColor = ColorPrimario
            btn.ForeColor = Color.White
            btn.TextAlign = ContentAlignment.MiddleCenter
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = ColorPrimarioHover
            btn.FlatAppearance.MouseDownBackColor = ColorPrimarioPresionado
        Else
            btn.BackColor = ColorFondoSuave
            btn.ForeColor = ColorTexto
            btn.FlatAppearance.BorderSize = 1
            btn.FlatAppearance.BorderColor = ColorBorde
            btn.FlatAppearance.MouseOverBackColor = ColorHover
            btn.FlatAppearance.MouseDownBackColor = ColorPresionado
        End If
        AddHandler btn.Click, AddressOf Ircuenta
        Return btn
    End Function

    Public Sub BuscarCuentas(ByVal Sender As Object, ByVal e As EventArgs)
        DeletePanelItems(FLPMisCuentas)
        SelControlCuentas = -1
        Me.FLPMisCuentas.VerticalScroll.Value = 0
        '//////////////// ESTE BOTON VIENE DE FLPMisMesas, capturo el sender y lo consulto //////
        Dim BtnFLPMesas As Button
        BtnFLPMesas = CType(Sender, Button)
        SelMesa = CStr(BtnFLPMesas.Tag)
        MarcarMesaSeleccionada()

        Dim DaCuentas As New SqlDataAdapter("Select * from Facturas Where Cod_Cliente = @CodCliente and Cod_Vendedor = @CodVendedor and Activo = 'True' and Cancelado = 'False'", TempConn)
        DaCuentas.SelectCommand.Parameters.AddWithValue("@CodCliente", SelMesa)
        DaCuentas.SelectCommand.Parameters.AddWithValue("@CodVendedor", CStr(Form1.CodVendedor))
        Dim DTCuentas As New DataTable
        DaCuentas.Fill(DTCuentas)

        '///////////////// RECORRO EL DATATABLE Y ASIGNO UN BOTON A CADA UNO DE LOS RESULTADOS
        For Each Fila As DataRow In DTCuentas.Rows
            Dim Texto As String = "Cuenta Nº " & Fila("Numero_Factura").ToString & "     Total: " & Fila("NetoPagar").ToString & vbCrLf & Fila("Nombre_cliente").ToString
            Me.FLPMisCuentas.Controls.Add(CrearBotonCuenta(Texto, Fila("Numero_Factura").ToString, False))
        Next

        Me.FLPMisCuentas.Controls.Add(CrearBotonCuenta("+  Nueva cuenta", "NuevoFactura", True))
    End Sub


    Public Sub Ircuenta(ByVal Sender As Object, ByVal e As EventArgs)
        Form1.Actualiza = True
        Form1.Show()
        Dim Btn As Button
        Btn = CType(Sender, Button)
        Dim Numero As String = CStr(Btn.Tag)
        If Numero = "NuevoFactura" Then
            Me.Close()
            Form1.BackToLife("")
            Form1.Actualiza = False
            Form1.cboMesas.Text = ""
            Form1.cboMesas.SelectedValue = SelMesa
            SelMesa = ""
        Else
            Form1.BackToLife(Numero)
            Form1.NumeroFactura = Numero

            Dim Adap As New SqlDataAdapter("SELECT Fecha_Factura, Propina, Cod_Cliente, Cod_Vendedor, Nombre_Cliente, Apellido_Cliente, Direccion_Cliente, Telefono_Cliente, Observaciones, SubTotal, IVA, Pagado, NetoPagar, Exonerado  FROM  Facturas  WHERE     (Numero_Factura = @Numero) and Tipo_Factura = 'Factura'", TempConn)
            Adap.SelectCommand.Parameters.AddWithValue("@Numero", Numero)
            Dim tConsulta As New DataTable
            Adap.Fill(tConsulta)

            If tConsulta.Rows.Count <> 0 Then
                Form1.cboMesas.SelectedValue = tConsulta.Rows(0)("Cod_Cliente")
                Form1.FechaFactura = Format(tConsulta.Rows(0)("Fecha_Factura"), "dd/MM/yyyy")
                Form1.CodVendedor = tConsulta.Rows(0)("Cod_Vendedor")
                Form1.txtNombre.Value = tConsulta.Rows(0)("Nombre_Cliente")
                Form1.txtRuc.Value = tConsulta.Rows(0)("Apellido_Cliente")
                Form1.txtDireccion.Value = tConsulta.Rows(0)("Direccion_Cliente")
                Form1.txtTelefono.Value = tConsulta.Rows(0)("Telefono_Cliente")
                Form1.txtSubTotal.Text = Format(tConsulta.Rows(0)("SubTotal"), "##,##0.00")
                Form1.txtIva.Text = Format(tConsulta.Rows(0)("Iva"), "##,##0.00")
                Form1.txtTotal.Text = Format(tConsulta.Rows(0)("Pagado"), "##,##0.00")
                Form1.ckExonerar.Checked = tConsulta.Rows(0)("Exonerado")
                Form1.txtpropina.Text = Format(tConsulta.Rows(0)("Propina"), "##,##0.00")
                Form1.txtObservacion.Value = tConsulta.Rows(0)("Observaciones")
            End If
            Form1.ActualizarEstadoPago()
            Me.Close()

        End If

    End Sub

    '///////////////////////// NAVEGACION CON LOS BOTONES ARRIBA / ABAJO //////////////////////
    Private Function MoverSeleccion(ByVal Panel As FlowLayoutPanel, ByVal Actual As Integer, ByVal Paso As Integer) As Integer
        If Panel.Controls.Count = 0 Then
            Return Actual
        End If
        Dim Nuevo As Integer = Actual + Paso
        If Nuevo < 0 Then
            Nuevo = 0
        End If
        If Nuevo > Panel.Controls.Count - 1 Then
            Nuevo = Panel.Controls.Count - 1
        End If
        Panel.ScrollControlIntoView(Panel.Controls(Nuevo))
        Panel.Controls(Nuevo).Select()
        Return Nuevo
    End Function

    Private Sub C1Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button1.Click
        SelControlCuentas = MoverSeleccion(FLPMisCuentas, SelControlCuentas, -1)
    End Sub

    Private Sub C1Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button2.Click
        SelControlCuentas = MoverSeleccion(FLPMisCuentas, SelControlCuentas, 1)
    End Sub

    Private Sub C1Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button4.Click
        SelControlMesDis = MoverSeleccion(FLPMesasDisponibles, SelControlMesDis, -1)
    End Sub

    Private Sub C1Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button3.Click
        SelControlMesDis = MoverSeleccion(FLPMesasDisponibles, SelControlMesDis, 1)
    End Sub
End Class
