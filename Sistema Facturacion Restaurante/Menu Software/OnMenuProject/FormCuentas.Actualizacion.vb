Imports System.Data.SqlClient

'///////////////////////// ACTUALIZACION AUTOMATICA DE MESAS Y CUENTAS //////////////////////
' Si el cajero cierra una cuenta o otro mesero ocupa una mesa, la pantalla del mesero se pone al dia
' sola. Solo se redibuja cuando algo cambio, para no interrumpir un toque sobre un boton.
Partial Class FormCuentas

    Private Const IntervaloActualizacion As Integer = 5000

    Private WithEvents TmrActualizar As Timer
    Private FirmaCuentas As String = Nothing

    Private Sub IniciarActualizacion(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        FirmaCuentas = LeerFirmaCuentas()
        TmrActualizar = New Timer
        TmrActualizar.Interval = IntervaloActualizacion
        TmrActualizar.Start()
    End Sub

    Private Sub DetenerActualizacion(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        If TmrActualizar IsNot Nothing Then TmrActualizar.Stop()
    End Sub

    Private Sub TmrActualizar_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TmrActualizar.Tick
        ' No se redibuja si otra ventana (p. ej. Form1) esta por encima
        If Form.ActiveForm IsNot Me Then Return

        Try
            Dim Firma As String = LeerFirmaCuentas()
            If Firma = FirmaCuentas Then Return
            FirmaCuentas = Firma

            CargarDatos()
            CargarMisMesas()
            BuscarMesasDisponiles()
            ReabrirMesaSeleccionada()
        Catch
            ' Sin conexion en este instante: se reintenta en el siguiente ciclo
        End Try
    End Sub

    ''' <summary>Vuelve a mostrar las cuentas de la mesa elegida; si ya no es del mesero se limpia la lista.</summary>
    Private Sub ReabrirMesaSeleccionada()
        If SelMesa <> "" Then
            For Each Ctl As Control In FLPMisMesas.Controls
                If CStr(Ctl.Tag) = SelMesa Then
                    BuscarCuentas(Ctl, EventArgs.Empty)
                    Return
                End If
            Next
        End If

        SelMesa = ""
        DeletePanelItems(FLPMisCuentas)
        SelControlCuentas = -1
    End Sub

    ''' <summary>Huella de las cuentas abiertas: cambia cuando se abre, se cierra o cambia de mesero alguna cuenta.</summary>
    Private Function LeerFirmaCuentas() As String
        Dim Firma As New System.Text.StringBuilder
        Using cn As New SqlConnection(Conexion)
            Using cmd As New SqlCommand("SELECT Numero_Factura, Cod_Cliente, Cod_Vendedor, NetoPagar FROM Facturas WHERE Activo = 1 AND Cancelado = 0 ORDER BY Numero_Factura", cn)
                cn.Open()
                Using rd As SqlDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        Firma.Append(rd(0).ToString()).Append("|").Append(rd(1).ToString()).Append("|").Append(rd(2).ToString()).Append("|").Append(rd(3).ToString()).Append(";")
                    End While
                End Using
            End Using
        End Using
        Return Firma.ToString()
    End Function

End Class
