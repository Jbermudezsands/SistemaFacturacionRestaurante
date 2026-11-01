Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Text

''' <summary>
''' Ensamble del dia: lista los productos tipo Ensamble facturados en la fecha con sus componentes y la
''' cantidad a ensamblar, imprime la hoja de produccion y procesa la salida de bodega de los componentes y
''' el ingreso (Mercancia Recibida) de los productos ensamblados, dejando trazabilidad.
''' </summary>
Public Class frmEnsambleDia

    Private Const Titulo As String = "Zeus Facturación"
    Private Const FormatoCantidad As String = "#,##0.####"

    Private _servicio As EnsambleDiaServicio
    Private _plan As New List(Of EnsambleDiaProducto)
    Private _cargando As Boolean = False

    Private DtpFecha As DateTimePicker
    Private CboOrigen As ComboBox
    Private CboDestino As ComboBox
    Private GridEnsamble As DataGridView
    Private LblResumen As Label
    Private LblProcesos As Label
    Private WithEvents BtnActualizar As Button
    Private WithEvents BtnImprimir As Button
    Private WithEvents BtnProcesar As Button
    Private WithEvents BtnCerrar As Button

    Private ReadOnly Property Servicio() As EnsambleDiaServicio
        Get
            If _servicio Is Nothing Then _servicio = New EnsambleDiaServicio(Conexion)
            Return _servicio
        End Get
    End Property

#Region "Diseno"

    Private Sub frmEnsambleDia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConstruirDiseno()
        Try
            CargarBodegas()
            _cargando = True
            DtpFecha.Value = Date.Today
            _cargando = False
            AddHandler DtpFecha.ValueChanged, AddressOf DtpFecha_ValueChanged
            CargarPlan()
        Catch ex As Exception
            _cargando = False
            MessageBox.Show("No se pudo cargar la información del formulario." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConstruirDiseno()
        Me.SuspendLayout()

        Dim Encabezado As Panel = CajaEncabezado("Ensamble del día", "Productos ensamble facturados en la fecha y los componentes que se descargan")

        '--- Filtros
        Dim Filtros As Panel = CajaTarjeta()
        Filtros.Dock = DockStyle.Top
        Filtros.Height = 64
        Filtros.Margin = New Padding(0)

        Dim LblFecha As Label = CajaEtiqueta("Fecha")
        LblFecha.SetBounds(16, 18, 56, 28)
        DtpFecha = New DateTimePicker
        DtpFecha.Format = DateTimePickerFormat.Short
        DtpFecha.Font = CajaFuenteTexto
        DtpFecha.SetBounds(74, 18, 130, 28)

        Dim LblOrigen As Label = CajaEtiqueta("Sale de bodega")
        LblOrigen.SetBounds(226, 18, 120, 28)
        CboOrigen = New ComboBox
        CboOrigen.DropDownStyle = ComboBoxStyle.DropDownList
        CboOrigen.Font = CajaFuenteTexto
        CboOrigen.SetBounds(348, 18, 190, 28)

        Dim LblDestino As Label = CajaEtiqueta("Entra a bodega")
        LblDestino.SetBounds(560, 18, 120, 28)
        CboDestino = New ComboBox
        CboDestino.DropDownStyle = ComboBoxStyle.DropDownList
        CboDestino.Font = CajaFuenteTexto
        CboDestino.SetBounds(682, 18, 190, 28)

        BtnActualizar = CajaBoton("Actualizar", False)
        BtnActualizar.SetBounds(894, 14, 120, 36)
        BtnActualizar.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        Filtros.Controls.AddRange(New Control() {LblFecha, DtpFecha, LblOrigen, CboOrigen, LblDestino, CboDestino, BtnActualizar})

        '--- Grid
        Dim Tarjeta As Panel = CajaTarjeta()
        Tarjeta.Dock = DockStyle.Fill
        Tarjeta.Margin = New Padding(0)

        LblResumen = New Label
        LblResumen.Dock = DockStyle.Bottom
        LblResumen.Height = 28
        LblResumen.Font = CajaFuenteTexto
        LblResumen.ForeColor = CajaTextoSecundario
        LblResumen.TextAlign = ContentAlignment.MiddleLeft

        LblProcesos = New Label
        LblProcesos.Dock = DockStyle.Bottom
        LblProcesos.Height = 28
        LblProcesos.Font = CajaFuenteTexto
        LblProcesos.ForeColor = CajaExito
        LblProcesos.TextAlign = ContentAlignment.MiddleLeft

        GridEnsamble = New DataGridView
        GridEnsamble.Dock = DockStyle.Fill
        CajaEstilizarGrid(GridEnsamble)
        GridEnsamble.ReadOnly = True
        GridEnsamble.AutoGenerateColumns = False
        GridEnsamble.AllowUserToResizeColumns = True
        AgregarColumna("Codigo", "Código", 100, False)
        AgregarColumna("Descripcion", "Descripción", 300, False).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        AgregarColumna("Vendido", "Facturado", 90, True)
        AgregarColumna("Procesado", "Ya ensamblado", 140, True)
        AgregarColumna("Ensamblar", "A ensamblar", 100, True)
        AgregarColumna("PorUnidad", "Por unidad", 100, True)
        AgregarColumna("Usar", "Cantidad a usar", 150, True)
        AddHandler GridEnsamble.CellFormatting, AddressOf GridEnsamble_CellFormatting
        AddHandler GridEnsamble.SelectionChanged, AddressOf GridEnsamble_SelectionChanged

        Tarjeta.Controls.Add(GridEnsamble)
        Tarjeta.Controls.Add(LblProcesos)
        Tarjeta.Controls.Add(LblResumen)

        '--- Botones
        Dim Pie As New Panel
        Pie.Dock = DockStyle.Bottom
        Pie.Height = 64
        Pie.BackColor = CajaFondo

        BtnProcesar = CajaBoton("Procesar", True)
        BtnProcesar.SetBounds(30, 11, 160, 42)
        BtnImprimir = CajaBoton("Imprimir", False)
        BtnImprimir.SetBounds(202, 11, 160, 42)
        BtnCerrar = CajaBoton("Cerrar", False)
        BtnCerrar.SetBounds(374, 11, 160, 42)
        Pie.Controls.AddRange(New Control() {BtnProcesar, BtnImprimir, BtnCerrar})

        Me.Controls.Add(Tarjeta)
        Me.Controls.Add(Pie)
        Me.Controls.Add(Filtros)
        Me.Controls.Add(Encabezado)
        Me.ResumeLayout(True)
    End Sub

    Private Function AgregarColumna(ByVal nombre As String, ByVal encabezado As String, ByVal ancho As Integer, ByVal numerica As Boolean) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn
        col.Name = nombre
        col.DataPropertyName = nombre
        col.HeaderText = encabezado
        col.Width = ancho
        col.SortMode = DataGridViewColumnSortMode.NotSortable
        If numerica Then
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            col.DefaultCellStyle.Format = FormatoCantidad
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        GridEnsamble.Columns.Add(col)
        Return col
    End Function

#End Region

#Region "Carga"

    Private Sub CargarBodegas()
        _cargando = True
        Try
            Dim bodegas As DataTable = Servicio.ObtenerBodegas()
            CboOrigen.DisplayMember = "Cod_Bodega"
            CboOrigen.ValueMember = "Cod_Bodega"
            CboOrigen.DataSource = bodegas
            'Cada combo necesita su propia tabla; si compartieran la fuente, cambiar uno movería al otro
            CboDestino.DisplayMember = "Cod_Bodega"
            CboDestino.ValueMember = "Cod_Bodega"
            CboDestino.DataSource = bodegas.Copy()

            If bodegas.Rows.Count > 0 Then
                CboOrigen.SelectedIndex = 0
                CboDestino.SelectedIndex = 0
                If Not String.IsNullOrEmpty(PBodega) Then
                    Dim indice As Integer = CboOrigen.FindStringExact(PBodega)
                    If indice >= 0 Then
                        CboOrigen.SelectedIndex = indice
                        CboDestino.SelectedIndex = indice
                    End If
                End If
            End If
        Finally
            _cargando = False
        End Try
    End Sub

    ''' <summary>Lee de la base lo facturado del dia, arma el plan y lo muestra en el grid.</summary>
    Private Sub CargarPlan()
        Dim fecha As Date = DtpFecha.Value.Date
        _plan = Servicio.ConstruirPlan(fecha)

        Dim tabla As New DataTable("Ensamble")
        tabla.Columns.Add("Tipo", GetType(String))
        tabla.Columns.Add("Codigo", GetType(String))
        tabla.Columns.Add("Descripcion", GetType(String))
        tabla.Columns.Add("Vendido", GetType(Double))
        tabla.Columns.Add("Procesado", GetType(Double))
        tabla.Columns.Add("Ensamblar", GetType(Double))
        tabla.Columns.Add("PorUnidad", GetType(Double))
        tabla.Columns.Add("Usar", GetType(Double))

        Dim pendientes As Integer = 0
        Dim sinReceta As Integer = 0

        For Each producto As EnsambleDiaProducto In _plan
            Dim fila As DataRow = tabla.NewRow()
            fila("Tipo") = If(producto.Pendiente > 0 AndAlso producto.SinReceta, "S", "P")
            fila("Codigo") = producto.Codigo
            fila("Descripcion") = producto.Descripcion & If(producto.SinReceta, " (sin componentes)", "")
            fila("Vendido") = producto.Vendida
            fila("Procesado") = producto.Procesada
            fila("Ensamblar") = producto.Pendiente
            tabla.Rows.Add(fila)

            If producto.Pendiente > 0 Then
                If producto.SinReceta Then
                    sinReceta += 1
                Else
                    pendientes += 1
                End If
            End If

            For Each componente As EnsambleDiaComponente In producto.Componentes
                Dim detalle As DataRow = tabla.NewRow()
                detalle("Tipo") = "C"
                detalle("Codigo") = componente.Codigo
                detalle("Descripcion") = componente.Descripcion & If(componente.EsServicio, " (servicio, no descarga)", "")
                detalle("PorUnidad") = componente.CantidadUnidad
                detalle("Usar") = componente.CantidadTotal
                tabla.Rows.Add(detalle)
            Next
        Next

        GridEnsamble.DataSource = tabla
        GridEnsamble.ClearSelection()

        Dim resumen As String = _plan.Count.ToString(CultureInfo.InvariantCulture) & If(_plan.Count = 1, " producto ensamble facturado", " productos ensamble facturados") & _
                                " - " & pendientes.ToString(CultureInfo.InvariantCulture) & " por ensamblar"
        If sinReceta > 0 Then resumen &= " - " & sinReceta.ToString(CultureInfo.InvariantCulture) & " sin componentes (no se procesan)"
        LblResumen.Text = resumen

        MostrarProcesosDelDia(fecha)

        BtnProcesar.Enabled = pendientes > 0
        BtnImprimir.Enabled = pendientes > 0 OrElse sinReceta > 0
    End Sub

    Private Sub MostrarProcesosDelDia(ByVal fecha As Date)
        Dim procesos As DataTable = Servicio.ObtenerProcesosDelDia(fecha)
        If procesos.Rows.Count = 0 Then
            LblProcesos.Text = ""
            Return
        End If

        Dim texto As New StringBuilder("Ya procesado: ")
        For Each fila As DataRow In procesos.Rows
            If texto.Length > 14 Then texto.Append("  |  ")
            texto.Append("Salida ").Append(Convert.ToString(fila("Numero_Salida"))).Append(" / Compra ").Append(Convert.ToString(fila("Numero_Compra")))
        Next
        LblProcesos.Text = texto.ToString()
    End Sub

#End Region

#Region "Grid"

    Private Sub GridEnsamble_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 Then Return
        Dim vista As DataRowView = TryCast(GridEnsamble.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If vista Is Nothing Then Return

        Select Case Convert.ToString(vista("Tipo"))
            Case "P", "S"
                e.CellStyle.Font = CajaFuenteBoton
                e.CellStyle.BackColor = Color.FromArgb(220, 230, 241)
                If Convert.ToString(vista("Tipo")) = "S" Then e.CellStyle.ForeColor = CajaPeligro
                If Convert.ToString(vista("Tipo")) = "P" AndAlso Not IsDBNull(vista("Ensamblar")) AndAlso Convert.ToDouble(vista("Ensamblar")) <= 0 Then
                    e.CellStyle.ForeColor = Color.Gray
                End If
            Case "C"
                If e.ColumnIndex = GridEnsamble.Columns("Descripcion").Index Then e.CellStyle.Padding = New Padding(24, 0, 0, 0)
                If e.ColumnIndex = GridEnsamble.Columns("Codigo").Index Then e.CellStyle.Padding = New Padding(16, 0, 0, 0)
        End Select
    End Sub

    Private Sub GridEnsamble_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        'Solo para lectura: se limpia la seleccion para que no tape el resaltado de los productos
        If GridEnsamble.SelectedRows.Count > 0 Then GridEnsamble.ClearSelection()
    End Sub

#End Region

#Region "Acciones"

    Private Sub DtpFecha_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Not _cargando Then Recargar()
    End Sub

    Private Sub BtnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnActualizar.Click
        Recargar()
    End Sub

    Private Sub Recargar()
        Try
            Cursor = Cursors.WaitCursor
            CargarPlan()
        Catch ex As Exception
            MessageBox.Show("No se pudo cargar el ensamble del día." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprimir.Click
        Try
            EnsambleDiaImpresion.Mostrar(Servicio, _plan, DtpFecha.Value.Date, Convert.ToString(CboOrigen.SelectedValue), Convert.ToString(CboDestino.SelectedValue))
        Catch ex As EnsambleDiaNegocioException
            MessageBox.Show(ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("No se pudo imprimir el ensamble del día." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnProcesar.Click
        If CboOrigen.SelectedValue Is Nothing OrElse CboDestino.SelectedValue Is Nothing Then
            MessageBox.Show("Seleccione las bodegas de origen y destino.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim solicitud As New EnsambleDiaSolicitud
        solicitud.Fecha = DtpFecha.Value.Date
        solicitud.BodegaOrigen = Convert.ToString(CboOrigen.SelectedValue)
        solicitud.BodegaDestino = Convert.ToString(CboDestino.SelectedValue)
        solicitud.Usuario = Convert.ToString(Form1.NombreVendedor)

        Dim aEnsamblar As Integer = 0
        For Each producto As EnsambleDiaProducto In _plan
            If producto.Pendiente > 0 AndAlso Not producto.SinReceta Then aEnsamblar += 1
        Next

        Dim pregunta As String = "Se descargarán de la bodega " & solicitud.BodegaOrigen & " los componentes de " & aEnsamblar.ToString(CultureInfo.InvariantCulture) & _
                                 If(aEnsamblar = 1, " producto", " productos") & " y se ingresarán a la bodega " & solicitud.BodegaDestino & _
                                 " (ensamble del " & solicitud.Fecha.ToString("dd/MM/yyyy") & ")." & vbCrLf & vbCrLf & "¿Desea procesarlo?"
        If MessageBox.Show(pregunta, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try
            Cursor = Cursors.WaitCursor
            Dim resultado As EnsambleDiaResultado = Servicio.Procesar(solicitud)
            Cursor = Cursors.Default

            Dim mensaje As New StringBuilder
            mensaje.AppendLine("Ensamble procesado.")
            mensaje.AppendLine("Salida de bodega: " & resultado.NumeroSalida)
            mensaje.AppendLine("Mercancía recibida: " & resultado.NumeroCompra)
            mensaje.AppendLine("Productos ensamblados: " & resultado.ProductosProcesados.ToString(CultureInfo.InvariantCulture))
            mensaje.AppendLine("Costo total: " & resultado.TotalCosto.ToString("N2"))
            If resultado.SinReceta.Count > 0 Then
                mensaje.AppendLine()
                mensaje.AppendLine("No se procesaron por no tener componentes:")
                For Each item As String In resultado.SinReceta
                    mensaje.AppendLine(" - " & item)
                Next
            End If
            MessageBox.Show(mensaje.ToString().TrimEnd(), Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)

            CargarPlan()
        Catch ex As EnsambleDiaNegocioException
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show("No se pudo procesar el ensamble. No se grabó ningún movimiento." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCerrar.Click
        Me.Close()
    End Sub

#End Region

End Class
