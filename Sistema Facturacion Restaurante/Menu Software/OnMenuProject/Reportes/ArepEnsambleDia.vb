Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

''' <summary>
''' Ensamble del dia: por cada producto Ensamble facturado, la cantidad a ensamblar y los componentes
''' con la cantidad por unidad y la cantidad total a usar.
''' </summary>
Public Class ArepEnsambleDia

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        Me.LblPagina.Text = "Página " & Me.PageNumber.ToString()
    End Sub

End Class
