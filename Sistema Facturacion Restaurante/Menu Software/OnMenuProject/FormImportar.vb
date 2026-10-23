Imports System.Data.SqlClient
Public Class FormImportar
    Dim Importable As Boolean
    Dim Command As SqlCommand
    Dim SqlString As String, SubTotalCompra As Double = 0, TasaCambio As Double = 0
    Dim TablaConsulta, TablaAuxiliar, TablaImporte, TablaBodegas, TablaSucursales As DataTable
    Dim AdapterConsulta, AdapterAuxiliar As SqlDataAdapter
    Dim pOFD As OpenFileDialog
    Dim Conexion As New SqlConnection("Password=P@ssword;Persist Security Info=True;User ID=sa;Initial Catalog=Firefact;Data Source=DESKTOP-E2SQPU1\SQL2005")
    Private Sub cboTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipo.SelectedIndexChanged
        Me.txtRuta.Value = ""
        SubTotalCompra = 0
        Select Case Me.cboTipo.Text
            Case "Productos"
                Me.cboBodega.Visible = True
                Me.lblBodega.Visible = True
                SqlString = "SELECT     Codigo, Descripcion, TipoProducto, UnidadMedida, PrecioVentaCor AS Cantidad, PrecioVentaCor AS CostoUnitario, PrecioVentaCor AS Total, PrecioVentaCor, PrecioVentaDol,      Descripcion AS NombreImpuesto, Descripcion AS NombreLinea FROM         Productos  WHERE     (Codigo = '-010Maol_')"
            Case "Proveedores"
                Me.cboBodega.Visible = False
                Me.lblBodega.Visible = False
                SqlString = "SELECT     Codigo, Nombre, Direccion, Telefono, Ruc, Departamento, Responsable, Exonerado    FROM         Proveedores  Where Codigo = '-010Maol_'"
            Case "Clientes"
                Me.cboBodega.Visible = False
                Me.lblBodega.Visible = False
                SqlString = "SELECT     Codigo, Nombre, Direccion, Telefono, Ruc, Departamento, Responsable, Exonerado  FROM         Clientes  WHERE     (Codigo = '-010Maol_')"
        End Select

        AdapterConsulta = New SqlDataAdapter(SqlString, Conexion)
        TablaImporte = New DataTable
        AdapterConsulta.Fill(TablaImporte)

        gridImportacion.DataSource = TablaImporte
        gridImportacion.RowHeight = 18
        gridImportacion.Caption = Me.cboTipo.Text
    End Sub

    Private Sub FormImportar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboTipo.Text = "Productos"
        Importable = False
        SubTotalCompra = 0
        '//////// BODEGAS /////////

        TablaBodegas = New DataTable
        AdapterConsulta = New SqlDataAdapter("Select Codigo, Descripcion From Bodegas", Conexion)
        AdapterConsulta.Fill(TablaBodegas)
        cboBodega.DataSource = TablaBodegas

        cboBodega.ValueMember = "Codigo"
        cboBodega.DisplayMember = "Descripcion"

        '//////// SUCURSALES //////

        TablaSucursales = New DataTable
        AdapterConsulta = New SqlDataAdapter("Select Codigo, Sucursal From ConsecutivoSucursales", Conexion)
        AdapterConsulta.Fill(TablaSucursales)
        cboSucursal.DataSource = TablaSucursales

        cboSucursal.ValueMember = "Codigo"
        cboSucursal.DisplayMember = "Sucursal"
    End Sub

    Private Sub btnBuscarArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarArchivo.Click
        pOFD = New OpenFileDialog
        pOFD.Filter = "Archivos CSV|*.csv"
        pOFD.Title = "Por favor, seleccioná un archivo para importar"
        If pOFD.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Application.DoEvents()
            Me.txtRuta.Value = ""
            Me.TablaImporte.Rows.Clear()
            Me.cboTipo.Enabled = False
            Me.txtRuta.Value = pOFD.FileName
            Dim TotalLineas = IO.File.ReadAllLines(pOFD.FileName).Length
            Dim HiloLector As New IO.StreamReader(pOFD.FileName, System.Text.Encoding.Default)
            Dim sline As String = "", i As Integer = 1
            Me.pbImportacion.Maximum = TotalLineas
            Do
                Importable = True
                pbImportacion.Value = i
                sline = HiloLector.ReadLine
                If sline Is Nothing Then Exit Do
                Dim Columnas() As String = sline.Split(";")
                Select Case Me.cboTipo.Text
                    Case "Productos"
                        My.Application.DoEvents()
                        Dim NuevaLinea As DataRow = TablaImporte.NewRow
                        NuevaLinea("Codigo") = Columnas(0)
                        NuevaLinea("Descripcion") = Columnas(1)
                        Me.lblRegistro.Text = Columnas(0) & " " & Columnas(1)
                        NuevaLinea("TipoProducto") = Columnas(2)
                        NuevaLinea("UnidadMedida") = Columnas(3)
                        'EVALUO SI LA CANTIDAD ES NUMERICA
                        If Not IsNumeric(Columnas(4)) Then
                            NuevaLinea("Cantidad") = 0
                        Else
                            NuevaLinea("Cantidad") = CDbl(Columnas(4))
                        End If
                        'EVALUO SI EL COSTO ES NUMERICO
                        If Not IsNumeric(Columnas(5)) Then
                            NuevaLinea("CostoUnitario") = 0
                        Else
                            NuevaLinea("CostoUnitario") = Columnas(5)
                        End If
                        'EVALUO SIN EL TOTAL ES NUMERICO
                        If Not IsNumeric(Columnas(6)) Then
                            NuevaLinea("Total") = 0
                        Else
                            NuevaLinea("Total") = Columnas(6)
                        End If
                        'EVALUO SI LOS PRECIOS DE VENTA SON NUMERICOS
                        If Not IsNumeric(Columnas(7)) Then
                            NuevaLinea("PrecioVentaCor") = 0
                        Else
                            NuevaLinea("PrecioVentaCor") = Columnas(7)
                        End If
                        If Not IsNumeric(Columnas(8)) Then
                            NuevaLinea("PrecioVentaDol") = 0
                        Else
                            NuevaLinea("PrecioVentaDol") = Columnas(8)
                        End If

                        'CONSULTO SI EL IMPUESTO EXISTE
                        TablaAuxiliar = New DataTable
                        AdapterAuxiliar = New SqlDataAdapter("SELECT     TOP (1) Codigo FROM         Impuestos  WHERE     (Descripcion = '" & Columnas(9) & "')", Conexion)
                        AdapterAuxiliar.Fill(TablaAuxiliar)

                        If TablaAuxiliar.Rows.Count > 0 Then
                            NuevaLinea("NombreImpuesto") = TablaAuxiliar.Rows(0)("Codigo")
                        Else
                            Importable = False
                            If MessageBox.Show("El nombre del impuesto " & Columnas(9) & " no existe en el sistema, tenès que agregarlo antes de importar. ¿Querés continuar?", "ByFire Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            Else
                                HiloLector.Close()
                                Me.cboTipo.Enabled = True
                                Exit Sub
                            End If
                        End If

                        'CONSULTO SI LA LINEA EXISTE
                        TablaAuxiliar = New DataTable
                        AdapterAuxiliar = New SqlDataAdapter("SELECT     TOP (1) Id FROM         Lineas  WHERE     (Descripcion = '" & Columnas(10) & "')", Conexion)
                        AdapterAuxiliar.Fill(TablaAuxiliar)

                        If TablaAuxiliar.Rows.Count > 0 Then
                            NuevaLinea("NombreLinea") = TablaAuxiliar.Rows(0)("Id")
                        Else
                            Importable = False
                            If MessageBox.Show("El nombre de la linea " & Columnas(10) & " no existe en el sistema, tenès que agregarla antes de importar. ¿Querés continuar?", "ByFire Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            Else
                                HiloLector.Close()
                                Me.cboTipo.Enabled = True
                                Exit Sub
                            End If
                        End If


                        TablaImporte.Rows.Add(NuevaLinea)
                    Case "Proveedores"
                        Dim NuevaLinea As DataRow = TablaImporte.NewRow
                        NuevaLinea("Codigo") = Columnas(0)
                        NuevaLinea("Nombre") = Columnas(1)
                        NuevaLinea("Direccion") = Columnas(2)
                        NuevaLinea("Telefono") = Columnas(3)
                        NuevaLinea("Ruc") = Columnas(4)
                        NuevaLinea("Departamento") = Columnas(5)
                        NuevaLinea("Responsable") = Columnas(6)
                        NuevaLinea("Exonerado") = Columnas(7)
                        TablaImporte.Rows.Add(NuevaLinea)
                    Case "Clientes"
                        Dim NuevaLinea As DataRow = TablaImporte.NewRow
                        NuevaLinea("Codigo") = Columnas(0)
                        NuevaLinea("Nombre") = Columnas(1)
                        NuevaLinea("Direccion") = Columnas(2)
                        NuevaLinea("Telefono") = Columnas(3)
                        NuevaLinea("Ruc") = Columnas(4)
                        NuevaLinea("Departamento") = Columnas(5)
                        NuevaLinea("Responsable") = Columnas(6)
                        NuevaLinea("Exonerado") = Columnas(7)
                        TablaImporte.Rows.Add(NuevaLinea)
                End Select
                i = 1 + 1
            Loop
            Me.cboTipo.Enabled = True
            HiloLector.Close()
            gridImportacion.DataSource = (TablaImporte)
            gridImportacion.RowHeight = 18
        End If
    End Sub

    Private Sub btnImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportar.Click

        SubTotalCompra = 0
        TasaCambio = 1
        If Me.gridImportacion.RowCount <= 0 Then
            MessageBox.Show("No hay nada que importar, por favor, buscá el archivo con contenido importable segun su tipo.", "ByFire Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.cboTipo.DroppedDown = True
            Exit Sub
        End If
        If Importable = False Then
            MessageBox.Show("Los datos que subiste no son importables", "ByFire Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Me.cboTipo.Enabled = False
        Me.cboBodega.Enabled = False
        Me.cboSucursal.Enabled = False
        Dim Contador As Integer = 0
        Me.pbImportacion.Maximum = gridImportacion.RowCount

        Select Case Me.cboTipo.Text
            Case "Productos" '/////////////////////// PRODUCTOS /////////////////////////

                'Busco tasa de cambio del día de la factura, si no hay mando a llamar el formulario tasas de cambio
                TablaConsulta = New DataTable
                AdapterConsulta = New SqlDataAdapter("SELECT     Fecha, Monto  FROM         TasasDeCambio     WHERE     (Fecha = '" & Format(DateTime.Now, "yyyyMMdd") & "')", Conexion)
                AdapterConsulta.Fill(TablaConsulta)
                If TablaConsulta.Rows.Count <= 0 Then
                    MessageBox.Show("Uh, tenés que agregar tasa de cambio en la fecha de la importación, actualmente no existe en el sistema para la fecha " & Format(DateTime.Now, "dd/MM/yyyy") & "", "ByFire Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.cboTipo.Enabled = True
                    Me.cboBodega.Enabled = True
                    Me.cboSucursal.Enabled = True
                    Exit Sub
                    'FormTasasCambio.Show()
                Else
                    TasaCambio = CDbl(TablaConsulta.Rows(0)("Monto"))
                End If

                If Me.cboBodega.Text = "" Then
                    MessageBox.Show("Tenés que seleccionar una bodega del catálogo", "ByFire Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.cboBodega.DroppedDown = True
                    Me.cboTipo.Enabled = True
                    Me.cboBodega.Enabled = True
                    Me.cboSucursal.Enabled = True
                    Exit Sub
                End If

                If Me.cboSucursal.Text = "" Then
                    MessageBox.Show("Tenés que seleccionar una Sucursal del catálogo", "ByFire Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.cboSucursal.DroppedDown = True
                    Me.cboTipo.Enabled = True
                    Me.cboBodega.Enabled = True
                    Me.cboSucursal.Enabled = True
                    Exit Sub
                End If


                '///// INSERTO LOS PRODUCTOS ///////
                Do While Contador < Me.gridImportacion.RowCount
                    My.Application.DoEvents()

                    Me.pbImportacion.Value = Contador + 1
                    Me.lblRegistro.Text = TablaImporte.Rows(Contador)("Codigo") & TablaImporte.Rows(Contador)("Descripcion")
                    SubTotalCompra = SubTotalCompra + CDbl(TablaImporte.Rows(Contador)("Total"))

                    TablaConsulta = New DataTable
                    AdapterConsulta = New SqlDataAdapter("Select * from Productos Where Codigo = '" & TablaImporte.Rows(Contador)("Codigo") & "'", Conexion)
                    AdapterConsulta.Fill(TablaConsulta)
                    If TablaConsulta.Rows.Count > 0 Then
                        SqlString = "UPDATE  [Productos]"
                        SqlString = SqlString + " SET [Descripcion] = '" & TablaImporte.Rows(Contador)("Descripcion") & "'"
                        SqlString = SqlString + "  ,[TipoProducto] = '" & TablaImporte.Rows(Contador)("TipoProducto") & "' "
                        SqlString = SqlString + " ,[UnidadMedida] = '" & TablaImporte.Rows(Contador)("UnidadMedida") & "'"
                        SqlString = SqlString + " ,[Activo] = 'True'"
                        SqlString = SqlString + " ,[ExistenciaNegativa] = 'False'"
                        SqlString = SqlString + " ,[RutaFoto] = ''"
                        SqlString = SqlString + " ,[PrecioVentaCor] = " & CDbl(TablaImporte.Rows(Contador)("PrecioVentaCor")) & ""
                        SqlString = SqlString + " ,[PrecioVentaDol] = " & CDbl(TablaImporte.Rows(Contador)("PrecioVentaDol")) & ""
                        SqlString = SqlString + " ,[CostoEstimadoCor] = 0"
                        SqlString = SqlString + " ,[CostoEstimadoDol] = 0"
                        SqlString = SqlString + " ,[PorAumentoCor] = 0"
                        SqlString = SqlString + " ,[PorAumentoDol] = 0"
                        SqlString = SqlString + " ,[CodImpuesto] = '" & TablaImporte.Rows(Contador)("NombreImpuesto") & "'"
                        SqlString = SqlString + " ,[CodLinea] = " & CInt(TablaImporte.Rows(Contador)("NombreLinea")) & ""
                        SqlString = SqlString + " WHERE [Codigo] = '" & TablaImporte.Rows(Contador)("Codigo") & "'"
                        Command = New SqlCommand(SqlString, Conexion)
                    Else
                        Command = New SqlCommand("Insert into Productos (Codigo, Descripcion, TipoProducto, UnidadMedida, Ubicacion, Activo, ExistenciaNegativa, RutaFoto, PrecioVentaCor, PrecioVentaDol, CostoEstimadoCor, CostoEstimadoDol, PorAumentoCor, PorAumentoDol, CodImpuesto, CodLinea) values ('" & TablaImporte.Rows(Contador)("Codigo") & "', '" & TablaImporte.Rows(Contador)("Descripcion") & "', '" & TablaImporte.Rows(Contador)("TipoProducto") & "', '" & TablaImporte.Rows(Contador)("UnidadMedida") & "', '', 'True', 'False', '', " & CDbl(TablaImporte.Rows(Contador)("PrecioVentaCor")) & ", " & CDbl(TablaImporte.Rows(Contador)("PrecioVentaDol")) & ", 0, 0, 0, 0, '" & TablaImporte.Rows(Contador)("NombreImpuesto") & "', " & CInt(TablaImporte.Rows(Contador)("NombreLinea")) & ")", Conexion)
                    End If

                    Conexion.Open()
                    Command.ExecuteNonQuery()
                    Conexion.Close()

                    Contador = Contador + 1
                Loop

                '//////////////// CREO LA COMPRA ////////////////////////
                SqlString = "INSERT INTO [Compras]"
                SqlString = SqlString + "   ([NumeroCompra]"
                SqlString = SqlString + "   ,[TipoCompra]"
                SqlString = SqlString + "    ,[CodSucursal]"
                SqlString = SqlString + "    ,[FechaCompra]"
                SqlString = SqlString + "    ,[FechaVencimiento]"
                SqlString = SqlString + "    ,[Credito]"
                SqlString = SqlString + "    ,[CodBodega]"
                SqlString = SqlString + "    ,[CodProveedor]"
                SqlString = SqlString + "     ,[Ruc]"
                SqlString = SqlString + "    ,[NombreProveedor]"
                SqlString = SqlString + "    ,[DireccionProveedor]"
                SqlString = SqlString + "    ,[Telefono]"
                SqlString = SqlString + "    ,[Observaciones]"
                SqlString = SqlString + "    ,[SubTotal]"
                SqlString = SqlString + "    ,[Impuestos]"
                SqlString = SqlString + "    ,[Descuento]"
                SqlString = SqlString + "    ,[TotalCompra]"
                SqlString = SqlString + "    ,[PersonaContacto]"
                SqlString = SqlString + "    ,[Exonerado]"
                SqlString = SqlString + "    ,[Activo]"
                SqlString = SqlString + "    ,[Moneda]"
                SqlString = SqlString + "    ,[MonedaImprime]"
                SqlString = SqlString + "    ,[PorcentajeTarjeta]"
                SqlString = SqlString + "    ,[TasaCambio]"
                SqlString = SqlString + "    ,[TotalPagado])"

                SqlString = SqlString + "  VALUES "

                'Saco el consecutivo de la compra
                AdapterConsulta = New SqlDataAdapter("SELECT     Codigo, Sucursal, Telefono, Compras  FROM         ConsecutivoSucursales where Codigo = '" & Me.cboSucursal.Text & "'", Conexion)
                TablaConsulta = New DataTable
                AdapterConsulta.Fill(TablaConsulta)
                Dim ConsectivoCompra As Integer = Nothing
                If TablaConsulta.Rows.Count > 0 Then
                    ConsectivoCompra = TablaConsulta.Rows(0)("Compras") '////  CONSECUTIVO DE LA COMPRA
                End If

                'Saco el proveedor general
                AdapterConsulta = New SqlDataAdapter("SELECT     * FROM         Proveedores where Codigo = 'ProveedorImportacion'", Conexion)
                TablaConsulta = New DataTable
                AdapterConsulta.Fill(TablaConsulta)
                Dim CodProveedor As String = "ProveedorImportacion"
                If TablaConsulta.Rows.Count <= 0 Then           '////// SI NO EXISTE EL PROVEEDOR GENERAL LO AGREGO COMO NUEVO //////
                    SqlString = "INSERT INTO  [Proveedores]"
                    SqlString = SqlString & "   ([Codigo]"
                    SqlString = SqlString & "  ,[Nombre]"
                    SqlString = SqlString & "  ,[Direccion]"
                    SqlString = SqlString & "  ,[Telefono]"
                    SqlString = SqlString & "  ,[Activo]"
                    SqlString = SqlString & "  ,[Ruc]"
                    SqlString = SqlString & "  ,[Departamento]"
                    SqlString = SqlString & "  ,[Responsable]"
                    SqlString = SqlString & "  ,[TelefonoResponsable]"
                    SqlString = SqlString & "  ,[Exonerado])"

                    SqlString = SqlString & "    VALUES "

                    SqlString = SqlString & "  ('ProveedorImportacion'"
                    SqlString = SqlString & "  ,'Proveedor Importacion'"
                    SqlString = SqlString & "  ,''"
                    SqlString = SqlString & "  ,''"
                    SqlString = SqlString & "  ,'True'"
                    SqlString = SqlString & "  ,''"
                    SqlString = SqlString & "  ,''"
                    SqlString = SqlString & "  ,''"
                    SqlString = SqlString & "  ,''"
                    SqlString = SqlString & "  ,'True')"

                    Command = New SqlCommand(SqlString, Conexion)
                    Conexion.Open()
                    Command.ExecuteNonQuery()
                    Conexion.Close()

                End If


                SqlString = SqlString + "    ('" & ConsectivoCompra & "'"
                SqlString = SqlString + "    ,'Compra'"
                SqlString = SqlString + "    ,'" & Me.cboSucursal.Text & "'"
                SqlString = SqlString + "    ,'" & Format(DateTime.Now, "yyyyMMdd") & "'"
                SqlString = SqlString + "    ,'" & Format(DateTime.Now, "yyyyMMdd") & "'"
                SqlString = SqlString + "    ,'False'"
                SqlString = SqlString + "     ,'" & Me.cboBodega.SelectedValue & "'"
                SqlString = SqlString + "    ,'" & CodProveedor & "'"
                SqlString = SqlString + "    ,'Inventario Inicial'"
                SqlString = SqlString + "    ,'Inventario Inicial'"
                SqlString = SqlString + "    ,'Inventario Inicial'"
                SqlString = SqlString + "    ,'Inventario Inicial'"
                SqlString = SqlString + "    ,'Moviemiento de inventario inicial a la bodega " & Me.cboBodega.Text & "'"
                SqlString = SqlString + "    ," & CDbl(SubTotalCompra) & ""
                SqlString = SqlString + "    ,0"
                SqlString = SqlString + "    ,0"
                SqlString = SqlString + "    ," & CDbl(SubTotalCompra) & ""
                SqlString = SqlString + "    ,'Inventario Inicial'"
                SqlString = SqlString + "    ,'True'"
                SqlString = SqlString + "    ,'True'"
                SqlString = SqlString + "    ,'Cordobas'"
                SqlString = SqlString + "    ,'Cordobas'"
                SqlString = SqlString + "    ,'False'"
                SqlString = SqlString + "    ," & CDbl(TasaCambio) & ""
                SqlString = SqlString + "    ," & CDbl(SubTotalCompra) & ")"

                Command = New SqlCommand(SqlString, Conexion)
                Conexion.Open()
                Command.ExecuteNonQuery()
                Conexion.Close()

                '//////////////// INSERTO EL DETALLE DE LA COMPRA ////////////////
                Contador = 0
                pbImportacion.Maximum = Me.TablaImporte.Rows.Count
                Do While Contador < TablaImporte.Rows.Count

                    Contador = Contador + 1
                Loop



            Case "Proveedores" '//////////////// PROVEEDORES /////////////////
            Case "Clientes"     '/////////////// CLIENTES  //////////////////
        End Select

        Me.TablaImporte.Clear()
        Me.pbImportacion.Maximum = 0
        Me.pbImportacion.Value = 0
        Me.lblRegistro.Text = ""
        Me.txtRuta.Value = ""
        Me.cboTipo.Enabled = True
        Me.cboBodega.Enabled = True
        Me.cboSucursal.Enabled = True
        Importable = False
    End Sub
End Class