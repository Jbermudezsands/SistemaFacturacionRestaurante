Imports System.Data.SqlClient

''' <summary>Catalogo de metodos de pago (tabla MetodoPago): agregar, modificar y eliminar los que no se han usado en facturas.</summary>
Public Class frmMetodoPago

    Private Const Titulo As String = "Zeus Facturación"

    Private WithEvents Grid As DataGridView
    Private TxtNombre As TextBox
    Private CmbTipo As ComboBox
    Private TxtCuenta As TextBox
    Private CmbMoneda As ComboBox
    Private LblEstado As Label
    Private WithEvents BtnNuevo As Button
    Private WithEvents BtnGuardar As Button
    Private WithEvents BtnEliminar As Button
    Private WithEvents BtnSalir As Button

    Private Tabla As DataTable
    Private EsNuevo As Boolean = True
    Private Cargando As Boolean = False

    Private Sub frmMetodoPago_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConstruirDiseno()
        CargarMetodos()
        NuevoMetodo()
    End Sub

#Region "Diseño"

    Private Sub ConstruirDiseno()
        Me.SuspendLayout()

        Dim Encabezado As Panel = CajaEncabezado("Métodos de pago", "Formas de pago disponibles al cobrar una cuenta")

        Dim Cuerpo As New TableLayoutPanel
        Cuerpo.Dock = DockStyle.Fill
        Cuerpo.Padding = New Padding(14)
        Cuerpo.BackColor = CajaFondo
        Cuerpo.ColumnCount = 2
        Cuerpo.RowCount = 1
        Cuerpo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 58.0!))
        Cuerpo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 42.0!))
        Cuerpo.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

        '--- Lista
        Dim TarjetaLista As Panel = CajaTarjeta()
        TarjetaLista.Dock = DockStyle.Fill
        Grid = New DataGridView
        Grid.Dock = DockStyle.Fill
        CajaEstilizarGrid(Grid)
        Grid.ReadOnly = True
        Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        TarjetaLista.Controls.Add(Grid)
        TarjetaLista.Controls.Add(CajaTituloSeccion("Métodos registrados"))

        '--- Datos
        Dim TarjetaDatos As Panel = CajaTarjeta()
        TarjetaDatos.Dock = DockStyle.Fill

        Dim Campos As New TableLayoutPanel
        Campos.Dock = DockStyle.Top
        Campos.Height = 250
        Campos.ColumnCount = 1
        Campos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        For i As Integer = 1 To 8
            Campos.RowStyles.Add(New RowStyle(SizeType.Absolute, If(i Mod 2 = 1, 24.0!, 38.0!)))
        Next

        TxtNombre = New TextBox
        TxtNombre.MaxLength = 100
        CmbTipo = New ComboBox
        CmbTipo.DropDownStyle = ComboBoxStyle.DropDown
        CmbTipo.MaxLength = 100
        TxtCuenta = New TextBox
        TxtCuenta.MaxLength = 100
        CmbMoneda = New ComboBox
        CmbMoneda.DropDownStyle = ComboBoxStyle.DropDownList
        CmbMoneda.Items.AddRange(New Object() {"Cordobas", "Dolares"})

        For Each Ctl As Control In New Control() {TxtNombre, CmbTipo, TxtCuenta, CmbMoneda}
            Ctl.Dock = DockStyle.Fill
            Ctl.Font = CajaFuenteTexto
        Next

        AgregarCampo(Campos, 0, "Nombre del método", TxtNombre)
        AgregarCampo(Campos, 2, "Tipo de pago", CmbTipo)
        AgregarCampo(Campos, 4, "Cuenta contable", TxtCuenta)
        AgregarCampo(Campos, 6, "Moneda", CmbMoneda)

        LblEstado = New Label
        LblEstado.Dock = DockStyle.Top
        LblEstado.Height = 44
        LblEstado.Font = New Font("Segoe UI", 9.5!)
        LblEstado.ForeColor = CajaTextoSecundario
        LblEstado.TextAlign = ContentAlignment.MiddleLeft

        Dim Botones As New FlowLayoutPanel
        Botones.Dock = DockStyle.Bottom
        Botones.Height = 56
        Botones.Padding = New Padding(0, 8, 0, 0)
        BtnGuardar = CajaBoton("Guardar", True)
        BtnGuardar.Size = New Size(100, 42)
        BtnNuevo = CajaBoton("Nuevo", False)
        BtnNuevo.Size = New Size(90, 42)
        BtnEliminar = CajaBoton("Eliminar", False)
        BtnEliminar.Size = New Size(100, 42)
        BtnSalir = CajaBoton("Salir", False)
        BtnSalir.Size = New Size(80, 42)
        Botones.Controls.Add(BtnGuardar)
        Botones.Controls.Add(BtnNuevo)
        Botones.Controls.Add(BtnEliminar)
        Botones.Controls.Add(BtnSalir)

        TarjetaDatos.Controls.Add(LblEstado)
        TarjetaDatos.Controls.Add(Campos)
        TarjetaDatos.Controls.Add(CajaTituloSeccion("Datos del método"))
        TarjetaDatos.Controls.Add(Botones)

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

#End Region

#Region "Datos"

    Private Sub CargarMetodos()
        Tabla = New DataTable
        Using cn As New SqlConnection(Conexion)
            Using da As New SqlDataAdapter("SELECT NombrePago, TipoPago, Cod_Cuenta, Moneda FROM MetodoPago ORDER BY NombrePago", cn)
                da.Fill(Tabla)
            End Using
        End Using

        Cargando = True
        Try
            Grid.DataSource = Tabla
            Grid.Columns("NombrePago").HeaderText = "Método"
            Grid.Columns("TipoPago").HeaderText = "Tipo"
            Grid.Columns("Cod_Cuenta").HeaderText = "Cuenta"
            Grid.Columns("Moneda").HeaderText = "Moneda"
            Grid.ClearSelection()

            CmbTipo.Items.Clear()
            For Each Tipo As String In New String() {"Efectivo", "Tarjetas", "Cheques", "Cambio", "Transferencia"}
                CmbTipo.Items.Add(Tipo)
            Next
            For Each Fila As DataRow In Tabla.Rows
                If Not IsDBNull(Fila("TipoPago")) AndAlso Not CmbTipo.Items.Contains(Fila("TipoPago").ToString()) Then
                    CmbTipo.Items.Add(Fila("TipoPago").ToString())
                End If
            Next
        Finally
            Cargando = False
        End Try
    End Sub

    Private Sub NuevoMetodo()
        EsNuevo = True
        Grid.ClearSelection()
        TxtNombre.ReadOnly = False
        TxtNombre.Text = ""
        CmbTipo.Text = ""
        TxtCuenta.Text = ""
        CmbMoneda.SelectedIndex = 0
        BtnEliminar.Enabled = False
        LblEstado.ForeColor = CajaTextoSecundario
        LblEstado.Text = "Nuevo método de pago."
        TxtNombre.Focus()
    End Sub

    Private Sub Grid_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Grid.SelectionChanged
        If Cargando OrElse Grid.SelectedRows.Count = 0 Then Return

        Dim Fila As DataRow = CType(Grid.SelectedRows(0).DataBoundItem, DataRowView).Row
        EsNuevo = False
        TxtNombre.Text = Fila("NombrePago").ToString()
        TxtNombre.ReadOnly = True
        CmbTipo.Text = If(IsDBNull(Fila("TipoPago")), "", Fila("TipoPago").ToString())
        TxtCuenta.Text = If(IsDBNull(Fila("Cod_Cuenta")), "", Fila("Cod_Cuenta").ToString().Trim())
        CmbMoneda.Text = If(IsDBNull(Fila("Moneda")), "Cordobas", Fila("Moneda").ToString())
        BtnEliminar.Enabled = True

        Try
            If EstaEnUso(TxtNombre.Text) Then
                LblEstado.ForeColor = CajaAdvertencia
                LblEstado.Text = "Este método ya se usó en facturas: se puede modificar pero no eliminar. El nombre no se puede cambiar."
            Else
                LblEstado.ForeColor = CajaTextoSecundario
                LblEstado.Text = "El nombre no se puede cambiar; para renombrarlo cree uno nuevo."
            End If
        Catch ex As Exception
            LblEstado.Text = ""
        End Try
    End Sub

    ''' <summary>Un metodo esta en uso cuando alguna factura tiene un pago registrado con ese nombre.</summary>
    Private Function EstaEnUso(ByVal Nombre As String) As Boolean
        Using cn As New SqlConnection(Conexion)
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM Detalle_MetodoFacturas WHERE NombrePago = @Nombre", cn)
                cmd.Parameters.AddWithValue("@Nombre", Nombre)
                cn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

#End Region

#Region "Acciones"

    Private Sub BtnNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNuevo.Click
        NuevoMetodo()
    End Sub

    Private Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGuardar.Click
        Dim Nombre As String = TxtNombre.Text.Trim()
        Dim Tipo As String = CmbTipo.Text.Trim()

        If Nombre = "" Then
            MessageBox.Show("Digite el nombre del método de pago.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtNombre.Focus()
            Return
        End If

        If Tipo = "" Then
            MessageBox.Show("Seleccione o digite el tipo de pago.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbTipo.Focus()
            Return
        End If

        Try
            If EsNuevo Then
                If ExisteMetodo(Nombre) Then
                    MessageBox.Show("Ya existe un método de pago llamado '" & Nombre & "'.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    TxtNombre.Focus()
                    Return
                End If
                Ejecutar("INSERT INTO MetodoPago (NombrePago, TipoPago, Cod_Cuenta, Moneda) VALUES (@Nombre, @Tipo, @Cuenta, @Moneda)", Nombre, Tipo)
            Else
                ' Cambiar la moneda de un metodo ya usado altera como se convierten sus pagos anteriores
                If EstaEnUso(Nombre) AndAlso MonedaCambio(Nombre) Then
                    If MessageBox.Show("Este método ya se usó en facturas y cambiar su moneda afecta cómo se calculan esos pagos. ¿Desea continuar?", Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                        Return
                    End If
                End If
                Ejecutar("UPDATE MetodoPago SET TipoPago = @Tipo, Cod_Cuenta = @Cuenta, Moneda = @Moneda WHERE NombrePago = @Nombre", Nombre, Tipo)
            End If

            CargarMetodos()
            SeleccionarMetodo(Nombre)
            MessageBox.Show("El método de pago se guardó correctamente.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar el método de pago." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEliminar.Click
        If EsNuevo Then Return
        Dim Nombre As String = TxtNombre.Text.Trim()

        Try
            If EstaEnUso(Nombre) Then
                MessageBox.Show("El método '" & Nombre & "' ya se usó en facturas y no se puede eliminar.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If MessageBox.Show("¿Seguro que desea eliminar el método de pago '" & Nombre & "'?", Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            Using cn As New SqlConnection(Conexion)
                Using cmd As New SqlCommand("DELETE FROM MetodoPago WHERE NombrePago = @Nombre", cn)
                    cmd.Parameters.AddWithValue("@Nombre", Nombre)
                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            CargarMetodos()
            NuevoMetodo()
        Catch ex As SqlException When ex.Number = 547
            MessageBox.Show("El método '" & Nombre & "' está en uso en otros documentos y no se puede eliminar.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("No se pudo eliminar el método de pago." & vbCrLf & ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnSalir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

    Private Function ExisteMetodo(ByVal Nombre As String) As Boolean
        Using cn As New SqlConnection(Conexion)
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM MetodoPago WHERE NombrePago = @Nombre", cn)
                cmd.Parameters.AddWithValue("@Nombre", Nombre)
                cn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Private Function MonedaCambio(ByVal Nombre As String) As Boolean
        Using cn As New SqlConnection(Conexion)
            Using cmd As New SqlCommand("SELECT Moneda FROM MetodoPago WHERE NombrePago = @Nombre", cn)
                cmd.Parameters.AddWithValue("@Nombre", Nombre)
                cn.Open()
                Dim Actual As Object = cmd.ExecuteScalar()
                Return Actual IsNot Nothing AndAlso Not IsDBNull(Actual) AndAlso Not String.Equals(Actual.ToString().Trim(), CmbMoneda.Text, StringComparison.OrdinalIgnoreCase)
            End Using
        End Using
    End Function

    Private Sub Ejecutar(ByVal Sql As String, ByVal Nombre As String, ByVal Tipo As String)
        Using cn As New SqlConnection(Conexion)
            Using cmd As New SqlCommand(Sql, cn)
                cmd.Parameters.AddWithValue("@Nombre", Nombre)
                cmd.Parameters.AddWithValue("@Tipo", Tipo)
                cmd.Parameters.AddWithValue("@Cuenta", TxtCuenta.Text.Trim())
                cmd.Parameters.AddWithValue("@Moneda", CmbMoneda.Text)
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub SeleccionarMetodo(ByVal Nombre As String)
        For Each Fila As DataGridViewRow In Grid.Rows
            If String.Equals(Convert.ToString(Fila.Cells("NombrePago").Value), Nombre, StringComparison.OrdinalIgnoreCase) Then
                Grid.CurrentCell = Fila.Cells("NombrePago")
                Fila.Selected = True
                Exit For
            End If
        Next
    End Sub

#End Region

End Class
