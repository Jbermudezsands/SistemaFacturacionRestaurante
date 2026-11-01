Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Data

''' <summary>Arma y muestra el reporte ArepEnsambleDia a partir del plan del dia.</summary>
Public NotInheritable Class EnsambleDiaImpresion

    Private Sub New()
    End Sub

    Public Shared Sub Mostrar(ByVal servicio As EnsambleDiaServicio, ByVal plan As List(Of EnsambleDiaProducto), _
                              ByVal fecha As Date, ByVal bodegaOrigen As String, ByVal bodegaDestino As String)
        Dim datos As DataTable = servicio.ConstruirTablaReporte(plan)
        If datos.Rows.Count = 0 Then
            Throw New EnsambleDiaNegocioException("No hay productos pendientes de ensamblar para imprimir.")
        End If

        Dim reporte As New ArepEnsambleDia()

        Dim empresa As DataTable = servicio.ObtenerDatosEmpresa()
        If empresa.Rows.Count > 0 AndAlso Not IsDBNull(empresa.Rows(0)("Nombre_Empresa")) Then
            reporte.LblTitulo.Text = Convert.ToString(empresa.Rows(0)("Nombre_Empresa"))
        End If

        reporte.LblFecha.Text = "Fecha: " & fecha.ToString("dd/MM/yyyy")
        reporte.LblBodegas.Text = "Sale de bodega " & bodegaOrigen & "  -  Entra a bodega " & bodegaDestino

        reporte.DataSource = datos
        reporte.Document.Name = "ENSAMBLE DEL DIA"
        reporte.Run(False)
        reporte.Show()
    End Sub

End Class
