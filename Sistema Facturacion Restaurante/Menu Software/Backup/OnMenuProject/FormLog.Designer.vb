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
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboMesero = New C1.Win.C1List.C1Combo
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtContraseña = New C1.Win.C1Input.C1TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        CType(Me.cboMesero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContraseña, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(71, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Mesero:"
        '
        'cboMesero
        '
        Me.cboMesero.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboMesero.Caption = ""
        Me.cboMesero.CaptionHeight = 17
        Me.cboMesero.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboMesero.ColumnCaptionHeight = 17
        Me.cboMesero.ColumnFooterHeight = 17
        Me.cboMesero.ContentHeight = 30
        Me.cboMesero.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboMesero.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboMesero.EditorFont = New System.Drawing.Font("Segoe UI Light", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMesero.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMesero.EditorHeight = 30
        Me.cboMesero.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMesero.Images.Add(CType(resources.GetObject("cboMesero.Images"), System.Drawing.Image))
        Me.cboMesero.ItemHeight = 15
        Me.cboMesero.Location = New System.Drawing.Point(161, 46)
        Me.cboMesero.MatchEntryTimeout = CType(2000, Long)
        Me.cboMesero.MaxDropDownItems = CType(5, Short)
        Me.cboMesero.MaxLength = 32767
        Me.cboMesero.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboMesero.Name = "cboMesero"
        Me.cboMesero.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboMesero.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboMesero.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboMesero.Size = New System.Drawing.Size(301, 36)
        Me.cboMesero.TabIndex = 1
        Me.cboMesero.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboMesero.PropBag = resources.GetString("cboMesero.PropBag")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 30)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Contraseña:"
        '
        'txtContraseña
        '
        Me.txtContraseña.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContraseña.Location = New System.Drawing.Point(150, 95)
        Me.txtContraseña.Name = "txtContraseña"
        Me.txtContraseña.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtContraseña.Size = New System.Drawing.Size(311, 33)
        Me.txtContraseña.TabIndex = 3
        Me.txtContraseña.Tag = Nothing
        Me.txtContraseña.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtContraseña.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.PowderBlue
        Me.GroupBox1.Controls.Add(Me.cboMesero)
        Me.GroupBox1.Controls.Add(Me.txtContraseña)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(451, 269)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(503, 158)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Inicio de Sesión"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(612, 219)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(160, 30)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Zeus Facturación"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(601, 444)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(223, 50)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Acceder"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(620, 76)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(128, 128)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'FormLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 30.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SkyBlue
        Me.ClientSize = New System.Drawing.Size(1283, 741)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI Light", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.Name = "FormLog"
        Me.Text = "Inicio de sesión"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.cboMesero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContraseña, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboMesero As C1.Win.C1List.C1Combo
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtContraseña As C1.Win.C1Input.C1TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
