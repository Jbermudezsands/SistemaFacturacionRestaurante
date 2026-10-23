<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConsulta
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConsulta))
        Me.GridCuentas = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        CType(Me.GridCuentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridCuentas
        '
        Me.GridCuentas.AllowUpdate = False
        Me.GridCuentas.Caption = "Mesa 01"
        Me.GridCuentas.CaptionHeight = 17
        Me.GridCuentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridCuentas.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.GridCuentas.GroupByCaption = "Drag a column header here to group by that column"
        Me.GridCuentas.Images.Add(CType(resources.GetObject("GridCuentas.Images"), System.Drawing.Image))
        Me.GridCuentas.Location = New System.Drawing.Point(0, 0)
        Me.GridCuentas.Name = "GridCuentas"
        Me.GridCuentas.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.GridCuentas.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.GridCuentas.PreviewInfo.ZoomFactor = 75
        Me.GridCuentas.PrintInfo.PageSettings = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.GridCuentas.RowHeight = 15
        Me.GridCuentas.Size = New System.Drawing.Size(317, 261)
        Me.GridCuentas.TabIndex = 0
        Me.GridCuentas.Text = "C1TrueDBGrid1"
        Me.GridCuentas.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.GridCuentas.PropBag = resources.GetString("GridCuentas.PropBag")
        '
        'frmConsulta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(317, 261)
        Me.Controls.Add(Me.GridCuentas)
        Me.Name = "frmConsulta"
        Me.Text = "frmConsulta"
        CType(Me.GridCuentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridCuentas As C1.Win.C1TrueDBGrid.C1TrueDBGrid
End Class
