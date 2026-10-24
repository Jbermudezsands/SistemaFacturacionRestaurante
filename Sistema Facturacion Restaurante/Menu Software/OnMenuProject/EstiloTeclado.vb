Imports System.Drawing
Imports System.Windows.Forms

'///////////////////////// ESTILO COMUN DE LOS TECLADOS TACTILES //////////////////////
' Paleta de FormCuentas / Form1. Lo usan FrmTeclado y FrmTecladoNumerico.
Module EstiloTeclado
    Public ColorTecladoFondo As Color = Color.FromArgb(24, 41, 66)
    Public ColorTecladoBorde As Color = Color.FromArgb(52, 94, 152)
    Public ColorTecladoPrimario As Color = Color.FromArgb(52, 94, 152)
    Public ColorTecladoPrimarioHover As Color = Color.FromArgb(41, 78, 130)
    Public ColorTecladoPrimarioPresionado As Color = Color.FromArgb(30, 62, 110)
    Public ColorTecladoTexto As Color = Color.FromArgb(30, 41, 59)
    Public ColorTecladoFondoSuave As Color = Color.FromArgb(246, 248, 251)
    Public ColorTecladoHover As Color = Color.FromArgb(238, 241, 245)
    Public ColorTecladoPresionado As Color = Color.FromArgb(228, 232, 238)
    Public ColorTecladoBordeTecla As Color = Color.FromArgb(210, 216, 226)
    Public ColorTecladoEspecial As Color = Color.FromArgb(214, 228, 247)

    Public Enum TipoTecla
        Normal
        Especial
        Primaria
    End Enum

    Public Sub EstilizarTecla(ByVal btn As Button, ByVal Tipo As TipoTecla)
        Dim BotonC1 As C1.Win.C1Input.C1Button = TryCast(btn, C1.Win.C1Input.C1Button)
        If BotonC1 IsNot Nothing Then BotonC1.VisualStyle = C1.Win.C1Input.VisualStyle.System

        btn.Image = Nothing
        btn.Dock = DockStyle.Fill
        btn.Margin = New Padding(3)
        btn.FlatStyle = FlatStyle.Flat
        btn.UseVisualStyleBackColor = False
        btn.TextAlign = ContentAlignment.MiddleCenter
        btn.FlatAppearance.BorderSize = 1
        Select Case Tipo
            Case TipoTecla.Primaria
                btn.BackColor = ColorTecladoPrimario
                btn.ForeColor = Color.White
                btn.FlatAppearance.BorderColor = ColorTecladoPrimario
                btn.FlatAppearance.MouseOverBackColor = ColorTecladoPrimarioHover
                btn.FlatAppearance.MouseDownBackColor = ColorTecladoPrimarioPresionado
            Case TipoTecla.Especial
                btn.BackColor = ColorTecladoEspecial
                btn.ForeColor = ColorTecladoTexto
                btn.FlatAppearance.BorderColor = ColorTecladoBordeTecla
                btn.FlatAppearance.MouseOverBackColor = ColorTecladoHover
                btn.FlatAppearance.MouseDownBackColor = ColorTecladoPresionado
            Case Else
                btn.BackColor = Color.White
                btn.ForeColor = ColorTecladoTexto
                btn.FlatAppearance.BorderColor = ColorTecladoBordeTecla
                btn.FlatAppearance.MouseOverBackColor = ColorTecladoHover
                btn.FlatAppearance.MouseDownBackColor = ColorTecladoPresionado
        End Select
    End Sub

    Public Sub EstilizarCaja(ByVal txt As TextBox)
        txt.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txt.Margin = New Padding(0, 0, 0, 10)
        txt.BorderStyle = BorderStyle.FixedSingle
        txt.BackColor = Color.White
        txt.ForeColor = ColorTecladoTexto
    End Sub

    ' Tamaño de letra de las teclas segun el alto de cada fila (se recalcula al cambiar el tamaño).
    Public Sub AplicarFuenteTeclas(ByVal Teclas As IEnumerable(Of Button), ByVal AltoFila As Integer, ByRef Actual As Font, ByVal CajaTexto As TextBox)
        Dim Tam As Single = Math.Max(10.0!, Math.Min(22.0!, AltoFila / 4.0!))
        If Actual IsNot Nothing AndAlso Math.Abs(Actual.Size - Tam) < 0.5! Then Exit Sub
        Dim Anterior As Font = Actual
        Actual = New Font("Segoe UI Semibold", Tam, FontStyle.Bold)
        For Each Tecla As Button In Teclas
            Tecla.Font = Actual
        Next
        If CajaTexto IsNot Nothing Then CajaTexto.Font = New Font("Segoe UI", Math.Max(12.0!, Tam + 2.0!))
        If Anterior IsNot Nothing Then Anterior.Dispose()
    End Sub
End Module
