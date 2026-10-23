Public Class frmConsulta

    Private Sub frmConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Adapter As New SqlClient.SqlDataAdapter("Select Numero_Factura, NetoPagar from Facturas Where Cod_Cliente = '" & Form1.cboMesas.SelectedValue & "' and Activo = 'True' and Cancelado = 'False'", TempConn)
        Dim Table As New DataTable
        Adapter.Fill(Table)

        Me.GridCuentas.RowHeight = 22

        Me.GridCuentas.DataSource = Table
    End Sub
End Class