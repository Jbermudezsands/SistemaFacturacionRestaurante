Option Strict On
Option Explicit On

Imports System.Collections.Generic

'=====================================================================================================
'  ENSAMBLE DEL DIA - MODELOS
'  Productos tipo "Ensamble" facturados en un dia, su receta y el resultado del proceso.
'=====================================================================================================

''' <summary>Error de regla de negocio: el mensaje es apto para mostrarse tal cual al usuario.</summary>
Public Class EnsambleDiaNegocioException
    Inherits Exception

    Public Sub New(ByVal mensaje As String)
        MyBase.New(mensaje)
    End Sub
End Class

''' <summary>Componente de la receta de un producto ensamble, ya multiplicado por las unidades a ensamblar.</summary>
Public Class EnsambleDiaComponente

    Public Property Codigo() As String
    Public Property Descripcion() As String
    Public Property TipoProducto() As String
    ''' <summary>Cantidad requerida por cada unidad ensamblada (Componentes.Requerido).</summary>
    Public Property CantidadUnidad() As Double
    ''' <summary>Cantidad a descargar: CantidadUnidad x unidades a ensamblar del producto.</summary>
    Public Property CantidadTotal() As Double
    Public Property CostoUnitario() As Double
    Public Property Existencia() As Double

    Public ReadOnly Property CostoTotal() As Double
        Get
            Return CantidadTotal * CostoUnitario
        End Get
    End Property

    ''' <summary>Los servicios no mueven inventario: no se descargan ni se costean.</summary>
    Public ReadOnly Property EsServicio() As Boolean
        Get
            Return TipoProducto = "Servicio"
        End Get
    End Property

    Public Sub New()
        Codigo = ""
        Descripcion = ""
        TipoProducto = ""
    End Sub
End Class

''' <summary>Producto tipo Ensamble facturado en el dia con su receta.</summary>
Public Class EnsambleDiaProducto

    Public Property Codigo() As String
    Public Property Descripcion() As String
    Public Property CodComponente() As Double
    ''' <summary>Unidades facturadas en el dia (facturas no canceladas).</summary>
    Public Property Vendida() As Double
    ''' <summary>Unidades que ya se ensamblaron en procesos anteriores del mismo dia.</summary>
    Public Property Procesada() As Double
    Public Property Componentes() As List(Of EnsambleDiaComponente)

    ''' <summary>Unidades que faltan por ensamblar.</summary>
    Public ReadOnly Property Pendiente() As Double
        Get
            Return Math.Max(0.0, Vendida - Procesada)
        End Get
    End Property

    Public ReadOnly Property SinReceta() As Boolean
        Get
            Return Componentes.Count = 0
        End Get
    End Property

    Public Sub New()
        Codigo = ""
        Descripcion = ""
        Componentes = New List(Of EnsambleDiaComponente)
    End Sub
End Class

''' <summary>Resultado de un proceso: documentos generados y su costo.</summary>
Public Class EnsambleDiaResultado

    Public Property IdEnsambleDia() As Integer
    Public Property NumeroSalida() As String
    Public Property NumeroCompra() As String
    Public Property TotalCosto() As Double
    Public Property ProductosProcesados() As Integer
    ''' <summary>Productos que se omitieron por no tener componentes.</summary>
    Public Property SinReceta() As List(Of String)

    Public Sub New()
        NumeroSalida = ""
        NumeroCompra = ""
        SinReceta = New List(Of String)
    End Sub
End Class

''' <summary>Parametros de un proceso de ensamble del dia.</summary>
Public Class EnsambleDiaSolicitud

    Public Property Fecha() As Date
    Public Property BodegaOrigen() As String
    Public Property BodegaDestino() As String
    Public Property Usuario() As String

    Public Sub New()
        Fecha = Date.Today
        BodegaOrigen = ""
        BodegaDestino = ""
        Usuario = ""
    End Sub
End Class
