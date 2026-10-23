Imports System.Data.SqlClient
Public Class Autentificacion
    Dim DataReader As SqlDataReader
    Dim SqlCommand As New SqlCommand
    Dim AuxDataset As New DataSet
    Dim AuxDataAdapter As New SqlDataAdapter
    Private Sub Autentificacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PUsuario = ""
        PBodega = ""
        PCliente = ""
        PVendedor = ""
        PCajero = ""

        AuxDataAdapter = New SqlDataAdapter("SELECT [Usuario] ,[Contraseña] ,[Nivel] ,[TipoFactura] ,[CodVendedor] ,[CodCliente] ,[Bodega] ,[SerieFactura] FROM [dbo].[Usuarios]", TempConn)
        AuxDataset = New DataSet()
        AuxDataAdapter.Fill(AuxDataset, "CboUsuario")

        If AuxDataset.Tables(0).Rows.Count < 1 Then
            Form1.Show()
            Me.Close()
        Else
            Me.cboUsuario.DataSource = Me.AuxDataset.Tables("CboUsuarios")
        End If





    End Sub

    Private Sub btnEntrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEntrar.Click
        TempConn.Open()
        '/////////////////// Busco Datos para insertar en la factura (Clientes, vendedor, usuario, bodega, etc...) /////
        '///////////////   ////////////// Usuarios /////    ///////////    ////////////////
        AuxDataAdapter = New SqlDataAdapter("SELECT Top(1) [Usuario] ,[Contraseña] ,[Nivel] ,[TipoFactura] ,[CodVendedor] ,[CodCliente] ,[Bodega] ,[SerieFactura] FROM [dbo].[Usuarios] Where Usuario = '" & Me.cboUsuario.Text & "'", TempConn)
        AuxDataAdapter.Fill(AuxDataset, "Usuarios")

        ' Determino si el Usuario existe 
        If AuxDataset.Tables("Usuarios").Rows.Count > 0 Then

            ' Obtengo la contrasena y comparo con la ingresada 
            Dim Pass As String = AuxDataset.Tables("Usuarios").Rows(0)("Contraseña")
            If Pass = Me.txtContrasena.Text Then


                ' Si existe la contrasena entonces.
                PUsuario = AuxDataset.Tables("Usuarios").Rows(0)("Usuario")

                '///////////////////////////// Bodegas //////////////////////////////////////
                If Not IsDBNull(AuxDataset.Tables("Usuarios").Rows(0)("Bodega")) And AuxDataset.Tables("Usuarios").Rows(0)("Bodega") <> "" Then
                    PBodega = AuxDataset.Tables("Usuarios").Rows(0)("Bodega")
                Else
                    'Consulto si existe una bodega en el usuario si no busco la primera
                    Me.AuxDataAdapter = New SqlDataAdapter("SELECT Top(1) [Cod_Bodega] ,[Nombre_Bodega] ,[CuentaInventario] ,[CuentaIngresoAjustes] ,[CuentaGastosAjustes] ,[CuentaVentas] ,[CuentaCostos] FROM [dbo].[Bodegas]", TempConn)
                    AuxDataAdapter.Fill(AuxDataset, "Bodegas")

                    If AuxDataset.Tables("Bodegas").Rows.Count > 0 Then
                        PBodega = AuxDataset.Tables("Bodegas").Rows(0)("Cod_Bodega")
                    Else
                        MessageBox.Show("No existen Bodegas en el Sistema, por favor agregue una", "Sistema de Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If

                '/////////////////////////////// Clientes ////////////////////////////
                If Not IsDBNull(AuxDataset.Tables("Usuarios").Rows(0)("CodCliente")) And AuxDataset.Tables("Usuarios").Rows(0)("CodCliente") <> "" Then
                    PCliente = AuxDataset.Tables("Usuarios").Rows(0)("CodCliente")
                Else
                    Me.AuxDataAdapter = New SqlDataAdapter("SELECT Top(1) [Cod_Cliente] ,[Nombre_Cliente] ,[Apellido_Cliente] ,[Direccion_Cliente] ,[Telefono] ,[Cod_Cuenta_Cliente] ,[Limite_Credito] ,[RUC] ,[CausaIva]  ,[Credito_Disponible] ,[Año], [Grado] ,[Turno] ,[InventarioFisico] ,[InteresMoratorio] ,[DiasCredito] FROM [dbo].[Clientes]", TempConn)
                    AuxDataAdapter.Fill(AuxDataset, "Clientes")

                    If AuxDataset.Tables("Clientes").Rows.Count > 0 Then
                        PCliente = AuxDataset.Tables("Clientes").Rows(0)("Cod_Cliente")
                    Else
                        MessageBox.Show("No existen Clientes en el Sistema, por favor agregue uno", "Sistema de Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If

                '///////////////////////// Vendedores ///////////////////////////////////////////
                If Not IsDBNull(AuxDataset.Tables("Usuarios").Rows(0)("CodVendedor")) And AuxDataset.Tables("Usuarios").Rows(0)("CodVendedor") <> "" Then
                    PVendedor = AuxDataset.Tables("Usuarios").Rows(0)("CodVendedor")
                Else
                    Me.AuxDataAdapter = New SqlDataAdapter("SELECT Top(1) [Cod_Vendedor] ,[Nombre_Vendedor] ,[Apellido_Vendedor] ,[Direccion_Vendedor] ,[Telefono_Vendedor] ,[Cod_Cuenta_Vendedor] FROM [dbo].[Vendedores]", TempConn)
                    AuxDataAdapter.Fill(AuxDataset, "Vendedores")

                    If AuxDataset.Tables("Vendedores").Rows.Count > 0 Then
                        PVendedor = AuxDataset.Tables("Vendedores").Rows(0)("Cod_Vendedor")
                    Else
                        MessageBox.Show("No existen Vendedores en el Sistema, por favor agregue uno", "Sistema de Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If

                '//////////////// Cajero ///////////////////////

                AuxDataAdapter = New SqlDataAdapter("SELECT Top(1) [Cod_Cajero] ,[Nombre_Cajero] ,[Apellido_Cajero] ,[Direccion_Cajero] ,[Telefono_Cajero] FROM [dbo].[Cajeros]", TempConn)
                AuxDataAdapter.Fill(AuxDataset, "Cajeros")
                If AuxDataset.Tables("Cajeros").Rows.Count > 0 Then
                    PCajero = AuxDataset.Tables("Cajeros").Rows(0)("Cod_Cajero")
                Else
                    MessageBox.Show("No existen Cajeros en el Sistema, porfavor agregue uno.", "Sistema Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                MessageBox.Show("Contrasena incorrecta", "Sistema de Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.txtContrasena.Text = ""

            End If
            TempConn.Close()

            Form1.Show()
            Me.Close()
        Else
            '//////////// si el usuario no existe, entonces... //////////////////
            MessageBox.Show("No Existe el usuario en el sistema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.cboUsuario.Text = ""
            Me.txtContrasena.Text = ""
            TempConn.Close()

        End If


    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class