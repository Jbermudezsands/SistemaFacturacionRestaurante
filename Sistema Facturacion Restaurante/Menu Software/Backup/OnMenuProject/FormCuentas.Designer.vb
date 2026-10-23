<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCuentas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCuentas))
        Me.Label1 = New System.Windows.Forms.Label
        Me.FLPMisMesas = New System.Windows.Forms.FlowLayoutPanel
        Me.FLPMisCuentas = New System.Windows.Forms.FlowLayoutPanel
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.FLPMesasDisponibles = New System.Windows.Forms.FlowLayoutPanel
        Me.C1Button1 = New C1.Win.C1Input.C1Button
        Me.C1Button2 = New C1.Win.C1Input.C1Button
        Me.C1Button3 = New C1.Win.C1Input.C1Button
        Me.C1Button4 = New C1.Win.C1Input.C1Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Mis Mesas"
        '
        'FLPMisMesas
        '
        Me.FLPMisMesas.AutoScroll = True
        Me.FLPMisMesas.BackColor = System.Drawing.Color.White
        Me.FLPMisMesas.Location = New System.Drawing.Point(12, 41)
        Me.FLPMisMesas.Name = "FLPMisMesas"
        Me.FLPMisMesas.Size = New System.Drawing.Size(529, 189)
        Me.FLPMisMesas.TabIndex = 1
        '
        'FLPMisCuentas
        '
        Me.FLPMisCuentas.AutoScroll = True
        Me.FLPMisCuentas.BackColor = System.Drawing.Color.White
        Me.FLPMisCuentas.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLPMisCuentas.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.FLPMisCuentas.Location = New System.Drawing.Point(547, 41)
        Me.FLPMisCuentas.Name = "FLPMisCuentas"
        Me.FLPMisCuentas.Size = New System.Drawing.Size(227, 189)
        Me.FLPMisCuentas.TabIndex = 2
        Me.FLPMisCuentas.WrapContents = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(554, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 21)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Mis cuentas abiertas"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.PowderBlue
        Me.GroupBox1.Controls.Add(Me.FLPMisMesas)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.FLPMisCuentas)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(791, 244)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Juan"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.PowderBlue
        Me.GroupBox2.Controls.Add(Me.FLPMesasDisponibles)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 262)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(791, 259)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Mesas Disponibles"
        '
        'FLPMesasDisponibles
        '
        Me.FLPMesasDisponibles.AutoScroll = True
        Me.FLPMesasDisponibles.BackColor = System.Drawing.Color.White
        Me.FLPMesasDisponibles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLPMesasDisponibles.Location = New System.Drawing.Point(3, 25)
        Me.FLPMesasDisponibles.Name = "FLPMesasDisponibles"
        Me.FLPMesasDisponibles.Size = New System.Drawing.Size(785, 231)
        Me.FLPMesasDisponibles.TabIndex = 1
        '
        'C1Button1
        '
        Me.C1Button1.Location = New System.Drawing.Point(809, 23)
        Me.C1Button1.Name = "C1Button1"
        Me.C1Button1.Size = New System.Drawing.Size(53, 100)
        Me.C1Button1.TabIndex = 6
        Me.C1Button1.Text = "↑"
        Me.C1Button1.UseVisualStyleBackColor = True
        '
        'C1Button2
        '
        Me.C1Button2.Location = New System.Drawing.Point(809, 146)
        Me.C1Button2.Name = "C1Button2"
        Me.C1Button2.Size = New System.Drawing.Size(53, 100)
        Me.C1Button2.TabIndex = 7
        Me.C1Button2.Text = "↓"
        Me.C1Button2.UseVisualStyleBackColor = True
        '
        'C1Button3
        '
        Me.C1Button3.Location = New System.Drawing.Point(809, 405)
        Me.C1Button3.Name = "C1Button3"
        Me.C1Button3.Size = New System.Drawing.Size(53, 100)
        Me.C1Button3.TabIndex = 9
        Me.C1Button3.Text = "↓"
        Me.C1Button3.UseVisualStyleBackColor = True
        '
        'C1Button4
        '
        Me.C1Button4.Location = New System.Drawing.Point(809, 286)
        Me.C1Button4.Name = "C1Button4"
        Me.C1Button4.Size = New System.Drawing.Size(53, 100)
        Me.C1Button4.TabIndex = 8
        Me.C1Button4.Text = "↑"
        Me.C1Button4.UseVisualStyleBackColor = True
        '
        'FormCuentas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SkyBlue
        Me.ClientSize = New System.Drawing.Size(874, 527)
        Me.Controls.Add(Me.C1Button3)
        Me.Controls.Add(Me.C1Button4)
        Me.Controls.Add(Me.C1Button2)
        Me.Controls.Add(Me.C1Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(890, 565)
        Me.MinimumSize = New System.Drawing.Size(890, 565)
        Me.Name = "FormCuentas"
        Me.Text = "FormCuentas"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FLPMisMesas As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLPMisCuentas As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents FLPMesasDisponibles As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents C1Button1 As C1.Win.C1Input.C1Button
    Friend WithEvents C1Button2 As C1.Win.C1Input.C1Button
    Friend WithEvents C1Button3 As C1.Win.C1Input.C1Button
    Friend WithEvents C1Button4 As C1.Win.C1Input.C1Button
End Class
