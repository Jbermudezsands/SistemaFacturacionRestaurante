Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Public Class ArepComanda 

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        Me.LblImpresion.Text = "Impreso: " & Format(DateTime.Now, "dd/MM/yyyy hh:mm:ss tt")
    End Sub

End Class 
