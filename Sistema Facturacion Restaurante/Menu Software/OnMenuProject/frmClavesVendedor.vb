Imports System.Data.SqlClient

''' <summary>El administrador cambia el nombre y la contraseña de cualquier vendedor. Una contraseña vacía se guarda como Null.</summary>
Public Class frmClavesVendedor

    Private Const Titulo As String = "Zeus Facturación"

    Private WithEvents Grid As DataGridView
    Private TxtCodigo As TextBox
    Private TxtNombre As TextBox
    Private TxtClave As TextBox
    Private CboTipo As ComboBox
    Private WithEvents ChkMostrar As CheckBox
    Private LblAyuda As Label
    Private WithEvents BtnGuardar As Button
    Private WithEvents BtnSalir As Button
    Private Cargando As Boolean = False

    Private Sub frmClavesVendedor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConstruirDiseno()
        CargarVendedores("")
    End Sub

    Private Sub ConstruirDiseno()
        Me.SuspendLayout()

        Dim Encabezado As Panel = CajaEncabezado("Vendedores", "Cambia el nombre, el tipo o la contraseña de un vendedor")

        Dim Cuerpo As New TableLayoutPanel
        Cuerpo.Dock = DockStyle.Fill
        Cuerpo.Padding = New Padding(14)
        Cuerpo.BackColor = CajaFondo
        Cuerpo.ColumnCount = 2
        Cuerpo.RowCount = 1
        Cuerpo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 55.0!))
        Cuerpo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45.0!))
        Cuerpo.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

        Dim TarjetaLista As Panel = CajaTarjeta()
        TarjetaLista.Dock = DockStyle.Fill
        Grid = New DataGridView
        Grid.Dock = DockStyle.Fill
        CajaEstilizarGrid(Grid)
        Grid.ReadOnly = True
        Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        TarjetaLista.Controls.Add(Grid)
        TarjetaLista.Controls.Add(CajaTituloSeccion("Vendedores registrados"))

        Dim TarjetaDatos As Panel = CajaTarjeta()
        TarjetaDatos.Dock = DockStyle.Fill

        Dim Campos As New TableLayoutPanel
        Campos.Dock = DockStyle.Top
        Campos.Height = 224
        Campos.ColumnCount = 1
        Campos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        For i As Integer = 1 To 8
            Campos.RowStyles.Add(New RowStyle(SizeType.Absolute, If(i Mod 2 = 1, 22.0!, 34.0!)))
        Next

        TxtCodigo = New TextBox
        TxtCodigo.ReadOnly = True
        TxtNombre = New TextBox
        TxtNombre.MaxLength = 100
        TxtClave = New TextBox
        TxtClave.MaxLength = 50
        TxtClave.UseSystemPasswordChar = True
        For Each Ctl As TextBox In New TextBox() {TxtCodigo, TxtNombre, TxtClave}
            Ctl.Dock = DockStyle.Fill
            Ctl.Font = CajaFuenteTexto
        Next

        AgregarCampo(Campos, 0, "Código", TxtCodigo)
        AgregarCampo(Campos, 2, "Nombre", TxtNombre)

        CboTipo = New ComboBox
        CboTipo.DropDownStyle = ComboBoxStyle.DropDownList
        CboTipo.Dock = DockStyle.Fill
        CboTipo.Font = CajaFuenteTexto
        CboTipo.Items.AddRange(New Object() {"Mesero", "Cajero", "Administrador"})
        CboTipo.SelectedIndex = 0

        AgregarCampo(Campos, 4, "Tipo", CboTipo)
        AgregarCampo(Campos, 6, "Contraseña", TxtClave)

        ChkMostrar = New CheckBox
        ChkMostrar.Text = "Mostrar contraseña"
        ChkMostrar.AutoSize = True
        ChkMostrar.Font = CajaFuenteTexto
        ChkMostrar.ForeColor = CajaTextoSecundario
        ChkMostrar.Location = New Point(0, 6)

        LblAyuda = New Label
        LblAyuda.Dock = DockStyle.Fill
        LblAyuda.Font = New Font("Segoe UI", 9.5!)
        LblAyuda.ForeColor = CajaTextoSecundario
        LblAyuda.Text = "Deja la contraseña vacía para quitarla. Un Administrador sin contraseña entra solo con Aceptar."

        Dim Botones As New FlowLayoutPanel
        Botones.Dock = DockStyle.Bottom
        Botones.Height = 56
        Botones.Padding = New Padding(0, 8, 0, 0)
        BtnGuardar = CajaBoton("Guardar", True)
        BtnGuardar.Size = New Size(110, 42)
        BtnSalir = CajaBoton("Salir", False)
        BtnSalir.Size = New Size(90, 42)
        Botones.Controls.Add(BtnGuardar)
        Botones.Controls.Add(BtnSalir)

        Dim Contenido As New Panel
        Contenido.Dock = DockStyle.Fill
        Contenido.Controls.Add(LblAyuda)
        Contenido.Controls.Add(ChkMostrar)

        TarjetaDatos.Controls.Add(Contenido)
        TarjetaDatos.Controls.Add(Campos)
        TarjetaDatos.Controls.Add(CajaTituloSeccion("Datos del vendedor"))
        TarjetaDatos.Controls.Add(Botones)
        LblAyuda.Dock = DockStyle.Bottom
        LblAyuda.Height = 60

        Cuerpo.Controls.Add(TarjetaLista, 0, 0)
        Cuerpo.Controls.Add(TarjetaDatos, 1, 0)

        Me.Controls.Add(Cuerpo)
        Me.Controls.Add(Encabezado)
        Me.ResumeLayout(True)
    End Sub

    Private Sub AgregarCampo(ByVal Campos As TableLayoutPanel, ByVal Fila As Integer, ByVal Texto As String, ByVal Ctl As Control)
        Dim Etiqueta As Label = CajaEtiqueta(Texto)
        Etiqueta.Dock = DockStyle.Fill
        Campos.Controls.Add(Etiqueta, 0, Fila)
        Campos.Controls.Add(Ctl, 0, Fila + 1)
    End Sub

    Private Sub CargarVendedores(ByVal Seleccionar As String)
        Dim Tabla As New DataTable
        Using cn As New SqlConnection(Conexion)
            Using da As New SqlDataAdapter("SELECT Cod_Vendedor, Nombre_Vendedor, Tipo, [Contraseña] AS Clave FROM Vendedores ORDER BY Nombre_Vendedor", cn)
                da.Fill(Tabla)
            End Using
        End Using

        Cargando = True
        Try
            Grid.DataSource = Tabla
            Grid.Columns("Cod_Vendedor").HeaderText = "Código"
            Grid.Columns("Nombre_Vendedor").HeaderText = "Nombre"
            Grid.Columns("Tipo").HeaderText = "Tipo"
            Grid.Columns("Clave").Visible = False
            Grid.ClearSelection()
        Finally
            Cargando = False
        End Try

        TxtCodigo.Text = ""
        TxtNombre.Text = ""
        TxtClave.Text = ""
        CboTipo.SelectedIndex = 0
        If Seleccionar <> "" Then
            For Each Fila As DataGridViewRow In Grid.Rows
                If Convert.ToString(Fila.Cells("Cod_Vendedor").Value) = Seleccionar Then
                    Grid.CurrentCell = Fila.Cells("Cod_Vendedor")
                    Fila.Selected = True
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub Grid_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Grid.SelectionChanged
        If Cargando OrElse Grid.SelectedRows.Count = 0 Then Return
        Dim Fila As DataRow = CType(Grid.SelectedRows(0).DataBoundItem, DataRowView).Row
        TxtCodigo.Text = Fila("Cod_Vendedor").ToString()
        TxtNombre.Text = If(IsDBNull(Fila("Nombre_Vendedor")), "", Fila("Nombre_Vendedor").ToString())
        TxtClave.Text = If(IsDBNull(Fila("Clave")), "", Fila("Clave").ToString())

        'Tipo Null o desconocido se muestra como Mesero (igual que en FormLog)
        Dim Tipo As String = If(IsDBNull(Fila("Tipo")), "", Fila("Tipo").ToString().Trim())
        Dim Indice As Integer = CboTipo.FindStringExact(Tipo)
        CboTipo.SelectedIndex = If(Indice >= 0, Indice, 0)
    End Sub

    Private Sub ChkMostrar_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkMostrar.CheckedChanged
        TxtClave.UseSystemPasswordChar = Not ChkMostrar.Checked
    End Sub

    Private Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGuardar.Click
        If TxtCodigo.Text = "" Then
            MessageBox.Show("Seleccione un vendedor de la lista.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If TxtNombre.Text.Trim() = "" Then
            MessageBox.Show("El nombre no puede quedar vacío.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtNombre.Focus()
            Return
        End If

        Try
            Using cn As New SqlConnection(Conexion)
                Using cmd As New SqlCommand("UPDATE Vendedores SET Nombre_Vendedor = @Nombre, Tipo = @Tipo, [Contraseña] = @Clave WHERE Cod_Vendedor = @Codigo", cn)
                    cmd.Parameters.AddWithValue("@Nombre", TxtNombre.Text.Trim())
                    cmd.Parameters.AddWithValue("@Tipo", CboTipo.Text)
                    cmd.Parameters.AddWithValue("@Clave", If(TxtClave.Text = "", CType(DBNull.Value, Object), TxtClave.Text))
                    cmd.Parameters.AddWithValue("@Codigo", TxtCodigo.Text)
                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Dim Codigo As String = TxtCodigo.Text
            CargarVendedores(Codigo)
            MessageBox.Show("El vendedor se guardó correctamente.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar el vendedor." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnSalir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

End Class
