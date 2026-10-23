Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D

Public Class FormLog

    Private Const TituloMensaje As String = "Zeus Facturación"
    Private Const RadioTarjeta As Integer = 18

#Region "Carga del formulario"

    Private Sub FormLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            InicializarConexiones()
            CargarMeseros()
            CargarLogoEmpresa()
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos." & vbCrLf & ex.Message, TituloMensaje, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
            Return
        End Try

        Version = My.Application.Info.Version.ToString
        Me.LblVersion.Text = "V." & Mid(Version, 1, Len(Version) - 2)

        RedondearTarjeta()
        CentrarTarjeta()
        LimpiarCampos()
    End Sub

    ''' <summary>Lee la cadena de conexión desde el Access local y crea las conexiones globales.</summary>
    Private Sub InicializarConexiones()
        Dim Datos As New DataTable

        Using ConexionAccessLocal As New OleDb.OleDbConnection(ConexionAccess)
            Using Adapter As New OleDb.OleDbDataAdapter("Select * From Servidor", ConexionAccessLocal)
                Adapter.Fill(Datos)
            End Using
        End Using

        Conexion = Datos.Rows(0)("Servidor")
        MiConexion = New SqlClient.SqlConnection(Conexion)
        TempConn = New SqlClient.SqlConnection(Conexion)
    End Sub

    Private Sub CargarMeseros()
        Dim Meseros As New DataTable

        Using Adapter As New SqlDataAdapter("Select Cod_Vendedor as Codigo, Nombre_Vendedor as Nombre from vendedores", TempConn)
            Adapter.Fill(Meseros)
        End Using

        Me.cboMesero.DisplayMember = "Nombre"
        Me.cboMesero.ValueMember = "Codigo"
        Me.cboMesero.DataSource = Meseros
    End Sub

    Private Sub CargarLogoEmpresa()
        Dim Empresa As New DataTable

        Using Adapter As New SqlDataAdapter("SELECT * FROM DatosEmpresa", MiConexion)
            Adapter.Fill(Empresa)
        End Using

        If Empresa.Rows.Count > 0 AndAlso Not IsDBNull(Empresa.Rows(0)("Logo")) Then
            LogoEmpresa = Empresa.Rows(0)("Logo")
            Me.PictureBox1.Image = BytesToImagen(StringtoByte(LogoEmpresa))
        End If

        If Me.PictureBox1.Image Is Nothing Then OcultarLogo()
    End Sub

    ''' <summary>Sin logo no se reserva el espacio en la tarjeta.</summary>
    Private Sub OcultarLogo()
        Dim Espacio As Integer = Me.lblTitulo.Top - Me.PictureBox1.Top

        Me.PictureBox1.Visible = False
        For Each Control As Control In Me.pnlTarjeta.Controls
            If Control IsNot Me.PictureBox1 Then Control.Top -= Espacio
        Next
        Me.pnlTarjeta.Height -= Espacio
    End Sub

    Private Sub LimpiarCampos()
        Me.cboMesero.SelectedIndex = -1
        Me.txtContraseña.Text = ""
        Me.cboMesero.Focus()
    End Sub

#End Region

#Region "Apariencia"

    Private Sub FormLog_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        CentrarTarjeta()
    End Sub

    Private Sub CentrarTarjeta()
        Dim X As Integer = Math.Max(0, (Me.ClientSize.Width - Me.pnlTarjeta.Width) \ 2)
        Dim Y As Integer = Math.Max(0, (Me.ClientSize.Height - Me.pnlTarjeta.Height) \ 2)
        Me.pnlTarjeta.Location = New Point(X, Y)
    End Sub

    Private Sub RedondearTarjeta()
        Dim Diametro As Integer = RadioTarjeta * 2
        Dim Rect As New Rectangle(0, 0, Me.pnlTarjeta.Width, Me.pnlTarjeta.Height)

        Using Ruta As New GraphicsPath()
            Ruta.AddArc(Rect.X, Rect.Y, Diametro, Diametro, 180, 90)
            Ruta.AddArc(Rect.Right - Diametro, Rect.Y, Diametro, Diametro, 270, 90)
            Ruta.AddArc(Rect.Right - Diametro, Rect.Bottom - Diametro, Diametro, Diametro, 0, 90)
            Ruta.AddArc(Rect.X, Rect.Bottom - Diametro, Diametro, Diametro, 90, 90)
            Ruta.CloseFigure()
            Me.pnlTarjeta.Region = New Region(Ruta)
        End Using
    End Sub

    Private Sub chkMostrar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMostrar.CheckedChanged
        Me.txtContraseña.UseSystemPasswordChar = Not Me.chkMostrar.Checked
        Me.txtContraseña.Focus()
    End Sub

#End Region

#Region "Inicio de sesión"

    Private Sub btnAcceder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcceder.Click
        IniciarSesion()
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub cboMesero_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMesero.SelectedIndexChanged
        Me.txtContraseña.Text = ""
    End Sub

    Private Sub cboMesero_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboMesero.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.txtContraseña.Focus()
        End If
    End Sub

    Private Sub txtContraseña_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtContraseña.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            IniciarSesion()
        End If
    End Sub

    Private Sub IniciarSesion()
        If Me.cboMesero.SelectedValue Is Nothing OrElse Me.txtContraseña.Text = "" Then
            MessageBox.Show("Tenés que completar los datos para Acceder", TituloMensaje, MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return
        End If

        Dim Credencial As New DataTable
        Try
            Using Adapter As New SqlDataAdapter("Select top(1) Contraseña from vendedores Where Cod_Vendedor = @Codigo", TempConn)
                Adapter.SelectCommand.Parameters.AddWithValue("@Codigo", Me.cboMesero.SelectedValue)
                Adapter.Fill(Credencial)
            End Using
        Catch ex As Exception
            MessageBox.Show("No se pudo validar el acceso." & vbCrLf & ex.Message, TituloMensaje, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If Credencial.Rows.Count = 0 Then
            MessageBox.Show("El usuario no existe", TituloMensaje, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Me.txtContraseña.Text <> Convert.ToString(Credencial.Rows(0)("Contraseña")) Then
            MessageBox.Show("La Contraseña es incorrecta", TituloMensaje, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.txtContraseña.SelectAll()
            Me.txtContraseña.Focus()
            Return
        End If

        Form1.CodVendedor = Me.cboMesero.SelectedValue
        Form1.NombreVendedor = Me.cboMesero.Text
        FormCuentas.Show()
        Me.Close()
    End Sub

    Private Sub pnlTarjeta_Paint(sender As Object, e As PaintEventArgs) Handles pnlTarjeta.Paint

    End Sub

#End Region

End Class
