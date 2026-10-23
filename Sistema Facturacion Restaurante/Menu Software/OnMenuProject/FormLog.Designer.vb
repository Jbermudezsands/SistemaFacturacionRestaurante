<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLog
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLog))
        Me.pnlTarjeta = New System.Windows.Forms.Panel()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnAcceder = New System.Windows.Forms.Button()
        Me.chkMostrar = New System.Windows.Forms.CheckBox()
        Me.txtContraseña = New System.Windows.Forms.TextBox()
        Me.lblContraseña = New System.Windows.Forms.Label()
        Me.cboMesero = New System.Windows.Forms.ComboBox()
        Me.lblMesero = New System.Windows.Forms.Label()
        Me.lblSubtitulo = New System.Windows.Forms.Label()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LblVersion = New System.Windows.Forms.Label()
        Me.pnlTarjeta.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTarjeta
        '
        Me.pnlTarjeta.BackColor = System.Drawing.Color.White
        Me.pnlTarjeta.Controls.Add(Me.btnSalir)
        Me.pnlTarjeta.Controls.Add(Me.btnAcceder)
        Me.pnlTarjeta.Controls.Add(Me.chkMostrar)
        Me.pnlTarjeta.Controls.Add(Me.txtContraseña)
        Me.pnlTarjeta.Controls.Add(Me.lblContraseña)
        Me.pnlTarjeta.Controls.Add(Me.cboMesero)
        Me.pnlTarjeta.Controls.Add(Me.lblMesero)
        Me.pnlTarjeta.Controls.Add(Me.lblSubtitulo)
        Me.pnlTarjeta.Controls.Add(Me.lblTitulo)
        Me.pnlTarjeta.Controls.Add(Me.PictureBox1)
        Me.pnlTarjeta.Location = New System.Drawing.Point(370, 20)
        Me.pnlTarjeta.Name = "pnlTarjeta"
        Me.pnlTarjeta.Size = New System.Drawing.Size(460, 580)
        Me.pnlTarjeta.TabIndex = 0
        '
        'btnSalir
        '
        Me.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalir.FlatAppearance.BorderSize = 0
        Me.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(228, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalir.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.btnSalir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSalir.Location = New System.Drawing.Point(40, 506)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(380, 42)
        Me.btnSalir.TabIndex = 5
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnAcceder
        '
        Me.btnAcceder.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.btnAcceder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAcceder.FlatAppearance.BorderSize = 0
        Me.btnAcceder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.btnAcceder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.btnAcceder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAcceder.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.btnAcceder.ForeColor = System.Drawing.Color.White
        Me.btnAcceder.Location = New System.Drawing.Point(40, 440)
        Me.btnAcceder.Name = "btnAcceder"
        Me.btnAcceder.Size = New System.Drawing.Size(380, 56)
        Me.btnAcceder.TabIndex = 4
        Me.btnAcceder.Text = "Acceder"
        Me.btnAcceder.UseVisualStyleBackColor = False
        '
        'chkMostrar
        '
        Me.chkMostrar.AutoSize = True
        Me.chkMostrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkMostrar.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkMostrar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkMostrar.Location = New System.Drawing.Point(40, 394)
        Me.chkMostrar.Name = "chkMostrar"
        Me.chkMostrar.Size = New System.Drawing.Size(148, 23)
        Me.chkMostrar.TabIndex = 3
        Me.chkMostrar.Text = "Mostrar contraseña"
        Me.chkMostrar.UseVisualStyleBackColor = True
        '
        'txtContraseña
        '
        Me.txtContraseña.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtContraseña.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContraseña.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.txtContraseña.Location = New System.Drawing.Point(40, 342)
        Me.txtContraseña.Name = "txtContraseña"
        Me.txtContraseña.Size = New System.Drawing.Size(380, 36)
        Me.txtContraseña.TabIndex = 2
        Me.txtContraseña.UseSystemPasswordChar = True
        '
        'lblContraseña
        '
        Me.lblContraseña.AutoSize = True
        Me.lblContraseña.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblContraseña.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblContraseña.Location = New System.Drawing.Point(38, 316)
        Me.lblContraseña.Name = "lblContraseña"
        Me.lblContraseña.Size = New System.Drawing.Size(79, 19)
        Me.lblContraseña.TabIndex = 7
        Me.lblContraseña.Text = "Contraseña"
        '
        'cboMesero
        '
        Me.cboMesero.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.cboMesero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMesero.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboMesero.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.cboMesero.FormattingEnabled = True
        Me.cboMesero.Location = New System.Drawing.Point(40, 262)
        Me.cboMesero.Name = "cboMesero"
        Me.cboMesero.Size = New System.Drawing.Size(380, 38)
        Me.cboMesero.TabIndex = 1
        '
        'lblMesero
        '
        Me.lblMesero.AutoSize = True
        Me.lblMesero.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblMesero.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblMesero.Location = New System.Drawing.Point(38, 236)
        Me.lblMesero.Name = "lblMesero"
        Me.lblMesero.Size = New System.Drawing.Size(55, 19)
        Me.lblMesero.TabIndex = 6
        Me.lblMesero.Text = "Mesero"
        '
        'lblSubtitulo
        '
        Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(145, Byte), Integer))
        Me.lblSubtitulo.Location = New System.Drawing.Point(40, 184)
        Me.lblSubtitulo.Name = "lblSubtitulo"
        Me.lblSubtitulo.Size = New System.Drawing.Size(380, 26)
        Me.lblSubtitulo.TabIndex = 8
        Me.lblSubtitulo.Text = "Inicia sesión para continuar"
        Me.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitulo
        '
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI Semibold", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.lblTitulo.Location = New System.Drawing.Point(40, 140)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(380, 42)
        Me.lblTitulo.TabIndex = 9
        Me.lblTitulo.Text = "Zeus Facturación"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(182, 28)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(96, 96)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'LblVersion
        '
        Me.LblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblVersion.AutoSize = True
        Me.LblVersion.BackColor = System.Drawing.Color.Transparent
        Me.LblVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.LblVersion.Location = New System.Drawing.Point(1180, 520)
        Me.LblVersion.Name = "LblVersion"
        Me.LblVersion.Size = New System.Drawing.Size(0, 15)
        Me.LblVersion.TabIndex = 1
        '
        'FormLog
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1201, 620)
        Me.Controls.Add(Me.LblVersion)
        Me.Controls.Add(Me.pnlTarjeta)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormLog"
        Me.Text = "Inicio de sesión"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTarjeta.ResumeLayout(False)
        Me.pnlTarjeta.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTarjeta As System.Windows.Forms.Panel
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnAcceder As System.Windows.Forms.Button
    Friend WithEvents chkMostrar As System.Windows.Forms.CheckBox
    Friend WithEvents txtContraseña As System.Windows.Forms.TextBox
    Friend WithEvents lblContraseña As System.Windows.Forms.Label
    Friend WithEvents cboMesero As System.Windows.Forms.ComboBox
    Friend WithEvents lblMesero As System.Windows.Forms.Label
    Friend WithEvents lblSubtitulo As System.Windows.Forms.Label
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LblVersion As System.Windows.Forms.Label
End Class
