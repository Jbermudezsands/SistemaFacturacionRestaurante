<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ArepEnsambleDia
    Inherits DataDynamics.ActiveReports.ActiveReport3

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Reporte armado en codigo: encabezado de pagina, un grupo por producto ensamble (cantidad a ensamblar)
    'y un renglon de detalle por componente (cantidad por unidad y cantidad a usar).
    Private WithEvents ReportHeader1 As DataDynamics.ActiveReports.ReportHeader
    Private WithEvents PageHeader1 As DataDynamics.ActiveReports.PageHeader
    Private WithEvents GroupHeader1 As DataDynamics.ActiveReports.GroupHeader
    Private WithEvents Detail1 As DataDynamics.ActiveReports.Detail
    Private WithEvents GroupFooter1 As DataDynamics.ActiveReports.GroupFooter
    Private WithEvents ReportFooter1 As DataDynamics.ActiveReports.ReportFooter
    Private WithEvents PageFooter1 As DataDynamics.ActiveReports.PageFooter

    Friend WithEvents LblTitulo As DataDynamics.ActiveReports.Label
    Friend WithEvents LblSubtitulo As DataDynamics.ActiveReports.Label
    Friend WithEvents LblFecha As DataDynamics.ActiveReports.Label
    Friend WithEvents LblBodegas As DataDynamics.ActiveReports.Label
    Friend WithEvents LblPagina As DataDynamics.ActiveReports.Label
    Friend WithEvents TxtCodProducto As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TxtDescProducto As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TxtEnsamblar As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TxtCodComponente As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TxtDescComponente As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TxtUnidad As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TxtUsar As DataDynamics.ActiveReports.TextBox

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ReportHeader1 = New DataDynamics.ActiveReports.ReportHeader
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.GroupHeader1 = New DataDynamics.ActiveReports.GroupHeader
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.GroupFooter1 = New DataDynamics.ActiveReports.GroupFooter
        Me.ReportFooter1 = New DataDynamics.ActiveReports.ReportFooter
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter

        Me.LblTitulo = CrearLabel("LblTitulo", "", 0.0!, 0.0!, 7.5!, 0.25!, "text-align: center; font-weight: bold; font-size: 14pt; ")
        Me.LblSubtitulo = CrearLabel("LblSubtitulo", "ENSAMBLE DEL DIA", 0.0!, 0.27!, 7.5!, 0.22!, "text-align: center; font-weight: bold; font-size: 11pt; ")
        Me.LblFecha = CrearLabel("LblFecha", "", 0.0!, 0.54!, 3.0!, 0.2!, "font-size: 9pt; ")
        Me.LblBodegas = CrearLabel("LblBodegas", "", 3.0!, 0.54!, 4.5!, 0.2!, "text-align: right; font-size: 9pt; ")
        Dim lineaEncabezado As New DataDynamics.ActiveReports.Line
        lineaEncabezado.Name = "LineaEncabezado"
        lineaEncabezado.X1 = 0.0!
        lineaEncabezado.X2 = 7.5!
        lineaEncabezado.Y1 = 0.8!
        lineaEncabezado.Y2 = 0.8!
        Me.ReportHeader1.Height = 0.0!
        Me.ReportHeader1.Name = "ReportHeader1"
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.LblTitulo, Me.LblSubtitulo, Me.LblFecha, Me.LblBodegas, lineaEncabezado})
        Me.PageHeader1.Height = 0.85!
        Me.PageHeader1.Name = "PageHeader1"

        'Un grupo por producto ensamble
        Me.TxtCodProducto = CrearTextBox("TxtCodProducto", "Cod_Producto", "", 0.0!, 0.02!, 0.9!, 0.2!, "font-weight: bold; font-size: 9.5pt; background-color: #DCE6F1; ")
        Me.TxtDescProducto = CrearTextBox("TxtDescProducto", "Descripcion_Producto", "", 0.9!, 0.02!, 3.7!, 0.2!, "font-weight: bold; font-size: 9.5pt; background-color: #DCE6F1; ")
        Dim lblEnsamblar As DataDynamics.ActiveReports.Label = CrearLabel("LblEnsamblar", "A ensamblar:", 4.6!, 0.02!, 1.3!, 0.2!, "text-align: right; font-weight: bold; font-size: 9.5pt; background-color: #DCE6F1; ")
        Me.TxtEnsamblar = CrearTextBox("TxtEnsamblar", "Cantidad_Ensamblar", "#,##0.####", 5.9!, 0.02!, 1.6!, 0.2!, "text-align: right; font-weight: bold; font-size: 9.5pt; background-color: #DCE6F1; ")
        Dim lblCodigo As DataDynamics.ActiveReports.Label = CrearLabel("LblCodigo", "Componente", 0.2!, 0.26!, 0.9!, 0.18!, "font-weight: bold; font-size: 8.5pt; ")
        Dim lblDescripcion As DataDynamics.ActiveReports.Label = CrearLabel("LblDescripcion", "Descripcion", 1.1!, 0.26!, 3.7!, 0.18!, "font-weight: bold; font-size: 8.5pt; ")
        Dim lblUnidad As DataDynamics.ActiveReports.Label = CrearLabel("LblUnidad", "Por unidad", 4.8!, 0.26!, 1.1!, 0.18!, "text-align: right; font-weight: bold; font-size: 8.5pt; ")
        Dim lblUsar As DataDynamics.ActiveReports.Label = CrearLabel("LblUsar", "Cantidad a usar", 5.9!, 0.26!, 1.6!, 0.18!, "text-align: right; font-weight: bold; font-size: 8.5pt; ")
        Me.GroupHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.TxtCodProducto, Me.TxtDescProducto, lblEnsamblar, Me.TxtEnsamblar, lblCodigo, lblDescripcion, lblUnidad, lblUsar})
        Me.GroupHeader1.DataField = "Cod_Producto"
        Me.GroupHeader1.Height = 0.48!
        Me.GroupHeader1.Name = "GroupHeader1"
        Me.GroupHeader1.KeepTogether = True

        'Un renglon por componente
        Me.TxtCodComponente = CrearTextBox("TxtCodComponente", "Cod_Componente", "", 0.2!, 0.0!, 0.9!, 0.2!, "font-size: 9pt; ")
        Me.TxtDescComponente = CrearTextBox("TxtDescComponente", "Descripcion_Componente", "", 1.1!, 0.0!, 3.7!, 0.2!, "font-size: 9pt; ")
        Me.TxtUnidad = CrearTextBox("TxtUnidad", "Cantidad_Unidad", "#,##0.####", 4.8!, 0.0!, 1.1!, 0.2!, "text-align: right; font-size: 9pt; ")
        Me.TxtUsar = CrearTextBox("TxtUsar", "Cantidad_Usar", "#,##0.####", 5.9!, 0.0!, 1.6!, 0.2!, "text-align: right; font-weight: bold; font-size: 9pt; ")
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.TxtCodComponente, Me.TxtDescComponente, Me.TxtUnidad, Me.TxtUsar})
        Me.Detail1.Height = 0.2!
        Me.Detail1.Name = "Detail1"

        Me.GroupFooter1.Height = 0.1!
        Me.GroupFooter1.Name = "GroupFooter1"

        'Firmas al final del reporte
        Dim lblPreparado As DataDynamics.ActiveReports.Label = CrearLabel("LblPreparado", "Preparado por: ______________________________", 0.0!, 0.5!, 3.6!, 0.2!, "font-size: 9pt; ")
        Dim lblAutorizado As DataDynamics.ActiveReports.Label = CrearLabel("LblAutorizado", "Autorizado por: ______________________________", 3.9!, 0.5!, 3.6!, 0.2!, "font-size: 9pt; ")
        Me.ReportFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {lblPreparado, lblAutorizado})
        Me.ReportFooter1.Height = 0.8!
        Me.ReportFooter1.Name = "ReportFooter1"

        Me.LblPagina = CrearLabel("LblPagina", "", 5.5!, 0.02!, 2.0!, 0.2!, "text-align: right; font-size: 8.5pt; ")
        Me.PageFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.LblPagina})
        Me.PageFooter1.Height = 0.25!
        Me.PageFooter1.Name = "PageFooter1"

        Me.MasterReport = False
        Me.PageSettings.Margins.Bottom = 0.5!
        Me.PageSettings.Margins.Left = 0.5!
        Me.PageSettings.Margins.Right = 0.5!
        Me.PageSettings.Margins.Top = 0.5!
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 7.5!
        Me.Sections.Add(Me.ReportHeader1)
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.GroupHeader1)
        Me.Sections.Add(Me.Detail1)
        Me.Sections.Add(Me.GroupFooter1)
        Me.Sections.Add(Me.PageFooter1)
        Me.Sections.Add(Me.ReportFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                    "l; font-size: 10pt; color: Black; ", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                    "lic; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"))
    End Sub

    Private Shared Function CrearLabel(ByVal nombre As String, ByVal texto As String, ByVal izquierda As Single, ByVal arriba As Single, _
                                       ByVal ancho As Single, ByVal alto As Single, ByVal estilo As String) As DataDynamics.ActiveReports.Label
        Dim etiqueta As New DataDynamics.ActiveReports.Label
        etiqueta.Name = nombre
        etiqueta.Text = texto
        etiqueta.Left = izquierda
        etiqueta.Top = arriba
        etiqueta.Width = ancho
        etiqueta.Height = alto
        etiqueta.Style = estilo
        etiqueta.HyperLink = Nothing
        Return etiqueta
    End Function

    Private Shared Function CrearTextBox(ByVal nombre As String, ByVal campo As String, ByVal formato As String, ByVal izquierda As Single, _
                                         ByVal arriba As Single, ByVal ancho As Single, ByVal alto As Single, ByVal estilo As String) As DataDynamics.ActiveReports.TextBox
        Dim caja As New DataDynamics.ActiveReports.TextBox
        caja.Name = nombre
        caja.DataField = campo
        If formato <> "" Then caja.OutputFormat = formato
        caja.Left = izquierda
        caja.Top = arriba
        caja.Width = ancho
        caja.Height = alto
        caja.Style = estilo
        caja.Text = Nothing
        Return caja
    End Function

End Class
