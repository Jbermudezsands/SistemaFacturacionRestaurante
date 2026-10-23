Public Class Form3
    Public PrecioUnitario As Double
    Private Sub C1Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpunto.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & "."
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub C1Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button13.Click
        If Me.txtPrecioUnitario.Text <> "" Then
            PrecioUnitario = Me.txtPrecioUnitario.Text
        Else
            PrecioUnitario = 0
        End If
        Me.Close()
    End Sub

    Private Sub btn0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn0.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 0
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Application.DoEvents()
        Me.txtPrecioUnitario.Text = ""
        PrecioUnitario = 0
    End Sub

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 1
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 2
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub btn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 3
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub btn4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn4.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 4
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub btn5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 5
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub btn6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn6.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 6
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub btn7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn7.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 7
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub btn8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn8.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 8
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub btn9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn9.Click
        Me.txtPrecioUnitario.Text = txtPrecioUnitario.Text & 9
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.txtPrecioUnitario.Text.Length > 0 Then
            Me.txtPrecioUnitario.Text = Me.txtPrecioUnitario.Text.Substring(0, Me.txtPrecioUnitario.Text.Length - 1)
            Me.txtPrecioUnitario.Focus()
            txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
        End If
        Me.txtPrecioUnitario.Focus()
        txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1
    End Sub

    Private Sub txtPrecioUnitario_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrecioUnitario.TextChanged
        If Me.txtPrecioUnitario.Text = "" Then
            Exit Sub
        End If

        If Not IsNumeric(Me.txtPrecioUnitario.Text) Then
            Me.txtPrecioUnitario.Text = Me.txtPrecioUnitario.Text.Substring(0, Me.txtPrecioUnitario.Text.Length - 1)
            Me.txtPrecioUnitario.Focus()
            txtPrecioUnitario.SelectionStart = txtPrecioUnitario.Text.Length + 1

        End If
    End Sub
End Class