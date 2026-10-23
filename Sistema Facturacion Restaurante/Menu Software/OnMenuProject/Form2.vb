Public Class Form2
    Dim Pnl As FlowLayoutPanel
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CrearProductos()
    End Sub


    Public Sub CrearProductos()
        Me.Controls.Remove(Pnl)

        Pnl = New FlowLayoutPanel
        Pnl.Location = New Point(25, 25)
        Pnl.Height = 600
        Pnl.Width = 800
        Pnl.BackColor = Color.White
        Pnl.BorderStyle = BorderStyle.FixedSingle
        Pnl.AutoScroll = True
        Me.Controls.Add(Pnl)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        CrearProductos()
        '/////// Desde aqui voy a agregar el panel //////////////
        Dim counter As Integer = 0
        Do While counter < 100
            Dim button As New Button
            button.Name = "btn" & counter
            button.Text = "btn " & counter

            button.Width = 300
            button.Height = 200
            counter = counter + 1

            Pnl.Controls.Add(button)
        Loop
    End Sub

End Class