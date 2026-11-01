Imports System.Drawing.Printing

''' <summary>Impresora que usa cada documento del sistema (misma configuracion Impresoras.dll que el Manager).</summary>
Public Class frmImpresoras

    Private CmbFactura As ComboBox
    Private CmbRecibo As ComboBox
    Private CmbComanda As ComboBox
    Private CmbComandaBar As ComboBox
    Private WithEvents BtnGuardar As Button
    Private WithEvents BtnSalir As Button

    Private Sub frmImpresoras_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConstruirDiseno()
        CargarImpresoras()
    End Sub

    Private Sub ConstruirDiseno()
        Me.SuspendLayout()

        Dim Encabezado As Panel = CajaEncabezado("Impresoras", "Impresora que usa cada documento del sistema")

        Dim Tarjeta As Panel = CajaTarjeta()
        Tarjeta.Dock = DockStyle.Fill
        Tarjeta.Padding = New Padding(24, 20, 24, 12)

        Dim Filas As New TableLayoutPanel
        Filas.Dock = DockStyle.Fill
        Filas.ColumnCount = 1
        Filas.RowCount = 9
        Filas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        For i As Integer = 1 To 8
            Filas.RowStyles.Add(New RowStyle(SizeType.Absolute, If(i Mod 2 = 1, 26.0!, 40.0!)))
        Next
        Filas.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

        CmbFactura = NuevoCombo()
        CmbRecibo = NuevoCombo()
        CmbComanda = NuevoCombo()
        CmbComandaBar = NuevoCombo()

        AgregarFila(Filas, 0, "Impresora de Factura", CmbFactura)
        AgregarFila(Filas, 2, "Impresora de Recibo", CmbRecibo)
        AgregarFila(Filas, 4, "Impresora de Comanda (Cocina)", CmbComanda)
        AgregarFila(Filas, 6, "Impresora de Comanda Bar", CmbComandaBar)
        Tarjeta.Controls.Add(Filas)

        Dim Pie As New FlowLayoutPanel
        Pie.Dock = DockStyle.Bottom
        Pie.Height = 64
        Pie.Padding = New Padding(26, 11, 0, 0)
        Pie.BackColor = CajaFondo
        BtnGuardar = CajaBoton("Guardar", True)
        BtnGuardar.Size = New Size(140, 42)
        BtnSalir = CajaBoton("Salir", False)
        BtnSalir.Size = New Size(140, 42)
        Pie.Controls.Add(BtnGuardar)
        Pie.Controls.Add(BtnSalir)

        Dim Contenedor As New Panel
        Contenedor.Dock = DockStyle.Fill
        Contenedor.Padding = New Padding(14)
        Contenedor.BackColor = CajaFondo
        Contenedor.Controls.Add(Tarjeta)

        Me.Controls.Add(Contenedor)
        Me.Controls.Add(Pie)
        Me.Controls.Add(Encabezado)
        Me.ResumeLayout(True)
    End Sub

    Private Function NuevoCombo() As ComboBox
        Dim Cmb As New ComboBox
        Cmb.Dock = DockStyle.Fill
        Cmb.DropDownStyle = ComboBoxStyle.DropDownList
        Cmb.Font = CajaFuenteTexto
        Cmb.FlatStyle = FlatStyle.Flat
        Return Cmb
    End Function

    Private Sub AgregarFila(ByVal Filas As TableLayoutPanel, ByVal Fila As Integer, ByVal Texto As String, ByVal Cmb As ComboBox)
        Dim Etiqueta As Label = CajaEtiqueta(Texto)
        Etiqueta.Dock = DockStyle.Fill
        Filas.Controls.Add(Etiqueta, 0, Fila)
        Filas.Controls.Add(Cmb, 0, Fila + 1)
    End Sub

    Private Sub CargarImpresoras()
        For Each Cmb As ComboBox In New ComboBox() {CmbFactura, CmbRecibo, CmbComanda, CmbComandaBar}
            Cmb.Items.Clear()
            For Each Impresora As String In PrinterSettings.InstalledPrinters
                Cmb.Items.Add(Impresora)
            Next
        Next

        CmbFactura.Text = BuscaImpresora("Factura")
        CmbRecibo.Text = BuscaImpresora("Recibo")
        CmbComanda.Text = BuscaImpresora("Comanda")
        CmbComandaBar.Text = BuscaImpresora("ComandaBar")
    End Sub

    Private Sub BtnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        Try
            GuardaImpresora("Factura", CmbFactura.Text)
            GuardaImpresora("Recibo", CmbRecibo.Text)
            GuardaImpresora("Comanda", CmbComanda.Text)
            GuardaImpresora("ComandaBar", CmbComandaBar.Text)

            MessageBox.Show("Se ha guardado con éxito.", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar la configuración de impresoras." & vbCrLf & ex.Message, "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

End Class
