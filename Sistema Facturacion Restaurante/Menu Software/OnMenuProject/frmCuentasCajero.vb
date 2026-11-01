Imports System.Data.SqlClient
Imports System.Drawing.Printing

''' <summary>
''' Cuentas abiertas de todas las mesas para el cajero: ver el detalle, imprimir prefacturas, cobrar y cerrar cuentas.
''' El cajero no toma pedidos: aqui no se agregan ni se quitan productos.
''' </summary>
Public Class frmCuentasCajero

    Private Const Titulo As String = "Zeus Facturación"

    '///////////////////////// ESTADO //////////////////////
    Private SelVendedor As String = ""
    Private SelMesa As String = ""
    Private SelNumero As String = ""
    Private SelTipo As String = "Factura"
    Private SelFecha As Date

    Private TablaAbiertas As DataTable
    Private NombresVendedor As New Dictionary(Of String, String)
    Private PrefacturaImpresa As New List(Of String)
    Private TablaDetalle As DataTable
    Private FotosMesa As New Dictionary(Of String, String)
    Private Shared CacheImagenes As New Dictionary(Of String, Image)
    Private Cargando As Boolean = False
    Private ChkPropina As CheckBox
    Private ChkExonerar As CheckBox

    '///////////////////////// CONTROLES //////////////////////
    Private FLPVendedores As FlowLayoutPanel
    Private FLPMesas As FlowLayoutPanel
    Private FLPCuentas As FlowLayoutPanel
    Private LblCuenta As Label
    Private GridDetalle As DataGridView
    Private LblSubTotal As Label
    Private LblPropina As Label
    Private LblIva As Label
    Private LblExonerada As Label
    Private LblTotal As Label
    Private LblPagado As Label
    Private LblPendiente As Label
    Private WithEvents BtnActualizar As Button
    Private WithEvents BtnSalir As Button
    Private WithEvents BtnPreFactura As Button
    Private WithEvents BtnPago As Button
    Private WithEvents BtnCerrarCuenta As Button

    Private Sub frmCuentasCajero_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConstruirDiseno()
        Try
            CargarVendedores()
            CargarFotosMesas()
            CargarCuentasAbiertas()
            RefrescarMesas()
            LimpiarDetalle()
        Catch ex As Exception
            MessageBox.Show("No se pudieron cargar las cuentas." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Diseño"

    Private Sub ConstruirDiseno()
        Me.SuspendLayout()

        '--- Encabezado con las acciones generales
        Dim Encabezado As Panel = CajaEncabezado("Cuentas de mesas", "Selecciona una mesa y una cuenta para cobrar o imprimir")
        BtnSalir = CajaBoton("Salir", False)
        BtnSalir.Size = New Size(110, 42)
        BtnSalir.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnSalir.Location = New Point(Me.ClientSize.Width - 130, 20)
        BtnActualizar = CajaBoton("Actualizar", True)
        BtnActualizar.Size = New Size(130, 42)
        BtnActualizar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnActualizar.Location = New Point(Me.ClientSize.Width - 270, 20)
        Encabezado.Controls.Add(BtnSalir)
        Encabezado.Controls.Add(BtnActualizar)

        '--- Botones de vendedores (se crean en ejecucion)
        FLPVendedores = New FlowLayoutPanel
        FLPVendedores.Dock = DockStyle.Top
        FLPVendedores.Height = 58
        FLPVendedores.Padding = New Padding(20, 6, 20, 0)
        FLPVendedores.BackColor = CajaFondo
        FLPVendedores.AutoScroll = True

        '--- Cuerpo: mesas | cuentas | detalle
        Dim Cuerpo As New TableLayoutPanel
        Cuerpo.Dock = DockStyle.Fill
        Cuerpo.Padding = New Padding(14, 4, 14, 14)
        Cuerpo.BackColor = CajaFondo
        Cuerpo.ColumnCount = 3
        Cuerpo.RowCount = 1
        Cuerpo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 27.0!))
        Cuerpo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 27.0!))
        Cuerpo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 46.0!))
        Cuerpo.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

        Dim TarjetaMesas As Panel = CajaTarjeta()
        TarjetaMesas.Dock = DockStyle.Fill
        FLPMesas = New FlowLayoutPanel
        FLPMesas.Dock = DockStyle.Fill
        FLPMesas.AutoScroll = True
        FLPMesas.BackColor = Color.White
        TarjetaMesas.Controls.Add(FLPMesas)
        TarjetaMesas.Controls.Add(CajaTituloSeccion("Mesas con cuentas"))

        Dim TarjetaCuentas As Panel = CajaTarjeta()
        TarjetaCuentas.Dock = DockStyle.Fill
        FLPCuentas = New FlowLayoutPanel
        FLPCuentas.Dock = DockStyle.Fill
        FLPCuentas.AutoScroll = True
        FLPCuentas.FlowDirection = FlowDirection.TopDown
        FLPCuentas.WrapContents = False
        FLPCuentas.BackColor = Color.White
        AddHandler FLPCuentas.Resize, AddressOf AjustarAnchoCuentas
        TarjetaCuentas.Controls.Add(FLPCuentas)
        TarjetaCuentas.Controls.Add(CajaTituloSeccion("Cuentas abiertas"))

        Dim TarjetaDetalle As Panel = ConstruirDetalle()

        Cuerpo.Controls.Add(TarjetaMesas, 0, 0)
        Cuerpo.Controls.Add(TarjetaCuentas, 1, 0)
        Cuerpo.Controls.Add(TarjetaDetalle, 2, 0)

        Me.Controls.Add(Cuerpo)
        Me.Controls.Add(FLPVendedores)
        Me.Controls.Add(Encabezado)
        Me.ResumeLayout(True)
    End Sub

    Private Function ConstruirDetalle() As Panel
        Dim Tarjeta As Panel = CajaTarjeta()
        Tarjeta.Dock = DockStyle.Fill

        LblCuenta = CajaTituloSeccion("Detalle de la cuenta")

        GridDetalle = New DataGridView
        GridDetalle.Dock = DockStyle.Fill
        CajaEstilizarGrid(GridDetalle)
        GridDetalle.ReadOnly = True
        GridDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        '--- Propina e IVA (como ckPropina y ckExonerar de Form1)
        Dim Opciones As New FlowLayoutPanel
        Opciones.Dock = DockStyle.Bottom
        Opciones.Height = 40
        Opciones.Padding = New Padding(0, 6, 0, 0)
        ChkPropina = New CheckBox
        ChkPropina.Text = "Cobrar propina"
        ChkPropina.AutoSize = True
        ChkPropina.Font = CajaFuenteBoton
        ChkPropina.ForeColor = CajaTexto
        ChkPropina.Margin = New Padding(0, 0, 24, 0)
        ChkExonerar = New CheckBox
        ChkExonerar.Text = "Exonerar IVA"
        ChkExonerar.AutoSize = True
        ChkExonerar.Font = CajaFuenteBoton
        ChkExonerar.ForeColor = CajaTexto
        Opciones.Controls.Add(ChkPropina)
        Opciones.Controls.Add(ChkExonerar)
        AddHandler ChkPropina.CheckedChanged, AddressOf CambioPropinaIva
        AddHandler ChkExonerar.CheckedChanged, AddressOf CambioPropinaIva

        '--- Totales
        Dim Totales As New TableLayoutPanel
        Totales.Dock = DockStyle.Bottom
        Totales.Height = 6 * 28 + 40 + 6
        Totales.Padding = New Padding(0, 6, 0, 0)
        Totales.ColumnCount = 2
        Totales.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        Totales.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        For i As Integer = 1 To 6
            Totales.RowStyles.Add(New RowStyle(SizeType.Absolute, 28.0!))
        Next
        Totales.RowStyles.Add(New RowStyle(SizeType.Absolute, 40.0!))

        LblSubTotal = AgregarTotal(Totales, 0, "Subtotal")
        LblPropina = AgregarTotal(Totales, 1, "Propina")
        LblIva = AgregarTotal(Totales, 2, "IVA")
        LblExonerada = AgregarTotal(Totales, 3, "Exonerada")
        LblTotal = AgregarTotal(Totales, 4, "Total a pagar")
        LblTotal.Font = New Font("Segoe UI Semibold", 13.0!, FontStyle.Bold)
        LblTotal.ForeColor = CajaPrimario
        LblPagado = AgregarTotal(Totales, 5, "Pagado")
        LblPendiente = AgregarTotal(Totales, 6, "Pendiente")
        LblPendiente.Font = New Font("Segoe UI Semibold", 18.0!, FontStyle.Bold)
        Totales.GetControlFromPosition(0, 6).Font = New Font("Segoe UI Semibold", 16.0!, FontStyle.Bold)

        '--- Acciones sobre la cuenta (mismas de Form1)
        Dim Acciones As New FlowLayoutPanel
        Acciones.Dock = DockStyle.Bottom
        Acciones.Height = 64
        Acciones.Padding = New Padding(0, 10, 0, 0)

        BtnPreFactura = CajaBoton("PreFactura", False)
        BtnPreFactura.Size = New Size(130, 46)
        BtnPago = CajaBoton("Pago", True)
        BtnPago.Size = New Size(130, 46)
        BtnCerrarCuenta = CajaBoton("Cerrar cuenta", True)
        BtnCerrarCuenta.Size = New Size(160, 46)
        Acciones.Controls.Add(BtnPreFactura)
        Acciones.Controls.Add(BtnPago)
        Acciones.Controls.Add(BtnCerrarCuenta)

        Tarjeta.Controls.Add(GridDetalle)
        Tarjeta.Controls.Add(LblCuenta)
        Tarjeta.Controls.Add(Opciones)
        Tarjeta.Controls.Add(Totales)
        Tarjeta.Controls.Add(Acciones)
        Return Tarjeta
    End Function

    Private Function AgregarTotal(ByVal Totales As TableLayoutPanel, ByVal Fila As Integer, ByVal Texto As String) As Label
        Dim Etiqueta As Label = CajaEtiqueta(Texto)
        Etiqueta.Dock = DockStyle.Fill

        Dim Valor As New Label
        Valor.Dock = DockStyle.Fill
        Valor.Font = CajaFuenteBoton
        Valor.ForeColor = CajaTexto
        Valor.TextAlign = ContentAlignment.MiddleRight
        Valor.Text = "0.00"

        Totales.Controls.Add(Etiqueta, 0, Fila)
        Totales.Controls.Add(Valor, 1, Fila)
        Return Valor
    End Function

    Private Sub AjustarAnchoCuentas(ByVal sender As Object, ByVal e As EventArgs)
        For Each Ctl As Control In FLPCuentas.Controls
            Ctl.Width = Math.Max(200, FLPCuentas.ClientSize.Width - 20)
        Next
    End Sub

#End Region

#Region "Carga de datos"

    Private Function Consultar(ByVal Sql As String, ByVal ParamArray Parametros() As SqlParameter) As DataTable
        Dim Tabla As New DataTable
        Using cn As New SqlConnection(Conexion)
            Using da As New SqlDataAdapter(Sql, cn)
                If Parametros IsNot Nothing Then da.SelectCommand.Parameters.AddRange(Parametros)
                da.Fill(Tabla)
            End Using
        End Using
        Return Tabla
    End Function

    Private Sub CargarVendedores()
        Dim Vendedores As DataTable = Consultar("SELECT Cod_Vendedor, Nombre_Vendedor, Tipo FROM Vendedores ORDER BY Nombre_Vendedor")

        NombresVendedor.Clear()
        DeletePanelItems(FLPVendedores)
        AgregarBotonVendedor("Todos", "")

        For Each Fila As DataRow In Vendedores.Rows
            Dim Codigo As String = Fila("Cod_Vendedor").ToString()
            Dim Nombre As String = If(IsDBNull(Fila("Nombre_Vendedor")), Codigo, Fila("Nombre_Vendedor").ToString().Trim())
            NombresVendedor(Codigo) = Nombre

            ' Solo los meseros abren cuentas: Cajero y Administrador no tienen boton
            If Not IsDBNull(Fila("Tipo")) AndAlso Not String.Equals(Fila("Tipo").ToString().Trim(), "Mesero", StringComparison.OrdinalIgnoreCase) AndAlso Fila("Tipo").ToString().Trim() <> "" Then Continue For

            Dim Primero As String = Nombre
            If Nombre.IndexOf(" "c) > 0 Then Primero = Nombre.Substring(0, Nombre.IndexOf(" "c))
            AgregarBotonVendedor(StrConv(Primero, VbStrConv.ProperCase), Codigo)
        Next
        MarcarVendedor()
    End Sub

    Private Sub CargarFotosMesas()
        FotosMesa.Clear()
        For Each Fila As DataRow In Consultar("SELECT Cod_Cliente, Foto FROM Clientes WHERE Limite_Credito = 0").Rows
            If Not IsDBNull(Fila("Foto")) Then FotosMesa(Fila("Cod_Cliente").ToString()) = Fila("Foto").ToString()
        Next
    End Sub

    Private Sub AgregarBotonVendedor(ByVal Texto As String, ByVal Codigo As String)
        Dim btn As Button = CajaBoton(Texto, False)
        btn.Tag = Codigo
        btn.AutoSize = True
        btn.MinimumSize = New Size(100, 44)
        btn.Padding = New Padding(10, 0, 10, 0)
        AddHandler btn.Click, AddressOf CambiarVendedor
        FLPVendedores.Controls.Add(btn)
    End Sub

    Private Sub MarcarVendedor()
        For Each Ctl As Control In FLPVendedores.Controls
            Dim btn As Button = CType(Ctl, Button)
            CajaMarcarFiltro(btn, CStr(btn.Tag) = SelVendedor)
        Next
    End Sub

    Private Sub CambiarVendedor(ByVal Sender As Object, ByVal e As EventArgs)
        SelVendedor = CStr(CType(Sender, Button).Tag)
        MarcarVendedor()
        SelMesa = ""
        LimpiarDetalle()
        RefrescarMesas()
        RefrescarCuentas()
    End Sub

    ''' <summary>Cuentas en curso de todo el restaurante (las mismas que ocupan las mesas en FormCuentas).</summary>
    Private Sub CargarCuentasAbiertas()
        TablaAbiertas = Consultar("SELECT Numero_Factura, Tipo_Factura, Fecha_Factura, Cod_Cliente, Cod_Vendedor, Nombre_Cliente, NetoPagar " &
                                  "FROM Facturas WHERE Activo = 1 AND Cancelado = 0 ORDER BY Cod_Cliente, Numero_Factura")
    End Sub

    Private Function CuentaVisible(ByVal Fila As DataRow) As Boolean
        Return SelVendedor = "" OrElse Fila("Cod_Vendedor").ToString() = SelVendedor
    End Function

#End Region

#Region "Mesas y cuentas"

    Public Sub DeletePanelItems(ByVal Pnl As FlowLayoutPanel)
        While Pnl.Controls.Count > 0
            Dim Quitar As Control = Pnl.Controls(0)
            Pnl.Controls.Remove(Quitar)
            Quitar.Dispose()
        End While
    End Sub

    Private Sub RefrescarMesas()
        DeletePanelItems(FLPMesas)

        Dim Mesas As New List(Of String)
        Dim Cantidad As New Dictionary(Of String, Integer)
        For Each Fila As DataRow In TablaAbiertas.Rows
            If Not CuentaVisible(Fila) Then Continue For
            Dim Mesa As String = Fila("Cod_Cliente").ToString()
            If Not Cantidad.ContainsKey(Mesa) Then
                Mesas.Add(Mesa)
                Cantidad(Mesa) = 0
            End If
            Cantidad(Mesa) += 1
        Next

        For Each Mesa As String In Mesas
            FLPMesas.Controls.Add(CrearBotonMesa(Mesa, Cantidad(Mesa)))
        Next
        MarcarMesa()
    End Sub

    '///////////////////////// IMAGEN DE LA MESA (Clientes.Foto) //////////////////////
    Private Function Miniatura(ByVal Origen As Image) As Image
        Dim Ancho As Integer = 110, Alto As Integer = 66
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

    Private Function ImagenMesa(ByVal CodMesa As String) As Image
        Dim Clave As String = "#mesa"
        Dim Foto As String = ""
        If FotosMesa.TryGetValue(CodMesa, Foto) AndAlso Trim(Foto) <> "" Then Clave = Foto

        Dim Img As Image = Nothing
        If Not CacheImagenes.TryGetValue(Clave, Img) Then
            Try
                If Clave = "#mesa" Then
                    Img = Miniatura(My.Resources.Mesa_Roja)
                Else
                    Img = Miniatura(BytesToImagen(StringtoByte(Clave)))
                End If
            Catch
                Img = Miniatura(My.Resources.Mesa_Roja)
            End Try
            CacheImagenes(Clave) = Img
        End If
        Return Img
    End Function

    Private Function CrearBotonMesa(ByVal CodMesa As String, ByVal Cuentas As Integer) As Button
        Dim btn As New Button
        btn.Tag = CodMesa
        btn.Text = CodMesa & vbCrLf & Cuentas.ToString() & If(Cuentas = 1, " cuenta", " cuentas")
        btn.Image = ImagenMesa(CodMesa)
        btn.ImageAlign = ContentAlignment.TopCenter
        btn.TextAlign = ContentAlignment.BottomCenter
        btn.Padding = New Padding(0, 6, 0, 2)
        btn.Size = New Size(134, 124)
        btn.Margin = New Padding(5)
        btn.BackColor = Color.White
        btn.ForeColor = CajaTexto
        btn.Font = New Font("Segoe UI Semibold", 11.0!, FontStyle.Bold)
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.BorderColor = CajaBorde
        btn.FlatAppearance.MouseOverBackColor = CajaHover
        btn.FlatAppearance.MouseDownBackColor = CajaPresionado
        btn.UseVisualStyleBackColor = False
        btn.Cursor = Cursors.Hand
        AddHandler btn.Click, AddressOf ElegirMesa
        Return btn
    End Function

    Private Sub MarcarMesa()
        For Each Ctl As Control In FLPMesas.Controls
            Dim btn As Button = CType(Ctl, Button)
            If CStr(btn.Tag) = SelMesa Then
                btn.FlatAppearance.BorderColor = CajaPrimario
                btn.FlatAppearance.BorderSize = 3
            Else
                btn.FlatAppearance.BorderColor = CajaBorde
                btn.FlatAppearance.BorderSize = 1
            End If
        Next
    End Sub

    Private Sub ElegirMesa(ByVal Sender As Object, ByVal e As EventArgs)
        SelMesa = CStr(CType(Sender, Button).Tag)
        MarcarMesa()
        LimpiarDetalle()
        RefrescarCuentas()
    End Sub

    Private Sub RefrescarCuentas()
        DeletePanelItems(FLPCuentas)
        If SelMesa = "" Then Return

        For Each Fila As DataRow In TablaAbiertas.Rows
            If Fila("Cod_Cliente").ToString() <> SelMesa OrElse Not CuentaVisible(Fila) Then Continue For

            Dim Numero As String = Fila("Numero_Factura").ToString()
            Dim Mesero As String = ""
            NombresVendedor.TryGetValue(Fila("Cod_Vendedor").ToString(), Mesero)

            Dim Total As Double = If(IsDBNull(Fila("NetoPagar")), 0.0, Convert.ToDouble(Fila("NetoPagar")))
            Dim Texto As String = "Cuenta Nº " & Numero & "     Total: " & Total.ToString("N2") & vbCrLf &
                                  Mesero & If(IsDBNull(Fila("Nombre_Cliente")) OrElse Fila("Nombre_Cliente").ToString() = "", "", "  ·  " & Fila("Nombre_Cliente").ToString())
            FLPCuentas.Controls.Add(CrearBotonCuenta(Texto, Numero, Fila("Tipo_Factura").ToString()))
        Next
        AjustarAnchoCuentas(Nothing, Nothing)
        MarcarCuenta()
    End Sub

    Private Function CrearBotonCuenta(ByVal Texto As String, ByVal Numero As String, ByVal Tipo As String) As Button
        Dim btn As New Button
        btn.Text = Texto
        btn.Tag = Numero
        btn.Name = Tipo
        btn.Size = New Size(Math.Max(200, FLPCuentas.ClientSize.Width - 20), 64)
        btn.Margin = New Padding(0, 0, 0, 8)
        btn.Font = CajaFuenteTexto
        btn.TextAlign = ContentAlignment.MiddleLeft
        btn.Padding = New Padding(10, 0, 0, 0)
        btn.FlatStyle = FlatStyle.Flat
        btn.UseVisualStyleBackColor = False
        btn.BackColor = CajaFondoSuave
        btn.ForeColor = CajaTexto
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.BorderColor = CajaBorde
        btn.FlatAppearance.MouseOverBackColor = CajaHover
        btn.FlatAppearance.MouseDownBackColor = CajaPresionado
        btn.Cursor = Cursors.Hand
        AddHandler btn.Click, AddressOf ElegirCuenta
        Return btn
    End Function

    Private Sub MarcarCuenta()
        For Each Ctl As Control In FLPCuentas.Controls
            Dim btn As Button = CType(Ctl, Button)
            If CStr(btn.Tag) = SelNumero Then
                btn.FlatAppearance.BorderColor = CajaPrimario
                btn.FlatAppearance.BorderSize = 3
            Else
                btn.FlatAppearance.BorderColor = CajaBorde
                btn.FlatAppearance.BorderSize = 1
            End If
        Next
    End Sub

    Private Sub ElegirCuenta(ByVal Sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(Sender, Button)
        SelNumero = CStr(btn.Tag)
        SelTipo = btn.Name
        MarcarCuenta()
        CargarDetalle()
    End Sub

#End Region

#Region "Detalle de la cuenta"

    Private Sub LimpiarDetalle()
        SelNumero = ""
        TablaDetalle = Nothing
        If GridDetalle Is Nothing Then Return

        GridDetalle.DataSource = Nothing
        LblCuenta.Text = "Detalle de la cuenta"
        For Each Lbl As Label In New Label() {LblSubTotal, LblPropina, LblIva, LblTotal, LblPagado, LblPendiente}
            Lbl.Text = "0.00"
        Next
        LblExonerada.Text = "No"
        Cargando = True
        ChkPropina.Checked = False
        ChkExonerar.Checked = False
        Cargando = False
        ChkPropina.Enabled = False
        ChkExonerar.Enabled = False
        BtnPreFactura.Enabled = False
        BtnPago.Enabled = False
        BtnCerrarCuenta.Enabled = False
    End Sub

    Private Sub CargarDetalle()
        Try
            Dim Cabecera As DataTable = Consultar("SELECT * FROM Facturas WHERE Numero_Factura = @Numero AND Tipo_Factura = @Tipo AND Activo = 1 AND Cancelado = 0",
                                                  New SqlParameter("@Numero", SelNumero), New SqlParameter("@Tipo", SelTipo))
            If Cabecera.Rows.Count = 0 Then
                MessageBox.Show("La cuenta " & SelNumero & " ya no está abierta.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ActualizarTodo()
                Return
            End If

            Dim Fila As DataRow = Cabecera.Rows(0)
            SelFecha = Convert.ToDateTime(Fila("Fecha_Factura"))

            TablaDetalle = Consultar("SELECT Cod_Producto, Descripcion_Producto, Cantidad, Precio_Unitario, Precio_Neto, Numero_Factura, Fecha_Factura, Tipo_Factura, Id_Detalle_Factura, Importe " &
                                     "FROM Detalle_Facturas WHERE Numero_Factura = @Numero AND Tipo_Factura = @Tipo ORDER BY Id_Detalle_Factura",
                                     New SqlParameter("@Numero", SelNumero), New SqlParameter("@Tipo", SelTipo))

            MostrarProductos()

            Dim Mesero As String = ""
            NombresVendedor.TryGetValue(Fila("Cod_Vendedor").ToString(), Mesero)
            LblCuenta.Text = "Cuenta Nº " & SelNumero & "  ·  Mesa " & Fila("Cod_Cliente").ToString() & "  ·  " & Mesero

            Dim SubTotal As Double = LeerDoble(Fila("SubTotal"))
            Dim Propina As Double = LeerDoble(Fila("Propina"))
            Dim Iva As Double = LeerDoble(Fila("IVA"))
            Dim Total As Double = LeerDoble(Fila("NetoPagar"))
            Dim Exonerada As Boolean = Not IsDBNull(Fila("Exonerado")) AndAlso Convert.ToBoolean(Fila("Exonerado"))
            Dim Pagado As Decimal = ObtenerMontoPagadoCordobas(SelNumero, SelFecha, SelTipo)
            Dim Pendiente As Decimal = Math.Max(0D, Convert.ToDecimal(Total) - Pagado)

            LblSubTotal.Text = SubTotal.ToString("N2")
            LblPropina.Text = Propina.ToString("N2")
            LblIva.Text = Iva.ToString("N2")
            LblExonerada.Text = If(Exonerada, "Sí", "No")
            LblTotal.Text = Total.ToString("N2")
            LblPagado.Text = Pagado.ToString("N2")
            LblPendiente.Text = Pendiente.ToString("N2")
            LblPendiente.ForeColor = If(Pendiente > 0D, CajaAdvertencia, CajaExito)

            Cargando = True
            ChkExonerar.Checked = Exonerada
            If IsDBNull(Fila("CalculaPropina")) Then
                ChkPropina.Checked = Propina > 0
            Else
                ChkPropina.Checked = Convert.ToBoolean(Fila("CalculaPropina"))
            End If
            Cargando = False
            ChkPropina.Enabled = True
            ChkExonerar.Enabled = True

            ' Igual que Form1: cerrar la cuenta se habilita al imprimir la prefactura o al aplicar pagos
            BtnPreFactura.Enabled = True
            BtnPago.Enabled = True
            BtnCerrarCuenta.Enabled = Pagado > 0D OrElse PrefacturaImpresa.Contains(SelNumero)
        Catch ex As Exception
            MessageBox.Show("No se pudo cargar la cuenta." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MostrarProductos()
        Dim Productos As New DataTable
        Productos.Columns.Add("Producto", GetType(String))
        Productos.Columns.Add("Precio", GetType(Double))
        Productos.Columns.Add("Cantidad", GetType(Double))
        Productos.Columns.Add("Importe", GetType(Double))

        For Each Fila As DataRow In TablaDetalle.Rows
            Productos.Rows.Add(Fila("Descripcion_Producto").ToString(), LeerDoble(Fila("Precio_Unitario")), LeerDoble(Fila("Cantidad")), LeerDoble(Fila("Precio_Neto")))
        Next

        GridDetalle.DataSource = Productos
        GridDetalle.Columns("Producto").FillWeight = 220
        GridDetalle.Columns("Precio").FillWeight = 80
        GridDetalle.Columns("Cantidad").FillWeight = 70
        GridDetalle.Columns("Importe").FillWeight = 90
        GridDetalle.Columns("Precio").DefaultCellStyle.Format = "N2"
        GridDetalle.Columns("Cantidad").DefaultCellStyle.Format = "0.##"
        GridDetalle.Columns("Importe").DefaultCellStyle.Format = "N2"
        For Each Nombre As String In New String() {"Precio", "Cantidad", "Importe"}
            GridDetalle.Columns(Nombre).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridDetalle.Columns(Nombre).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        Next
        GridDetalle.ClearSelection()
    End Sub

    Private Shared Function LeerDoble(ByVal Valor As Object) As Double
        If Valor Is Nothing OrElse IsDBNull(Valor) Then Return 0.0
        Return Convert.ToDouble(Valor)
    End Function

    ''' <summary>Quita o pone propina e IVA y regraba los totales de la cuenta, con las mismas reglas de Form1.Calcular.</summary>
    Private Sub CambioPropinaIva(ByVal sender As Object, ByVal e As EventArgs)
        If Cargando OrElse SelNumero = "" Then Return

        Try
            Dim Cab As DataTable = Consultar("SELECT SubTotal FROM Facturas WHERE Numero_Factura = @Numero AND Tipo_Factura = @Tipo",
                                             New SqlParameter("@Numero", SelNumero), New SqlParameter("@Tipo", SelTipo))
            If Cab.Rows.Count = 0 Then Return

            Dim SubTotal As Double = LeerDoble(Cab.Rows(0)("SubTotal"))
            Dim Iva As Double = If(ChkExonerar.Checked, 0.0, SubTotal * 0.15)
            Dim Propina As Double = 0.0
            If ChkPropina.Checked Then
                Dim Empresa As DataTable = Consultar("SELECT PorcentajePropina FROM DatosEmpresa")
                If Empresa.Rows.Count > 0 Then Propina = SubTotal * (LeerDoble(Empresa.Rows(0)("PorcentajePropina")) / 100)
            End If
            Dim Total As Double = SubTotal + Iva + Propina

            Using cn As New SqlConnection(Conexion)
                Using cmd As New SqlCommand("UPDATE Facturas SET IVA = @Iva, Propina = @Propina, Pagado = @Total, NetoPagar = @Total, Exonerado = @Exonerado, CalculaPropina = @CalculaPropina " &
                                            "WHERE Numero_Factura = @Numero AND Tipo_Factura = @Tipo", cn)
                    cmd.Parameters.AddWithValue("@Iva", Iva)
                    cmd.Parameters.AddWithValue("@Propina", Propina)
                    cmd.Parameters.AddWithValue("@Total", Total)
                    cmd.Parameters.AddWithValue("@Exonerado", ChkExonerar.Checked)
                    cmd.Parameters.AddWithValue("@CalculaPropina", ChkPropina.Checked)
                    cmd.Parameters.AddWithValue("@Numero", SelNumero)
                    cmd.Parameters.AddWithValue("@Tipo", SelTipo)
                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ActualizarTodo()
        Catch ex As Exception
            MessageBox.Show("No se pudo actualizar la propina o el IVA." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ActualizarTodo()
        Dim Mesa As String = SelMesa
        Dim Cuenta As String = SelNumero

        CargarCuentasAbiertas()
        RefrescarMesas()
        LimpiarDetalle()

        SelMesa = Mesa
        MarcarMesa()
        RefrescarCuentas()

        ' Si la cuenta sigue abierta se vuelve a mostrar con sus datos al dia
        For Each Ctl As Control In FLPCuentas.Controls
            If CStr(Ctl.Tag) = Cuenta Then
                SelNumero = Cuenta
                SelTipo = Ctl.Name
                MarcarCuenta()
                CargarDetalle()
                Exit For
            End If
        Next
    End Sub

#End Region

#Region "Acciones"

    Private Sub BtnActualizar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnActualizar.Click
        Try
            ActualizarTodo()
        Catch ex As Exception
            MessageBox.Show("No se pudo actualizar." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnSalir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

    ' Igual que BtnPreFactura_Click de Form1: imprime la proforma de la cuenta
    Private Sub BtnPreFactura_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnPreFactura.Click
        If SelNumero = "" OrElse TablaDetalle Is Nothing Then Return

        Try
            Establecer_Impresora(BuscaImpresora("Factura"))

            Dim Reporte As ArepFacturasTiras2 = ArmarReporte("PROFORMA", 0D)
            Dim Visor As New FrmViewer()
            Visor.arvMain.Document = Reporte.Document
            My.Application.DoEvents()

            Reporte.DataSource = TablaDetalle
            Reporte.Run(False)
            Visor.Show(Me)

            If Not PrefacturaImpresa.Contains(SelNumero) Then PrefacturaImpresa.Add(SelNumero)
            BtnCerrarCuenta.Enabled = True
        Catch ex As Exception
            MessageBox.Show("No se pudo imprimir la prefactura." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Igual que BtnPago_Click de Form1: abre el cobro por el saldo pendiente
    Private Sub BtnPago_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnPago.Click
        If SelNumero = "" Then Return

        Try
            Dim Total As Decimal
            If Not Decimal.TryParse(LblTotal.Text, Globalization.NumberStyles.Number, Globalization.CultureInfo.CurrentCulture, Total) Then
                MessageBox.Show("El total de la cuenta no es válido.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim Pagado As Decimal = ObtenerMontoPagadoCordobas(SelNumero, SelFecha, SelTipo)
            Dim Pendiente As Decimal = Math.Max(0D, Total - Pagado)

            FrmPago.MontoAPagar = Pendiente.ToString("0.00")
            FrmPago.Numero_Factura = SelNumero
            FrmPago.ShowDialog(Me)

            CargarDetalle()
        Catch ex As Exception
            MessageBox.Show("Error al calcular el monto pendiente: " & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Igual que C1Button4_Click de Form1: cierra la cuenta, libera la mesa e imprime la factura
    Private Sub BtnCerrarCuenta_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCerrarCuenta.Click
        If SelNumero = "" OrElse TablaDetalle Is Nothing Then Return

        Try
            Dim Pagado As Decimal = ObtenerMontoPagadoCordobas(SelNumero, SelFecha, SelTipo)
            Dim Total As Double = LeerDoble(Consultar("SELECT NetoPagar FROM Facturas WHERE Numero_Factura = @Numero AND Tipo_Factura = @Tipo",
                                                   New SqlParameter("@Numero", SelNumero), New SqlParameter("@Tipo", SelTipo)).Rows(0)("NetoPagar"))

            If MessageBox.Show("¿Seguro que querés cerrar la cuenta " & SelNumero & "?", Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            If Pagado = 0D Then
                If MessageBox.Show("¿La factura será de crédito?", Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                    Return
                End If
            End If

            ' Con pagos aplicados la factura es de Contado; sin pagos es de Credito
            Dim MetodoFactura As String = "Credito"
            Dim MontoCredito As Double = Total
            If Pagado > 0D Then
                MetodoFactura = "Contado"
                MontoCredito = 0
            End If

            Using cn As New SqlConnection(Conexion)
                Using cmd As New SqlCommand("UPDATE Facturas SET Activo = 0, Cancelado = 0, MetodoPago = @MetodoPago, MontoCredito = @MontoCredito " &
                                            "WHERE Numero_Factura = @Numero AND Tipo_Factura = @Tipo", cn)
                    cmd.Parameters.AddWithValue("@MetodoPago", MetodoFactura)
                    cmd.Parameters.AddWithValue("@MontoCredito", MontoCredito)
                    cmd.Parameters.AddWithValue("@Numero", SelNumero)
                    cmd.Parameters.AddWithValue("@Tipo", SelTipo)
                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Dim NumeroCerrado As String = SelNumero
            PrefacturaImpresa.Remove(NumeroCerrado)

            ImprimirFactura(Pagado)

            SelNumero = ""
            ActualizarTodo()
        Catch ex As Exception
            MessageBox.Show("No se pudo cerrar la cuenta." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ImprimirFactura(ByVal Pagado As Decimal)
        Dim ImprimeFacturaPreview As Boolean = ObtenerImprimirSinPreview()

        Dim Reporte As ArepFacturasTiras2 = ArmarReporte("FACTURA", Pagado)
        Dim Visor As New FrmViewer()
        Visor.arvMain.Document = Reporte.Document
        My.Application.DoEvents()

        Reporte.DataSource = TablaDetalle
        Establecer_Impresora(BuscaImpresora("Factura"))

        If ImprimeFacturaPreview Then
            Reporte.Run(True)
            Visor.Show(Me)
        Else
            Reporte.Run(True)
            Visor.arvMain.Document.Print(False, False, False)
        End If
    End Sub

    ''' <summary>Arma el reporte de tira (proforma o factura) de la cuenta seleccionada con sus datos guardados.</summary>
    Private Function ArmarReporte(ByVal Documento As String, ByVal Pagado As Decimal) As ArepFacturasTiras2
        Dim Cabecera As DataRow = Consultar("SELECT * FROM Facturas WHERE Numero_Factura = @Numero AND Tipo_Factura = @Tipo",
                                            New SqlParameter("@Numero", SelNumero), New SqlParameter("@Tipo", SelTipo)).Rows(0)
        Dim Empresa As DataRow = Consultar("SELECT * FROM DatosEmpresa").Rows(0)

        Dim Reporte As New ArepFacturasTiras2

        If LogoEmpresa <> "" Then
            Reporte.ImgLogo.Image = BytesToImagen(StringtoByte(LogoEmpresa))
        ElseIf Not IsDBNull(Empresa("Ruta_Logo")) AndAlso Empresa("Ruta_Logo").ToString() <> "" AndAlso System.IO.File.Exists(Empresa("Ruta_Logo").ToString()) Then
            Reporte.ImgLogo.Image = New System.Drawing.Bitmap(Empresa("Ruta_Logo").ToString())
        End If

        Dim Mesero As String = ""
        NombresVendedor.TryGetValue(Cabecera("Cod_Vendedor").ToString(), Mesero)

        Reporte.Label6.Text = Documento
        Reporte.LblEncabezado.Text = Empresa("Nombre_Empresa").ToString()
        Reporte.LblRuc.Text = Empresa("Numero_Ruc").ToString()
        Reporte.LblDireccion.Text = Empresa("Direccion_Empresa").ToString()
        Reporte.LblTelefono.Text = Empresa("Telefono").ToString()
        Reporte.LblOrden.Text = SelNumero
        Reporte.LblFechaOrden.Text = Format(DateTime.Now, "dd/MM/yyyy")
        Reporte.LblNombres.Text = Cabecera("Nombre_Cliente").ToString()
        If Documento = "PROFORMA" Then
            ' Como en Form1: en la proforma va el nombre de la mesa
            Dim Mesa As DataTable = Consultar("SELECT Nombre_Cliente FROM Clientes WHERE Cod_Cliente = @Cod", New SqlParameter("@Cod", Cabecera("Cod_Cliente").ToString()))
            Reporte.LblApellidos.Text = If(Mesa.Rows.Count > 0, Mesa.Rows(0)("Nombre_Cliente").ToString(), Cabecera("Cod_Cliente").ToString())
        Else
            Reporte.LblApellidos.Text = Cabecera("Apellido_Cliente").ToString()
        End If
        Reporte.TxtVendedor.Text = Mesero
        Reporte.LblSubTotal.Text = LeerDoble(Cabecera("SubTotal")).ToString("##,##0.00")
        Reporte.LblIva.Text = LeerDoble(Cabecera("IVA")).ToString("##,##0.00")
        Reporte.Label10.Text = LeerDoble(Cabecera("Propina")).ToString("##,##0.00")
        Reporte.LblPagado.Text = Pagado.ToString("##,##0.00")
        Reporte.LblTotal1.Text = LeerDoble(Cabecera("NetoPagar")).ToString("##,##0.00")
        Reporte.LblTotal.Text = "0.00"
        Return Reporte
    End Function

#End Region

End Class
