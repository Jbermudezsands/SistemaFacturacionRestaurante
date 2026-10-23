<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ArepComanda 
    Inherits DataDynamics.ActiveReports.ActiveReport3 

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
        End If
        MyBase.Dispose(disposing)
    End Sub
    
    'NOTE: The following procedure is required by the ActiveReports Designer
    'It can be modified using the ActiveReports Designer.
    'Do not modify it using the code editor.
    Private WithEvents PageHeader1 As DataDynamics.ActiveReports.PageHeader
    Private WithEvents Detail1 As DataDynamics.ActiveReports.Detail
    Private WithEvents PageFooter1 As DataDynamics.ActiveReports.PageFooter
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ArepComanda))
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.Label6 = New DataDynamics.ActiveReports.Label
        Me.LblTipoCompra = New DataDynamics.ActiveReports.Label
        Me.lblfactura = New DataDynamics.ActiveReports.Label
        Me.lblOrderDate = New DataDynamics.ActiveReports.Label
        Me.LblFecha = New DataDynamics.ActiveReports.Label
        Me.Label1 = New DataDynamics.ActiveReports.Label
        Me.LblMesa = New DataDynamics.ActiveReports.Label
        Me.Label5 = New DataDynamics.ActiveReports.Label
        Me.LblMesero = New DataDynamics.ActiveReports.TextBox
        Me.Label18 = New DataDynamics.ActiveReports.Label
        Me.Label23 = New DataDynamics.ActiveReports.Label
        Me.Label15 = New DataDynamics.ActiveReports.Label
        Me.txtCodigo = New DataDynamics.ActiveReports.TextBox
        Me.txtDescripcion = New DataDynamics.ActiveReports.TextBox
        Me.txtCantidad = New DataDynamics.ActiveReports.TextBox
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTipoCompra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfactura, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblFecha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblMesa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblMesero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader1
        '
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Label6, Me.LblTipoCompra, Me.lblfactura, Me.lblOrderDate, Me.LblFecha, Me.Label1, Me.LblMesa, Me.Label5, Me.LblMesero, Me.Label18, Me.Label23, Me.Label15})
        Me.PageHeader1.Height = 1.208333!
        Me.PageHeader1.Name = "PageHeader1"
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtCodigo, Me.txtDescripcion, Me.txtCantidad})
        Me.Detail1.Height = 0.1979167!
        Me.Detail1.Name = "Detail1"
        '
        'PageFooter1
        '
        Me.PageFooter1.Height = 0.0!
        Me.PageFooter1.Name = "PageFooter1"
        '
        'Label6
        '
        Me.Label6.Border.BottomColor = System.Drawing.Color.Black
        Me.Label6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Border.LeftColor = System.Drawing.Color.Black
        Me.Label6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Border.RightColor = System.Drawing.Color.Black
        Me.Label6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Border.TopColor = System.Drawing.Color.Black
        Me.Label6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Height = 0.1875!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 0.0625!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "ddo-char-set: 0; text-align: center; font-weight: bold; font-size: 12pt; font-fam" & _
            "ily: Microsoft Sans Serif; "
        Me.Label6.Text = "Comanda"
        Me.Label6.Top = 0.0625!
        Me.Label6.Width = 2.375!
        '
        'LblTipoCompra
        '
        Me.LblTipoCompra.Border.BottomColor = System.Drawing.Color.Black
        Me.LblTipoCompra.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTipoCompra.Border.LeftColor = System.Drawing.Color.Black
        Me.LblTipoCompra.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTipoCompra.Border.RightColor = System.Drawing.Color.Black
        Me.LblTipoCompra.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTipoCompra.Border.TopColor = System.Drawing.Color.Black
        Me.LblTipoCompra.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTipoCompra.Height = 0.19!
        Me.LblTipoCompra.HyperLink = Nothing
        Me.LblTipoCompra.Left = 0.0!
        Me.LblTipoCompra.Name = "LblTipoCompra"
        Me.LblTipoCompra.Style = "color: Black; ddo-char-set: 0; text-align: left; font-weight: bold; font-size: 8." & _
            "25pt; font-family: Microsoft Sans Serif; "
        Me.LblTipoCompra.Text = "Factura:"
        Me.LblTipoCompra.Top = 0.3125!
        Me.LblTipoCompra.Width = 0.5263157!
        '
        'lblfactura
        '
        Me.lblfactura.Border.BottomColor = System.Drawing.Color.Black
        Me.lblfactura.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblfactura.Border.LeftColor = System.Drawing.Color.Black
        Me.lblfactura.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblfactura.Border.RightColor = System.Drawing.Color.Black
        Me.lblfactura.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblfactura.Border.TopColor = System.Drawing.Color.Black
        Me.lblfactura.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblfactura.Height = 0.19!
        Me.lblfactura.HyperLink = Nothing
        Me.lblfactura.Left = 0.5!
        Me.lblfactura.Name = "lblfactura"
        Me.lblfactura.Style = "ddo-char-set: 0; font-size: 8.25pt; font-family: Microsoft Sans Serif; "
        Me.lblfactura.Text = ""
        Me.lblfactura.Top = 0.3125!
        Me.lblfactura.Width = 0.6842105!
        '
        'lblOrderDate
        '
        Me.lblOrderDate.Border.BottomColor = System.Drawing.Color.Black
        Me.lblOrderDate.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderDate.Border.LeftColor = System.Drawing.Color.Black
        Me.lblOrderDate.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderDate.Border.RightColor = System.Drawing.Color.Black
        Me.lblOrderDate.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderDate.Border.TopColor = System.Drawing.Color.Black
        Me.lblOrderDate.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderDate.Height = 0.19!
        Me.lblOrderDate.HyperLink = Nothing
        Me.lblOrderDate.Left = 1.1875!
        Me.lblOrderDate.Name = "lblOrderDate"
        Me.lblOrderDate.Style = "ddo-char-set: 0; text-align: left; font-weight: bold; font-size: 8.25pt; font-fam" & _
            "ily: Microsoft Sans Serif; "
        Me.lblOrderDate.Text = "Fecha:"
        Me.lblOrderDate.Top = 0.3125!
        Me.lblOrderDate.Width = 0.4736841!
        '
        'LblFecha
        '
        Me.LblFecha.Border.BottomColor = System.Drawing.Color.Black
        Me.LblFecha.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblFecha.Border.LeftColor = System.Drawing.Color.Black
        Me.LblFecha.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblFecha.Border.RightColor = System.Drawing.Color.Black
        Me.LblFecha.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblFecha.Border.TopColor = System.Drawing.Color.Black
        Me.LblFecha.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblFecha.Height = 0.1875!
        Me.LblFecha.HyperLink = Nothing
        Me.LblFecha.Left = 1.6875!
        Me.LblFecha.Name = "LblFecha"
        Me.LblFecha.Style = "ddo-char-set: 0; font-size: 8.25pt; font-family: Microsoft Sans Serif; "
        Me.LblFecha.Text = ""
        Me.LblFecha.Top = 0.3125!
        Me.LblFecha.Width = 0.75!
        '
        'Label1
        '
        Me.Label1.Border.BottomColor = System.Drawing.Color.Black
        Me.Label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Border.LeftColor = System.Drawing.Color.Black
        Me.Label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Border.RightColor = System.Drawing.Color.Black
        Me.Label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Border.TopColor = System.Drawing.Color.Black
        Me.Label1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Height = 0.19!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 0.0!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = "color: Black; ddo-char-set: 0; text-align: left; font-weight: bold; font-size: 8." & _
            "25pt; font-family: Microsoft Sans Serif; "
        Me.Label1.Text = "Mesa:"
        Me.Label1.Top = 0.5!
        Me.Label1.Width = 0.5263157!
        '
        'LblMesa
        '
        Me.LblMesa.Border.BottomColor = System.Drawing.Color.Black
        Me.LblMesa.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblMesa.Border.LeftColor = System.Drawing.Color.Black
        Me.LblMesa.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblMesa.Border.RightColor = System.Drawing.Color.Black
        Me.LblMesa.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblMesa.Border.TopColor = System.Drawing.Color.Black
        Me.LblMesa.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblMesa.Height = 0.19!
        Me.LblMesa.HyperLink = Nothing
        Me.LblMesa.Left = 0.5625!
        Me.LblMesa.Name = "LblMesa"
        Me.LblMesa.Style = "ddo-char-set: 0; font-size: 8.25pt; font-family: Microsoft Sans Serif; "
        Me.LblMesa.Text = ""
        Me.LblMesa.Top = 0.5!
        Me.LblMesa.Width = 1.875!
        '
        'Label5
        '
        Me.Label5.Border.BottomColor = System.Drawing.Color.Black
        Me.Label5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Border.LeftColor = System.Drawing.Color.Black
        Me.Label5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Border.RightColor = System.Drawing.Color.Black
        Me.Label5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Border.TopColor = System.Drawing.Color.Black
        Me.Label5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Height = 0.19!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 0.0!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "color: Black; ddo-char-set: 0; text-align: left; font-weight: bold; font-size: 8." & _
            "25pt; font-family: Microsoft Sans Serif; "
        Me.Label5.Text = "Mesero:"
        Me.Label5.Top = 0.6875!
        Me.Label5.Width = 0.6315789!
        '
        'LblMesero
        '
        Me.LblMesero.Border.BottomColor = System.Drawing.Color.Black
        Me.LblMesero.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblMesero.Border.LeftColor = System.Drawing.Color.Black
        Me.LblMesero.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblMesero.Border.RightColor = System.Drawing.Color.Black
        Me.LblMesero.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblMesero.Border.TopColor = System.Drawing.Color.Black
        Me.LblMesero.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblMesero.Height = 0.1875!
        Me.LblMesero.Left = 0.625!
        Me.LblMesero.Name = "LblMesero"
        Me.LblMesero.Style = "ddo-char-set: 0; font-size: 8.25pt; font-family: Microsoft Sans Serif; "
        Me.LblMesero.Text = Nothing
        Me.LblMesero.Top = 0.6875!
        Me.LblMesero.Width = 1.8125!
        '
        'Label18
        '
        Me.Label18.Border.BottomColor = System.Drawing.Color.Black
        Me.Label18.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label18.Border.LeftColor = System.Drawing.Color.Black
        Me.Label18.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label18.Border.RightColor = System.Drawing.Color.Black
        Me.Label18.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label18.Border.TopColor = System.Drawing.Color.Black
        Me.Label18.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label18.Height = 0.1875!
        Me.Label18.HyperLink = Nothing
        Me.Label18.Left = 0.625!
        Me.Label18.Name = "Label18"
        Me.Label18.Style = "color: #000040; ddo-char-set: 0; text-align: center; font-weight: normal; backgro" & _
            "und-color: White; font-size: 8.25pt; font-family: Microsoft Sans Serif; "
        Me.Label18.Text = "Descripcion"
        Me.Label18.Top = 1.0!
        Me.Label18.Width = 1.25!
        '
        'Label23
        '
        Me.Label23.Border.BottomColor = System.Drawing.Color.Black
        Me.Label23.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label23.Border.LeftColor = System.Drawing.Color.Black
        Me.Label23.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label23.Border.RightColor = System.Drawing.Color.Black
        Me.Label23.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label23.Border.TopColor = System.Drawing.Color.Black
        Me.Label23.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label23.Height = 0.1875!
        Me.Label23.HyperLink = Nothing
        Me.Label23.Left = 1.875!
        Me.Label23.Name = "Label23"
        Me.Label23.Style = "color: #000040; ddo-char-set: 0; text-align: center; font-weight: normal; backgro" & _
            "und-color: White; font-size: 8.25pt; font-family: Microsoft Sans Serif; "
        Me.Label23.Text = "Cantidad"
        Me.Label23.Top = 1.0!
        Me.Label23.Width = 0.5625!
        '
        'Label15
        '
        Me.Label15.Border.BottomColor = System.Drawing.Color.Black
        Me.Label15.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label15.Border.LeftColor = System.Drawing.Color.Black
        Me.Label15.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label15.Border.RightColor = System.Drawing.Color.Black
        Me.Label15.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label15.Border.TopColor = System.Drawing.Color.Black
        Me.Label15.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
        Me.Label15.Height = 0.1875!
        Me.Label15.HyperLink = Nothing
        Me.Label15.Left = 0.0!
        Me.Label15.Name = "Label15"
        Me.Label15.Style = "color: #000040; ddo-char-set: 0; text-align: center; font-weight: normal; backgro" & _
            "und-color: White; font-size: 8.25pt; font-family: Microsoft Sans Serif; "
        Me.Label15.Text = "Codigo"
        Me.Label15.Top = 1.0!
        Me.Label15.Width = 0.625!
        '
        'txtCodigo
        '
        Me.txtCodigo.Border.BottomColor = System.Drawing.Color.Black
        Me.txtCodigo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCodigo.Border.LeftColor = System.Drawing.Color.Black
        Me.txtCodigo.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCodigo.Border.RightColor = System.Drawing.Color.Black
        Me.txtCodigo.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCodigo.Border.TopColor = System.Drawing.Color.Black
        Me.txtCodigo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCodigo.DataField = "Codigo"
        Me.txtCodigo.Height = 0.1875!
        Me.txtCodigo.Left = 0.0!
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Style = "text-align: center; font-size: 8.25pt; font-family: Microsoft Sans Serif; vertica" & _
            "l-align: middle; "
        Me.txtCodigo.Top = 0.0!
        Me.txtCodigo.Width = 0.625!
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Border.BottomColor = System.Drawing.Color.Black
        Me.txtDescripcion.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDescripcion.Border.LeftColor = System.Drawing.Color.Black
        Me.txtDescripcion.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDescripcion.Border.RightColor = System.Drawing.Color.Black
        Me.txtDescripcion.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDescripcion.Border.TopColor = System.Drawing.Color.Black
        Me.txtDescripcion.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDescripcion.DataField = "Leyenda"
        Me.txtDescripcion.Height = 0.1875!
        Me.txtDescripcion.Left = 0.625!
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Style = "text-align: center; font-size: 8.25pt; font-family: Microsoft Sans Serif; vertica" & _
            "l-align: middle; "
        Me.txtDescripcion.Top = 0.0!
        Me.txtDescripcion.Width = 1.25!
        '
        'txtCantidad
        '
        Me.txtCantidad.Border.BottomColor = System.Drawing.Color.Black
        Me.txtCantidad.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCantidad.Border.LeftColor = System.Drawing.Color.Black
        Me.txtCantidad.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCantidad.Border.RightColor = System.Drawing.Color.Black
        Me.txtCantidad.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCantidad.Border.TopColor = System.Drawing.Color.Black
        Me.txtCantidad.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCantidad.DataField = "Cantidad"
        Me.txtCantidad.Height = 0.1875!
        Me.txtCantidad.Left = 1.875!
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Style = "text-align: center; font-size: 8.25pt; font-family: Microsoft Sans Serif; vertica" & _
            "l-align: middle; "
        Me.txtCantidad.Top = 0.0!
        Me.txtCantidad.Width = 0.5625!
        '
        'ArepComanda
        '
        Me.MasterReport = False
        Me.PageSettings.Margins.Bottom = 0.0!
        Me.PageSettings.Margins.Left = 0.01!
        Me.PageSettings.Margins.Right = 0.0!
        Me.PageSettings.Margins.Top = 0.0!
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 2.46875!
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.Detail1)
        Me.Sections.Add(Me.PageFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                    "l; font-size: 10pt; color: Black; ", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                    "lic; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"))
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTipoCompra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfactura, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblFecha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblMesa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblMesero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Label6 As DataDynamics.ActiveReports.Label
    Friend WithEvents LblTipoCompra As DataDynamics.ActiveReports.Label
    Friend WithEvents lblfactura As DataDynamics.ActiveReports.Label
    Private WithEvents lblOrderDate As DataDynamics.ActiveReports.Label
    Friend WithEvents LblFecha As DataDynamics.ActiveReports.Label
    Friend WithEvents Label1 As DataDynamics.ActiveReports.Label
    Friend WithEvents LblMesa As DataDynamics.ActiveReports.Label
    Friend WithEvents Label5 As DataDynamics.ActiveReports.Label
    Friend WithEvents LblMesero As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Label18 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label23 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label15 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtCodigo As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtDescripcion As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtCantidad As DataDynamics.ActiveReports.TextBox
End Class 
