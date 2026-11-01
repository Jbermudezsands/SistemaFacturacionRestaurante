''' <summary>Menu del administrador: impresoras y metodos de pago.</summary>
Public Class frmOpcionesAdministrador

    Private WithEvents BtnImpresoras As Button
    Private WithEvents BtnMetodosPago As Button
    Private WithEvents BtnClaves As Button
    Private WithEvents BtnSalir As Button

    Private Sub BtnClaves_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClaves.Click
        Using Pantalla As New frmClavesVendedor
            Pantalla.ShowDialog(Me)
        End Using
    End Sub

    Private Sub frmOpcionesAdministrador_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConstruirDiseno()
    End Sub

    Private Sub ConstruirDiseno()
        Me.SuspendLayout()

        Dim Saludo As String = "Hola, " & StrConv(CStr(Form1.NombreVendedor), VbStrConv.ProperCase)
        Dim Encabezado As Panel = CajaEncabezado(Saludo, "Configuración del restaurante")

        Dim Opciones As New FlowLayoutPanel
        Opciones.Dock = DockStyle.Fill
        Opciones.Padding = New Padding(20, 24, 20, 0)
        Opciones.BackColor = CajaFondo

        BtnImpresoras = CajaOpcion("Impresoras", "Impresoras de comanda, bar, facturas y recibos", "impresoras")
        BtnMetodosPago = CajaOpcion("Métodos de pago", "Agregar y modificar los métodos de pago", "metodos")
        Opciones.Controls.Add(BtnImpresoras)
        Opciones.Controls.Add(BtnMetodosPago)
        BtnClaves = CajaOpcion("Vendedores", "Cambiar el nombre y la contraseña de cada vendedor", "usuarios")
        Opciones.Controls.Add(BtnClaves)

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

    Private Sub BtnImpresoras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImpresoras.Click
        Using Pantalla As New frmImpresoras
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
