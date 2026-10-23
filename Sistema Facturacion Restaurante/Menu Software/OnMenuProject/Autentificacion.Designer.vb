<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Autentificacion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Autentificacion))
        Me.btnEntrar = New C1.Win.C1Input.C1Button
        Me.btnSalir = New C1.Win.C1Input.C1Button
        Me.txtContrasena = New C1.Win.C1Input.C1TextBox
        Me.cboUsuario = New C1.Win.C1List.C1Combo
        CType(Me.txtContrasena, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnEntrar
        '
        Me.btnEntrar.Location = New System.Drawing.Point(20, 58)
        Me.btnEntrar.Name = "btnEntrar"
        Me.btnEntrar.Size = New System.Drawing.Size(104, 55)
        Me.btnEntrar.TabIndex = 0
        Me.btnEntrar.Text = "Entrar"
        Me.btnEntrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnEntrar.UseVisualStyleBackColor = True
        Me.btnEntrar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnEntrar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(152, 58)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(104, 55)
        Me.btnSalir.TabIndex = 2
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSalir.UseVisualStyleBackColor = True
        Me.btnSalir.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnSalir.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtContrasena
        '
        Me.txtContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContrasena.Location = New System.Drawing.Point(20, 34)
        Me.txtContrasena.Name = "txtContrasena"
        Me.txtContrasena.Size = New System.Drawing.Size(236, 18)
        Me.txtContrasena.TabIndex = 3
        Me.txtContrasena.Tag = Nothing
        Me.txtContrasena.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtContrasena.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cboUsuario
        '
        Me.cboUsuario.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboUsuario.Caption = ""
        Me.cboUsuario.CaptionHeight = 17
        Me.cboUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboUsuario.ColumnCaptionHeight = 17
        Me.cboUsuario.ColumnFooterHeight = 17
        Me.cboUsuario.ContentHeight = 15
        Me.cboUsuario.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboUsuario.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboUsuario.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUsuario.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboUsuario.EditorHeight = 15
        Me.cboUsuario.Images.Add(CType(resources.GetObject("cboUsuario.Images"), System.Drawing.Image))
        Me.cboUsuario.ItemHeight = 15
        Me.cboUsuario.Location = New System.Drawing.Point(20, 7)
        Me.cboUsuario.MatchEntryTimeout = CType(2000, Long)
        Me.cboUsuario.MaxDropDownItems = CType(5, Short)
        Me.cboUsuario.MaxLength = 32767
        Me.cboUsuario.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboUsuario.Name = "cboUsuario"
        Me.cboUsuario.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboUsuario.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboUsuario.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboUsuario.Size = New System.Drawing.Size(236, 21)
        Me.cboUsuario.TabIndex = 4
        Me.cboUsuario.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboUsuario.PropBag = resources.GetString("cboUsuario.PropBag")
        '
        'Autentificacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 128)
        Me.Controls.Add(Me.cboUsuario)
        Me.Controls.Add(Me.txtContrasena)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.btnEntrar)
        Me.Name = "Autentificacion"
        Me.Text = "Autentificacion"
        CType(Me.txtContrasena, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnEntrar As C1.Win.C1Input.C1Button
    Friend WithEvents btnSalir As C1.Win.C1Input.C1Button
    Friend WithEvents txtContrasena As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboUsuario As C1.Win.C1List.C1Combo
End Class
