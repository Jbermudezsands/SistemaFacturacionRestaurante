Imports System.Drawing
Imports System.Windows.Forms

'///////////////////////// DISEÑO MODERNO Y RESPONSIVO DE Form1 //////////////////////
' Los controles se siguen declarando en Form1.Designer.vb (mismos nombres); aqui se
' reubican dentro de contenedores TableLayoutPanel y se ajustan segun el tamaño de pantalla.
Partial Class Form1

    '///////////////////////// PALETA (misma de FormCuentas) //////////////////////
    Dim ColorFondo As Color = Color.FromArgb(24, 41, 66)
    Dim ColorPrimario As Color = Color.FromArgb(52, 94, 152)
    Dim ColorPrimarioHover As Color = Color.FromArgb(41, 78, 130)
    Dim ColorPrimarioPresionado As Color = Color.FromArgb(30, 62, 110)
    Dim ColorTexto As Color = Color.FromArgb(30, 41, 59)
    Dim ColorTextoSecundario As Color = Color.FromArgb(60, 70, 85)
    Dim ColorTextoClaro As Color = Color.FromArgb(170, 185, 205)
    Dim ColorFondoSuave As Color = Color.FromArgb(246, 248, 251)
    Dim ColorHover As Color = Color.FromArgb(238, 241, 245)
    Dim ColorPresionado As Color = Color.FromArgb(228, 232, 238)
    Dim ColorBorde As Color = Color.FromArgb(210, 216, 226)
    Dim ColorSeleccion As Color = Color.FromArgb(214, 228, 247)

    '///////////////////////// CONTENEDORES //////////////////////
    Dim tlpRaiz, tlpEncabezado, tlpCuenta, tlpAjustes, tlpOpciones, tlpTotales, tlpMenu, tlpAcciones As TableLayoutPanel
    Dim pnlCuenta, pnlCategorias, pnlProductos As Panel
    Dim Acciones() As C1.Win.C1Input.C1Button
    Dim LblEstadoPago As Label

    '///////////////////////// ESTADO RESPONSIVO //////////////////////
    Dim ModoActual As Integer = -1
    Dim EscalaFuente As Single = 1.0!
    Dim AnchoTarjeta As Integer = 118
    Dim AltoTarjeta As Integer = 92
    Dim FuentesDiseno As New List(Of Font)

    Private Function NuevaTabla(ByVal Columnas As Integer, ByVal Filas As Integer) As TableLayoutPanel
        Dim Tabla As New TableLayoutPanel
        Tabla.Dock = DockStyle.Fill
        Tabla.Margin = New Padding(0)
        Tabla.Padding = New Padding(0)
        Tabla.ColumnCount = Columnas
        Tabla.RowCount = Filas
        Return Tabla
    End Function

    Private Function NuevoPanelBlanco(ByVal Titulo As Label) As Panel
        Dim Pnl As New Panel
        Pnl.BackColor = Color.White
        Pnl.Dock = DockStyle.Fill
        Pnl.Margin = New Padding(0)
        Pnl.Padding = New Padding(14, 10, 14, 10)
        Titulo.AutoSize = False
        Titulo.Dock = DockStyle.Top
        Titulo.Height = 32
        Titulo.ForeColor = ColorTexto
        Titulo.TextAlign = ContentAlignment.MiddleLeft
        Titulo.Visible = True
        Return Pnl
    End Function

    '///////////////////////// ESTILOS DE BOTONES //////////////////////
    Public Sub EstilizarBoton(ByVal btn As C1.Win.C1Input.C1Button, ByVal Primario As Boolean)
        btn.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btn.FlatStyle = FlatStyle.Flat
        btn.UseVisualStyleBackColor = False
        btn.FlatAppearance.BorderSize = 1
        If Primario Then
            btn.BackColor = ColorPrimario
            btn.ForeColor = Color.White
            btn.FlatAppearance.BorderColor = ColorPrimario
            btn.FlatAppearance.MouseOverBackColor = ColorPrimarioHover
            btn.FlatAppearance.MouseDownBackColor = ColorPrimarioPresionado
        Else
            btn.BackColor = Color.White
            btn.ForeColor = ColorTexto
            btn.FlatAppearance.BorderColor = ColorBorde
            btn.FlatAppearance.MouseOverBackColor = ColorHover
            btn.FlatAppearance.MouseDownBackColor = ColorPresionado
        End If
    End Sub

    Public Sub EstilizarTarjeta(ByVal btn As C1.Win.C1Input.C1Button, ByVal ConImagen As Boolean)
        EstilizarBoton(btn, False)
        btn.BackColor = If(ConImagen, Color.White, ColorFondoSuave)
        btn.Margin = New Padding(4)
        btn.Size = New Size(AnchoTarjeta, AltoTarjeta)
        btn.Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold)
    End Sub

    Private Sub EstilizarCheck(ByVal ck As CheckBox)
        ck.Appearance = Appearance.Button
        ck.AutoSize = False
        ck.Dock = DockStyle.Fill
        ck.TextAlign = ContentAlignment.MiddleCenter
        ck.FlatStyle = FlatStyle.Flat
        ck.UseVisualStyleBackColor = False
        ck.BackColor = Color.White
        ck.ForeColor = ColorTexto
        ck.FlatAppearance.BorderSize = 1
        ck.FlatAppearance.BorderColor = ColorBorde
        ck.FlatAppearance.CheckedBackColor = ColorSeleccion
        ck.FlatAppearance.MouseOverBackColor = ColorHover
        ck.FlatAppearance.MouseDownBackColor = ColorPresionado
    End Sub

    Private Sub EstilizarEtiqueta(ByVal lbl As Label)
        lbl.AutoSize = False
        lbl.Dock = DockStyle.Fill
        lbl.Margin = New Padding(0, 0, 6, 0)
        lbl.TextAlign = ContentAlignment.MiddleRight
        lbl.ForeColor = ColorTextoSecundario
        lbl.BackColor = Color.Transparent
    End Sub

    Private Sub EstilizarValor(ByVal txt As TextBox)
        txt.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txt.Margin = New Padding(0, 0, 8, 0)
        txt.TextAlign = HorizontalAlignment.Right
        txt.BorderStyle = BorderStyle.FixedSingle
        txt.BackColor = ColorFondoSuave
        txt.ForeColor = ColorTexto
    End Sub

    '///////////////////////// CONSTRUCCION DEL DISEÑO //////////////////////
    Private Sub ConstruirDiseno()
        Me.SuspendLayout()
        Me.BackColor = ColorFondo
        Me.MinimumSize = New Size(720, 520)

        '--- raiz: encabezado arriba, cuenta | menu | acciones
        tlpRaiz = NuevaTabla(3, 3)
        tlpRaiz.Padding = New Padding(14, 10, 14, 14)
        tlpRaiz.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 38.0!))
        tlpRaiz.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 62.0!))
        tlpRaiz.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120.0!))
        tlpRaiz.RowStyles.Add(New RowStyle(SizeType.Absolute, 64.0!))
        tlpRaiz.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
        tlpRaiz.RowStyles.Add(New RowStyle(SizeType.Absolute, 0.0!))

        '--- encabezado: logo | mesero | Mesa: [combo] | Serie: [combo]
        tlpEncabezado = NuevaTabla(6, 1)
        tlpEncabezado.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 64.0!))
        tlpEncabezado.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        tlpEncabezado.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        tlpEncabezado.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 230.0!))
        tlpEncabezado.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        tlpEncabezado.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 80.0!))
        tlpEncabezado.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

        Me.ImgFoto.Dock = DockStyle.Fill
        Me.ImgFoto.Margin = New Padding(0, 4, 10, 4)
        Me.ImgFoto.SizeMode = PictureBoxSizeMode.Zoom
        Me.lblNombreVendedor.AutoSize = False
        Me.lblNombreVendedor.AutoEllipsis = True
        Me.lblNombreVendedor.Dock = DockStyle.Fill
        Me.lblNombreVendedor.Margin = New Padding(0)
        Me.lblNombreVendedor.ForeColor = Color.White
        Me.lblNombreVendedor.TextAlign = ContentAlignment.MiddleLeft
        Me.LblMesas.AutoSize = True
        Me.LblMesas.Anchor = AnchorStyles.Right
        Me.LblMesas.Margin = New Padding(8, 0, 8, 0)
        Me.LblMesas.ForeColor = ColorTextoClaro
        Me.LblMesas.Text = "Mesa:"
        Me.cboMesas.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        Me.cboMesas.Margin = New Padding(0)
        Me.Label13.AutoSize = True
        Me.Label13.Anchor = AnchorStyles.Right
        Me.Label13.Margin = New Padding(16, 0, 8, 0)
        Me.Label13.ForeColor = ColorTextoClaro
        Me.cboSerie.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        Me.cboSerie.Margin = New Padding(0)

        tlpEncabezado.Controls.Add(Me.ImgFoto, 0, 0)
        tlpEncabezado.Controls.Add(Me.lblNombreVendedor, 1, 0)
        tlpEncabezado.Controls.Add(Me.LblMesas, 2, 0)
        tlpEncabezado.Controls.Add(Me.cboMesas, 3, 0)
        tlpEncabezado.Controls.Add(Me.Label13, 4, 0)
        tlpEncabezado.Controls.Add(Me.cboSerie, 5, 0)

        '--- tarjeta de la cuenta: grid, ajustes, opciones, totales
        pnlCuenta = NuevoPanelBlanco(New Label)
        pnlCuenta.Padding = New Padding(12)
        pnlCuenta.Margin = New Padding(0, 0, 10, 0)
        tlpCuenta = NuevaTabla(1, 5)
        tlpCuenta.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        tlpCuenta.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
        tlpCuenta.RowStyles.Add(New RowStyle(SizeType.Absolute, 58.0!))
        tlpCuenta.RowStyles.Add(New RowStyle(SizeType.Absolute, 46.0!))
        tlpCuenta.RowStyles.Add(New RowStyle(SizeType.Absolute, 84.0!))
        tlpCuenta.RowStyles.Add(New RowStyle(SizeType.Absolute, 34.0!))

        Me.C1DockingTab1.Dock = DockStyle.Fill
        Me.C1DockingTab1.Margin = New Padding(0, 0, 0, 8)
        Me.C1TrueDBGrid1.Dock = DockStyle.Fill
        For Each Caja As C1.Win.C1Input.C1TextBox In New C1.Win.C1Input.C1TextBox() {Me.txtNombre, Me.txtRuc, Me.txtTelefono, Me.txtDireccion, Me.txtObservacion}
            Caja.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Next

        tlpAjustes = NuevaTabla(6, 1)
        Dim PesosAjustes() As Single = {12.0!, 12.0!, 12.0!, 12.0!, 26.0!, 26.0!}
        For Each Peso As Single In PesosAjustes
            tlpAjustes.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, Peso))
        Next
        tlpAjustes.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
        Me.btnEliminar.Text = "Eliminar"
        Dim BotonesAjuste() As C1.Win.C1Input.C1Button = {Me.btnAtras, Me.btnSiguiente, Me.btnAgregar, Me.btnDisminuir, Me.btnEliminar, Me.C1Button2}
        For i As Integer = 0 To BotonesAjuste.Length - 1
            EstilizarBoton(BotonesAjuste(i), False)
            BotonesAjuste(i).Dock = DockStyle.Fill
            BotonesAjuste(i).Margin = New Padding(0, 0, If(i = BotonesAjuste.Length - 1, 0, 6), 0)
            tlpAjustes.Controls.Add(BotonesAjuste(i), i, 0)
        Next
        Me.btnEliminar.ImageAlign = ContentAlignment.MiddleLeft
        Me.btnEliminar.TextAlign = ContentAlignment.MiddleRight
        Me.C1Button2.TextAlign = ContentAlignment.MiddleRight
        Me.C1Button2.Text = "Precio Unitario"

        tlpOpciones = NuevaTabla(2, 1)
        tlpOpciones.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        tlpOpciones.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        tlpOpciones.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
        tlpOpciones.Margin = New Padding(0, 8, 0, 0)
        EstilizarCheck(Me.ckPropina)
        EstilizarCheck(Me.ckExonerar)
        Me.ckPropina.Margin = New Padding(0, 0, 6, 0)
        Me.ckExonerar.Margin = New Padding(0)
        tlpOpciones.Controls.Add(Me.ckPropina, 0, 0)
        tlpOpciones.Controls.Add(Me.ckExonerar, 1, 0)

        tlpTotales = NuevaTabla(4, 2)
        tlpTotales.Margin = New Padding(0, 8, 0, 0)
        Dim PesosTotales() As Single = {22.0!, 28.0!, 22.0!, 28.0!}
        For Each Peso As Single In PesosTotales
            tlpTotales.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, Peso))
        Next
        tlpTotales.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0!))
        tlpTotales.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0!))
        For Each Etiqueta As Label In New Label() {Me.LblSubTotal, Me.LblIva, Me.LblPropina, Me.LblTotal}
            EstilizarEtiqueta(Etiqueta)
        Next
        For Each Valor As TextBox In New TextBox() {Me.txtSubTotal, Me.txtIva, Me.txtpropina, Me.txtTotal}
            EstilizarValor(Valor)
        Next
        Me.txtTotal.ForeColor = ColorPrimario
        tlpTotales.Controls.Add(Me.LblSubTotal, 0, 0)
        tlpTotales.Controls.Add(Me.txtSubTotal, 1, 0)
        tlpTotales.Controls.Add(Me.LblIva, 2, 0)
        tlpTotales.Controls.Add(Me.txtIva, 3, 0)
        tlpTotales.Controls.Add(Me.LblPropina, 0, 1)
        tlpTotales.Controls.Add(Me.txtpropina, 1, 1)
        tlpTotales.Controls.Add(Me.LblTotal, 2, 1)
        tlpTotales.Controls.Add(Me.txtTotal, 3, 1)

        tlpCuenta.Controls.Add(Me.C1DockingTab1, 0, 0)
        tlpCuenta.Controls.Add(tlpAjustes, 0, 1)
        tlpCuenta.Controls.Add(tlpOpciones, 0, 2)
        tlpCuenta.Controls.Add(tlpTotales, 0, 3)
        LblEstadoPago = New Label
        LblEstadoPago.AutoSize = False
        LblEstadoPago.Dock = DockStyle.Fill
        LblEstadoPago.Margin = New Padding(0, 6, 0, 0)
        LblEstadoPago.TextAlign = ContentAlignment.MiddleCenter
        LblEstadoPago.ForeColor = ColorTextoSecundario
        LblEstadoPago.Text = "Sin pagos aplicados"
        tlpCuenta.Controls.Add(LblEstadoPago, 0, 4)
        pnlCuenta.Controls.Add(tlpCuenta)

        '--- menu: categorias arriba, productos abajo
        tlpMenu = NuevaTabla(1, 2)
        tlpMenu.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        tlpMenu.RowStyles.Add(New RowStyle(SizeType.Percent, 34.0!))
        tlpMenu.RowStyles.Add(New RowStyle(SizeType.Percent, 66.0!))

        pnlCategorias = NuevoPanelBlanco(Me.Label6)
        pnlCategorias.Margin = New Padding(0, 0, 0, 10)
        Me.Label6.Text = "Categorías"
        Me.FlowLayoutPanel1.Dock = DockStyle.Fill
        Me.FlowLayoutPanel1.BorderStyle = BorderStyle.None
        Me.FlowLayoutPanel1.BackColor = Color.White
        Me.FlowLayoutPanel1.Margin = New Padding(0)
        Me.FlowLayoutPanel1.AutoScroll = True
        pnlCategorias.Controls.Add(Me.FlowLayoutPanel1)
        pnlCategorias.Controls.Add(Me.Label6)

        pnlProductos = NuevoPanelBlanco(Me.LblProductos)
        pnlProductos.Margin = New Padding(0)
        Me.LblProductos.Text = "Productos"
        pnlProductos.Controls.Add(Me.LblProductos)

        tlpMenu.Controls.Add(pnlCategorias, 0, 0)
        tlpMenu.Controls.Add(pnlProductos, 0, 1)
        AddHandler tlpMenu.SizeChanged, AddressOf AjustarAlturaCategorias
        AddHandler Me.FlowLayoutPanel1.ControlAdded, AddressOf AjustarAlturaCategorias

        '--- acciones
        tlpAcciones = NuevaTabla(1, 7)
        Acciones = New C1.Win.C1Input.C1Button() {Me.C1Button5, Me.btGuardar, Me.C1Button3, Me.BtnPago, Me.BtnPreFactura, Me.C1Button4, Me.C1Button1}
        For Each Boton As C1.Win.C1Input.C1Button In Acciones
            EstilizarBoton(Boton, Boton Is Me.BtnPago OrElse Boton Is Me.btGuardar)
            Boton.Dock = DockStyle.Fill
            Boton.ImageAlign = ContentAlignment.TopCenter
            Boton.TextAlign = ContentAlignment.BottomCenter
            Boton.Padding = New Padding(0, 4, 0, 4)
        Next
        Me.BtnPreFactura.Text = "PreFactura"

        '--- ensamblo
        tlpRaiz.Controls.Add(tlpEncabezado, 0, 0)
        tlpRaiz.SetColumnSpan(tlpEncabezado, 3)
        tlpRaiz.Controls.Add(pnlCuenta, 0, 1)
        tlpRaiz.Controls.Add(tlpMenu, 1, 1)
        tlpRaiz.Controls.Add(tlpAcciones, 2, 1)
        Me.Controls.Add(tlpRaiz)
        tlpRaiz.BringToFront()

        Me.ResumeLayout(False)
        ModoActual = -1
        Responsivo()
    End Sub

    '///////////////////////// PANEL DE PRODUCTOS //////////////////////
    ' Se llama al cargar el formulario y cada vez que se elige una linea.
    Private Sub ReiniciarProductos()
        If FlowPProductos IsNot Nothing Then
            pnlProductos.Controls.Remove(FlowPProductos)
            FlowPProductos.Dispose()
        End If
        FlowPProductos = New FlowLayoutPanel
        FlowPProductos.Dock = DockStyle.Fill
        FlowPProductos.Margin = New Padding(0)
        FlowPProductos.BackColor = Color.White
        FlowPProductos.BorderStyle = BorderStyle.None
        FlowPProductos.AutoScroll = True
        pnlProductos.Controls.Add(FlowPProductos)
        FlowPProductos.BringToFront() 'el "Fill" se acomoda al final, despues del titulo
    End Sub

    Private Sub AplicarTamanoTarjetas(ByVal Contenedor As FlowLayoutPanel)
        If Contenedor Is Nothing Then Exit Sub
        Contenedor.SuspendLayout()
        For Each Ctl As Control In Contenedor.Controls
            Ctl.Size = New Size(AnchoTarjeta, AltoTarjeta)
        Next
        Contenedor.ResumeLayout()
    End Sub

    ' La zona de categorias crece segun cuantas filas necesita (hasta la mitad del menu);
    ' el resto del alto queda para los productos.
    Private Sub AjustarAlturaCategorias(ByVal sender As Object, ByVal e As EventArgs)
        If tlpMenu Is Nothing OrElse tlpMenu.Height <= 0 Then Exit Sub
        Dim Celda As Integer = AnchoTarjeta + 8
        Dim PorFila As Integer = Math.Max(1, (Me.FlowLayoutPanel1.Width - 24) \ Celda)
        Dim Filas As Integer = Math.Max(1, CInt(Math.Ceiling(Me.FlowLayoutPanel1.Controls.Count / PorFila)))
        Dim Necesario As Integer = Filas * (AltoTarjeta + 8) + Me.Label6.Height + pnlCategorias.Padding.Vertical + 6
        Dim Alto As Single = Math.Min(Necesario, CInt(tlpMenu.Height * 0.5))
        If Math.Abs(tlpMenu.RowStyles(0).Height - Alto) < 1 AndAlso tlpMenu.RowStyles(0).SizeType = SizeType.Absolute Then Exit Sub
        tlpMenu.RowStyles(0).SizeType = SizeType.Absolute
        tlpMenu.RowStyles(0).Height = Alto
        tlpMenu.RowStyles(1).SizeType = SizeType.Percent
        tlpMenu.RowStyles(1).Height = 100.0!
    End Sub

    '///////////////////////// MOVER BOTONES //////////////////////
    ' Reubica las zonas segun el modo: en pantallas anchas las acciones van a la derecha
    ' (columna vertical); en pantallas estrechas pasan abajo (fila horizontal).
    Public Sub MoverBotones()
        If tlpRaiz Is Nothing Then Exit Sub
        Dim Estrecho As Boolean = (ModoActual = 3)
        Dim PorcCuenta As Single, AnchoAcciones As Single, AltoAcciones As Single
        Select Case ModoActual
            Case 0 : PorcCuenta = 34.0! : AnchoAcciones = 140.0!
            Case 1 : PorcCuenta = 38.0! : AnchoAcciones = 120.0!
            Case 2 : PorcCuenta = 42.0! : AnchoAcciones = 104.0!
            Case Else : PorcCuenta = 46.0! : AnchoAcciones = 0.0!
        End Select
        AltoAcciones = If(Estrecho, 84.0!, 0.0!)

        tlpRaiz.SuspendLayout()
        tlpAcciones.SuspendLayout()

        tlpRaiz.ColumnStyles(0).Width = PorcCuenta
        tlpRaiz.ColumnStyles(1).Width = 100.0! - PorcCuenta
        tlpRaiz.ColumnStyles(2).Width = AnchoAcciones
        tlpRaiz.RowStyles(0).Height = If(Estrecho, 56.0!, 64.0!)
        tlpRaiz.RowStyles(2).Height = AltoAcciones

        '--- posicion de la zona de acciones
        If Estrecho Then
            tlpRaiz.SetCellPosition(tlpAcciones, New TableLayoutPanelCellPosition(0, 2))
            tlpRaiz.SetColumnSpan(tlpAcciones, 3)
            tlpAcciones.Margin = New Padding(0, 10, 0, 0)
        Else
            tlpRaiz.SetCellPosition(tlpAcciones, New TableLayoutPanelCellPosition(2, 1))
            tlpRaiz.SetColumnSpan(tlpAcciones, 1)
            tlpAcciones.Margin = New Padding(10, 0, 0, 0)
        End If

        '--- orientacion de los botones de accion
        tlpAcciones.Controls.Clear()
        tlpAcciones.ColumnStyles.Clear()
        tlpAcciones.RowStyles.Clear()
        Dim N As Integer = Acciones.Length
        If Estrecho Then
            tlpAcciones.ColumnCount = N
            tlpAcciones.RowCount = 1
            tlpAcciones.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
        Else
            tlpAcciones.ColumnCount = 1
            tlpAcciones.RowCount = N
            tlpAcciones.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        End If
        For i As Integer = 0 To N - 1
            If Estrecho Then
                tlpAcciones.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0! / N))
                Acciones(i).Margin = New Padding(0, 0, If(i = N - 1, 0, 6), 0)
                tlpAcciones.Controls.Add(Acciones(i), i, 0)
            Else
                tlpAcciones.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0! / N))
                Acciones(i).Margin = New Padding(0, 0, 0, If(i = N - 1, 0, 6))
                tlpAcciones.Controls.Add(Acciones(i), 0, i)
            End If
        Next

        '--- alturas de la tarjeta de cuenta segun la escala
        tlpCuenta.RowStyles(1).Height = CInt(58 * EscalaFuente)
        tlpCuenta.RowStyles(2).Height = CInt(46 * EscalaFuente)
        tlpCuenta.RowStyles(3).Height = CInt(84 * EscalaFuente)
        tlpCuenta.RowStyles(4).Height = CInt(34 * EscalaFuente)
        tlpEncabezado.ColumnStyles(3).Width = CInt(230 * EscalaFuente)
        tlpEncabezado.ColumnStyles(5).Width = CInt(80 * EscalaFuente)
        tlpEncabezado.ColumnStyles(0).Width = If(Estrecho, 52.0!, 64.0!)

        tlpAcciones.ResumeLayout(True)
        tlpRaiz.ResumeLayout(True)
    End Sub

    Private Function Fuente(ByVal Tam As Single, ByVal Estilo As FontStyle, Optional ByVal Nombre As String = "Segoe UI") As Font
        Dim F As New Font(Nombre, Tam * EscalaFuente, Estilo)
        FuentesDiseno.Add(F)
        Return F
    End Function

    Private Sub AplicarFuentes()
        Dim Anteriores As List(Of Font) = FuentesDiseno
        FuentesDiseno = New List(Of Font)

        Me.lblNombreVendedor.Font = Fuente(18.0!, FontStyle.Bold, "Segoe UI Semibold")
        Me.LblMesas.Font = Fuente(12.0!, FontStyle.Regular)
        Me.Label13.Font = Fuente(12.0!, FontStyle.Regular)
        Me.cboSerie.Font = Fuente(12.0!, FontStyle.Regular)
        Me.Label6.Font = Fuente(12.0!, FontStyle.Bold, "Segoe UI Semibold")
        Me.LblProductos.Font = Fuente(12.0!, FontStyle.Bold, "Segoe UI Semibold")

        For Each Boton As C1.Win.C1Input.C1Button In Acciones
            Boton.Font = Fuente(9.0!, FontStyle.Bold, "Segoe UI Semibold")
        Next
        For Each Boton As C1.Win.C1Input.C1Button In New C1.Win.C1Input.C1Button() {Me.btnAtras, Me.btnSiguiente, Me.btnAgregar, Me.btnDisminuir}
            Boton.Font = Fuente(16.0!, FontStyle.Bold, "Segoe UI Semibold")
        Next
        Me.btnEliminar.Font = Fuente(10.0!, FontStyle.Bold, "Segoe UI Semibold")
        Me.C1Button2.Font = Fuente(10.0!, FontStyle.Bold, "Segoe UI Semibold")
        Me.ckPropina.Font = Fuente(11.0!, FontStyle.Bold, "Segoe UI Semibold")
        Me.ckExonerar.Font = Fuente(11.0!, FontStyle.Bold, "Segoe UI Semibold")

        For Each Etiqueta As Label In New Label() {Me.LblSubTotal, Me.LblIva, Me.LblPropina, Me.LblTotal}
            Etiqueta.Font = Fuente(10.0!, FontStyle.Regular)
        Next
        For Each Valor As TextBox In New TextBox() {Me.txtSubTotal, Me.txtIva, Me.txtpropina}
            Valor.Font = Fuente(12.0!, FontStyle.Regular)
        Next
        Me.txtTotal.Font = Fuente(14.0!, FontStyle.Bold, "Segoe UI Semibold")
        LblEstadoPago.Font = Fuente(11.0!, FontStyle.Bold, "Segoe UI Semibold")

        For Each F As Font In Anteriores
            F.Dispose()
        Next
    End Sub

    '///////////////////////// RESPONSIVO //////////////////////
    ' Elige el modo segun el tamaño de la ventana (PC grande, normal, tablet/14", muy estrecho)
    ' y solo vuelve a acomodar cuando cambia de modo; entre modos las tablas se estiran solas.
    Public Sub Responsivo()
        If tlpRaiz Is Nothing Then Exit Sub
        Dim Ancho As Integer = Me.ClientSize.Width
        Dim Alto As Integer = Me.ClientSize.Height
        If Ancho = 0 OrElse Alto = 0 Then Exit Sub

        Dim Modo As Integer
        If Ancho >= 1500 Then
            Modo = 0
        ElseIf Ancho >= 1100 Then
            Modo = 1
        ElseIf Ancho >= 860 Then
            Modo = 2
        Else
            Modo = 3
        End If
        If Alto < 640 AndAlso Modo < 2 Then Modo = 2 'pantallas bajitas se tratan como compactas
        If Modo = ModoActual Then Exit Sub
        ModoActual = Modo

        Me.SuspendLayout()
        Select Case Modo
            Case 0 : EscalaFuente = 1.2! : AnchoTarjeta = 132 : AltoTarjeta = 100
            Case 1 : EscalaFuente = 1.0! : AnchoTarjeta = 118 : AltoTarjeta = 92
            Case 2 : EscalaFuente = 0.9! : AnchoTarjeta = 104 : AltoTarjeta = 84
            Case Else : EscalaFuente = 0.8! : AnchoTarjeta = 92 : AltoTarjeta = 76
        End Select

        AplicarFuentes()
        MoverBotones()
        AplicarTamanoTarjetas(Me.FlowLayoutPanel1)
        AplicarTamanoTarjetas(FlowPProductos)
        Me.ResumeLayout(True)
        AjustarAlturaCategorias(Nothing, EventArgs.Empty)
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
        Responsivo()
    End Sub

End Class
