Imports System.Data.SqlClient

'///////////////////////// VALIDACION DE LA CUENTA CONTRA LA BASE DE DATOS //////////////////////
' El mesero puede tener la cuenta abierta en una tablet o una isla mientras el cajero la cierra.
' Antes de cada accion sobre la cuenta se verifica que siga abierta; si no, se le manda a sus cuentas.
Partial Class Form1

    ''' <summary>
    ''' Devuelve True si la cuenta cargada ya fue cerrada (o cancelada) por otro usuario. En ese caso avisa al mesero,
    ''' limpia la pantalla y lo envía a FormCuentas, igual que el botón "Mis cuentas". Una cuenta aún no grabada no cuenta como cerrada.
    ''' </summary>
    Public Function CuentaCerradaPorCajero() As Boolean
        If String.IsNullOrEmpty(NumeroFactura) Then Return False

        Dim Existe As Boolean = False
        Dim Abierta As Boolean = False
        Try
            Using cn As New SqlConnection(Conexion)
                Using cmd As New SqlCommand("SELECT Activo, Cancelado FROM Facturas WHERE Numero_Factura = @Numero AND Tipo_Factura = 'Factura'", cn)
                    cmd.Parameters.AddWithValue("@Numero", NumeroFactura)
                    cn.Open()
                    Using rd As SqlDataReader = cmd.ExecuteReader()
                        If rd.Read() Then
                            Existe = True
                            Abierta = Convert.ToBoolean(rd("Activo")) AndAlso Not Convert.ToBoolean(rd("Cancelado"))
                        End If
                    End Using
                End Using
            End Using
        Catch
            ' Si no se puede consultar no se bloquea al mesero: la acción sigue su curso normal
            Return False
        End Try

        If Not Existe OrElse Abierta Then Return False

        MessageBox.Show("La cuenta " & NumeroFactura & " ya fue cerrada. Te llevamos a tus cuentas.", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Information)
        BackToLife("")
        ClearTxtData()
        Actualiza = False
        Me.Hide()
        FormCuentas.Show()
        Return True
    End Function

End Class
