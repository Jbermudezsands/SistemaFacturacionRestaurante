Imports System.Data.SqlClient

''' <summary>Lista los arqueos registrados para abrir uno desde FrmArqueo.</summary>
Public Class FrmBuscarArqueo

    ''' <summary>Codigo del arqueo elegido; vacio si se cancela.</summary>
    Public CodigoSeleccionado As String = ""

    Private WithEvents Grid As DataGridView
    Private WithEvents BtnAbrir As Button
    Private WithEvents BtnCancelar As Button

    Private Sub FrmBuscarArqueo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConstruirDiseno()
        CargarArqueos()
    End Sub

    Private Sub ConstruirDiseno()
        Me.SuspendLayout()

        Dim Encabezado As Panel = CajaEncabezado("Buscar arqueo", "Seleccione el arqueo que desea abrir")

        Grid = New DataGridView
        Grid.Dock = DockStyle.Fill
        CajaEstilizarGrid(Grid)
        Grid.ReadOnly = True
        Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Dim Tarjeta As Panel = CajaTarjeta()
        Tarjeta.Dock = DockStyle.Fill
        Tarjeta.Controls.Add(Grid)

        Dim Contenedor As New Panel
        Contenedor.Dock = DockStyle.Fill
        Contenedor.Padding = New Padding(14)
        Contenedor.BackColor = CajaFondo
        Contenedor.Controls.Add(Tarjeta)

        Dim Pie As New FlowLayoutPanel
        Pie.Dock = DockStyle.Bottom
        Pie.Height = 64
        Pie.Padding = New Padding(20, 11, 0, 0)
        Pie.BackColor = CajaFondo
        BtnAbrir = CajaBoton("Abrir", True)
        BtnAbrir.Size = New Size(120, 42)
        BtnCancelar = CajaBoton("Cancelar", False)
        BtnCancelar.Size = New Size(120, 42)
        Pie.Controls.Add(BtnAbrir)
        Pie.Controls.Add(BtnCancelar)

        Me.Controls.Add(Contenedor)
        Me.Controls.Add(Pie)
        Me.Controls.Add(Encabezado)
        Me.ResumeLayout(True)
    End Sub

    Private Sub CargarArqueos()
        Dim Tabla As New DataTable
        Try
            Using cn As New SqlConnection(Conexion)
                Using da As New SqlDataAdapter("SELECT CodArqueo AS Codigo, FechaArqueo AS Fecha, Cod_Cajero AS Cajero, " &
                                               "CASE WHEN ValorFacturas IS NULL THEN 'Pendiente' ELSE 'Grabado' END AS Estado " &
                                               "FROM Arqueo ORDER BY FechaArqueo DESC, CodArqueo DESC", cn)
                    da.Fill(Tabla)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("No se pudieron cargar los arqueos." & vbCrLf & ex.Message, "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Grid.DataSource = Tabla
        If Grid.Columns.Contains("Fecha") Then Grid.Columns("Fecha").DefaultCellStyle.Format = "dd/MM/yyyy"
    End Sub

    Private Sub Elegir()
        If Grid.CurrentRow Is Nothing Then Return
        CodigoSeleccionado = Convert.ToString(Grid.CurrentRow.Cells("Codigo").Value)
        Me.Close()
    End Sub

    Private Sub BtnAbrir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAbrir.Click
        Elegir()
    End Sub

    Private Sub Grid_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles Grid.CellDoubleClick
        If e.RowIndex >= 0 Then Elegir()
    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCancelar.Click
        Me.Close()
    End Sub

End Class
