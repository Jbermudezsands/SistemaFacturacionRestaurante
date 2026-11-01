''' <summary>
''' Paleta y controles compartidos por las pantallas de Cajero y Administrador.
''' Usa los mismos colores de FormLog y FormCuentas.
''' </summary>
Module EstiloCaja

    Public ReadOnly CajaFondo As Color = Color.FromArgb(24, 41, 66)
    Public ReadOnly CajaPrimario As Color = Color.FromArgb(52, 94, 152)
    Public ReadOnly CajaPrimarioHover As Color = Color.FromArgb(41, 78, 130)
    Public ReadOnly CajaPrimarioPresionado As Color = Color.FromArgb(30, 62, 110)
    Public ReadOnly CajaTexto As Color = Color.FromArgb(30, 41, 59)
    Public ReadOnly CajaTextoSecundario As Color = Color.FromArgb(60, 70, 85)
    Public ReadOnly CajaSubtitulo As Color = Color.FromArgb(170, 185, 205)
    Public ReadOnly CajaFondoSuave As Color = Color.FromArgb(246, 248, 251)
    Public ReadOnly CajaHover As Color = Color.FromArgb(238, 241, 245)
    Public ReadOnly CajaPresionado As Color = Color.FromArgb(228, 232, 238)
    Public ReadOnly CajaBorde As Color = Color.FromArgb(210, 216, 226)
    Public ReadOnly CajaExito As Color = Color.FromArgb(30, 130, 60)
    Public ReadOnly CajaAdvertencia As Color = Color.FromArgb(200, 90, 0)
    Public ReadOnly CajaPeligro As Color = Color.FromArgb(185, 28, 28)

    Public ReadOnly CajaFuenteTitulo As New Font("Segoe UI Semibold", 20.0!, FontStyle.Bold)
    Public ReadOnly CajaFuenteSubtitulo As New Font("Segoe UI", 11.0!)
    Public ReadOnly CajaFuenteSeccion As New Font("Segoe UI Semibold", 12.0!, FontStyle.Bold)
    Public ReadOnly CajaFuenteBoton As New Font("Segoe UI Semibold", 11.0!, FontStyle.Bold)
    Public ReadOnly CajaFuenteTexto As New Font("Segoe UI", 11.0!)

    ''' <summary>Boton plano: primario (azul) o secundario (claro con borde).</summary>
    Public Function CajaBoton(ByVal Texto As String, ByVal Primario As Boolean) As Button
        Dim btn As New Button
        btn.Text = Texto
        btn.Font = CajaFuenteBoton
        btn.FlatStyle = FlatStyle.Flat
        btn.UseVisualStyleBackColor = False
        btn.Cursor = Cursors.Hand
        btn.Margin = New Padding(0, 0, 8, 0)
        If Primario Then
            btn.BackColor = CajaPrimario
            btn.ForeColor = Color.White
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = CajaPrimarioHover
            btn.FlatAppearance.MouseDownBackColor = CajaPrimarioPresionado
        Else
            btn.BackColor = CajaFondoSuave
            btn.ForeColor = CajaTextoSecundario
            btn.FlatAppearance.BorderSize = 1
            btn.FlatAppearance.BorderColor = CajaBorde
            btn.FlatAppearance.MouseOverBackColor = CajaHover
            btn.FlatAppearance.MouseDownBackColor = CajaPresionado
        End If
        Return btn
    End Function

    ''' <summary>Aplica el aspecto de boton seleccionado o normal a un boton de filtro.</summary>
    Public Sub CajaMarcarFiltro(ByVal btn As Button, ByVal Seleccionado As Boolean)
        If Seleccionado Then
            btn.BackColor = CajaPrimario
            btn.ForeColor = Color.White
            btn.FlatAppearance.BorderColor = CajaPrimario
            btn.FlatAppearance.MouseOverBackColor = CajaPrimarioHover
            btn.FlatAppearance.MouseDownBackColor = CajaPrimarioPresionado
        Else
            btn.BackColor = CajaFondoSuave
            btn.ForeColor = CajaTextoSecundario
            btn.FlatAppearance.BorderColor = CajaBorde
            btn.FlatAppearance.MouseOverBackColor = CajaHover
            btn.FlatAppearance.MouseDownBackColor = CajaPresionado
        End If
    End Sub

    ''' <summary>Panel blanco que agrupa una seccion sobre el fondo azul oscuro.</summary>
    Public Function CajaTarjeta() As Panel
        Dim pnl As New Panel
        pnl.BackColor = Color.White
        pnl.Padding = New Padding(12)
        pnl.Margin = New Padding(6)
        Return pnl
    End Function

    Public Function CajaTituloSeccion(ByVal Texto As String) As Label
        Dim lbl As New Label
        lbl.Text = Texto
        lbl.Font = CajaFuenteSeccion
        lbl.ForeColor = CajaTexto
        lbl.Dock = DockStyle.Top
        lbl.Height = 32
        lbl.TextAlign = ContentAlignment.MiddleLeft
        Return lbl
    End Function

    ''' <summary>Encabezado de pantalla: titulo y subtitulo en blanco sobre el fondo oscuro. El boton de salida lo agrega cada formulario.</summary>
    Public Function CajaEncabezado(ByVal Titulo As String, ByVal Subtitulo As String) As Panel
        Dim pnl As New Panel
        pnl.Dock = DockStyle.Top
        pnl.Height = 84
        pnl.Padding = New Padding(20, 12, 20, 8)
        pnl.BackColor = CajaFondo

        Dim lblTitulo As New Label
        lblTitulo.Text = Titulo
        lblTitulo.Font = CajaFuenteTitulo
        lblTitulo.ForeColor = Color.White
        lblTitulo.AutoSize = True
        lblTitulo.Location = New Point(20, 10)

        Dim lblSubtitulo As New Label
        lblSubtitulo.Text = Subtitulo
        lblSubtitulo.Font = CajaFuenteSubtitulo
        lblSubtitulo.ForeColor = CajaSubtitulo
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Location = New Point(22, 50)

        pnl.Controls.Add(lblTitulo)
        pnl.Controls.Add(lblSubtitulo)
        Return pnl
    End Function

    Public Sub CajaEstilizarGrid(ByVal grid As DataGridView)
        grid.BackgroundColor = Color.White
        grid.BorderStyle = BorderStyle.None
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersDefaultCellStyle.BackColor = CajaPrimario
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        grid.ColumnHeadersDefaultCellStyle.Font = CajaFuenteBoton
        grid.ColumnHeadersHeight = 36
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.DefaultCellStyle.Font = CajaFuenteTexto
        grid.DefaultCellStyle.ForeColor = CajaTexto
        grid.DefaultCellStyle.SelectionBackColor = CajaHover
        grid.DefaultCellStyle.SelectionForeColor = CajaTexto
        grid.AlternatingRowsDefaultCellStyle.BackColor = CajaFondoSuave
        grid.GridColor = CajaBorde
        grid.RowHeadersVisible = False
        grid.RowTemplate.Height = 30
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.AllowUserToResizeRows = False
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.MultiSelect = False
    End Sub

    ''' <summary>Icono lineal dibujado en codigo (cuentas, arqueo, metodos, impresoras) con el azul primario.</summary>
    Public Function CajaIcono(ByVal Tipo As String) As Bitmap
        Dim Bmp As New Bitmap(72, 72)
        Using G As Graphics = Graphics.FromImage(Bmp)
            G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Using Lapiz As New Pen(CajaPrimario, 4.0!), Relleno As New SolidBrush(CajaPrimario)
                Lapiz.LineJoin = Drawing2D.LineJoin.Round
                Lapiz.StartCap = Drawing2D.LineCap.Round
                Lapiz.EndCap = Drawing2D.LineCap.Round
                Select Case Tipo
                    Case "cuentas"
                        G.DrawRectangle(Lapiz, 16, 6, 40, 60)
                        G.DrawLine(Lapiz, 24, 20, 48, 20)
                        G.DrawLine(Lapiz, 24, 31, 48, 31)
                        G.DrawLine(Lapiz, 24, 42, 48, 42)
                        G.FillEllipse(Relleno, 40, 49, 9, 9)
                    Case "arqueo"
                        G.DrawRectangle(Lapiz, 6, 18, 60, 36)
                        G.DrawEllipse(Lapiz, 26, 25, 20, 22)
                        G.FillEllipse(Relleno, 12, 33, 6, 6)
                        G.FillEllipse(Relleno, 54, 33, 6, 6)
                    Case "metodos"
                        G.DrawRectangle(Lapiz, 6, 15, 60, 42)
                        G.FillRectangle(Relleno, 6, 25, 60, 9)
                        G.DrawLine(Lapiz, 14, 46, 32, 46)
                    Case "usuarios"
                        G.DrawEllipse(Lapiz, 24, 6, 24, 24)
                        G.DrawArc(Lapiz, 8, 36, 56, 56, 200, 140)
                    Case "impresoras"
                        G.DrawRectangle(Lapiz, 20, 6, 32, 20)
                        G.DrawRectangle(Lapiz, 6, 26, 60, 26)
                        G.DrawRectangle(Lapiz, 20, 42, 32, 24)
                        G.FillEllipse(Relleno, 54, 32, 5, 5)
                End Select
            End Using
        End Using
        Return Bmp
    End Function

    ''' <summary>Boton grande de menu: icono arriba, titulo y descripcion corta sobre fondo blanco.</summary>
    Public Function CajaOpcion(ByVal Titulo As String, ByVal Detalle As String, ByVal Icono As String) As Button
        Dim btn As New Button
        btn.Text = Titulo & vbCrLf & Detalle
        btn.Image = CajaIcono(Icono)
        btn.ImageAlign = ContentAlignment.TopCenter
        btn.Padding = New Padding(8, 16, 8, 8)
        btn.Size = New Size(230, 220)
        btn.Margin = New Padding(10)
        btn.BackColor = Color.White
        btn.ForeColor = CajaTexto
        btn.Font = CajaFuenteSeccion
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.BorderColor = CajaBorde
        btn.FlatAppearance.MouseOverBackColor = CajaHover
        btn.FlatAppearance.MouseDownBackColor = CajaPresionado
        btn.TextAlign = ContentAlignment.BottomCenter
        btn.TextImageRelation = TextImageRelation.Overlay
        btn.UseVisualStyleBackColor = False
        btn.Cursor = Cursors.Hand
        Return btn
    End Function

    Public Function CajaEtiqueta(ByVal Texto As String) As Label
        Dim lbl As New Label
        lbl.Text = Texto
        lbl.Font = CajaFuenteTexto
        lbl.ForeColor = CajaTextoSecundario
        lbl.AutoSize = False
        lbl.TextAlign = ContentAlignment.MiddleLeft
        Return lbl
    End Function

End Module
