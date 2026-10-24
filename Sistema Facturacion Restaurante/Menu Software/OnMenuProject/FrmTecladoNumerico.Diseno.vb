Imports System.Drawing
Imports System.Windows.Forms

'///////////////////////// DISEÑO MODERNO DEL TECLADO NUMERICO //////////////////////
Partial Class FrmTecladoNumerico

    Dim tlpTeclado As TableLayoutPanel
    Dim Teclas As New List(Of Button)
    Dim FuenteTeclas As Font
    Dim FuenteSignos As Font

    Private Sub PonerTecla(ByVal Ctl As Button, ByVal Tipo As TipoTecla, ByVal Columna As Integer, ByVal Fila As Integer, ByVal Ancho As Integer)
        EstilizarTecla(Ctl, Tipo)
        tlpTeclado.Controls.Add(Ctl, Columna, Fila)
        tlpTeclado.SetColumnSpan(Ctl, Ancho)
        Teclas.Add(Ctl)
    End Sub

    Private Sub ConstruirDiseno()
        Dim Area As Rectangle = Screen.PrimaryScreen.WorkingArea
        Me.SuspendLayout()
        Me.BackColor = ColorTecladoBorde
        Me.Padding = New Padding(2)
        Me.ClientSize = New Size(Math.Min(420, Area.Width - 20), Math.Min(500, Area.Height - 20))

        Dim Fondo As New Panel
        Fondo.Dock = DockStyle.Fill
        Fondo.BackColor = ColorTecladoFondo
        Fondo.Padding = New Padding(12)

        Dim Raiz As New TableLayoutPanel
        Raiz.Dock = DockStyle.Fill
        Raiz.ColumnCount = 1
        Raiz.RowCount = 2
        Raiz.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        Raiz.RowStyles.Add(New RowStyle(SizeType.Absolute, 60.0!))
        Raiz.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

        EstilizarCaja(Me.TxtCadena)
        Me.TxtCadena.Font = New Font("Segoe UI", 20.0!)
        Me.TxtCadena.TextAlign = HorizontalAlignment.Right

        '--- cuadricula de 4 columnas x 4 filas
        tlpTeclado = New TableLayoutPanel
        tlpTeclado.Dock = DockStyle.Fill
        tlpTeclado.Margin = New Padding(0)
        tlpTeclado.ColumnCount = 4
        tlpTeclado.RowCount = 4
        For i As Integer = 1 To 4
            tlpTeclado.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, If(i = 4, 34.0!, 22.0!)))
            tlpTeclado.RowStyles.Add(New RowStyle(SizeType.Percent, 25.0!))
        Next

        PonerTecla(Me.Cmd7, TipoTecla.Normal, 0, 0, 1)
        PonerTecla(Me.Cmd8, TipoTecla.Normal, 1, 0, 1)
        PonerTecla(Me.Cmd9, TipoTecla.Normal, 2, 0, 1)
        PonerTecla(Me.CmdBorrar, TipoTecla.Especial, 3, 0, 1)
        Me.CmdBorrar.Text = "Borrar"
        PonerTecla(Me.Cmd4, TipoTecla.Normal, 0, 1, 1)
        PonerTecla(Me.Cmd5, TipoTecla.Normal, 1, 1, 1)
        PonerTecla(Me.Cmd6, TipoTecla.Normal, 2, 1, 1)
        PonerTecla(Me.CmdMas, TipoTecla.Especial, 3, 1, 1)
        PonerTecla(Me.Cmd1, TipoTecla.Normal, 0, 2, 1)
        PonerTecla(Me.Cmd2, TipoTecla.Normal, 1, 2, 1)
        PonerTecla(Me.Cmd3, TipoTecla.Normal, 2, 2, 1)
        PonerTecla(Me.CmdMenos, TipoTecla.Especial, 3, 2, 1)
        PonerTecla(Me.Cmd0, TipoTecla.Normal, 0, 3, 2)
        PonerTecla(Me.Button2, TipoTecla.Primaria, 2, 3, 2)
        Me.Button2.Text = "OK"
        Me.Button1.Visible = False 'logo decorativo sin funcion
        Me.AcceptButton = Me.Button2

        Raiz.Controls.Add(Me.TxtCadena, 0, 0)
        Raiz.Controls.Add(tlpTeclado, 0, 1)
        Fondo.Controls.Add(Raiz)
        Me.Controls.Add(Fondo)
        Fondo.BringToFront()
        Me.ResumeLayout(True)
        AjustarFuentes()
    End Sub

    Private Sub AjustarFuentes()
        If tlpTeclado Is Nothing Then Exit Sub
        AplicarFuenteTeclas(Teclas, tlpTeclado.Height \ 4, FuenteTeclas, Nothing)
        Dim Anterior As Font = FuenteSignos
        FuenteSignos = New Font(FuenteTeclas.FontFamily, FuenteTeclas.Size + 8.0!, FontStyle.Bold)
        Me.CmdMas.Font = FuenteSignos
        Me.CmdMenos.Font = FuenteSignos
        If Anterior IsNot Nothing Then Anterior.Dispose()
    End Sub

    Private Sub FrmTecladoNumerico_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
        AjustarFuentes()
    End Sub
End Class
