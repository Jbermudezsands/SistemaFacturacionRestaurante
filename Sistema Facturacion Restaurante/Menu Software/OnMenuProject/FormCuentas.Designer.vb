<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCuentas
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCuentas))
        Me.tlpPrincipal = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlEncabezado = New System.Windows.Forms.Panel()
        Me.lblSubtitulo = New System.Windows.Forms.Label()
        Me.lblSaludo = New System.Windows.Forms.Label()
        Me.tlpArriba = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlMisMesas = New System.Windows.Forms.Panel()
        Me.FLPMisMesas = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlMisCuentas = New System.Windows.Forms.Panel()
        Me.FLPMisCuentas = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tlpNavArriba = New System.Windows.Forms.TableLayoutPanel()
        Me.C1Button1 = New System.Windows.Forms.Button()
        Me.C1Button2 = New System.Windows.Forms.Button()
        Me.pnlDisponibles = New System.Windows.Forms.Panel()
        Me.FLPMesasDisponibles = New System.Windows.Forms.FlowLayoutPanel()
        Me.FLPZonas = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tlpNavAbajo = New System.Windows.Forms.TableLayoutPanel()
        Me.C1Button4 = New System.Windows.Forms.Button()
        Me.C1Button3 = New System.Windows.Forms.Button()
        Me.tlpPrincipal.SuspendLayout()
        Me.pnlEncabezado.SuspendLayout()
        Me.tlpArriba.SuspendLayout()
        Me.pnlMisMesas.SuspendLayout()
        Me.pnlMisCuentas.SuspendLayout()
        Me.tlpNavArriba.SuspendLayout()
        Me.pnlDisponibles.SuspendLayout()
        Me.tlpNavAbajo.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpPrincipal
        '
        Me.tlpPrincipal.ColumnCount = 2
        Me.tlpPrincipal.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPrincipal.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92.0!))
        Me.tlpPrincipal.Controls.Add(Me.pnlEncabezado, 0, 0)
        Me.tlpPrincipal.Controls.Add(Me.tlpArriba, 0, 1)
        Me.tlpPrincipal.Controls.Add(Me.tlpNavArriba, 1, 1)
        Me.tlpPrincipal.Controls.Add(Me.pnlDisponibles, 0, 2)
        Me.tlpPrincipal.Controls.Add(Me.tlpNavAbajo, 1, 2)
        Me.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.tlpPrincipal.Name = "tlpPrincipal"
        Me.tlpPrincipal.Padding = New System.Windows.Forms.Padding(16, 12, 16, 16)
        Me.tlpPrincipal.RowCount = 3
        Me.tlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74.0!))
        Me.tlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.0!))
        Me.tlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.0!))
        Me.tlpPrincipal.TabIndex = 0
        '
        'pnlEncabezado
        '
        Me.tlpPrincipal.SetColumnSpan(Me.pnlEncabezado, 2)
        Me.pnlEncabezado.Controls.Add(Me.lblSubtitulo)
        Me.pnlEncabezado.Controls.Add(Me.lblSaludo)
        Me.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEncabezado.Location = New System.Drawing.Point(19, 15)
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.TabIndex = 0
        '
        'lblSubtitulo
        '
        Me.lblSubtitulo.AutoSize = True
        Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(170, 185, 205)
        Me.lblSubtitulo.Location = New System.Drawing.Point(2, 42)
        Me.lblSubtitulo.Name = "lblSubtitulo"
        Me.lblSubtitulo.Text = "Selecciona una mesa para continuar una cuenta o abre una nueva"
        '
        'lblSaludo
        '
        Me.lblSaludo.AutoSize = True
        Me.lblSaludo.Font = New System.Drawing.Font("Segoe UI Semibold", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblSaludo.ForeColor = System.Drawing.Color.White
        Me.lblSaludo.Location = New System.Drawing.Point(0, 0)
        Me.lblSaludo.Name = "lblSaludo"
        Me.lblSaludo.Text = "Hola"
        '
        'tlpArriba
        '
        Me.tlpArriba.ColumnCount = 2
        Me.tlpArriba.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.tlpArriba.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.tlpArriba.Controls.Add(Me.pnlMisMesas, 0, 0)
        Me.tlpArriba.Controls.Add(Me.pnlMisCuentas, 1, 0)
        Me.tlpArriba.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpArriba.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpArriba.Name = "tlpArriba"
        Me.tlpArriba.RowCount = 1
        Me.tlpArriba.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpArriba.TabIndex = 1
        '
        'pnlMisMesas
        '
        Me.pnlMisMesas.BackColor = System.Drawing.Color.White
        Me.pnlMisMesas.Controls.Add(Me.FLPMisMesas)
        Me.pnlMisMesas.Controls.Add(Me.Label1)
        Me.pnlMisMesas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMisMesas.Margin = New System.Windows.Forms.Padding(0, 0, 6, 8)
        Me.pnlMisMesas.Name = "pnlMisMesas"
        Me.pnlMisMesas.Padding = New System.Windows.Forms.Padding(14, 10, 14, 10)
        Me.pnlMisMesas.TabIndex = 0
        '
        'FLPMisMesas
        '
        Me.FLPMisMesas.AutoScroll = True
        Me.FLPMisMesas.BackColor = System.Drawing.Color.White
        Me.FLPMisMesas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLPMisMesas.Name = "FLPMisMesas"
        Me.FLPMisMesas.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Mis mesas"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMisCuentas
        '
        Me.pnlMisCuentas.BackColor = System.Drawing.Color.White
        Me.pnlMisCuentas.Controls.Add(Me.FLPMisCuentas)
        Me.pnlMisCuentas.Controls.Add(Me.Label2)
        Me.pnlMisCuentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMisCuentas.Margin = New System.Windows.Forms.Padding(6, 0, 0, 8)
        Me.pnlMisCuentas.Name = "pnlMisCuentas"
        Me.pnlMisCuentas.Padding = New System.Windows.Forms.Padding(14, 10, 14, 10)
        Me.pnlMisCuentas.TabIndex = 1
        '
        'FLPMisCuentas
        '
        Me.FLPMisCuentas.AutoScroll = True
        Me.FLPMisCuentas.BackColor = System.Drawing.Color.White
        Me.FLPMisCuentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLPMisCuentas.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLPMisCuentas.Name = "FLPMisCuentas"
        Me.FLPMisCuentas.TabIndex = 1
        Me.FLPMisCuentas.WrapContents = False
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 32)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Mis cuentas abiertas"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tlpNavArriba
        '
        Me.tlpNavArriba.ColumnCount = 1
        Me.tlpNavArriba.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpNavArriba.Controls.Add(Me.C1Button1, 0, 0)
        Me.tlpNavArriba.Controls.Add(Me.C1Button2, 0, 1)
        Me.tlpNavArriba.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpNavArriba.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
        Me.tlpNavArriba.Name = "tlpNavArriba"
        Me.tlpNavArriba.RowCount = 2
        Me.tlpNavArriba.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpNavArriba.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpNavArriba.TabIndex = 2
        '
        'C1Button1
        '
        Me.C1Button1.BackColor = System.Drawing.Color.FromArgb(52, 94, 152)
        Me.C1Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Button1.FlatAppearance.BorderSize = 0
        Me.C1Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(30, 62, 110)
        Me.C1Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 78, 130)
        Me.C1Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.C1Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 22.0!, System.Drawing.FontStyle.Bold)
        Me.C1Button1.ForeColor = System.Drawing.Color.White
        Me.C1Button1.Margin = New System.Windows.Forms.Padding(8, 0, 0, 4)
        Me.C1Button1.Name = "C1Button1"
        Me.C1Button1.TabIndex = 0
        Me.C1Button1.Text = "↑"
        Me.C1Button1.UseVisualStyleBackColor = False
        '
        'C1Button2
        '
        Me.C1Button2.BackColor = System.Drawing.Color.FromArgb(52, 94, 152)
        Me.C1Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Button2.FlatAppearance.BorderSize = 0
        Me.C1Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(30, 62, 110)
        Me.C1Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 78, 130)
        Me.C1Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.C1Button2.Font = New System.Drawing.Font("Segoe UI Semibold", 22.0!, System.Drawing.FontStyle.Bold)
        Me.C1Button2.ForeColor = System.Drawing.Color.White
        Me.C1Button2.Margin = New System.Windows.Forms.Padding(8, 4, 0, 0)
        Me.C1Button2.Name = "C1Button2"
        Me.C1Button2.TabIndex = 1
        Me.C1Button2.Text = "↓"
        Me.C1Button2.UseVisualStyleBackColor = False
        '
        'pnlDisponibles
        '
        Me.pnlDisponibles.BackColor = System.Drawing.Color.White
        Me.pnlDisponibles.Controls.Add(Me.FLPMesasDisponibles)
        Me.pnlDisponibles.Controls.Add(Me.FLPZonas)
        Me.pnlDisponibles.Controls.Add(Me.Label3)
        Me.pnlDisponibles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDisponibles.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlDisponibles.Name = "pnlDisponibles"
        Me.pnlDisponibles.Padding = New System.Windows.Forms.Padding(14, 10, 14, 10)
        Me.pnlDisponibles.TabIndex = 3
        '
        'FLPMesasDisponibles
        '
        Me.FLPMesasDisponibles.AutoScroll = True
        Me.FLPMesasDisponibles.BackColor = System.Drawing.Color.White
        Me.FLPMesasDisponibles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLPMesasDisponibles.Name = "FLPMesasDisponibles"
        Me.FLPMesasDisponibles.TabIndex = 2
        '
        'FLPZonas
        '
        Me.FLPZonas.AutoScroll = True
        Me.FLPZonas.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLPZonas.Name = "FLPZonas"
        Me.FLPZonas.Size = New System.Drawing.Size(100, 52)
        Me.FLPZonas.TabIndex = 1
        Me.FLPZonas.WrapContents = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 32)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Mesas disponibles"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tlpNavAbajo
        '
        Me.tlpNavAbajo.ColumnCount = 1
        Me.tlpNavAbajo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpNavAbajo.Controls.Add(Me.C1Button4, 0, 0)
        Me.tlpNavAbajo.Controls.Add(Me.C1Button3, 0, 1)
        Me.tlpNavAbajo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpNavAbajo.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpNavAbajo.Name = "tlpNavAbajo"
        Me.tlpNavAbajo.RowCount = 2
        Me.tlpNavAbajo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpNavAbajo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpNavAbajo.TabIndex = 4
        '
        'C1Button4
        '
        Me.C1Button4.BackColor = System.Drawing.Color.FromArgb(52, 94, 152)
        Me.C1Button4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Button4.FlatAppearance.BorderSize = 0
        Me.C1Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(30, 62, 110)
        Me.C1Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 78, 130)
        Me.C1Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.C1Button4.Font = New System.Drawing.Font("Segoe UI Semibold", 22.0!, System.Drawing.FontStyle.Bold)
        Me.C1Button4.ForeColor = System.Drawing.Color.White
        Me.C1Button4.Margin = New System.Windows.Forms.Padding(8, 0, 0, 4)
        Me.C1Button4.Name = "C1Button4"
        Me.C1Button4.TabIndex = 0
        Me.C1Button4.Text = "↑"
        Me.C1Button4.UseVisualStyleBackColor = False
        '
        'C1Button3
        '
        Me.C1Button3.BackColor = System.Drawing.Color.FromArgb(52, 94, 152)
        Me.C1Button3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Button3.FlatAppearance.BorderSize = 0
        Me.C1Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(30, 62, 110)
        Me.C1Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 78, 130)
        Me.C1Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.C1Button3.Font = New System.Drawing.Font("Segoe UI Semibold", 22.0!, System.Drawing.FontStyle.Bold)
        Me.C1Button3.ForeColor = System.Drawing.Color.White
        Me.C1Button3.Margin = New System.Windows.Forms.Padding(8, 4, 0, 0)
        Me.C1Button3.Name = "C1Button3"
        Me.C1Button3.TabIndex = 1
        Me.C1Button3.Text = "↓"
        Me.C1Button3.UseVisualStyleBackColor = False
        '
        'FormCuentas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(24, 41, 66)
        Me.ClientSize = New System.Drawing.Size(1100, 680)
        Me.Controls.Add(Me.tlpPrincipal)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "FormCuentas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mesas y cuentas"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tlpPrincipal.ResumeLayout(False)
        Me.pnlEncabezado.ResumeLayout(False)
        Me.pnlEncabezado.PerformLayout()
        Me.tlpArriba.ResumeLayout(False)
        Me.pnlMisMesas.ResumeLayout(False)
        Me.pnlMisCuentas.ResumeLayout(False)
        Me.tlpNavArriba.ResumeLayout(False)
        Me.pnlDisponibles.ResumeLayout(False)
        Me.tlpNavAbajo.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpPrincipal As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlEncabezado As System.Windows.Forms.Panel
    Friend WithEvents lblSubtitulo As System.Windows.Forms.Label
    Friend WithEvents lblSaludo As System.Windows.Forms.Label
    Friend WithEvents tlpArriba As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlMisMesas As System.Windows.Forms.Panel
    Friend WithEvents FLPMisMesas As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlMisCuentas As System.Windows.Forms.Panel
    Friend WithEvents FLPMisCuentas As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tlpNavArriba As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents C1Button1 As System.Windows.Forms.Button
    Friend WithEvents C1Button2 As System.Windows.Forms.Button
    Friend WithEvents pnlDisponibles As System.Windows.Forms.Panel
    Friend WithEvents FLPMesasDisponibles As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLPZonas As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tlpNavAbajo As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents C1Button4 As System.Windows.Forms.Button
    Friend WithEvents C1Button3 As System.Windows.Forms.Button
End Class
