Imports System.Drawing
Imports System.Windows.Forms

'///////////////////////// DISEÑO MODERNO DEL TECLADO ALFANUMERICO //////////////////////
' Los controles siguen declarados en FrmTeclado.Designer.vb (Funciones.vb los usa por nombre);
' aqui se acomodan en una cuadricula que se estira con el tamaño del formulario.
Partial Class FrmTeclado

    Dim tlpTeclado As TableLayoutPanel
    Dim pnlModoMayus, pnlModoNumeros As Panel
    Dim Teclas As New List(Of Button)
    Dim FuenteTeclas As Font

    Private Sub PonerTecla(ByVal Ctl As Control, ByVal Columna As Integer, ByVal Fila As Integer, ByVal Ancho As Integer)
        tlpTeclado.Controls.Add(Ctl, Columna, Fila)
        tlpTeclado.SetColumnSpan(Ctl, Ancho)
    End Sub

    Private Sub ConstruirDiseno()
        Dim Area As Rectangle = Screen.PrimaryScreen.WorkingArea
        Me.SuspendLayout()
        Me.BackColor = ColorTecladoBorde
        Me.Padding = New Padding(2)
        Me.ClientSize = New Size(Math.Min(920, Area.Width - 20), Math.Min(440, Area.Height - 20))

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
        Me.TxtCadena.Font = New Font("Segoe UI", 18.0!)

        '--- cuadricula de 20 columnas (cada tecla ocupa 2) x 4 filas
        tlpTeclado = New TableLayoutPanel
        tlpTeclado.Dock = DockStyle.Fill
        tlpTeclado.Margin = New Padding(0)
        tlpTeclado.ColumnCount = 20
        tlpTeclado.RowCount = 4
        For i As Integer = 1 To 20
            tlpTeclado.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 5.0!))
        Next
        For i As Integer = 1 To 4
            tlpTeclado.RowStyles.Add(New RowStyle(SizeType.Percent, 25.0!))
        Next

        Dim Fila1() As Button = {Me.CmdQ, Me.CmdW, Me.CmdE, Me.CmdR, Me.CmdT, Me.CmdY, Me.CmdU, Me.CmdI, Me.CmdO, Me.CmdP}
        Dim Fila2() As Button = {Me.CmdA, Me.CmdS, Me.CmdD, Me.CmdF, Me.CmdG, Me.CmdH, Me.CmdJ, Me.CmdK, Me.CmdL, Me.CmdÑ}
        Dim Fila3() As Button = {Me.CmdZ, Me.CmdX, Me.CmdC, Me.CmdV, Me.CmdB, Me.CmdN, Me.CmdM}
        For i As Integer = 0 To 9
            EstilizarTecla(Fila1(i), TipoTecla.Normal)
            EstilizarTecla(Fila2(i), TipoTecla.Normal)
            PonerTecla(Fila1(i), i * 2, 0, 2)
            PonerTecla(Fila2(i), i * 2, 1, 2)
            Teclas.Add(Fila1(i))
            Teclas.Add(Fila2(i))
        Next
        For i As Integer = 0 To 6
            EstilizarTecla(Fila3(i), TipoTecla.Normal)
            PonerTecla(Fila3(i), 3 + i * 2, 2, 2)
            Teclas.Add(Fila3(i))
        Next

        '--- Mayusculas/minusculas comparten lugar: se alternan con Visible (ver los Click)
        pnlModoMayus = New Panel
        pnlModoMayus.Dock = DockStyle.Fill
        pnlModoMayus.Margin = New Padding(3)
        EstilizarTecla(Me.CmdMayusculas, TipoTecla.Especial)
        EstilizarTecla(Me.CmdMinusculas, TipoTecla.Especial)
        Me.CmdMayusculas.Margin = New Padding(0)
        Me.CmdMinusculas.Margin = New Padding(0)
        Me.CmdMayusculas.Text = "ABC"
        Me.CmdMinusculas.Text = "abc"
        pnlModoMayus.Controls.Add(Me.CmdMayusculas)
        pnlModoMayus.Controls.Add(Me.CmdMinusculas)
        Me.CmdMinusculas.BringToFront()
        PonerTecla(pnlModoMayus, 0, 2, 3)

        EstilizarTecla(Me.CmdBorrar, TipoTecla.Especial)
        Me.CmdBorrar.Text = "Borrar"
        PonerTecla(Me.CmdBorrar, 17, 2, 3)

        '--- numeros/letras comparten lugar
        pnlModoNumeros = New Panel
        pnlModoNumeros.Dock = DockStyle.Fill
        pnlModoNumeros.Margin = New Padding(3)
        EstilizarTecla(Me.CmdNumeros, TipoTecla.Especial)
        EstilizarTecla(Me.CmdLetras, TipoTecla.Especial)
        Me.CmdNumeros.Margin = New Padding(0)
        Me.CmdLetras.Margin = New Padding(0)
        Me.CmdNumeros.Text = "123"
        Me.CmdLetras.Text = "Letras"
        pnlModoNumeros.Controls.Add(Me.CmdNumeros)
        pnlModoNumeros.Controls.Add(Me.CmdLetras)
        PonerTecla(pnlModoNumeros, 0, 3, 3)

        EstilizarTecla(Me.CmdEspacio, TipoTecla.Normal)
        PonerTecla(Me.CmdEspacio, 3, 3, 10)
        EstilizarTecla(Me.C1Button28, TipoTecla.Normal)
        PonerTecla(Me.C1Button28, 13, 3, 2)
        EstilizarTecla(Me.Button2, TipoTecla.Primaria)
        Me.Button2.Text = "OK"
        PonerTecla(Me.Button2, 15, 3, 5)
        Me.Button1.Visible = False 'logo decorativo sin funcion

        Teclas.AddRange(New Button() {Me.CmdMayusculas, Me.CmdMinusculas, Me.CmdBorrar, Me.CmdNumeros, Me.CmdLetras, Me.CmdEspacio, Me.C1Button28, Me.Button2})

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
    End Sub

    Private Sub FrmTeclado_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
        AjustarFuentes()
    End Sub

    ' Evita que el teclado quede cortado fuera de la pantalla (la posicion se fija en Form1_Load).
    Private Sub FrmTeclado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Dim Area As Rectangle = Screen.FromControl(Me).WorkingArea
        Dim X As Integer = Math.Max(Area.Left, Math.Min(Me.Left, Area.Right - Me.Width))
        Dim Y As Integer = Math.Max(Area.Top, Math.Min(Me.Top, Area.Bottom - Me.Height))
        Me.Location = New Point(X, Y)
    End Sub
End Class
