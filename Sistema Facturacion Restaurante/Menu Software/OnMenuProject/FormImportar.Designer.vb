<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormImportar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormImportar))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtRuta = New C1.Win.C1Input.C1TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboTipo = New System.Windows.Forms.ComboBox
        Me.btnBuscarArchivo = New C1.Win.C1Input.C1Button
        Me.gridImportacion = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.btnImportar = New C1.Win.C1Input.C1Button
        Me.pbImportacion = New System.Windows.Forms.ProgressBar
        Me.lblRegistro = New System.Windows.Forms.Label
        Me.cboBodega = New System.Windows.Forms.ComboBox
        Me.lblBodega = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboSucursal = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRuta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridImportacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.PaleGreen
        Me.PictureBox1.Location = New System.Drawing.Point(-34, -27)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(868, 178)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.PaleGreen
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(12, 6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.PaleGreen
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Light", 14.0!)
        Me.Label1.Location = New System.Drawing.Point(82, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(239, 50)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Herramienta de importación" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " de archivos (*.csv)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tipo:"
        '
        'txtRuta
        '
        Me.txtRuta.AutoSize = False
        Me.txtRuta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRuta.Location = New System.Drawing.Point(63, 52)
        Me.txtRuta.Multiline = True
        Me.txtRuta.Name = "txtRuta"
        Me.txtRuta.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRuta.Size = New System.Drawing.Size(235, 41)
        Me.txtRuta.TabIndex = 4
        Me.txtRuta.Tag = Nothing
        Me.txtRuta.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtRuta.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 17)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Ruta:"
        '
        'cboTipo
        '
        Me.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipo.FormattingEnabled = True
        Me.cboTipo.Items.AddRange(New Object() {"Productos", "Proveedores", "Clientes"})
        Me.cboTipo.Location = New System.Drawing.Point(63, 21)
        Me.cboTipo.Name = "cboTipo"
        Me.cboTipo.Size = New System.Drawing.Size(116, 25)
        Me.cboTipo.TabIndex = 6
        '
        'btnBuscarArchivo
        '
        Me.btnBuscarArchivo.Image = CType(resources.GetObject("btnBuscarArchivo.Image"), System.Drawing.Image)
        Me.btnBuscarArchivo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBuscarArchivo.Location = New System.Drawing.Point(185, 21)
        Me.btnBuscarArchivo.Name = "btnBuscarArchivo"
        Me.btnBuscarArchivo.Size = New System.Drawing.Size(113, 25)
        Me.btnBuscarArchivo.TabIndex = 7
        Me.btnBuscarArchivo.Text = "Buscar archivo"
        Me.btnBuscarArchivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBuscarArchivo.UseVisualStyleBackColor = True
        '
        'gridImportacion
        '
        Me.gridImportacion.AllowUpdate = False
        Me.gridImportacion.AlternatingRows = True
        Me.gridImportacion.Caption = "Productos"
        Me.gridImportacion.CaptionHeight = 17
        Me.gridImportacion.GroupByCaption = "Drag a column header here to group by that column"
        Me.gridImportacion.Images.Add(CType(resources.GetObject("gridImportacion.Images"), System.Drawing.Image))
        Me.gridImportacion.Location = New System.Drawing.Point(3, 154)
        Me.gridImportacion.Name = "gridImportacion"
        Me.gridImportacion.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.gridImportacion.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.gridImportacion.PreviewInfo.ZoomFactor = 75
        Me.gridImportacion.PrintInfo.PageSettings = CType(resources.GetObject("gridImportacion.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.gridImportacion.RowHeight = 15
        Me.gridImportacion.Size = New System.Drawing.Size(806, 297)
        Me.gridImportacion.TabIndex = 8
        Me.gridImportacion.Text = "C1TrueDBGrid1"
        Me.gridImportacion.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.gridImportacion.PropBag = resources.GetString("gridImportacion.PropBag")
        '
        'btnImportar
        '
        Me.btnImportar.Location = New System.Drawing.Point(12, 85)
        Me.btnImportar.Name = "btnImportar"
        Me.btnImportar.Size = New System.Drawing.Size(108, 45)
        Me.btnImportar.TabIndex = 9
        Me.btnImportar.Text = "Importar"
        Me.btnImportar.UseVisualStyleBackColor = True
        Me.btnImportar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnImportar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'pbImportacion
        '
        Me.pbImportacion.Location = New System.Drawing.Point(126, 85)
        Me.pbImportacion.Name = "pbImportacion"
        Me.pbImportacion.Size = New System.Drawing.Size(318, 28)
        Me.pbImportacion.TabIndex = 10
        '
        'lblRegistro
        '
        Me.lblRegistro.BackColor = System.Drawing.Color.PaleGreen
        Me.lblRegistro.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistro.Location = New System.Drawing.Point(126, 113)
        Me.lblRegistro.Name = "lblRegistro"
        Me.lblRegistro.Size = New System.Drawing.Size(318, 29)
        Me.lblRegistro.TabIndex = 11
        '
        'cboBodega
        '
        Me.cboBodega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBodega.FormattingEnabled = True
        Me.cboBodega.Items.AddRange(New Object() {"Productos", "Proveedores", "Clientes"})
        Me.cboBodega.Location = New System.Drawing.Point(63, 99)
        Me.cboBodega.Name = "cboBodega"
        Me.cboBodega.Size = New System.Drawing.Size(116, 25)
        Me.cboBodega.TabIndex = 13
        '
        'lblBodega
        '
        Me.lblBodega.AutoSize = True
        Me.lblBodega.Location = New System.Drawing.Point(5, 102)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(52, 17)
        Me.lblBodega.TabIndex = 12
        Me.lblBodega.Text = "Bodega:"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.LightGreen
        Me.GroupBox1.Controls.Add(Me.cboSucursal)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cboBodega)
        Me.GroupBox1.Controls.Add(Me.lblBodega)
        Me.GroupBox1.Controls.Add(Me.cboTipo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnBuscarArchivo)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtRuta)
        Me.GroupBox1.Location = New System.Drawing.Point(450, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(351, 136)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Criterios:"
        '
        'cboSucursal
        '
        Me.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSucursal.FormattingEnabled = True
        Me.cboSucursal.Items.AddRange(New Object() {"Productos", "Proveedores", "Clientes"})
        Me.cboSucursal.Location = New System.Drawing.Point(221, 99)
        Me.cboSucursal.Name = "cboSucursal"
        Me.cboSucursal.Size = New System.Drawing.Size(116, 25)
        Me.cboSucursal.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(185, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 17)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Suc:"
        '
        'FormImportar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 454)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblRegistro)
        Me.Controls.Add(Me.pbImportacion)
        Me.Controls.Add(Me.btnImportar)
        Me.Controls.Add(Me.gridImportacion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "FormImportar"
        Me.Text = "FormImportar"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRuta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridImportacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRuta As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboTipo As System.Windows.Forms.ComboBox
    Friend WithEvents btnBuscarArchivo As C1.Win.C1Input.C1Button
    Friend WithEvents gridImportacion As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btnImportar As C1.Win.C1Input.C1Button
    Friend WithEvents pbImportacion As System.Windows.Forms.ProgressBar
    Friend WithEvents lblRegistro As System.Windows.Forms.Label
    Friend WithEvents cboBodega As System.Windows.Forms.ComboBox
    Friend WithEvents lblBodega As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
