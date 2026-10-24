Imports System.Data.SqlClient
Imports System.Data
'Imports System.Data.Linq
Imports System.IO
Imports System.Drawing
Imports Microsoft.VisualBasic.Logging
Imports System.Drawing.Printing
Public Class Form1
    Dim AuxDataTable As DataTable
    Public Actualiza As Boolean = False
    Dim DS As New DataSet
    Dim DA As New SqlDataAdapter
    '///////////////////// VARIABLES GLOBALES
    Public CodVendedor, NombreVendedor As String
    Public FechaFactura As Date = Nothing

    Dim DTLineas As New DataTable
    Dim DALineas As New SqlDataAdapter
    Dim CommandString As String
    Dim Posicion As Integer
    Dim Contador As Integer
    Dim FilaConsulta() As DataRow
    Dim PrecioVentaCor As Double
    Dim Recorded As Integer = 0
    Dim Propina As Double = 0
    Dim DTProductos As New DataTable
    Dim DAProductos As New SqlDataAdapter
    Dim Iva, Subtotal, Total As Double
    Dim Builder As New SqlCommandBuilder
    Dim SqlCommand As New SqlCommand
    Dim DataReader As SqlDataReader
    Public NumeroFactura As String
    Dim DsAux As New DataSet
    Dim DaAux As New SqlDataAdapter
    Dim AuxDataAdapter As New SqlDataAdapter
    Dim AuxDataset As New DataSet
    Dim NFact As Integer

    Dim FlowPProductos As FlowLayoutPanel
    '/////////////////////////////////////////////////////////

    Public Sub DeletePanelItems(ByVal Pnl As FlowLayoutPanel)
        While Pnl.Controls.Count > 0 'Determine if the flow contains any controls
            Dim controltoremove = Pnl.Controls(0)
            Pnl.Controls.Remove(controltoremove)
            controltoremove.Dispose()
        End While
    End Sub
    Public Sub GrabarFacturas(Numer_Factura As String, Fecha_Factura As Date, Tipo_Factura As String)

    End Sub


    Public Sub CrearProductos(ByVal btnFP1 As Button)
        Dim Foto As String, myImagenConsulta As Image

        ReiniciarProductos() 'nuevo panel de productos

        CommandString = "Select RutaCompartida From DatosEmpresa"
        Dim dtEmpresa As New DataTable
        Dim daEmpresa As New SqlDataAdapter(CommandString, TempConn)
        daEmpresa.Fill(dtEmpresa)


        '/////////////// CARGO LA CONSULTA DE LOS PRODUCTOS /////////////////

        CommandString = "SELECT  Cod_Productos, Tipo_Producto, Descripcion_Producto, Cod_Linea, Precio_venta, Precio_Lista, Foto FROM Productos Where Cod_Linea = " & btnFP1.Name & " AND (Tipo_Producto <> N'Insumos') AND (Activo = N'Activo')"
        DAProductos = New SqlDataAdapter(CommandString, TempConn)
        DTProductos = New DataTable()
        DAProductos.Fill(DTProductos)

        '/////////////// RECORRO LA CONSULTA Y CREO UN BOTON POR CADA UNO DE LOS RESULTADOS /////////
        Posicion = 0

        If DTProductos.Rows.Count <> 0 Then
            Do While (Posicion < DTProductos.Rows.Count)
                Dim btn As C1.Win.C1Input.C1Button = New C1.Win.C1Input.C1Button
                AddHandler btn.Click, AddressOf ClickFPP
                btn.Text = Mid(DTProductos.Rows(Posicion)("Descripcion_Producto"), 1, 45)
                btn.Tag = DTProductos.Rows(Posicion)("Descripcion_Producto")
                btn.Name = DTProductos.Rows(Posicion)("Cod_Productos")
                EstilizarTarjeta(btn, False)
                btn.ImageAlign = ContentAlignment.MiddleCenter

                btn.TextAlign = ContentAlignment.BottomCenter

                If Not IsDBNull(DTProductos.Rows(Posicion)("Foto")) Then
                    Foto = DTProductos.Rows(Posicion)("Foto")
                    myImagenConsulta = BytesToImagen(StringtoByte(Foto))

                    EstilizarTarjeta(btn, True)
                    btn.BackgroundImage = myImagenConsulta
                    btn.BackgroundImageLayout = ImageLayout.Stretch
                    btn.Text = ""
                Else
                    '////////////SI NO TIENE IMAGEN SOLO MUESTRO EL TEXTO //////

                    btn.TextAlign = ContentAlignment.MiddleCenter

                    'btn.VisualStyle = C1.Win.C1Input.VisualStyle.System
                    'btn.BackgroundImage = My.Resources.NoDisponible
                    'btn.BackgroundImageLayout = ImageLayout.Stretch

                End If



                Me.FlowPProductos.Controls.Add(btn)
                Posicion = Posicion + 1
            Loop
        End If
    End Sub

    Public Sub CargarCuenta(ByVal NumeroFactura As String)
        BackToLife(NumeroFactura)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConstruirDiseno()
        Application.DoEvents()
        Me.lblNombreVendedor.Text = NombreVendedor
        Dim TablaClientes As New DataTable, Foto As String, myImagenConsulta As Image
        Dim AdapterClientes As New SqlDataAdapter("SELECT  Cod_Cliente as Codigo, Nombre_Cliente as Nombre FROM         Clientes  WHERE     (Limite_Credito <= 0)", TempConn)
        Dim Ancho As Double = 1386, Largo As Double = 734
        AdapterClientes.Fill(TablaClientes)

        Me.cboMesas.DataSource = TablaClientes
        Me.cboMesas.ItemHeight = 25
        cboMesas.DropDownWidth = 210
        Me.cboMesas.DisplayMember = "Nombre"
        Me.cboMesas.ValueMember = "Codigo"


        AuxDataAdapter = New SqlDataAdapter("Select ConsecutivoFacSerie from DatosEmpresa", TempConn)
        Dim AuxTabla As New DataTable
        AuxDataAdapter.Fill(AuxTabla)

        If AuxTabla.Rows(0)("ConsecutivoFacSerie") = False Then
            Me.Label13.Visible = False
            Me.cboSerie.Visible = False
        Else
            Me.cboSerie.Text = "A"
        End If

        If LogoEmpresa <> "" Then
            myImagenConsulta = BytesToImagen(StringtoByte(LogoEmpresa))
            ImgFoto.Image = myImagenConsulta
        End If


        AuxDataAdapter = New SqlDataAdapter("SELECT [Usuario] ,[Contraseña] ,[Nivel] ,[TipoFactura] ,[CodVendedor] ,[CodCliente] ,[Bodega] ,[SerieFactura] FROM [dbo].[Usuarios]", TempConn)
        AuxDataset = New DataSet()
        AuxDataAdapter.Fill(AuxDataset, "CboUsuario")

        If AuxDataset.Tables("CboUsuario").Rows.Count < 1 Then

            '///////////////////////////// Bodegas /////////////////////////
            Me.AuxDataAdapter = New SqlDataAdapter("SELECT Top(1) [Cod_Bodega] ,[Nombre_Bodega] ,[CuentaInventario] ,[CuentaIngresoAjustes] ,[CuentaGastosAjustes] ,[CuentaVentas] ,[CuentaCostos] FROM [dbo].[Bodegas]", TempConn)
            AuxDataAdapter.Fill(AuxDataset, "Bodegas")

            If AuxDataset.Tables("Bodegas").Rows.Count > 0 Then
                PBodega = AuxDataset.Tables("Bodegas").Rows(0)("Cod_Bodega")
            Else
                MessageBox.Show("No existen Bodegas en el Sistema, por favor agregue una", "Sistema de Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.btGuardar.Enabled = False
            End If

            '/////////////////////// Clientes ///////////////////////
            Me.AuxDataAdapter = New SqlDataAdapter("SELECT Top(1) [Cod_Cliente] ,[Nombre_Cliente] ,[Apellido_Cliente] ,[Direccion_Cliente] ,[Telefono] ,[Cod_Cuenta_Cliente] ,[Limite_Credito] ,[RUC] ,[CausaIva]  ,[Credito_Disponible] ,[Año], [Grado] ,[Turno] ,[InventarioFisico] ,[InteresMoratorio] ,[DiasCredito] FROM [dbo].[Clientes]", TempConn)
            AuxDataAdapter.Fill(AuxDataset, "Clientes")

            If AuxDataset.Tables("Clientes").Rows.Count > 0 Then
                PCliente = AuxDataset.Tables("Clientes").Rows(0)("Cod_Cliente")
            Else
                MessageBox.Show("No existen Clientes en el Sistema, por favor agregue uno", "Sistema de Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.btGuardar.Enabled = False
            End If

            '//////////////////////// Vendedores /////////////////////////////
            Me.AuxDataAdapter = New SqlDataAdapter("SELECT Top(1) [Cod_Vendedor] ,[Nombre_Vendedor] ,[Apellido_Vendedor] ,[Direccion_Vendedor] ,[Telefono_Vendedor] ,[Cod_Cuenta_Vendedor] FROM [dbo].[Vendedores]", TempConn)
            AuxDataAdapter.Fill(AuxDataset, "Vendedores")

            If AuxDataset.Tables("Vendedores").Rows.Count > 0 Then
                PVendedor = AuxDataset.Tables("Vendedores").Rows(0)("Cod_Vendedor")
            Else
                MessageBox.Show("No existen Vendedores en el Sistema, por favor agregue uno", "Sistema de Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                btGuardar.Enabled = False
            End If

            '////////////////////////////// Cajeros //////////////////////////////////////
            AuxDataAdapter = New SqlDataAdapter("SELECT Top(1) [Cod_Cajero] ,[Nombre_Cajero] ,[Apellido_Cajero] ,[Direccion_Cajero] ,[Telefono_Cajero] FROM [dbo].[Cajeros]", TempConn)
            AuxDataAdapter.Fill(AuxDataset, "Cajeros")
            If AuxDataset.Tables("Cajeros").Rows.Count > 0 Then
                PCajero = AuxDataset.Tables("Cajeros").Rows(0)("Cod_Cajero")
            Else
                MessageBox.Show("No existen Cajeros en el Sistema, porfavor agregue uno.", "Sistema Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Error)
                btGuardar.Enabled = False
            End If
        End If


        '///////////////  AGREGO EL NUEVO flowPanel2 //////////////
        ReiniciarProductos()


        ' CrearNumeroFactura()
        TempConn.Close()
        TempConn.Open()


        '///////////////////////////////////////////////////////////////////
        If Actualiza = False Then
            BackToLife("")
        End If


        If TempConn.State = ConnectionState.Open Then
            'MessageBox.Show("Conexion abierta")

            '///////////////////////// LLENO EL DATASET DE LAS LINEAS //////////////////////
            CommandString = "SELECT  Cod_Linea, Descripcion_Linea, RutaImagen, Foto FROM  Lineas"
            DALineas = New SqlDataAdapter(CommandString, TempConn)
            DTLineas = New DataTable()
            DALineas.Fill(DTLineas)

            '///////////////// RECORRO EL DATASET Y ASIGNO UN BOTON A CADA UNO DE LOS RESULTADOS
            If DTLineas.Rows.Count <> 0 Then
                Do While (Posicion < DTLineas.Rows.Count)
                    Dim btn As C1.Win.C1Input.C1Button = New C1.Win.C1Input.C1Button
                    AddHandler btn.Click, AddressOf ClickFP1
                    btn.Text = Mid(DTLineas.Rows(Posicion)("Descripcion_Linea").ToString, 1, 20)
                    btn.Name = DTLineas.Rows(Posicion)("Cod_Linea")
                    EstilizarTarjeta(btn, False)

                    btn.TextAlign = ContentAlignment.BottomCenter
                    btn.ImageAlign = ContentAlignment.TopCenter
                    'If Not IsDBNull(DTLineas.Rows(Posicion)("RutaImagen")) Then
                    '    Dim Ruta As String = DTLineas.Rows(Posicion)("RutaImagen")
                    '    If Ruta <> "" Then
                    '        ' btn.Image = Image.FromFile(Ruta)
                    '        btn.VisualStyle = C1.Win.C1Input.VisualStyle.System
                    '        btn.BackgroundImage = Bitmap.FromFile(Ruta)
                    '        btn.BackgroundImageLayout = ImageLayout.Stretch
                    '        btn.Text = ""
                    '    End If
                    'End If

                    If Not IsDBNull(DTLineas.Rows(Posicion)("Foto")) Then
                        Foto = DTLineas.Rows(Posicion)("Foto")
                        myImagenConsulta = BytesToImagen(StringtoByte(Foto))

                        EstilizarTarjeta(btn, True)
                        btn.BackgroundImage = myImagenConsulta
                        btn.BackgroundImageLayout = ImageLayout.Stretch
                        btn.Text = ""
                    Else
                        EstilizarTarjeta(btn, True)
                        btn.BackgroundImage = My.Resources.NoDisponible
                    End If



                    Me.FlowLayoutPanel1.Controls.Add(btn)

                    Posicion = Posicion + 1
                Loop
            End If

        End If


        Me.ckExonerar.Checked = False
    End Sub


    Public Sub ClickFP1(ByVal Sender As Object, ByVal e As EventArgs)
        'DeletePanelItems(FlowLayoutPanel2)

        '//////////////// ESTE BOTON VIENE DEL flowPanel1, capturo el sender y lo consulto //////
        Dim BtnFL1 As Button
        BtnFL1 = CType(Sender, Button)
        CrearProductos(BtnFL1)
    End Sub


    Public Sub ClickFPP(ByVal Sender As Object, ByVal e As EventArgs)

        If Me.cboMesas.Text = "" Then
            MessageBox.Show("Tenes que seleccionar una mesa", "Zeus Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        '///// asigno los datos del boton a un nuevo boton
        Dim Existe As Boolean = False
        Dim BtnFLP As Button
        BtnFLP = CType(Sender, Button)

        Dim Counter As Integer = 0

        Do While (Counter < DS.Tables(0).Rows.Count)
            If Not DS.Tables(0).Rows(Counter).RowState = DataRowState.Deleted Then
                If DS.Tables(0).Rows(Counter)(0).ToString = BtnFLP.Name Then
                    Existe = True

                    DS.Tables(0).Rows(Counter)(2) = Convert.ToInt32(DS.Tables(0).Rows(Counter)(2)) + 1
                    DS.Tables(0).Rows(Counter)(4) = Convert.ToDouble(DS.Tables(0).Rows(Counter)(2)) * Convert.ToDouble(DS.Tables(0).Rows(Counter)(3))

                    Counter = 99999
                End If
            End If
            Counter = Counter + 1
        Loop

        '/////////////// si el producto no existe en el grid entonces lo agrego //////////////
        If Existe = False Then

            '//////////// Me muevo al ultimo registro //////////
            'For index As Integer = 0 To DS.Tables(0).Rows.Count + 1
            '    Me.BindingSource1.MoveLast()
            '    Dim poss As Integer = Me.BindingSource1.Position
            '    Me.C1TrueDBGrid1.Row = poss
            '    Me.C1TrueDBGrid1.Row = Me.C1TrueDBGrid1.Row + 1
            '    Me.C1TrueDBGrid1.Select()

            'Next

            '//// Me muevo al ultimo registro del datatable //////

            Me.DS.Tables(0).Rows.Add()

            '/// Obtengo el precio de Venta en Cordobas y en Dolares
            Me.FilaConsulta = DTProductos.Select("Cod_Productos = '" & BtnFLP.Name & "'")
            PrecioVentaCor = FilaConsulta(0).Item(4).ToString()

            '//////////////// agrego los datos al grid /////////////////
            If Me.C1TrueDBGrid1.RowCount > 0 Then
                Me.DS.Tables(0).Rows(Me.DS.Tables(0).Rows.Count - 1)(0) = BtnFLP.Name
                Me.DS.Tables(0).Rows(Me.DS.Tables(0).Rows.Count - 1)(2) = 1
                Me.DS.Tables(0).Rows(Me.DS.Tables(0).Rows.Count - 1)(3) = PrecioVentaCor
                Me.DS.Tables(0).Rows(Me.DS.Tables(0).Rows.Count - 1)(1) = BtnFLP.Tag
                Me.DS.Tables(0).Rows(Me.DS.Tables(0).Rows.Count - 1)(4) = PrecioVentaCor
                'Me.C1TrueDBGrid1.Bookmark = Me.C1TrueDBGrid1.RowCount
                ' Me.BindingSource1.Position = C1TrueDBGrid1.Bookmark
            End If

        End If
        Calcular()
    End Sub




    Private Sub btnDisminuir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisminuir.Click


        If Me.C1TrueDBGrid1.RowCount > 0 Then
            Dim foundRows() As Data.DataRow = DS.Tables(0).Select("Cod_Producto = '" & Me.C1TrueDBGrid1.Columns("Cod_Producto").Text & "'")
            If foundRows.Length > 0 Then
                Dim pos As Integer = DS.Tables(0).Rows.IndexOf(foundRows(0))

                If DS.Tables(0).Rows(pos)(2) <> 0 Then

                    DS.Tables(0).Rows(pos)(2) = Convert.ToInt32(DS.Tables(0).Rows(pos)(2)) - 1
                    DS.Tables(0).Rows(pos)(4) = Convert.ToDouble(DS.Tables(0).Rows(pos)(2)) * Convert.ToDouble(DS.Tables(0).Rows(pos)(3))
                    If CDbl(DS.Tables(0).Rows(pos)(2)) = 0 Then
                        DS.Tables(0).Rows(pos).Delete()
                    End If
                    Calcular()

                End If
            End If
        End If

    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click

        If Me.C1TrueDBGrid1.RowCount > 0 Then
            Dim foundRows() As Data.DataRow = DS.Tables(0).Select("Cod_Producto = '" & Me.C1TrueDBGrid1.Columns("Cod_Producto").Text & "'")
            If foundRows.Length > 0 Then
                Dim pos As Integer = DS.Tables(0).Rows.IndexOf(foundRows(0))
                DS.Tables(0).Rows(pos)(2) = Convert.ToInt32(DS.Tables(0).Rows(pos)(2)) + 1
                DS.Tables(0).Rows(pos)(4) = Convert.ToDouble(DS.Tables(0).Rows(pos)(2)) * Convert.ToDouble(DS.Tables(0).Rows(pos)(3))
                Calcular()

            End If
        End If

    End Sub


    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        If Me.C1TrueDBGrid1.RowCount > 0 Then
            Dim foundRows() As Data.DataRow = DS.Tables(0).Select("Cod_Producto = '" & Me.C1TrueDBGrid1.Columns("Cod_Producto").Text & "'")
            If foundRows.Length > 0 Then
                Dim pos As Integer = DS.Tables(0).Rows.IndexOf(foundRows(0))
                Dim producto As String = Me.C1TrueDBGrid1.Columns("Cod_Producto").Text
                Me.DS.Tables(0).Rows(pos)("Cod_Producto") = ""
                Me.DS.Tables(0).Rows(pos)("Fecha_Factura") = Format(DateTime.Now, "dd/MM/yyyy")
                Me.DS.Tables(0).Rows(pos).Delete()
                Calcular()
            End If
        End If

    End Sub


    Private Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSiguiente.Click
        'If (BindingSource1.Position + 1 < BindingSource1.Count) Then
        '    BindingSource1.MoveNext()
        '    Me.C1TrueDBGrid1.Bookmark = Me.BindingSource1.Position
        '    Me.BindingSource1.Position = C1TrueDBGrid1.Bookmark
        'Else
        '    BindingSource1.MoveFirst()
        '    Me.C1TrueDBGrid1.Bookmark = Me.BindingSource1.Position
        '    Me.BindingSource1.Position = C1TrueDBGrid1.Bookmark
        'End If

        Me.C1TrueDBGrid1.MoveNext()
    End Sub

    Private Sub btnAtras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAtras.Click
        'If Me.BindingSource1.Count = 0 Then
        '    Me.BindingSource1.MoveLast()
        '    Me.C1TrueDBGrid1.Bookmark = Me.BindingSource1.Position
        '    Me.BindingSource1.Position = C1TrueDBGrid1.Bookmark
        'Else
        '    BindingSource1.MovePrevious()
        '    Me.C1TrueDBGrid1.Bookmark = Me.BindingSource1.Position
        '    Me.BindingSource1.Position = C1TrueDBGrid1.Bookmark
        'End If
        Me.C1TrueDBGrid1.MovePrevious()

    End Sub

    Private Sub C1Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button1.Click
        If DS.Tables(0).Rows.Count > 0 Then
            If MessageBox.Show("¿Querés cerrar sesión? Si aceptás podés perder datos que no guardaste", "Zeus Facturación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If
        CodVendedor = ""
        NombreVendedor = ""
        FormLog.Show()
        Me.Close()
    End Sub
    Public Sub Calcular()
        Propina = 0
        Recorded = 0
        Subtotal = 0
        Iva = 0
        Total = 0
        If IsNothing(DS) Then
            Exit Sub
        End If
        If DS.Tables.Count = 0 Then
            Exit Sub
        End If
        Do While Recorded < DS.Tables(0).Rows.Count
            If Not DS.Tables(0).Rows(Recorded).RowState = DataRowState.Deleted Then
                Subtotal = Convert.ToDouble(DS.Tables(0).Rows(Recorded)(4)) + Subtotal
            End If
            Recorded = Recorded + 1
        Loop
        If Me.ckExonerar.CheckState = CheckState.Checked Then
            Iva = Subtotal * 0
        Else
            Iva = Subtotal * 0.15
        End If


        If Me.ckPropina.CheckState = CheckState.Checked Then
            Dim AdapterDE As New SqlDataAdapter("Select PorcentajePropina from DatosEmpresa", TempConn)
            Dim TablaDE As New DataTable
            AdapterDE.Fill(TablaDE)

            Propina = Subtotal * (CDbl(TablaDE.Rows(0)("PorcentajePropina")) / 100)
            Total = Propina + Iva + Subtotal
        Else
            Total = Subtotal + Iva
        End If

        '//////// Asigno valores a los textboxs //////////

        Me.txtSubTotal.Text = Format(Subtotal, "##,##0.00")
        Me.txtIva.Text = Format(Iva, "##,##0.00")
        Me.txtTotal.Text = Format(Total, "##,##0.00")
        Me.txtpropina.Text = Format(Propina, "##,##0.00")
    End Sub

    Private Sub Guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGuardar.Click
        Dim Impresora_Defecto As String
        Dim ImprimeFacturaPreview As Boolean = ObtenerImprimirSinPreview()


        If Me.cboMesas.Text = "" Then
            MessageBox.Show("Tenès que seleccionar una mesa", "Zeus Facturaciòn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.C1TrueDBGrid1.RowCount = 0 Then
            MessageBox.Show("Tenés que agregar productos para guardar la factura", "Zeus Facturación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        '///////// Actualizo Detalle Factura ///////////////

        If Not DS.Tables(0).Rows.Count = 0 Then
            If DS.Tables(0).Rows.Count <> 0 Then
                '////////////////// Guardo datos en Tabla Factura ///////////
                TempConn.Close()
                If Actualiza = False Then
                    'Me.CrearNumeroFactura()
                    NumeroFactura = Consecutivo_Factura("Factura")
                    SqlCommand = New SqlCommand("INSERT INTO [Facturas] ([Numero_Factura] ,[Fecha_Factura] ,[Tipo_Factura] ,[MonedaFactura] ,[Cod_Cliente] ,[Cod_Bodega] ,[Cod_Vendedor] ,[Cod_Cajero] ,[Nombre_Cliente] ,[Apellido_Cliente] ,[Direccion_Cliente] ,[Telefono_Cliente] ,[Fecha_Vencimiento] ,[Observaciones], [SubTotal] ,[IVA] ,[Pagado] ,[NetoPagar], [MontoCredito] ,[Contabilizado] ,[Activo] ,[Cancelado] ,[MetodoPago] ,[Exonerado] ,[Descuentos] ,[Marca] ,[TransferenciaProcesada] ,[MonedaImprime] ,[FechaPago], [Propina]) VALUES ('" & Me.NumeroFactura & "', '" & Format(DateTime.Now, "yyyyMMdd") & "', 'Factura', 'Cordobas','" & Me.cboMesas.SelectedValue & "', '" & PBodega & "', '" & CodVendedor & "', '" & PCajero & "', '" & Me.txtNombre.Text & "', '" & Me.txtRuc.Text & "', '" & Me.txtDireccion.Text & "', '" & Me.txtTelefono.Text & "', '" & Format(DateTime.Now, "yyyyMMdd") & "', '" & Me.txtObservacion.Text & "', " & CDbl(Me.txtSubTotal.Text) & ", " & CDbl(Me.txtIva.Text) & ", " & CDbl(Me.txtTotal.Text) & ", " & CDbl(Me.txtTotal.Text) & ", 0, 'False', 'True', 'False', 'Contado', '" & Me.ckExonerar.CheckState & "', 0, 'True', 'False', 'Cordobas', '" & Format(DateTime.Now, "yyyyMMdd") & "', " & CDbl(Me.txtpropina.Text) & "  )", TempConn)
                Else
                    SqlCommand = New SqlCommand("Update Facturas set Cod_Cliente = '" & Me.cboMesas.SelectedValue & "', Cod_Vendedor = '" & CodVendedor & "', Nombre_Cliente = '" & Me.txtNombre.Text & "', Apellido_Cliente = '" & Me.txtRuc.Text & "', Direccion_Cliente = '" & Me.txtDireccion.Text & "', Telefono_Cliente = '" & Me.txtTelefono.Text & "', Observaciones = '" & Me.txtObservacion.Text & "', SubTotal = " & CDbl(Me.txtSubTotal.Text) & ", IVA = " & CDbl(Me.txtIva.Text) & ", Pagado = " & CDbl(Me.txtTotal.Text) & ", NetoPagar = " & CDbl(Me.txtTotal.Text) & ", Exonerado = '" & Me.ckExonerar.CheckState & "', CalculaPropina = '" & Me.ckPropina.CheckState & "', Propina = '" & CDbl(Me.txtpropina.Text) & "' Where Numero_Factura = '" & NumeroFactura & "'", TempConn)
                End If

                TempConn.Open()
                SqlCommand.ExecuteNonQuery()
                TempConn.Close()
                '//////////////// Guardo detalle de Factura /////////////////


                If Actualiza = False Then

                    Contador = 0
                    Do While Contador < DS.Tables(0).Rows.Count
                        If Not DS.Tables(0).Rows(Contador).RowState = DataRowState.Deleted Then
                            Me.DS.Tables(0).Rows(Contador)("Numero_Factura") = NumeroFactura
                            Me.DS.Tables(0).Rows(Contador)("Fecha_Factura") = Format(DateTime.Now, "dd/MM/yyyy")
                            Me.DS.Tables(0).Rows(Contador)("Tipo_Factura") = "Factura"
                            Me.DS.Tables(0).Rows(Contador)("Importe") = Me.DS.Tables(0).Rows(Contador)("Precio_Neto")
                        End If
                        Contador = Contador + 1
                    Loop
                    DA.Update(DS.Tables(0))

                    If Me.cboSerie.Visible = True Then
                        Dim Command As New SqlCommand("Update ConsecutivoSerie set Factura = " & NFact & " Where Serie = '" & Me.cboSerie.Text & "'", TempConn)
                        TempConn.Open()
                        Command.ExecuteNonQuery()
                        TempConn.Close()
                    Else
                        Dim Command As New SqlCommand("Update Consecutivos set Factura = " & NFact & "", TempConn)
                        TempConn.Open()
                        Command.ExecuteNonQuery()
                        TempConn.Close()
                    End If
                    NFact = 0
                Else
                    Contador = 0
                    Do While Contador < DS.Tables(0).Rows.Count
                        If Not DS.Tables(0).Rows(Contador).RowState = DataRowState.Deleted Then
                            Me.DS.Tables(0).Rows(Contador)("Numero_Factura") = NumeroFactura
                            Me.DS.Tables(0).Rows(Contador)("Fecha_Factura") = Format(FechaFactura, "dd/MM/yyyy")
                            Me.DS.Tables(0).Rows(Contador)("Tipo_Factura") = "Factura"
                            Me.DS.Tables(0).Rows(Contador)("Importe") = Me.DS.Tables(0).Rows(Contador)("Precio_Neto")
                        Else

                        End If
                        Contador = Contador + 1
                    Loop
                    DA.Update(DS.Tables(0))
                End If

                '////// Comienzo el proceso de guardado en el historial para luego comparar

                'Creo Tabla historial para guardar
                Dim AdapterHistorial As New SqlDataAdapter("SELECT Id, IdContGuardado, Numero_Factura, Cod_Producto, Cantidad, Descripcion_Producto    FROM         Historico_Comandas Where Id = -145", TempConn)
                Dim BuilderHistorial As New SqlCommandBuilder(AdapterHistorial)

                Dim TablaHistorial As New DataTable
                AdapterHistorial.Fill(TablaHistorial)

                ''Busco Contador Actual
                Dim CodHistorialContador As Integer

                If Me.Actualiza = True Then
                    AuxDataTable = New DataTable
                    Me.AuxDataAdapter = New SqlDataAdapter("SELECT top(1) Id, IdContGuardado, Numero_Factura, Cod_Producto, Cantidad  FROM   Historico_Comandas Where Numero_Factura = '" & Me.NumeroFactura & "'  ORDER BY IdContGuardado DESC ", TempConn)
                    AuxDataAdapter.Fill(AuxDataTable)

                    If AuxDataTable.Rows.Count <> 0 Then
                        CodHistorialContador = AuxDataTable.Rows(0)("IdContGuardado")
                        CodHistorialContador = CodHistorialContador + 1
                    Else
                        MsgBox("Comanda Anulada o Inexistente", vbExclamation, "Zeus Facturacion")
                    End If
                Else
                    CodHistorialContador = 1
                End If

                Dim i As Integer = 0
                Do While i < DS.Tables(0).Rows.Count
                    If Not DS.Tables(0).Rows(i).RowState = DataRowState.Deleted Then
                        TablaHistorial.Rows.Add()
                        TablaHistorial.Rows(TablaHistorial.Rows.Count - 1)("IdContGuardado") = CodHistorialContador
                        TablaHistorial.Rows(TablaHistorial.Rows.Count - 1)("Numero_Factura") = NumeroFactura
                        TablaHistorial.Rows(TablaHistorial.Rows.Count - 1)("Cod_Producto") = Me.DS.Tables(0).Rows(i)("Cod_Producto")
                        TablaHistorial.Rows(TablaHistorial.Rows.Count - 1)("Cantidad") = Me.DS.Tables(0).Rows(i)("Cantidad")
                        TablaHistorial.Rows(TablaHistorial.Rows.Count - 1)("Descripcion_Producto") = Me.DS.Tables(0).Rows(i)("Descripcion_Producto")
                    End If
                    i = i + 1
                Loop
                AdapterHistorial.Update(TablaHistorial)

                '///////////////////////////////////////////////////////
                '/////// Empiezo a comparar con el historial ///////////
                '///////////////////////////////////////////////////////

                '// Creo datatable para llenar resultado de comparació y mostrar en el reporte ///

                Dim TablaReporte As New DataTable
                Dim adaptor As New SqlDataAdapter("Select Cod_Producto as Codigo, Descripcion_Producto as Leyenda, Cantidad from Detalle_Facturas Where Numero_Factura = '-zyx998'", TempConn)
                adaptor.Fill(TablaReporte)

                If Actualiza = True Then
                    '/// Consulto los datos del historial pasado //////

                    AdapterHistorial = New SqlDataAdapter(" SELECT  Id, IdContGuardado, Numero_Factura, Cod_Producto, Cantidad, Descripcion_Producto    FROM         Historico_Comandas  where IdContGuardado = " & CodHistorialContador - 1 & " and Numero_Factura = '" & NumeroFactura & "'", TempConn)
                    TablaHistorial = New DataTable
                    AdapterHistorial.Fill(TablaHistorial)

                    i = 0
                    Dim oDatarow As DataRow

                    Do While i < DS.Tables(0).Rows.Count
                        If Not DS.Tables(0).Rows(i).RowState = DataRowState.Deleted Then
                            Dim result() As DataRow = TablaHistorial.Select("Cod_Producto='" & DS.Tables(0).Rows(i)("Cod_Producto") & "' ")
                            If result.Length > 0 Then
                                For Each row As DataRow In result
                                    If CDbl(row("Cantidad")) < CDbl(DS.Tables(0).Rows(i)("Cantidad")) Then
                                        oDatarow = TablaReporte.NewRow
                                        oDatarow("Codigo") = DS.Tables(0).Rows(i)("Cod_Producto")
                                        oDatarow("Leyenda") = DS.Tables(0).Rows(i)("Descripcion_Producto")
                                        oDatarow("Cantidad") = CDbl(DS.Tables(0).Rows(i)("Cantidad") - row("Cantidad"))
                                        TablaReporte.Rows.Add(oDatarow)
                                    ElseIf CDbl(row("Cantidad")) > CDbl(DS.Tables(0).Rows(i)("Cantidad")) Then
                                        oDatarow = TablaReporte.NewRow
                                        oDatarow("Codigo") = DS.Tables(0).Rows(i)("Cod_Producto")
                                        oDatarow("Leyenda") = "Se devuelve: " & DS.Tables(0).Rows(i)("Descripcion_Producto")
                                        oDatarow("Cantidad") = -CDbl(row("Cantidad") - DS.Tables(0).Rows(i)("Cantidad"))
                                        TablaReporte.Rows.Add(oDatarow)
                                    End If
                                Next
                            Else
                                oDatarow = TablaReporte.NewRow
                                oDatarow("Codigo") = DS.Tables(0).Rows(i)("Cod_Producto")
                                oDatarow("Leyenda") = DS.Tables(0).Rows(i)("Descripcion_Producto")
                                oDatarow("Cantidad") = CDbl(DS.Tables(0).Rows(i)("Cantidad"))
                                TablaReporte.Rows.Add(oDatarow)
                            End If
                        End If
                        i += 1
                    Loop

                    '///////////////////////////////////////////////////////////////////
                    '//////// VERIFICO SI ELIMINE REGISTROS DE LA TABLA HISTORIAL //////
                    '//////////////////////////////////////////////////////////////////

                    i = 0
                    Do While i < TablaHistorial.Rows.Count
                        Dim result() As DataRow = DS.Tables(0).Select("Cod_Producto = '" & TablaHistorial.Rows(i)("Cod_Producto") & "'")
                        If result.Length = 0 Then
                            oDatarow = TablaReporte.NewRow
                            oDatarow("Codigo") = TablaHistorial.Rows(i)("Cod_Producto")
                            oDatarow("Leyenda") = "Se devuelve: " & TablaHistorial.Rows(i)("Descripcion_Producto")
                            oDatarow("Cantidad") = -CDbl(TablaHistorial.Rows(i)("Cantidad"))
                            TablaReporte.Rows.Add(oDatarow)
                        End If
                        i = i + 1
                    Loop

                Else
                    i = 0
                    Dim oDatarow As DataRow
                    Do While i < DS.Tables(0).Rows.Count
                        oDatarow = TablaReporte.NewRow
                        oDatarow("Codigo") = DS.Tables(0).Rows(i)("Cod_Producto")
                        oDatarow("Leyenda") = DS.Tables(0).Rows(i)("Descripcion_Producto")
                        oDatarow("Cantidad") = CDbl(DS.Tables(0).Rows(i)("Cantidad"))
                        TablaReporte.Rows.Add(oDatarow)

                        i += 1
                    Loop
                End If


                '//////////////////////////////////////////////////////////////////////////////////////////
                '///// SEPARO LA COMANDA POR TIPO DE IMPRESION: Comanda = cocina, ComandaBar = bar /////
                '//////////////////////////////////////////////////////////////////////////////////////////
                AgregaTipoImpresion(TablaReporte)
                ImprimirComandaPorTipo(TablaReporte, "Comanda", ImprimeFacturaPreview)
                ImprimirComandaPorTipo(TablaReporte, "ComandaBar", ImprimeFacturaPreview)

                '/////////////////////////////////////////////////////////
                '/////////////////////////////////////////////////////////


                BackToLife("")
                CodHistorialContador = Nothing
                FechaFactura = Nothing
                Actualiza = False
                ClearTxtData()


            End If
        End If

    End Sub


    '// Agrega a la tabla de la comanda el TipoImpresion de cada producto (Comanda = cocina, ComandaBar = bar)
    Private Sub AgregaTipoImpresion(ByVal Tabla As DataTable)
        If Not Tabla.Columns.Contains("TipoImpresion") Then Tabla.Columns.Add("TipoImpresion", GetType(String))
        If Tabla.Rows.Count = 0 Then Exit Sub

        Dim Tipos As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Try
            If TempConn.State <> ConnectionState.Closed Then TempConn.Close()
            TempConn.Open()
            Using Cmd As New SqlCommand("SELECT TipoImpresion FROM Productos WHERE Cod_Productos = @Cod", TempConn)
                Cmd.Parameters.Add("@Cod", SqlDbType.VarChar, 50)
                For Each Fila As DataRow In Tabla.Rows
                    Dim Codigo As String = Convert.ToString(Fila("Codigo"))
                    If Not Tipos.ContainsKey(Codigo) Then
                        Cmd.Parameters("@Cod").Value = Codigo
                        Dim Valor As Object = Cmd.ExecuteScalar()
                        Dim Tipo As String = "Comanda"
                        If Valor IsNot Nothing AndAlso Not IsDBNull(Valor) AndAlso Convert.ToString(Valor).Trim() <> "" Then
                            Tipo = Convert.ToString(Valor).Trim()
                        End If
                        Tipos(Codigo) = Tipo
                    End If
                    Fila("TipoImpresion") = Tipos(Codigo)
                Next
            End Using
        Catch ex As Exception
            ' Si no se puede consultar (p. ej. la BD no tiene el campo TipoImpresion) todo va a cocina
        Finally
            If TempConn.State <> ConnectionState.Closed Then TempConn.Close()
        End Try

        For Each Fila As DataRow In Tabla.Rows
            If IsDBNull(Fila("TipoImpresion")) Then Fila("TipoImpresion") = "Comanda"
        Next
    End Sub

    '// Imprime la comanda de un tipo: "ComandaBar" va a la impresora del bar, todo lo demas a la de cocina
    Private Sub ImprimirComandaPorTipo(ByVal Tabla As DataTable, ByVal Tipo As String, ByVal ImprimeFacturaPreview As Boolean)
        Dim Vista As New DataView(Tabla)
        If Tipo = "ComandaBar" Then
            Vista.RowFilter = "TipoImpresion = 'ComandaBar'"
        Else
            Vista.RowFilter = "TipoImpresion <> 'ComandaBar'"
        End If
        If Vista.Count = 0 Then Exit Sub
        Dim TablaTipo As DataTable = Vista.ToTable()

        '///////// SELECCIONO LA IMPRESORA CONFIGURADA PARA EL TIPO /////////
        Dim Impresora As String = BuscaImpresora(Tipo)
        If Tipo = "ComandaBar" AndAlso (String.IsNullOrWhiteSpace(Impresora) OrElse Impresora.Trim().Equals("Ninguno", StringComparison.OrdinalIgnoreCase)) Then
            ' El bar no tiene impresora configurada: se envia a la de cocina para no perder la comanda
            Impresora = BuscaImpresora("Comanda")
        End If
        Establecer_Impresora(Impresora)

        '////// GENERO IMPRESION COMANDA  //////////
        Dim ArepComanda As New ArepComanda
        ArepComanda.LblFecha.Text = Format(DateTime.Now, "dd/MM/yyyy")
        ArepComanda.LblMesa.Text = Me.cboMesas.Text
        ArepComanda.lblfactura.Text = NumeroFactura
        ArepComanda.LblMesero.Text = NombreVendedor
        If Tipo = "ComandaBar" Then ArepComanda.Label6.Text = "Comanda Bar"

        Dim ViewerForm As New FrmViewer()
        ViewerForm.arvMain.Document = ArepComanda.Document
        My.Application.DoEvents()

        ArepComanda.DataSource = TablaTipo

        If ImprimeFacturaPreview = True Then
            ArepComanda.Run(True)
            ViewerForm.Show()
        Else
            ArepComanda.Run(True)
            ViewerForm.arvMain.Document.Print(False, False, False)
        End If
    End Sub


    Public Sub BackToLife(ByVal NumFactura As String)

        If NumFactura = "" Then
            Me.cboMesas.Text = ""
            DA = New SqlDataAdapter("SELECT  Cod_Producto, Descripcion_Producto, Cantidad, Precio_Unitario, Precio_Neto, Numero_Factura, Fecha_Factura, Tipo_Factura, Id_Detalle_Factura, Importe FROM  Detalle_Facturas WHERE (Cod_Producto = '999l099')", TempConn)
        Else
            DA = New SqlDataAdapter("SELECT  Cod_Producto, Descripcion_Producto, Cantidad, Precio_Unitario, Precio_Neto, Numero_Factura, Fecha_Factura, Tipo_Factura, Id_Detalle_Factura, Importe FROM  Detalle_Facturas WHERE (Numero_Factura = '" & NumFactura & "')", TempConn)
        End If

        '///////////////// Este proceso Reinicializa el Grid desde 0 con un Numero de Factura Nuevo /////////////////
        Builder = New SqlCommandBuilder(DA)
        DS = New DataSet
        DA.Fill(DS)
        Me.BindingSource1.DataSource = DS.Tables(0)
        Me.C1TrueDBGrid1.DataSource = DS.Tables(0)

        Me.C1TrueDBGrid1.Columns(6).DefaultValue = DateTime.Now
        Me.C1TrueDBGrid1.Columns(7).DefaultValue = "Factura"
        Me.C1TrueDBGrid1.RowHeight = 20
        Me.C1TrueDBGrid1.Splits(0).DisplayColumns(0).Width = 60
        Me.C1TrueDBGrid1.Splits(0).DisplayColumns(2).Width = 60
        Me.C1TrueDBGrid1.Columns(4).NumberFormat = "##,##0.00"
        ' Me.C1TrueDBGrid1.Columns(2).NumberFormat = "##,##0.00"
        Me.C1TrueDBGrid1.Columns(3).NumberFormat = "##,##0.00"
        Me.C1TrueDBGrid1.Splits(0).DisplayColumns(5).Visible = False
        Me.C1TrueDBGrid1.Splits(0).DisplayColumns(6).Visible = False
        Me.C1TrueDBGrid1.Splits(0).DisplayColumns(7).Visible = False
        Me.C1TrueDBGrid1.Splits(0).DisplayColumns("Id_Detalle_Factura").Visible = False
        Me.C1TrueDBGrid1.Splits(0).DisplayColumns("Importe").Visible = False
        NumeroFactura = ""

        '//////////////////////////////////////////////////////////////////////////////////////////////
    End Sub

    Public Sub CrearNumeroFactura()

        If Me.cboSerie.Visible = True Then
            Dim AuxTable As New DataTable
            Dim AuxAdapter As New SqlDataAdapter("Select * from ConsecutivoSerie where Serie = '" & Me.cboSerie.Text & "'", TempConn)
            AuxAdapter.Fill(AuxTable)
            If AuxTable.Rows.Count > 0 Then
                NumeroFactura = Me.cboSerie.Text & Format(Convert.ToInt32(AuxTable.Rows(0)("Factura")) + 1, "00000")
                NFact = AuxTable.Rows(0)("Factura") + 1
            Else
                MessageBox.Show("Tenès que escoger una serie valida", "Zeus Facturaciòn", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Else
            Dim AuxTable As New DataTable
            Dim AuxAdapter As New SqlDataAdapter("Select Factura from Consecutivos", TempConn)
            AuxAdapter.Fill(AuxTable)
            If AuxTable.Rows.Count > 0 Then
                NumeroFactura = Format(Convert.ToInt32(AuxTable.Rows(0)("Factura")) + 1, "00000")
            End If
            NFact = AuxTable.Rows(0)("Factura") + 1
        End If

    End Sub
    Public Function Consecutivo_Factura(TipoFactura As String) As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataAdapter As SqlClient.SqlDataAdapter
        Dim SqlString As String, DataSet As New DataSet, CadenaDiv As String
        Dim Numero As Double = 0, SqlConsecutivo As String, FacturaBodega As Boolean = True, CompraBodega As Boolean = False, FacturaSerie As Boolean = False
        Dim NumeroFactura As String = "00001", ConsecutivoFactura As Double

        '////////////////////////////////////////////////////////////////////////
        '/////////////////BUSCO EL CONSECUTIVO SEGUN TABLA CONSECUTIVO /////////
        '//////////////////////////////////////////////////////////////////////////////
        If Me.cboSerie.Visible = True Then
            Dim AuxTable As New DataTable
            Dim AuxAdapter As New SqlDataAdapter("Select * from ConsecutivoSerie where Serie = '" & Me.cboSerie.Text & "'", TempConn)
            AuxAdapter.Fill(AuxTable)
            If AuxTable.Rows.Count > 0 Then
                NumeroFactura = Me.cboSerie.Text & Format(Convert.ToInt32(AuxTable.Rows(0)("Factura")) + 1, "00000")
                NFact = AuxTable.Rows(0)("Factura") + 1
            Else
                MessageBox.Show("Tenès que escoger una serie valida", "Zeus Facturaciòn", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
        Else
            Dim AuxTable As New DataTable
            Dim AuxAdapter As New SqlDataAdapter("Select Factura from Consecutivos", TempConn)
            AuxAdapter.Fill(AuxTable)
            If AuxTable.Rows.Count > 0 Then
                NumeroFactura = Format(Convert.ToInt32(AuxTable.Rows(0)("Factura")) + 1, "00000")
            End If
            NFact = AuxTable.Rows(0)("Factura") + 1
        End If


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO SI TIENE ACTIVADA LA OPCION DE CONSECUTIVO X BODEGA /////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////
        SqlConsecutivo = "SELECT * FROM  DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Configuracion")
        If Not DataSet.Tables("Configuracion").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")) Then
                FacturaBodega = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")) Then
                CompraBodega = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacSerie")) Then
                FacturaSerie = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacSerie")
            End If

        End If


        '----------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------VERIFICO QUE EL CONSECUTIVO NO EXISTE EN LAS FACTURAS GRABADAS--------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------------
        SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') ORDER BY Numero_Factura DESC"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        If DataSet.Tables("Consulta").Rows.Count <> 0 Then
            '-------------------------SI EXISTE ESTA FACTURA GRABADA, ENTONCES RECOMIENDO EL SIGUIENTE SEGUN LA FACTURACION ------------------------------
            NumeroFactura = DataSet.Tables("Consulta").Rows(0)("Numero_Factura")

            If FacturaBodega = True Then
                CadenaDiv = NumeroFactura
                SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') AND (Numero_Factura LIKE '" & CadenaDiv(0) & "%') ORDER BY Numero_Factura DESC"
            ElseIf FacturaSerie = True Then
                SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') AND (Numero_Factura LIKE '" & cboSerie.Text & "%') ORDER BY Numero_Factura DESC"
            Else
                SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') ORDER BY Numero_Factura DESC"
            End If


            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "Facturas")
            If DataSet.Tables("Facturas").Rows.Count <> 0 Then
                NumeroFactura = DataSet.Tables("Facturas").Rows(0)("Numero_Factura")
                Numero = Mid(NumeroFactura, Len(cboSerie.Text) + 1, Len(NumeroFactura))
                ConsecutivoFactura = Numero + 1
                NFact = ConsecutivoFactura
            End If


            If FacturaBodega = True Then
                NumeroFactura = cboSerie.Text & "-" & Format(ConsecutivoFactura, "0000#")
            ElseIf FacturaSerie = True Then
                NumeroFactura = Me.cboSerie.Text & Format(ConsecutivoFactura, "0000#")
                Me.cboSerie.Enabled = False
            Else
                NumeroFactura = Format(ConsecutivoFactura, "0000#")
            End If

            Return NumeroFactura


        Else
            Return NumeroFactura
        End If
    End Function


    Private Sub C1Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button4.Click
        Dim MontoPagado As Double, Resultado As Double
        Dim Impresora_Defecto As String
        Dim ImprimeFacturaPreview As Boolean = ObtenerImprimirSinPreview()
        Dim Pagado As Double

        If MessageBox.Show("Seguro querés cancelar la cuenta?", "Zeus Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If Me.cboMesas.Text = "" Then
            MessageBox.Show("Tenès que seleccionar una mesa", "Zeus Facturaciòn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        MontoPagado = Monto_Pendiente(NumeroFactura)

        If MontoPagado = 0 Then
            Resultado = MessageBox.Show("¿La Factura sera de Credito?", "Zeus Facturaciòn", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If Resultado = 7 Then
                Exit Sub
            End If

        End If



        '///////// Actualizo Detalle Factura ///////////////

        'Validar si hay filas en el grid, si no hay no grabar la factura
        TempConn.Close()

        If Not DS.Tables(0).Rows.Count = 0 Then
            If DS.Tables(0).Rows.Count <> 0 Then
                If ExisteFactura(NumeroFactura, "Factura") = False Then
                    Me.CrearNumeroFactura()
                    '////////////////// Guardo datos en Tabla Factura ///////////
                    SqlCommand = New SqlCommand("INSERT INTO [Facturas] ([Numero_Factura] ,[Fecha_Factura] ,[Tipo_Factura] ,[MonedaFactura] ,[Cod_Cliente] ,[Cod_Bodega] ,[Cod_Vendedor] ,[Cod_Cajero] ,[Nombre_Cliente] ,[Apellido_Cliente] ,[Direccion_Cliente] ,[Telefono_Cliente] ,[Fecha_Vencimiento] ,[Observaciones], [SubTotal] ,[IVA] ,[Pagado] ,[NetoPagar], [MontoCredito] ,[Contabilizado] ,[Activo] ,[Cancelado] ,[MetodoPago] ,[Exonerado] ,[Descuentos] ,[Marca] ,[TransferenciaProcesada] ,[MonedaImprime] ,[FechaPago],[FechaHora]) VALUES ('" & Me.NumeroFactura & "', '" & Format(DateTime.Now, "dd/MM/yyyy") & "', 'Factura', 'Cordobas','" & Me.cboMesas.SelectedValue & "', '" & PBodega & "', '" & PVendedor & "', '" & PCajero & "', '" & Me.txtNombre.Text & "', '" & Me.txtRuc.Text & "', '" & Me.txtDireccion.Text & "', '" & Me.txtTelefono.Text & "', '" & Format(DateTime.Now, "dd/MM/yyyy") & "', '" & Me.txtObservacion.Text & "', " & CDbl(Me.txtSubTotal.Text) & ", " & CDbl(Me.txtIva.Text) & ", " & CDbl(Me.txtTotal.Text) & ", " & CDbl(Me.txtTotal.Text) & ", 0, 'False', 'False', 'False', 'Contado', '" & Me.ckExonerar.CheckState & "', 0, 'True', 'False', 'Cordobas', '" & Format(DateTime.Now, "dd/MM/yyyy") & "','" & Format(DateTime.Now, "dd/MM/yyyy HH:mm:ss") & "'   )", TempConn)
                    TempConn.Open()
                    SqlCommand.ExecuteNonQuery()
                    TempConn.Close()

                End If
                '//////////////// Guardo detalle de Factura /////////////////

                Dim CommandFactura As New SqlCommand("UPDATE Facturas SET [Activo] = 'False' ,[Cancelado] = 'False'   WHERE dbo.Facturas.Numero_Factura = '" & NumeroFactura & "' and dbo.Facturas.Tipo_Factura ='Factura' ", TempConn)
                TempConn.Open()
                CommandFactura.ExecuteNonQuery()
                TempConn.Close()
                Contador = 0
                Do While Contador < DS.Tables(0).Rows.Count
                    Me.DS.Tables(0).Rows(Contador)("Numero_Factura") = NumeroFactura
                    Me.DS.Tables(0).Rows(Contador)("Fecha_Factura") = Format(DateTime.Now, "dd/MM/yyyy")
                    Me.DS.Tables(0).Rows(Contador)("Tipo_Factura") = "Factura"
                    Contador = Contador + 1
                Loop
                DA.Update(DS.Tables(0))

                If Me.cboSerie.Visible = True Then
                    Dim Command As New SqlCommand("Update ConsecutivoSerie set Factura = " & NFact & " Where Serie = '" & Me.cboSerie.Text & "'", TempConn)
                    TempConn.Open()
                    Command.ExecuteNonQuery()
                    TempConn.Close()
                Else
                    Dim Command As New SqlCommand("Update Consecutivos set Factura = " & NFact & "", TempConn)
                    TempConn.Open()
                    Command.ExecuteNonQuery()
                    TempConn.Close()
                End If
                NFact = 0
            End If
        End If






        Dim ArepFactura As New ArepFacturasTiras2
        Dim AdapterDE As New SqlDataAdapter("Select * from DatosEmpresa", TempConn)
        Dim TablaDE As New DataTable

        AdapterDE.Fill(TablaDE)


        If LogoEmpresa <> "" Then
            ArepFactura.ImgLogo.Image = BytesToImagen(StringtoByte(LogoEmpresa))
        ElseIf Dir(TablaDE.Rows(0)("Ruta_Logo")) <> "" Then
            ArepFactura.ImgLogo.Image = New System.Drawing.Bitmap(TablaDE.Rows(0)("Ruta_Logo").ToString)
        End If

        Pagado = ObtenerMontoPagadoCordobas(NumeroFactura, Format(DateTime.Now, "dd/MM/yyyy"), "Factura")

        ArepFactura.Label6.Text = "FACTURA"
        ArepFactura.LblEncabezado.Text = TablaDE.Rows(0)("Nombre_Empresa")
        ArepFactura.LblRuc.Text = TablaDE.Rows(0)("Numero_Ruc")
        ArepFactura.LblDireccion.Text = TablaDE.Rows(0)("Direccion_Empresa")
        ArepFactura.LblTelefono.Text = TablaDE.Rows(0)("Telefono")
        ArepFactura.LblOrden.Text = NumeroFactura
        ArepFactura.LblFechaOrden.Text = Format(DateTime.Now, "dd/MM/yyyy")
        ArepFactura.LblNombres.Text = Me.txtNombre.Text
        ArepFactura.LblApellidos.Text = Me.txtRuc.Text
        ArepFactura.TxtVendedor.Text = NombreVendedor
        ArepFactura.LblSubTotal.Text = Me.txtSubTotal.Text
        ArepFactura.LblIva.Text = Me.txtIva.Text
        ArepFactura.Label10.Text = Me.txtpropina.Text
        ArepFactura.LblPagado.Text = Format(Pagado, "##,##0.00")  'Format(txtTotal.Text, "##,##0.00") 
        ArepFactura.LblTotal1.Text = Me.txtTotal.Text
        ArepFactura.LblTotal.Text = "0"

        Dim ViewerForm As New FrmViewer()
        ViewerForm.arvMain.Document = ArepFactura.Document
        My.Application.DoEvents()

        ArepFactura.DataSource = DS.Tables(0)
        'Rprt.Run(False)
        'ViewerForm.Show()

        '//////////////////////////////////////////////////////////////
        '////////////////IMPRIMO SEGUN CLASIFICACION /////////////////
        '//////////////////////////////////////////////////////////////

        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////////SELECCIONO LA IMPRESORA CONFIGURADA PARA LAS FACTURAS /////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Dim pd As New PrintDocument
        Dim ImpresoraFactura As String
        Dim s_Default_Printer As String = pd.PrinterSettings.PrinterName

        Impresora_Defecto = s_Default_Printer
        ImpresoraFactura = BuscaImpresora("Factura")
        Establecer_Impresora(ImpresoraFactura)


        If ImprimeFacturaPreview = True Then
            ArepFactura.Run(True)
            ViewerForm.Show()

        Else
            ArepFactura.Run(True)
            ViewerForm.arvMain.Document.Print(False, False, False)
        End If



        BackToLife("")
    End Sub

    Private Sub C1TextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombre.Click
        My.Forms.FrmTeclado.Posicion = "Nombre"
        My.Forms.FrmTeclado.ShowDialog()
    End Sub

    Private Sub C1TextBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRuc.Click
        My.Forms.FrmTeclado.Posicion = "Ruc"
        My.Forms.FrmTeclado.ShowDialog()
    End Sub

    Private Sub C1TextBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTelefono.Click
        My.Forms.FrmTeclado.Posicion = "Telefono"
        My.Forms.FrmTeclado.ShowDialog()
    End Sub

    Private Sub C1TextBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDireccion.Click
        My.Forms.FrmTeclado.Posicion = "Direccion"
        My.Forms.FrmTeclado.ShowDialog()
    End Sub

    Private Sub C1TextBox1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtObservacion.Click
        My.Forms.FrmTeclado.Posicion = "Observacion"
        My.Forms.FrmTeclado.ShowDialog()
    End Sub
    Public Sub ClearTxtData()
        Me.txtNombre.Value = ""
        Me.txtDireccion.Value = ""
        Me.txtRuc.Value = ""
        Me.txtObservacion.Value = ""
        Me.txtTelefono.Value = ""
        Me.txtpropina.Text = "0.00"
        Me.txtIva.Text = "0.00"
        Me.txtSubTotal.Text = "0.00"
        Me.txtTotal.Text = "0.00"
        Me.C1DockingTab1.SelectedIndex = 0
        NumeroFactura = ""
        FechaFactura = Nothing
    End Sub
    Private Sub C1Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button2.Click
        If Me.C1TrueDBGrid1.RowCount > 0 Then
            Dim foundRows() As Data.DataRow = DS.Tables(0).Select("Cod_Producto = '" & Me.C1TrueDBGrid1.Columns("Cod_Producto").Text & "'")
            If foundRows.Length > 0 Then
                Dim pos As Integer = DS.Tables(0).Rows.IndexOf(foundRows(0))
                Form3.ShowDialog()
                DS.Tables(0).Rows(pos)("Precio_Unitario") = Form3.PrecioUnitario
                DS.Tables(0).Rows(pos)(4) = Convert.ToDouble(DS.Tables(0).Rows(pos)(2)) * Convert.ToDouble(DS.Tables(0).Rows(pos)(3))
                Calcular()
            End If
        End If
    End Sub
    Private Sub ckExonerar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckExonerar.CheckedChanged
        Calcular()
    End Sub
    Private Sub C1Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button5.Click
        If DS.Tables(0).Rows.Count > 0 Then
            If MessageBox.Show("¿Querés crear una nueva cuenta? Si aceptás podés perder datos que no guardaste", "Zeus Facturación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If
        Actualiza = False
        BackToLife("")

        Me.ClearTxtData()
    End Sub




    Private Sub C1Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button3.Click
        If DS.Tables(0).Rows.Count > 0 Then
            If MessageBox.Show("¿Querés ver tus cuentas? Si aceptás podés perder datos que no guardaste", "Zeus Facturación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If
        BackToLife("")
        ClearTxtData()
        Actualiza = False
        FormCuentas.Show()
        Me.C1Button4.Enabled = True
    End Sub



    Private Sub ckPropina_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPropina.CheckedChanged
        Calcular()
    End Sub

    Private Sub lblNombreVendedor_Click(sender As Object, e As EventArgs) Handles lblNombreVendedor.Click

    End Sub

    Private Sub Form1_MarginChanged(sender As Object, e As EventArgs) Handles Me.MarginChanged

    End Sub

    Private Sub Form1_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged

    End Sub




    Private Sub BtnPago_Click(sender As Object, e As EventArgs) Handles BtnPago.Click

        If Me.cboMesas.Text = "" Then
            MessageBox.Show("Tenès que seleccionar una mesa", "Zeus Facturaciòn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If NumeroFactura = "" Then
            MessageBox.Show("Es Necesario Grabar Comanda", "Zeus Facturaciòn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try
            ' Obtener el total de la factura.
            Dim TotalFactura As Decimal

            If Not Decimal.TryParse(txtTotal.Text, TotalFactura) Then
                MessageBox.Show("El total de la factura no es válido.")
                Exit Sub
            End If

            ' Obtener el monto pagado, convirtiendo los dólares a córdobas.
            Dim MontoPagado As Decimal =
                ObtenerMontoPagadoCordobas(
                    NumeroFactura,
                    Now,
                    "Factura")

            ' Calcular el saldo pendiente.
            Dim MontoPendiente As Decimal =
                TotalFactura - MontoPagado

            ' Evitar saldos negativos.
            If MontoPendiente < 0D Then
                MontoPendiente = 0D
            End If

            ' Pasar los datos al formulario de pago.
            FrmPago.MontoAPagar = MontoPendiente.ToString("0.00")
            FrmPago.Numero_Factura = NumeroFactura
            Me.C1Button4.Enabled = True
            FrmPago.Show()

        Catch ex As Exception
            MessageBox.Show(
                "Error al calcular el monto pendiente: " & ex.Message,
                "Sistema de facturación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try


        'FrmPago.MontoAPagar = txtTotal.Text
        'FrmPago.Numero_Factura = NumeroFactura
        'FrmPago.Show()
    End Sub

    Private Sub BtnPreFactura_Click(sender As Object, e As EventArgs) Handles BtnPreFactura.Click

        Dim Impresora_Defecto As String
        Dim ImprimeFacturaPreview As Boolean = ObtenerImprimirSinPreview()

        Dim MontoPagado As Double, Resultado As Double


        If Me.cboMesas.Text = "" Then
            MessageBox.Show("Tenès que seleccionar una mesa", "Zeus Facturaciòn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If NumeroFactura = "" Then
            MessageBox.Show("Es Necesario Grabar Comanda", "Zeus Facturaciòn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////////SELECCIONO LA IMPRESORA CONFIGURADA PARA LAS FACTURAS /////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Dim pd As New PrintDocument
        Dim ImpresoraFactura As String
        Dim s_Default_Printer As String = pd.PrinterSettings.PrinterName

        Impresora_Defecto = s_Default_Printer
        ImpresoraFactura = BuscaImpresora("Factura")
        Establecer_Impresora(ImpresoraFactura)


        MontoPagado = Monto_Pendiente(NumeroFactura)


        '///////// Actualizo Detalle Factura ///////////////

        'Validar si hay filas en el grid, si no hay no grabar la factura
        TempConn.Close()

        If Not DS.Tables(0).Rows.Count = 0 Then
            If DS.Tables(0).Rows.Count <> 0 Then
                If ExisteFactura(NumeroFactura, "Factura") = False Then
                    Me.CrearNumeroFactura()
                    '////////////////// Guardo datos en Tabla Factura ///////////
                    SqlCommand = New SqlCommand("INSERT INTO [Facturas] ([Numero_Factura] ,[Fecha_Factura] ,[Tipo_Factura] ,[MonedaFactura] ,[Cod_Cliente] ,[Cod_Bodega] ,[Cod_Vendedor] ,[Cod_Cajero] ,[Nombre_Cliente] ,[Apellido_Cliente] ,[Direccion_Cliente] ,[Telefono_Cliente] ,[Fecha_Vencimiento] ,[Observaciones], [SubTotal] ,[IVA] ,[Pagado] ,[NetoPagar], [MontoCredito] ,[Contabilizado] ,[Activo] ,[Cancelado] ,[MetodoPago] ,[Exonerado] ,[Descuentos] ,[Marca] ,[TransferenciaProcesada] ,[MonedaImprime] ,[FechaPago],[FechaHora]) VALUES ('" & Me.NumeroFactura & "', '" & Format(DateTime.Now, "dd/MM/yyyy") & "', 'Factura', 'Cordobas','" & Me.cboMesas.SelectedValue & "', '" & PBodega & "', '" & PVendedor & "', '" & PCajero & "', '" & Me.txtNombre.Text & "', '" & Me.txtRuc.Text & "', '" & Me.txtDireccion.Text & "', '" & Me.txtTelefono.Text & "', '" & Format(DateTime.Now, "dd/MM/yyyy") & "', '" & Me.txtObservacion.Text & "', " & CDbl(Me.txtSubTotal.Text) & ", " & CDbl(Me.txtIva.Text) & ", " & CDbl(Me.txtTotal.Text) & ", " & CDbl(Me.txtTotal.Text) & ", 0, 'False', 'False', 'False', 'Contado', '" & Me.ckExonerar.CheckState & "', 0, 'True', 'False', 'Cordobas', '" & Format(DateTime.Now, "dd/MM/yyyy") & "','" & Format(DateTime.Now, "dd/MM/yyyy HH:mm:ss") & "'   )", TempConn)
                    TempConn.Open()
                    SqlCommand.ExecuteNonQuery()
                    TempConn.Close()

                End If
                '//////////////// Guardo detalle de Factura /////////////////

                Dim CommandFactura As New SqlCommand("UPDATE Facturas SET [Activo] = 'True' ,[Cancelado] = 'False', Exonerado = '" & Me.ckExonerar.CheckState & "'    WHERE dbo.Facturas.Numero_Factura = '" & NumeroFactura & "' and dbo.Facturas.Tipo_Factura ='Factura' ", TempConn)
                TempConn.Open()
                CommandFactura.ExecuteNonQuery()
                TempConn.Close()
                Contador = 0
                Do While Contador < DS.Tables(0).Rows.Count
                    Me.DS.Tables(0).Rows(Contador)("Numero_Factura") = NumeroFactura
                    Me.DS.Tables(0).Rows(Contador)("Fecha_Factura") = Format(DateTime.Now, "dd/MM/yyyy")
                    Me.DS.Tables(0).Rows(Contador)("Tipo_Factura") = "Factura"
                    Contador = Contador + 1
                Loop
                DA.Update(DS.Tables(0))

                If Me.cboSerie.Visible = True Then
                    Dim Command As New SqlCommand("Update ConsecutivoSerie set Factura = " & NFact & " Where Serie = '" & Me.cboSerie.Text & "'", TempConn)
                    TempConn.Open()
                    Command.ExecuteNonQuery()
                    TempConn.Close()
                Else
                    Dim Command As New SqlCommand("Update Consecutivos set Factura = " & NFact & "", TempConn)
                    TempConn.Open()
                    Command.ExecuteNonQuery()
                    TempConn.Close()
                End If
                NFact = 0
            End If
        End If



        Dim Rprt As New ArepFacturasTiras2
        Dim AdapterDE As New SqlDataAdapter("Select * from DatosEmpresa", TempConn)
        Dim TablaDE As New DataTable

        AdapterDE.Fill(TablaDE)




        If LogoEmpresa <> "" Then
            Rprt.ImgLogo.Image = BytesToImagen(StringtoByte(LogoEmpresa))
        ElseIf Dir(TablaDE.Rows(0)("Ruta_Logo")) <> "" Then
            Rprt.ImgLogo.Image = New System.Drawing.Bitmap(TablaDE.Rows(0)("Ruta_Logo").ToString)

        End If

        Rprt.Label6.Text = "PROFORMA"
        Rprt.LblEncabezado.Text = TablaDE.Rows(0)("Nombre_Empresa")
        Rprt.LblRuc.Text = TablaDE.Rows(0)("Numero_Ruc")
        Rprt.LblDireccion.Text = TablaDE.Rows(0)("Direccion_Empresa")
        Rprt.LblTelefono.Text = TablaDE.Rows(0)("Telefono")
        Rprt.LblOrden.Text = NumeroFactura
        Rprt.LblFechaOrden.Text = Format(DateTime.Now, "dd/MM/yyyy")
        Rprt.LblApellidos.Text = Me.cboMesas.Text
        Rprt.LblNombres.Text = Me.txtNombre.Text
        Rprt.TxtVendedor.Text = Me.lblNombreVendedor.Text
        'Rprt.LblApellidos.Text = Me.txtRuc.Text
        Rprt.LblSubTotal.Text = Me.txtSubTotal.Text
        Rprt.LblIva.Text = Me.txtIva.Text
        Rprt.Label10.Text = Me.txtpropina.Text
        Rprt.LblPagado.Text = 0.00
        Rprt.LblTotal1.Text = Me.txtTotal.Text
        Rprt.LblTotal.Text = "0.00"

        Dim ViewerForm As New FrmViewer()
        ViewerForm.arvMain.Document = Rprt.Document
        My.Application.DoEvents()

        Rprt.DataSource = DS.Tables(0)
        Rprt.Run(False)
        ViewerForm.Show()



        'BackToLife("")
        Me.C1Button4.Enabled = True
    End Sub



    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd

    End Sub

    Private Sub Form1_Click(sender As Object, e As EventArgs) Handles Me.Click
    End Sub


End Class
