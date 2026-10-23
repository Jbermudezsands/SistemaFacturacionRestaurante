Imports System.Data.SqlClient
Public Class FormLog

    Private Sub C1Combo1_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMesero.SelectedValueChanged
        Me.txtContraseña.Text = ""
    End Sub

    Private Sub FormLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim StrSQLAccess = "Select * From Servidor"
        Dim MiConexionAccess As New OleDb.OleDbConnection(ConexionAccess)
        Dim DataAdapterAccess As New OleDb.OleDbDataAdapter(StrSQLAccess, MiConexionAccess)
        Dim DatasetDatos As New DataSet, iPosicionFila As Double = 0

        MiConexionAccess.Open()
        DataAdapterAccess.Fill(DatasetDatos, "Compañias")
        MiConexionAccess.Close()

        Conexion = DatasetDatos.Tables("Compañias").Rows(iPosicionFila)("Servidor")
        MiConexion = New SqlClient.SqlConnection(Conexion)
        TempConn = New SqlClient.SqlConnection(Conexion)

        Application.DoEvents()
        Dim Adapter As New SqlDataAdapter("Select Cod_Vendedor as Codigo, Nombre_Vendedor as Nombre from vendedores", TempConn)
        Dim Table As New DataTable
        Adapter.Fill(Table)
        Me.cboMesero.DataSource = Table
        Me.cboMesero.DropDownWidth = 300
        Me.cboMesero.ItemHeight = 30
        Me.cboMesero.DisplayMember = "Nombre"
        Me.cboMesero.ValueMember = "Codigo"
        Me.cboMesero.Splits(0).DisplayColumns("Nombre").Width = 190

        Me.txtContraseña.Text = ""
        Me.cboMesero.Text = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.cboMesero.Text = "" Or Me.txtContraseña.Text = "" Then
            MessageBox.Show("Tenés que completar los datos para Acceder", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Sub
        End If

        Dim Adapter As New SqlDataAdapter("Select top(1) Contraseña from vendedores Where Cod_Vendedor = '" & Me.cboMesero.SelectedValue & "'", TempConn)
        Dim Table As New DataTable
        Adapter.Fill(Table)

        If Table.Rows.Count > 0 Then
            If Me.txtContraseña.Text <> Table.Rows(0)("Contraseña") Then
                MessageBox.Show("La Contraseña es incorrecta", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                Form1.CodVendedor = Me.cboMesero.SelectedValue
                Form1.NombreVendedor = Me.cboMesero.Text
                FormCuentas.Show()
                Me.Close()
            End If
        Else
            MessageBox.Show("El usuario no existe", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

   

    Private Sub cboMesero_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboMesero.KeyDown
        If e.KeyValue = 13 Then
            Me.txtContraseña.Focus()
        End If
    End Sub

    Private Sub txtContraseña_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtContraseña.KeyDown
        If e.KeyValue = 13 Then
            If Me.cboMesero.Text = "" Or Me.txtContraseña.Text = "" Then
                MessageBox.Show("Tenés que completar los datos para Acceder", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Exit Sub
            End If

            Dim Adapter As New SqlDataAdapter("Select top(1) Contraseña from vendedores Where Cod_Vendedor = '" & Me.cboMesero.SelectedValue & "'", TempConn)
            Dim Table As New DataTable
            Adapter.Fill(Table)

            If Table.Rows.Count > 0 Then
                If Me.txtContraseña.Text <> Table.Rows(0)("Contraseña") Then
                    MessageBox.Show("La Contraseña es incorrecta", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                Else
                    Form1.CodVendedor = Me.cboMesero.SelectedValue
                    Form1.NombreVendedor = Me.cboMesero.Text
                    FormCuentas.Show()
                    Me.Close()
                End If
            Else
                MessageBox.Show("El usuario no existe", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
    End Sub
End Class