Imports System.Data.Sqlclient
Public Class FormCuentas
    Dim SelControlCuentas As Integer = 0
    Dim SelControlMesDis As Integer = 0
    Dim SelMesa As String
    Private Sub FormCuentas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Application.DoEvents()
        Me.FLPMisMesas.VerticalScroll.Value = Me.FLPMisMesas.VerticalScroll.Value + 100
        GroupBox1.Text = Form1.NombreVendedor
        Dim codvendedor = Form1.CodVendedor
        '///////////////////////// LLENO Datatable con las mesas //////////////////////
        '  Dim CommandString = "Select * from Facturas Where Cod_Vendedor = '" & codvendedor & "' and Activo = 'True' and Cancelado = 'False'"
        Dim commandstring = "Select distinct Cod_Cliente  from Facturas Where Cod_Vendedor = '" & codvendedor & "' and Activo = 'True' and Cancelado = 'False'"
        Dim DAMesas = New SqlDataAdapter(CommandString, TempConn)
        Dim DTMisMesas = New DataTable()
        DAMesas.Fill(DTMisMesas)
        Dim Posicion As Integer = 0

        '///////////////// RECORRO EL DATASET Y ASIGNO UN BOTON A CADA UNO DE LOS RESULTADOS/
        If DTMisMesas.Rows.Count <> 0 Then
            Do While (Posicion < DTMisMesas.Rows.Count)
                Dim btn As C1.Win.C1Input.C1Button = New C1.Win.C1Input.C1Button
                AddHandler btn.Click, AddressOf BuscarCuentas
                btn.Text = Mid(DTMisMesas.Rows(Posicion)("Cod_Cliente").ToString, 1, 20)
                btn.Name = DTMisMesas.Rows(Posicion)("Cod_Cliente")
                btn.Height = 60
                btn.Width = 78

                btn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btn.TextAlign = ContentAlignment.MiddleCenter

                Me.FLPMisMesas.Controls.Add(btn)

                Posicion = Posicion + 1
            Loop
        End If

        Me.SelControlCuentas = 0
        SelControlMesDis = 0
        BuscarMesasDisponiles()
    End Sub

    Public Sub BuscarMesasDisponiles()

        DeletePanelItems(Me.FLPMesasDisponibles)
        Dim TablaClientes As New DataTable
        Dim AdapterClientes As New SqlDataAdapter("Select Cod_Cliente, Nombre_Cliente from Clientes Where Limite_Credito = 0", TempConn)
        AdapterClientes.Fill(TablaClientes)

        Dim TablaFacturas As New DataTable
        Dim AdapterFacturas As New SqlDataAdapter("Select Cod_Cliente, Nombre_Cliente from Facturas Where Activo = 'True' and Cancelado = 'False'", TempConn)
        AdapterFacturas.Fill(TablaFacturas)

        Dim Contador As Integer = 0
        Do While Contador < TablaClientes.Rows.Count

            Dim result() As DataRow = TablaFacturas.Select("Cod_Cliente = '" & TablaClientes.Rows(Contador)("Cod_Cliente") & "'")
            If result.Length = 0 Then
                Dim btn As C1.Win.C1Input.C1Button = New C1.Win.C1Input.C1Button
                AddHandler btn.Click, AddressOf CrearCuenta
                btn.Text = TablaClientes.Rows(Contador)("Cod_Cliente")
                btn.Name = TablaClientes.Rows(Contador)("Cod_Cliente")
                btn.Height = 75
                btn.Width = 120

                btn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btn.TextAlign = ContentAlignment.MiddleCenter
                Me.FLPMesasDisponibles.Controls.Add(btn)
            End If
            Contador = Contador + 1
        Loop
    End Sub

    Public Sub CrearCuenta(ByVal Sender As Object, ByVal e As EventArgs)
        Form1.Show()
        ' Form1.BackToLife("")
        Dim Btn As Button
        Btn = CType(Sender, Button)
        Form1.cboMesas.SelectedValue = (Btn.Name)
        Me.Close()
    End Sub

    Public Sub DeletePanelItems(ByVal Pnl As FlowLayoutPanel)
        While Pnl.Controls.Count > 0 'Determine if the flow contains any controls
            Dim controltoremove = Pnl.Controls(0)
            Pnl.Controls.Remove(controltoremove)
            controltoremove.Dispose()
        End While
    End Sub

    Public Sub BuscarCuentas(ByVal Sender As Object, ByVal e As EventArgs)
        DeletePanelItems(FLPMisCuentas)
        Me.FLPMisCuentas.VerticalScroll.Minimum = 0
        Me.FLPMisCuentas.VerticalScroll.Maximum = 100
        Me.FLPMisCuentas.VerticalScroll.Value = 0
        '//////////////// ESTE BOTON VIENE DEL flowPanel1, capturo el sender y lo consulto //////
        Dim BtnFLPMesas As Button
        BtnFLPMesas = CType(Sender, Button)
        SelMesa = BtnFLPMesas.Name

        Dim codvendedor = Form1.CodVendedor
        '///////////////////////// LLENO Datatable con las mesas //////////////////////
        Dim CommandString = "Select * from Facturas Where Cod_Cliente = '" & BtnFLPMesas.Name & "' and Cod_Vendedor = '" & codvendedor & "' and Activo = 'True' and Cancelado = 'False'"
        Dim DaCuentas = New SqlDataAdapter(CommandString, TempConn)
        Dim DTCuentas = New DataTable()
        DaCuentas.Fill(DTCuentas)
        Dim Posicion As Integer = 0

        '///////////////// RECORRO EL DATASET Y ASIGNO UN BOTON A CADA UNO DE LOS RESULTADOS
        If DTCuentas.Rows.Count <> 0 Then
            Do While (Posicion < DTCuentas.Rows.Count)
                Dim btn As C1.Win.C1Input.C1Button = New C1.Win.C1Input.C1Button
                AddHandler btn.Click, AddressOf Ircuenta
                btn.Text = "Nº: " & DTCuentas.Rows(Posicion)("Numero_Factura").ToString & "  Total: " & DTCuentas.Rows(Posicion)("NetoPagar").ToString & "  Nombre: " & DTCuentas.Rows(Posicion)("Nombre_cliente").ToString
                btn.Name = DTCuentas.Rows(Posicion)("Numero_Factura")
                btn.Height = 50
                btn.Width = 200


                btn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btn.TextAlign = ContentAlignment.MiddleCenter
                Me.FLPMisCuentas.Controls.Add(btn)

                Posicion = Posicion + 1

            Loop
            Dim btn2 As C1.Win.C1Input.C1Button = New C1.Win.C1Input.C1Button
            AddHandler btn2.Click, AddressOf Ircuenta
            btn2.Text = "* Nueva Cuenta"
            btn2.Name = "NuevoFactura"
            btn2.Height = 50
            btn2.Width = 200


            btn2.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
            btn2.TextAlign = ContentAlignment.MiddleCenter
            Me.FLPMisCuentas.Controls.Add(btn2)
        End If
        BuscarMesasDisponiles()
    End Sub


    Public Sub Ircuenta(ByVal Sender As Object, ByVal e As EventArgs)
        Form1.Actualiza = True
        Form1.Show()
        Dim Btn As Button
        Btn = CType(Sender, Button)
        If Btn.Name = "NuevoFactura" Then
            Me.Close()
            Form1.BackToLife("")
            Form1.Actualiza = False
            Form1.cboMesas.Text = ""
            Form1.cboMesas.SelectedValue = SelMesa
            SelMesa = ""
        Else
            Form1.BackToLife(Btn.Name)
            Form1.NumeroFactura = Btn.Name

            Dim Adap As New SqlDataAdapter("SELECT     Fecha_Factura, Propina, Cod_Cliente, Cod_Vendedor, Nombre_Cliente, Apellido_Cliente, Direccion_Cliente, Telefono_Cliente, Observaciones, SubTotal, IVA, Pagado, NetoPagar, Exonerado, Propina  FROM         Facturas  WHERE     (Numero_Factura = '" & Btn.Name & "') and Tipo_Factura = 'Factura'", TempConn)
            Dim tConsulta As New DataTable
            Adap.Fill(tConsulta)

            Form1.cboMesas.SelectedValue = tConsulta.Rows(0)("Cod_Cliente")
            Form1.FechaFactura = Format(tConsulta.Rows(0)("Fecha_Factura"), "dd/MM/yyyy")
            Form1.CodVendedor = tConsulta.Rows(0)("Cod_Vendedor")
            Form1.txtNombre.Value = tConsulta.Rows(0)("Nombre_Cliente")
            Form1.txtRuc.Value = tConsulta.Rows(0)("Apellido_Cliente")
            Form1.txtDireccion.Value = tConsulta.Rows(0)("Direccion_Cliente")
            Form1.txtTelefono.Value = tConsulta.Rows(0)("Telefono_Cliente")
            Form1.txtSubTotal.Text = Format(tConsulta.Rows(0)("SubTotal"), "##,##0.00")
            Form1.txtIva.Text = Format(tConsulta.Rows(0)("Iva"), "##,##0.00")
            Form1.txtTotal.Text = Format(tConsulta.Rows(0)("Pagado"), "##,##0.00")
            Form1.ckExonerar.Checked = tConsulta.Rows(0)("Exonerado")
            Form1.txtpropina.Text = Format(tConsulta.Rows(0)("Propina"), "##,##0.00")
            Form1.txtObservacion.Value = tConsulta.Rows(0)("Observaciones")
            Me.Close()
        End If
        
    End Sub

    Private Sub C1Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button1.Click

        If FLPMisCuentas.Controls.Count = 0 Then
            Exit Sub
        End If

        If SelControlCuentas - 1 > 0 Then
            Me.SelControlCuentas = SelControlCuentas - 1
            FLPMisCuentas.Controls(SelControlCuentas - 1).Select()
        End If

    End Sub

    Private Sub C1Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button2.Click

        If FLPMisCuentas.Controls.Count = 0 Then
            Exit Sub
        End If

        If SelControlCuentas < FLPMisCuentas.Controls.Count Then
            Me.SelControlCuentas = SelControlCuentas + 1
            FLPMisCuentas.Controls(SelControlCuentas - 1).Select()
        End If

    End Sub

    Private Sub FLPMisCuentas_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles FLPMisCuentas.Paint

    End Sub

    Private Sub C1Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button4.Click
        If FLPMesasDisponibles.Controls.Count = 0 Then
            Exit Sub
        End If

        If SelControlMesDis - 1 > 0 Then
            Me.SelControlMesDis = SelControlMesDis - 1
            FLPMesasDisponibles.Controls(SelControlMesDis - 1).Select()
        End If
    End Sub

    Private Sub C1Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button3.Click
        If FLPMesasDisponibles.Controls.Count = 0 Then
            Exit Sub
        End If

        If SelControlMesDis < FLPMesasDisponibles.Controls.Count Then
            Me.SelControlMesDis = SelControlMesDis + 1
            FLPMesasDisponibles.Controls(SelControlMesDis - 1).Select()
        End If

    End Sub

    Private Sub FLPMisMesas_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles FLPMisMesas.Paint

    End Sub
End Class