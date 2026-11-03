''' <summary>Menu del cajero: cuentas de mesas, arqueo de caja y metodos de pago.</summary>
Public Class frmOpcionesCajero

    Private WithEvents BtnCuentas As Button
    Private WithEvents BtnArqueo As Button
    Private WithEvents BtnMetodosPago As Button
    Private WithEvents BtnSalir As Button

    Private Sub frmOpcionesCajero_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConstruirDiseno()
    End Sub

    Private Sub ConstruirDiseno()
        Me.SuspendLayout()

        Dim Saludo As String = "Hola, " & StrConv(CStr(Form1.NombreVendedor), VbStrConv.ProperCase)
        Dim Encabezado As Panel = CajaEncabezado(Saludo, "Selecciona la opción de caja que necesitas")

        Dim Opciones As New FlowLayoutPanel
        Opciones.Dock = DockStyle.Fill
        Opciones.Padding = New Padding(20, 24, 20, 0)
        Opciones.BackColor = CajaFondo

        BtnCuentas = CajaOpcion("Cuentas de mesas", "Cerrar cuentas, cobrar e imprimir facturas y prefacturas", "cuentas")
        BtnArqueo = CajaOpcion("Arqueo de caja", "Contar el efectivo y cerrar la caja", "arqueo")
        BtnMetodosPago = CajaOpcion("Métodos de pago", "Agregar y modificar los métodos de pago", "metodos")
        Opciones.Controls.Add(BtnCuentas)
        Opciones.Controls.Add(BtnArqueo)
        Opciones.Controls.Add(BtnMetodosPago)

        Dim Pie As New Panel
        Pie.Dock = DockStyle.Bottom
        Pie.Height = 64
        Pie.BackColor = CajaFondo
        BtnSalir = CajaBoton("Cerrar sesión", False)
        BtnSalir.Size = New Size(160, 42)
        BtnSalir.Location = New Point(30, 11)
        Pie.Controls.Add(BtnSalir)

        Me.Controls.Add(Opciones)
        Me.Controls.Add(Pie)
        Me.Controls.Add(Encabezado)
        Me.ResumeLayout(True)
    End Sub

    Private Sub BtnCuentas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCuentas.Click
        Using Pantalla As New frmCuentasCajero
            Pantalla.ShowDialog(Me)
        End Using
    End Sub

    Private Sub BtnArqueo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnArqueo.Click
        Try
            If New ArqueoServicio(Conexion).ObtenerCajeros().Rows.Count = 0 Then
                MessageBox.Show("No hay cajeros creados, por lo que no se puede ingresar a los arqueos." & vbCrLf & "Cree un cajero e inténtelo de nuevo.", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo verificar los cajeros." & vbCrLf & ex.Message, "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Using Pantalla As New FrmArqueo
            Pantalla.ShowDialog(Me)
        End Using
    End Sub

    Private Sub BtnMetodosPago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMetodosPago.Click
        Using Pantalla As New frmMetodoPago
            Pantalla.ShowDialog(Me)
        End Using
    End Sub

    Private Sub BtnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSalir.Click
        FormLog.Show()
        Me.Close()
    End Sub

End Class
