''' <summary>
''' Datos del cliente de una cuenta (nombre, RUC, teléfono y dirección), como los captura Form1 para el mesero.
''' El cajero los usa desde frmCuentasCajero para que salgan en la factura impresa.
''' </summary>
Public Class FrmDatosCliente
    Inherits Form

    Private TxtNombre As TextBox
    Private TxtRuc As TextBox
    Private TxtTelefono As TextBox
    Private TxtDireccion As TextBox
    Private WithEvents BtnAceptar As Button
    Private WithEvents BtnCancelar As Button

    Public Property Nombre As String
        Get
            Return TxtNombre.Text.Trim()
        End Get
        Set(ByVal value As String)
            TxtNombre.Text = value
        End Set
    End Property

    Public Property Ruc As String
        Get
            Return TxtRuc.Text.Trim()
        End Get
        Set(ByVal value As String)
            TxtRuc.Text = value
        End Set
    End Property

    Public Property Telefono As String
        Get
            Return TxtTelefono.Text.Trim()
        End Get
        Set(ByVal value As String)
            TxtTelefono.Text = value
        End Set
    End Property

    Public Property Direccion As String
        Get
            Return TxtDireccion.Text.Trim()
        End Get
        Set(ByVal value As String)
            TxtDireccion.Text = value
        End Set
    End Property

    Public Sub New(ByVal NumeroCuenta As String)
        Me.SuspendLayout()
        Me.Text = "Datos del cliente"
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowInTaskbar = False
        Me.ClientSize = New Size(520, 480)
        Me.BackColor = CajaFondoSuave

        Dim Cuerpo As New TableLayoutPanel
        Cuerpo.Dock = DockStyle.Fill
        Cuerpo.Padding = New Padding(24, 16, 24, 8)
        Cuerpo.ColumnCount = 2
        Cuerpo.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 110.0!))
        Cuerpo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        For i As Integer = 1 To 4
            Cuerpo.RowStyles.Add(New RowStyle(SizeType.Absolute, 70.0!))
        Next

        TxtNombre = AgregarCampo(Cuerpo, 0, "Nombre")
        TxtRuc = AgregarCampo(Cuerpo, 1, "RUC")
        TxtTelefono = AgregarCampo(Cuerpo, 2, "Teléfono")
        TxtDireccion = AgregarCampo(Cuerpo, 3, "Dirección")

        Dim Pie As New FlowLayoutPanel
        Pie.Dock = DockStyle.Bottom
        Pie.Height = 64
        Pie.FlowDirection = FlowDirection.RightToLeft
        Pie.Padding = New Padding(24, 10, 16, 0)
        BtnCancelar = CajaBoton("Cancelar", False)
        BtnCancelar.Size = New Size(130, 44)
        BtnAceptar = CajaBoton("Aceptar", True)
        BtnAceptar.Size = New Size(130, 44)
        Pie.Controls.Add(BtnCancelar)
        Pie.Controls.Add(BtnAceptar)

        Me.Controls.Add(Cuerpo)
        Me.Controls.Add(Pie)
        Me.Controls.Add(CajaEncabezado("Datos del cliente", "Cuenta Nº " & NumeroCuenta & " · saldrán en la factura"))
        Me.AcceptButton = BtnAceptar
        Me.CancelButton = BtnCancelar
        Me.ResumeLayout(True)
    End Sub

    Private Function AgregarCampo(ByVal Cuerpo As TableLayoutPanel, ByVal Fila As Integer, ByVal Texto As String) As TextBox
        Dim Etiqueta As Label = CajaEtiqueta(Texto)
        Etiqueta.Dock = DockStyle.Fill

        Dim Caja As New TextBox
        Caja.Font = CajaFuenteTexto
        Caja.MaxLength = 100
        Caja.Location = New Point(0, 4)
        Caja.Width = 340
        Caja.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        ' Boton con el simbolo de teclado debajo del texto: abre FrmTeclado para escribir en pantalla tactil
        Dim BtnTeclado As Button = CajaBoton(ChrW(&H2328).ToString(), False)
        BtnTeclado.Font = New Font("Segoe UI Symbol", 12.0!)
        BtnTeclado.Size = New Size(56, 28)
        BtnTeclado.Location = New Point(0, 36)
        BtnTeclado.Tag = Caja
        AddHandler BtnTeclado.Click, AddressOf AbrirTeclado

        Dim Celda As New Panel
        Celda.Dock = DockStyle.Fill
        Celda.Controls.Add(Caja)
        Celda.Controls.Add(BtnTeclado)

        Cuerpo.Controls.Add(Etiqueta, 0, Fila)
        Cuerpo.Controls.Add(Celda, 1, Fila)
        Return Caja
    End Function

    Private Sub AbrirTeclado(ByVal sender As Object, ByVal e As EventArgs)
        Dim Caja As TextBox = DirectCast(DirectCast(sender, Button).Tag, TextBox)
        My.Forms.FrmTeclado.Posicion = "Externo"
        My.Forms.FrmTeclado.Destino = Caja
        My.Forms.FrmTeclado.ShowDialog(Me)
        My.Forms.FrmTeclado.Destino = Nothing
        Caja.Focus()
    End Sub

    Private Sub BtnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAceptar.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCancelar.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
