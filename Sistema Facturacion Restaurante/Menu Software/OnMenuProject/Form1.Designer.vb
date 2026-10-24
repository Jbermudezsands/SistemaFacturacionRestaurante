<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblNombreVendedor = New System.Windows.Forms.Label()
        Me.LblSubTotal = New System.Windows.Forms.Label()
        Me.LblIva = New System.Windows.Forms.Label()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.txtIva = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LblProductos = New System.Windows.Forms.Label()
        Me.btnAgregar = New C1.Win.C1Input.C1Button()
        Me.btnDisminuir = New C1.Win.C1Input.C1Button()
        Me.btnSiguiente = New C1.Win.C1Input.C1Button()
        Me.btnAtras = New C1.Win.C1Input.C1Button()
        Me.C1DockingTab1 = New C1.Win.C1Command.C1DockingTab()
        Me.C1DockingTabPage1 = New C1.Win.C1Command.C1DockingTabPage()
        Me.C1TrueDBGrid1 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.C1DockingTabPage2 = New C1.Win.C1Command.C1DockingTabPage()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtObservacion = New C1.Win.C1Input.C1TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtDireccion = New C1.Win.C1Input.C1TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTelefono = New C1.Win.C1Input.C1TextBox()
        Me.txtRuc = New C1.Win.C1Input.C1TextBox()
        Me.txtNombre = New C1.Win.C1Input.C1TextBox()
        Me.cboSerie = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ckExonerar = New System.Windows.Forms.CheckBox()
        Me.txtpropina = New System.Windows.Forms.TextBox()
        Me.LblPropina = New System.Windows.Forms.Label()
        Me.cboMesas = New C1.Win.C1List.C1Combo()
        Me.LblMesas = New System.Windows.Forms.Label()
        Me.ckPropina = New System.Windows.Forms.CheckBox()
        Me.C1Button5 = New C1.Win.C1Input.C1Button()
        Me.C1Button3 = New C1.Win.C1Input.C1Button()
        Me.C1Button2 = New C1.Win.C1Input.C1Button()
        Me.btnEliminar = New C1.Win.C1Input.C1Button()
        Me.C1Button4 = New C1.Win.C1Input.C1Button()
        Me.btGuardar = New C1.Win.C1Input.C1Button()
        Me.C1Button1 = New C1.Win.C1Input.C1Button()
        Me.ImgFoto = New System.Windows.Forms.PictureBox()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.BtnPago = New C1.Win.C1Input.C1Button()
        Me.BtnPreFactura = New C1.Win.C1Input.C1Button()
        CType(Me.C1DockingTab1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1DockingTab1.SuspendLayout()
        Me.C1DockingTabPage1.SuspendLayout()
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1DockingTabPage2.SuspendLayout()
        CType(Me.txtObservacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRuc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMesas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgFoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.FlowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(6, 409)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(670, 274)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'lblNombreVendedor
        '
        Me.lblNombreVendedor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNombreVendedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!)
        Me.lblNombreVendedor.ForeColor = System.Drawing.SystemColors.Control
        Me.lblNombreVendedor.Location = New System.Drawing.Point(1242, 102)
        Me.lblNombreVendedor.Name = "lblNombreVendedor"
        Me.lblNombreVendedor.Size = New System.Drawing.Size(90, 21)
        Me.lblNombreVendedor.TabIndex = 15
        Me.lblNombreVendedor.Text = "JUAN"
        Me.lblNombreVendedor.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LblSubTotal
        '
        Me.LblSubTotal.AutoSize = True
        Me.LblSubTotal.Font = New System.Drawing.Font("Lucida Sans Unicode", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSubTotal.Location = New System.Drawing.Point(12, 362)
        Me.LblSubTotal.Name = "LblSubTotal"
        Me.LblSubTotal.Size = New System.Drawing.Size(83, 20)
        Me.LblSubTotal.TabIndex = 17
        Me.LblSubTotal.Text = "SubTotal:"
        '
        'LblIva
        '
        Me.LblIva.AutoSize = True
        Me.LblIva.Font = New System.Drawing.Font("Lucida Sans Unicode", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIva.Location = New System.Drawing.Point(197, 362)
        Me.LblIva.Name = "LblIva"
        Me.LblIva.Size = New System.Drawing.Size(36, 20)
        Me.LblIva.TabIndex = 18
        Me.LblIva.Text = "Iva:"
        '
        'LblTotal
        '
        Me.LblTotal.AutoSize = True
        Me.LblTotal.Font = New System.Drawing.Font("Lucida Sans Unicode", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.Location = New System.Drawing.Point(498, 365)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.Size = New System.Drawing.Size(54, 20)
        Me.LblTotal.TabIndex = 19
        Me.LblTotal.Text = "Total:"
        '
        'txtSubTotal
        '
        Me.txtSubTotal.Font = New System.Drawing.Font("Lucida Sans Unicode", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.Location = New System.Drawing.Point(92, 359)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.Size = New System.Drawing.Size(99, 32)
        Me.txtSubTotal.TabIndex = 20
        Me.txtSubTotal.Text = "00.00"
        '
        'txtIva
        '
        Me.txtIva.Font = New System.Drawing.Font("Lucida Sans Unicode", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIva.Location = New System.Drawing.Point(236, 359)
        Me.txtIva.Name = "txtIva"
        Me.txtIva.Size = New System.Drawing.Size(99, 32)
        Me.txtIva.TabIndex = 21
        Me.txtIva.Text = "00.00"
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Lucida Sans Unicode", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(554, 359)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(109, 32)
        Me.txtTotal.TabIndex = 22
        Me.txtTotal.Text = "00.00"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Control
        Me.Label6.Location = New System.Drawing.Point(12, 390)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 16)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Lineas"
        '
        'LblProductos
        '
        Me.LblProductos.AutoSize = True
        Me.LblProductos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProductos.ForeColor = System.Drawing.SystemColors.Control
        Me.LblProductos.Location = New System.Drawing.Point(692, 9)
        Me.LblProductos.Name = "LblProductos"
        Me.LblProductos.Size = New System.Drawing.Size(68, 16)
        Me.LblProductos.TabIndex = 26
        Me.LblProductos.Text = "Productos"
        '
        'btnAgregar
        '
        Me.btnAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.Location = New System.Drawing.Point(547, 80)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(63, 59)
        Me.btnAgregar.TabIndex = 32
        Me.btnAgregar.Text = "+"
        Me.btnAgregar.UseVisualStyleBackColor = True
        Me.btnAgregar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnDisminuir
        '
        Me.btnDisminuir.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDisminuir.Location = New System.Drawing.Point(617, 80)
        Me.btnDisminuir.Name = "btnDisminuir"
        Me.btnDisminuir.Size = New System.Drawing.Size(63, 59)
        Me.btnDisminuir.TabIndex = 33
        Me.btnDisminuir.Text = "-"
        Me.btnDisminuir.UseVisualStyleBackColor = True
        Me.btnDisminuir.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSiguiente.Location = New System.Drawing.Point(6, 200)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(60, 98)
        Me.btnSiguiente.TabIndex = 35
        Me.btnSiguiente.Text = "↓"
        Me.btnSiguiente.UseVisualStyleBackColor = True
        Me.btnSiguiente.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnAtras
        '
        Me.btnAtras.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAtras.Location = New System.Drawing.Point(6, 96)
        Me.btnAtras.Name = "btnAtras"
        Me.btnAtras.Size = New System.Drawing.Size(60, 98)
        Me.btnAtras.TabIndex = 36
        Me.btnAtras.Text = "↑"
        Me.btnAtras.UseVisualStyleBackColor = True
        Me.btnAtras.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1DockingTab1
        '
        Me.C1DockingTab1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.C1DockingTab1.CanMoveTabs = True
        Me.C1DockingTab1.Controls.Add(Me.C1DockingTabPage1)
        Me.C1DockingTab1.Controls.Add(Me.C1DockingTabPage2)
        Me.C1DockingTab1.Indent = 1
        Me.C1DockingTab1.ItemSize = New System.Drawing.Size(100, 45)
        Me.C1DockingTab1.Location = New System.Drawing.Point(71, 22)
        Me.C1DockingTab1.Name = "C1DockingTab1"
        Me.C1DockingTab1.Padding = New System.Drawing.Point(19, 15)
        Me.C1DockingTab1.SelectedTabBold = True
        Me.C1DockingTab1.Size = New System.Drawing.Size(470, 326)
        Me.C1DockingTab1.SplitterWidth = 4
        Me.C1DockingTab1.TabAreaSpacing = 1
        Me.C1DockingTab1.TabIndex = 37
        Me.C1DockingTab1.TabsSpacing = -4
        Me.C1DockingTab1.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007
        Me.C1DockingTab1.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Blue
        Me.C1DockingTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue
        '
        'C1DockingTabPage1
        '
        Me.C1DockingTabPage1.Controls.Add(Me.C1TrueDBGrid1)
        Me.C1DockingTabPage1.Location = New System.Drawing.Point(1, 47)
        Me.C1DockingTabPage1.Name = "C1DockingTabPage1"
        Me.C1DockingTabPage1.Size = New System.Drawing.Size(468, 278)
        Me.C1DockingTabPage1.TabIndex = 1
        Me.C1DockingTabPage1.Text = "General"
        '
        'C1TrueDBGrid1
        '
        Me.C1TrueDBGrid1.AllowRowSelect = False
        Me.C1TrueDBGrid1.AllowUpdate = False
        Me.C1TrueDBGrid1.AlternatingRows = True
        Me.C1TrueDBGrid1.BackColor = System.Drawing.Color.PowderBlue
        Me.C1TrueDBGrid1.CaptionHeight = 17
        Me.C1TrueDBGrid1.Enabled = False
        Me.C1TrueDBGrid1.FetchRowStyles = True
        Me.C1TrueDBGrid1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1TrueDBGrid1.GroupByCaption = "Drag a column header here to group by that column"
        Me.C1TrueDBGrid1.Images.Add(CType(resources.GetObject("C1TrueDBGrid1.Images"), System.Drawing.Image))
        Me.C1TrueDBGrid1.Location = New System.Drawing.Point(4, 3)
        Me.C1TrueDBGrid1.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None
        Me.C1TrueDBGrid1.Name = "C1TrueDBGrid1"
        Me.C1TrueDBGrid1.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.ZoomFactor = 75.0R
        Me.C1TrueDBGrid1.PrintInfo.PageSettings = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.C1TrueDBGrid1.RowHeight = 15
        Me.C1TrueDBGrid1.Size = New System.Drawing.Size(459, 265)
        Me.C1TrueDBGrid1.TabIndex = 7
        Me.C1TrueDBGrid1.Text = "C1TrueDBGrid1"
        Me.C1TrueDBGrid1.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.C1TrueDBGrid1.PropBag = resources.GetString("C1TrueDBGrid1.PropBag")
        '
        'C1DockingTabPage2
        '
        Me.C1DockingTabPage2.Controls.Add(Me.Label12)
        Me.C1DockingTabPage2.Controls.Add(Me.Label11)
        Me.C1DockingTabPage2.Controls.Add(Me.txtObservacion)
        Me.C1DockingTabPage2.Controls.Add(Me.Label10)
        Me.C1DockingTabPage2.Controls.Add(Me.Label9)
        Me.C1DockingTabPage2.Controls.Add(Me.Label8)
        Me.C1DockingTabPage2.Controls.Add(Me.txtDireccion)
        Me.C1DockingTabPage2.Controls.Add(Me.Label1)
        Me.C1DockingTabPage2.Controls.Add(Me.txtTelefono)
        Me.C1DockingTabPage2.Controls.Add(Me.txtRuc)
        Me.C1DockingTabPage2.Controls.Add(Me.txtNombre)
        Me.C1DockingTabPage2.Location = New System.Drawing.Point(1, 47)
        Me.C1DockingTabPage2.Name = "C1DockingTabPage2"
        Me.C1DockingTabPage2.Size = New System.Drawing.Size(468, 278)
        Me.C1DockingTabPage2.TabIndex = 2
        Me.C1DockingTabPage2.Text = "Cliente"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(81, 26)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(120, 18)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Datos del Cliente"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(20, 218)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 20)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Observacion:"
        '
        'txtObservacion
        '
        Me.txtObservacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObservacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacion.Location = New System.Drawing.Point(129, 216)
        Me.txtObservacion.Multiline = True
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(283, 43)
        Me.txtObservacion.TabIndex = 8
        Me.txtObservacion.Tag = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(40, 169)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 20)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Direccion:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(47, 133)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 20)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Telefono:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(80, 98)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 20)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Ruc:"
        '
        'txtDireccion
        '
        Me.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDireccion.Location = New System.Drawing.Point(129, 167)
        Me.txtDireccion.Multiline = True
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(283, 43)
        Me.txtDireccion.TabIndex = 4
        Me.txtDireccion.Tag = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(48, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Nombre:"
        '
        'txtTelefono
        '
        Me.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTelefono.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelefono.Location = New System.Drawing.Point(129, 131)
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(283, 24)
        Me.txtTelefono.TabIndex = 2
        Me.txtTelefono.Tag = Nothing
        '
        'txtRuc
        '
        Me.txtRuc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRuc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRuc.Location = New System.Drawing.Point(129, 96)
        Me.txtRuc.Name = "txtRuc"
        Me.txtRuc.Size = New System.Drawing.Size(283, 24)
        Me.txtRuc.TabIndex = 1
        Me.txtRuc.Tag = Nothing
        '
        'txtNombre
        '
        Me.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombre.Location = New System.Drawing.Point(129, 63)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(283, 24)
        Me.txtNombre.TabIndex = 0
        Me.txtNombre.Tag = Nothing
        '
        'cboSerie
        '
        Me.cboSerie.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.cboSerie.FormattingEnabled = True
        Me.cboSerie.Items.AddRange(New Object() {"A", "B"})
        Me.cboSerie.Location = New System.Drawing.Point(2, 41)
        Me.cboSerie.Name = "cboSerie"
        Me.cboSerie.Size = New System.Drawing.Size(69, 24)
        Me.cboSerie.TabIndex = 38
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.Control
        Me.Label13.Location = New System.Drawing.Point(3, 22)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(42, 16)
        Me.Label13.TabIndex = 39
        Me.Label13.Text = "Serie:"
        '
        'ckExonerar
        '
        Me.ckExonerar.AutoSize = True
        Me.ckExonerar.Checked = True
        Me.ckExonerar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckExonerar.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.5!)
        Me.ckExonerar.Location = New System.Drawing.Point(547, 309)
        Me.ckExonerar.Name = "ckExonerar"
        Me.ckExonerar.Size = New System.Drawing.Size(129, 33)
        Me.ckExonerar.TabIndex = 41
        Me.ckExonerar.Text = "Exonerar"
        Me.ckExonerar.UseVisualStyleBackColor = True
        '
        'txtpropina
        '
        Me.txtpropina.Font = New System.Drawing.Font("Lucida Sans Unicode", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpropina.Location = New System.Drawing.Point(408, 359)
        Me.txtpropina.Name = "txtpropina"
        Me.txtpropina.Size = New System.Drawing.Size(84, 32)
        Me.txtpropina.TabIndex = 43
        Me.txtpropina.Text = "00.00"
        '
        'LblPropina
        '
        Me.LblPropina.AutoSize = True
        Me.LblPropina.Font = New System.Drawing.Font("Lucida Sans Unicode", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPropina.Location = New System.Drawing.Point(335, 365)
        Me.LblPropina.Name = "LblPropina"
        Me.LblPropina.Size = New System.Drawing.Size(74, 20)
        Me.LblPropina.TabIndex = 42
        Me.LblPropina.Text = "Propina:"
        '
        'cboMesas
        '
        Me.cboMesas.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboMesas.Caption = ""
        Me.cboMesas.CaptionHeight = 17
        Me.cboMesas.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboMesas.ColumnCaptionHeight = 17
        Me.cboMesas.ColumnFooterHeight = 17
        Me.cboMesas.ContentHeight = 30
        Me.cboMesas.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboMesas.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboMesas.EditorFont = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMesas.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMesas.EditorHeight = 30
        Me.cboMesas.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMesas.Images.Add(CType(resources.GetObject("cboMesas.Images"), System.Drawing.Image))
        Me.cboMesas.ItemHeight = 15
        Me.cboMesas.Location = New System.Drawing.Point(547, 35)
        Me.cboMesas.MatchEntryTimeout = CType(2000, Long)
        Me.cboMesas.MaxDropDownItems = CType(5, Short)
        Me.cboMesas.MaxLength = 32767
        Me.cboMesas.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboMesas.Name = "cboMesas"
        Me.cboMesas.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboMesas.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboMesas.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboMesas.Size = New System.Drawing.Size(133, 36)
        Me.cboMesas.TabIndex = 44
        Me.cboMesas.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboMesas.PropBag = resources.GetString("cboMesas.PropBag")
        '
        'LblMesas
        '
        Me.LblMesas.AutoSize = True
        Me.LblMesas.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.LblMesas.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.LblMesas.Location = New System.Drawing.Point(574, 9)
        Me.LblMesas.Name = "LblMesas"
        Me.LblMesas.Size = New System.Drawing.Size(76, 26)
        Me.LblMesas.TabIndex = 45
        Me.LblMesas.Text = "Mesas"
        '
        'ckPropina
        '
        Me.ckPropina.AutoSize = True
        Me.ckPropina.Checked = True
        Me.ckPropina.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckPropina.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.5!)
        Me.ckPropina.ForeColor = System.Drawing.SystemColors.Control
        Me.ckPropina.Location = New System.Drawing.Point(547, 270)
        Me.ckPropina.Name = "ckPropina"
        Me.ckPropina.Size = New System.Drawing.Size(116, 33)
        Me.ckPropina.TabIndex = 47
        Me.ckPropina.Text = "Propina"
        Me.ckPropina.UseVisualStyleBackColor = True
        '
        'C1Button5
        '
        Me.C1Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Button5.Image = CType(resources.GetObject("C1Button5.Image"), System.Drawing.Image)
        Me.C1Button5.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.C1Button5.Location = New System.Drawing.Point(1239, 192)
        Me.C1Button5.Name = "C1Button5"
        Me.C1Button5.Size = New System.Drawing.Size(93, 73)
        Me.C1Button5.TabIndex = 48
        Me.C1Button5.Text = "Nueva cuenta"
        Me.C1Button5.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.C1Button5.UseVisualStyleBackColor = True
        Me.C1Button5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Button3
        '
        Me.C1Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Button3.Image = CType(resources.GetObject("C1Button3.Image"), System.Drawing.Image)
        Me.C1Button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.C1Button3.Location = New System.Drawing.Point(1239, 341)
        Me.C1Button3.Name = "C1Button3"
        Me.C1Button3.Size = New System.Drawing.Size(93, 69)
        Me.C1Button3.TabIndex = 46
        Me.C1Button3.Text = "Mis cuentas"
        Me.C1Button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.C1Button3.UseVisualStyleBackColor = True
        Me.C1Button3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Button2
        '
        Me.C1Button2.Image = CType(resources.GetObject("C1Button2.Image"), System.Drawing.Image)
        Me.C1Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Button2.Location = New System.Drawing.Point(548, 206)
        Me.C1Button2.Name = "C1Button2"
        Me.C1Button2.Size = New System.Drawing.Size(132, 58)
        Me.C1Button2.TabIndex = 40
        Me.C1Button2.Tag = ""
        Me.C1Button2.Text = "Precio Unitario"
        Me.C1Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Button2.UseVisualStyleBackColor = True
        Me.C1Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnEliminar
        '
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminar.Location = New System.Drawing.Point(548, 145)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(132, 55)
        Me.btnEliminar.TabIndex = 34
        Me.btnEliminar.Text = "Eliminar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnEliminar.UseVisualStyleBackColor = True
        Me.btnEliminar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Button4
        '
        Me.C1Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Button4.Enabled = False
        Me.C1Button4.Image = CType(resources.GetObject("C1Button4.Image"), System.Drawing.Image)
        Me.C1Button4.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.C1Button4.Location = New System.Drawing.Point(1239, 583)
        Me.C1Button4.Name = "C1Button4"
        Me.C1Button4.Size = New System.Drawing.Size(93, 82)
        Me.C1Button4.TabIndex = 24
        Me.C1Button4.Text = "Cancelar e Imprimir"
        Me.C1Button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.C1Button4.UseVisualStyleBackColor = True
        Me.C1Button4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btGuardar
        '
        Me.btGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btGuardar.Image = CType(resources.GetObject("btGuardar.Image"), System.Drawing.Image)
        Me.btGuardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btGuardar.Location = New System.Drawing.Point(1239, 269)
        Me.btGuardar.Name = "btGuardar"
        Me.btGuardar.Size = New System.Drawing.Size(93, 68)
        Me.btGuardar.TabIndex = 23
        Me.btGuardar.Text = "Guardar"
        Me.btGuardar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btGuardar.UseVisualStyleBackColor = True
        Me.btGuardar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Button1
        '
        Me.C1Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Button1.Image = CType(resources.GetObject("C1Button1.Image"), System.Drawing.Image)
        Me.C1Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.C1Button1.Location = New System.Drawing.Point(1239, 125)
        Me.C1Button1.Name = "C1Button1"
        Me.C1Button1.Size = New System.Drawing.Size(93, 64)
        Me.C1Button1.TabIndex = 16
        Me.C1Button1.Text = "Cerrar Sesion"
        Me.C1Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.C1Button1.UseVisualStyleBackColor = True
        Me.C1Button1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ImgFoto
        '
        Me.ImgFoto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ImgFoto.Location = New System.Drawing.Point(1232, 16)
        Me.ImgFoto.Name = "ImgFoto"
        Me.ImgFoto.Size = New System.Drawing.Size(100, 83)
        Me.ImgFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImgFoto.TabIndex = 14
        Me.ImgFoto.TabStop = False
        '
        'BtnPago
        '
        Me.BtnPago.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPago.Image = CType(resources.GetObject("BtnPago.Image"), System.Drawing.Image)
        Me.BtnPago.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPago.Location = New System.Drawing.Point(1239, 414)
        Me.BtnPago.Name = "BtnPago"
        Me.BtnPago.Size = New System.Drawing.Size(93, 69)
        Me.BtnPago.TabIndex = 49
        Me.BtnPago.Text = "Pago"
        Me.BtnPago.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPago.UseVisualStyleBackColor = True
        Me.BtnPago.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnPreFactura
        '
        Me.BtnPreFactura.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPreFactura.Image = CType(resources.GetObject("BtnPreFactura.Image"), System.Drawing.Image)
        Me.BtnPreFactura.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPreFactura.Location = New System.Drawing.Point(1239, 489)
        Me.BtnPreFactura.Name = "BtnPreFactura"
        Me.BtnPreFactura.Size = New System.Drawing.Size(93, 67)
        Me.BtnPreFactura.TabIndex = 51
        Me.BtnPreFactura.Text = "Pre-Factura"
        Me.BtnPreFactura.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPreFactura.UseVisualStyleBackColor = True
        Me.BtnPreFactura.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = False
        Me.BackColor = System.Drawing.Color.DodgerBlue
        Me.ClientSize = New System.Drawing.Size(1344, 695)
        Me.Controls.Add(Me.BtnPreFactura)
        Me.Controls.Add(Me.BtnPago)
        Me.Controls.Add(Me.C1Button5)
        Me.Controls.Add(Me.ckPropina)
        Me.Controls.Add(Me.C1Button3)
        Me.Controls.Add(Me.LblMesas)
        Me.Controls.Add(Me.cboMesas)
        Me.Controls.Add(Me.txtpropina)
        Me.Controls.Add(Me.LblPropina)
        Me.Controls.Add(Me.ckExonerar)
        Me.Controls.Add(Me.C1Button2)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cboSerie)
        Me.Controls.Add(Me.C1DockingTab1)
        Me.Controls.Add(Me.btnAtras)
        Me.Controls.Add(Me.btnSiguiente)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnDisminuir)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.LblProductos)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.C1Button4)
        Me.Controls.Add(Me.btGuardar)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtIva)
        Me.Controls.Add(Me.txtSubTotal)
        Me.Controls.Add(Me.LblTotal)
        Me.Controls.Add(Me.LblIva)
        Me.Controls.Add(Me.LblSubTotal)
        Me.Controls.Add(Me.C1Button1)
        Me.Controls.Add(Me.lblNombreVendedor)
        Me.Controls.Add(Me.ImgFoto)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Text = "Zeus Facturación"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.C1DockingTab1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1DockingTab1.ResumeLayout(False)
        Me.C1DockingTabPage1.ResumeLayout(False)
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1DockingTabPage2.ResumeLayout(False)
        Me.C1DockingTabPage2.PerformLayout()
        CType(Me.txtObservacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRuc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMesas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgFoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents C1TrueDBGrid1 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents ImgFoto As System.Windows.Forms.PictureBox
    Friend WithEvents lblNombreVendedor As System.Windows.Forms.Label
    Friend WithEvents C1Button1 As C1.Win.C1Input.C1Button
    Friend WithEvents LblSubTotal As System.Windows.Forms.Label
    Friend WithEvents LblIva As System.Windows.Forms.Label
    Friend WithEvents LblTotal As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtIva As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents btGuardar As C1.Win.C1Input.C1Button
    Friend WithEvents C1Button4 As C1.Win.C1Input.C1Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents LblProductos As System.Windows.Forms.Label
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents btnAgregar As C1.Win.C1Input.C1Button
    Friend WithEvents btnDisminuir As C1.Win.C1Input.C1Button
    Friend WithEvents btnEliminar As C1.Win.C1Input.C1Button
    Friend WithEvents btnSiguiente As C1.Win.C1Input.C1Button
    Friend WithEvents btnAtras As C1.Win.C1Input.C1Button
    Friend WithEvents C1DockingTab1 As C1.Win.C1Command.C1DockingTab
    Friend WithEvents C1DockingTabPage1 As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents C1DockingTabPage2 As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents txtTelefono As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtRuc As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtNombre As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDireccion As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtObservacion As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboSerie As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents C1Button2 As C1.Win.C1Input.C1Button
    Friend WithEvents ckExonerar As System.Windows.Forms.CheckBox
    Friend WithEvents txtpropina As System.Windows.Forms.TextBox
    Friend WithEvents LblPropina As System.Windows.Forms.Label
    Friend WithEvents cboMesas As C1.Win.C1List.C1Combo
    Friend WithEvents LblMesas As System.Windows.Forms.Label
    Friend WithEvents C1Button3 As C1.Win.C1Input.C1Button
    Friend WithEvents ckPropina As System.Windows.Forms.CheckBox
    Friend WithEvents C1Button5 As C1.Win.C1Input.C1Button
    Friend WithEvents BtnPago As C1.Win.C1Input.C1Button
    Friend WithEvents BtnPreFactura As C1.Win.C1Input.C1Button
End Class
