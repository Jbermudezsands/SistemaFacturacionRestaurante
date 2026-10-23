Imports System.Data.SqlClient
Module Globales
    ' Public TempConn As New SqlConnection("Password=P@ssword;Persist Security Info=True;User ID=sa;Initial Catalog=SistemaFacturacion;Data Source=USER-PC\SQL2005")
    Public Conexion As String
    Public RutaBD As String = My.Application.Info.DirectoryPath & "\CntFacturacion.dll", ConexionAccess As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & RutaBD & " "
    Public MiConexion As New SqlClient.SqlConnection
    Public LogoEmpresa As String

    Public TempConn As New SqlConnection("Password=P@ssword;Persist Security Info=True;User ID=sa;Initial Catalog=FacturacionFincaSantaClara;Data Source=DESKTOP-E2SQPU1\SQL2005")
    Public PUsuario, PBodega, PCliente, PVendedor, PCajero As String
    Public Version As String, Quien As String


End Module
