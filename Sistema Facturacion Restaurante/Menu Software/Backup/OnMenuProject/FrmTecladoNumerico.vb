Public Class FrmTecladoNumerico
    Public EscrituraTeclado As String = ""

    Private Sub CmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub SimpleButton20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Cmd1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd1.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "1"
    End Sub

    Private Sub Cmd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd2.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "2"
    End Sub

    Private Sub Cmd3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd3.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "3"
    End Sub

    Private Sub Cmd4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd4.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "4"
    End Sub

    Private Sub Cmd5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd5.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "5"
    End Sub

    Private Sub Cmd6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd6.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "6"
    End Sub

    Private Sub Cmd7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd7.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "7"
    End Sub

    Private Sub Cmd8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd8.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "8"
    End Sub

    Private Sub Cmd9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd9.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "9"
    End Sub

    Private Sub Cmd0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd0.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "0"
    End Sub

    Private Sub CmdMas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdMas.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "+"
    End Sub

    Private Sub CmdMenos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdMenos.Click
        Me.TxtCadena.Text = Me.TxtCadena.Text & "-"
    End Sub

    Private Sub CmdBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdBorrar.Click
        Dim Posicion As Double, i As Double = 0, Longitud As Double = 0, CadenaNueva As String = "", Cadena As String
        Dim PosicionFinal As Double = 0

        Cadena = Me.TxtCadena.Text
        Longitud = Len(Me.TxtCadena.Text)
        Posicion = Me.TxtCadena.SelectionStart
        PosicionFinal = Posicion + Me.TxtCadena.SelectionLength

        If Posicion = PosicionFinal Then
            For i = 0 To Longitud - 1
               If i <> Posicion - 1 Then
                    CadenaNueva = CadenaNueva & Cadena.Chars(i)
                End If
            Next
        Else
            For i = 0 To Longitud - 1
                If i < Posicion Then
                    CadenaNueva = CadenaNueva & Cadena.Chars(i)
                ElseIf i > PosicionFinal - 1 Then
                    CadenaNueva = CadenaNueva & Cadena.Chars(i)
                End If
            Next

        End If

        Me.TxtCadena.Text = CadenaNueva
        If Posicion = 0 Then
            Me.TxtCadena.SelectionStart = 0
        Else
            Me.TxtCadena.SelectionStart = Posicion - 1
        End If
        Me.TxtCadena.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        EscrituraTeclado = Me.TxtCadena.Text
        Me.Close()
    End Sub
End Class