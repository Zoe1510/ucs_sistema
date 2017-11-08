namespace UCS_NODO_FGC
{
    partial class Registrar_clientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbxFee = new System.Windows.Forms.ComboBox();
            this.txtNombreArea = new System.Windows.Forms.TextBox();
            this.txtNombreEmpresa = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtNombreContactoArea = new System.Windows.Forms.TextBox();
            this.btnGuardarCliente = new System.Windows.Forms.Button();
            this.txtTelefonoCliArea = new System.Windows.Forms.TextBox();
            this.txtCorreoCliArea = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.errorProviderEmpresa = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderFee = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderNArea = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderContacto = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderTlfn = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCorreo = new System.Windows.Forms.ErrorProvider(this.components);
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderContacto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).BeginInit();
            this.Panel_cabecera.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbxFee);
            this.groupBox1.Controls.Add(this.txtNombreArea);
            this.groupBox1.Controls.Add(this.txtNombreEmpresa);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 12F);
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(338, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 217);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos empresa";
            // 
            // cmbxFee
            // 
            this.cmbxFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxFee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxFee.FormattingEnabled = true;
            this.cmbxFee.Items.AddRange(new object[] {
            "FEE",
            "NO FEE"});
            this.cmbxFee.Location = new System.Drawing.Point(88, 102);
            this.cmbxFee.Name = "cmbxFee";
            this.cmbxFee.Size = new System.Drawing.Size(310, 27);
            this.cmbxFee.TabIndex = 3;
            // 
            // txtNombreArea
            // 
            this.txtNombreArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreArea.Location = new System.Drawing.Point(88, 154);
            this.txtNombreArea.MaxLength = 99;
            this.txtNombreArea.Multiline = true;
            this.txtNombreArea.Name = "txtNombreArea";
            this.txtNombreArea.Size = new System.Drawing.Size(310, 34);
            this.txtNombreArea.TabIndex = 2;
            this.txtNombreArea.Text = "Nombre del área";
            this.txtNombreArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreArea.Click += new System.EventHandler(this.txtNombreArea_Click);
            this.txtNombreArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreArea_KeyDown);
            this.txtNombreArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreArea_KeyPress);
            this.txtNombreArea.Leave += new System.EventHandler(this.txtNombreArea_Leave);
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreEmpresa.Location = new System.Drawing.Point(88, 42);
            this.txtNombreEmpresa.MaxLength = 99;
            this.txtNombreEmpresa.Multiline = true;
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.Size = new System.Drawing.Size(310, 34);
            this.txtNombreEmpresa.TabIndex = 1;
            this.txtNombreEmpresa.Text = "Nombre empresa";
            this.txtNombreEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreEmpresa.Click += new System.EventHandler(this.txtNombreEmpresa_Click);
            this.txtNombreEmpresa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreEmpresa_KeyDown);
            this.txtNombreEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreEmpresa_KeyPress);
            this.txtNombreEmpresa.Leave += new System.EventHandler(this.txtNombreEmpresa_Leave);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Location = new System.Drawing.Point(617, 631);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(147, 47);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtNombreContactoArea
            // 
            this.txtNombreContactoArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreContactoArea.Location = new System.Drawing.Point(88, 43);
            this.txtNombreContactoArea.MaxLength = 99;
            this.txtNombreContactoArea.Multiline = true;
            this.txtNombreContactoArea.Name = "txtNombreContactoArea";
            this.txtNombreContactoArea.Size = new System.Drawing.Size(310, 34);
            this.txtNombreContactoArea.TabIndex = 4;
            this.txtNombreContactoArea.Text = "Persona contacto del área";
            this.txtNombreContactoArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreContactoArea.Click += new System.EventHandler(this.txtNombreContacto_Click);
            this.txtNombreContactoArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreContacto_KeyDown);
            this.txtNombreContactoArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreContacto_KeyPress);
            this.txtNombreContactoArea.Leave += new System.EventHandler(this.txtNombreContacto_Leave);
            // 
            // btnGuardarCliente
            // 
            this.btnGuardarCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnGuardarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarCliente.FlatAppearance.BorderColor = System.Drawing.Color.DarkGreen;
            this.btnGuardarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarCliente.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold);
            this.btnGuardarCliente.ForeColor = System.Drawing.Color.White;
            this.btnGuardarCliente.Location = new System.Drawing.Point(403, 631);
            this.btnGuardarCliente.Name = "btnGuardarCliente";
            this.btnGuardarCliente.Size = new System.Drawing.Size(147, 47);
            this.btnGuardarCliente.TabIndex = 7;
            this.btnGuardarCliente.Text = "Registrar";
            this.btnGuardarCliente.UseVisualStyleBackColor = false;
            this.btnGuardarCliente.Click += new System.EventHandler(this.btnGuardarCliente_Click);
            // 
            // txtTelefonoCliArea
            // 
            this.txtTelefonoCliArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefonoCliArea.Location = new System.Drawing.Point(88, 98);
            this.txtTelefonoCliArea.MaxLength = 11;
            this.txtTelefonoCliArea.Multiline = true;
            this.txtTelefonoCliArea.Name = "txtTelefonoCliArea";
            this.txtTelefonoCliArea.Size = new System.Drawing.Size(310, 34);
            this.txtTelefonoCliArea.TabIndex = 5;
            this.txtTelefonoCliArea.Text = "Teléfono o celular";
            this.txtTelefonoCliArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTelefonoCliArea.Click += new System.EventHandler(this.txtTelefonoCli_Click);
            this.txtTelefonoCliArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelefonoCli_KeyDown);
            this.txtTelefonoCliArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefonoCli_KeyPress);
            this.txtTelefonoCliArea.Leave += new System.EventHandler(this.txtTelefonoCli_Leave);
            this.txtTelefonoCliArea.Validating += new System.ComponentModel.CancelEventHandler(this.txtTelefonoCliArea_Validating);
            // 
            // txtCorreoCliArea
            // 
            this.txtCorreoCliArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoCliArea.Location = new System.Drawing.Point(88, 158);
            this.txtCorreoCliArea.MaxLength = 255;
            this.txtCorreoCliArea.Multiline = true;
            this.txtCorreoCliArea.Name = "txtCorreoCliArea";
            this.txtCorreoCliArea.Size = new System.Drawing.Size(310, 34);
            this.txtCorreoCliArea.TabIndex = 6;
            this.txtCorreoCliArea.Text = "correo@ejemplo.com";
            this.txtCorreoCliArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCorreoCliArea.Click += new System.EventHandler(this.txtCorreoCli_Click);
            this.txtCorreoCliArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCorreoCli_KeyDown);
            this.txtCorreoCliArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCorreoCliArea_KeyPress);
            this.txtCorreoCliArea.Leave += new System.EventHandler(this.txtCorreoCli_Leave);
            this.txtCorreoCliArea.Validating += new System.ComponentModel.CancelEventHandler(this.txtCorreoCliArea_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNombreContactoArea);
            this.groupBox2.Controls.Add(this.txtCorreoCliArea);
            this.groupBox2.Controls.Add(this.txtTelefonoCliArea);
            this.groupBox2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox2.Location = new System.Drawing.Point(338, 378);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 214);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos persona";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 716);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1122, 25);
            this.panel8.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label9.Location = new System.Drawing.Point(241, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(663, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Derechos reservados Universidad de Margarita © 2017. Proyecto de Pasantías Elabor" +
    "ado por Br. Zoyla Bermúdez";
            // 
            // errorProviderEmpresa
            // 
            this.errorProviderEmpresa.ContainerControl = this;
            // 
            // errorProviderFee
            // 
            this.errorProviderFee.ContainerControl = this;
            // 
            // errorProviderNArea
            // 
            this.errorProviderNArea.ContainerControl = this;
            // 
            // errorProviderContacto
            // 
            this.errorProviderContacto.ContainerControl = this;
            // 
            // errorProviderTlfn
            // 
            this.errorProviderTlfn.ContainerControl = this;
            // 
            // errorProviderCorreo
            // 
            this.errorProviderCorreo.ContainerControl = this;
            // 
            // Panel_cabecera
            // 
            this.Panel_cabecera.BackColor = System.Drawing.Color.MidnightBlue;
            this.Panel_cabecera.BackgroundImage = global::UCS_NODO_FGC.Properties.Resources.panel_cabecera;
            this.Panel_cabecera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel_cabecera.Controls.Add(this.label8);
            this.Panel_cabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_cabecera.Location = new System.Drawing.Point(0, 0);
            this.Panel_cabecera.Name = "Panel_cabecera";
            this.Panel_cabecera.Size = new System.Drawing.Size(1122, 96);
            this.Panel_cabecera.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(357, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(407, 43);
            this.label8.TabIndex = 1;
            this.label8.Text = "Registro de clientes";
            // 
            // rectangleShape7
            // 
            this.rectangleShape7.BorderColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape7.Location = new System.Drawing.Point(0, 96);
            this.rectangleShape7.Name = "rectangleShape7";
            this.rectangleShape7.Size = new System.Drawing.Size(1120, 1);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape7});
            this.shapeContainer1.Size = new System.Drawing.Size(1122, 741);
            this.shapeContainer1.TabIndex = 45;
            this.shapeContainer1.TabStop = false;
            // 
            // Registrar_clientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1122, 741);
            this.Controls.Add(this.Panel_cabecera);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGuardarCliente);
            this.Controls.Add(this.shapeContainer1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Registrar_clientes";
            this.Load += new System.EventHandler(this.Registrar_clientes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderContacto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).EndInit();
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardarCliente;
        private System.Windows.Forms.TextBox txtNombreEmpresa;
        private System.Windows.Forms.TextBox txtNombreContactoArea;
        private System.Windows.Forms.TextBox txtTelefonoCliArea;
        private System.Windows.Forms.TextBox txtCorreoCliArea;
        private System.Windows.Forms.TextBox txtNombreArea;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbxFee;
        private System.Windows.Forms.ErrorProvider errorProviderEmpresa;
        private System.Windows.Forms.ErrorProvider errorProviderFee;
        private System.Windows.Forms.ErrorProvider errorProviderNArea;
        private System.Windows.Forms.ErrorProvider errorProviderContacto;
        private System.Windows.Forms.ErrorProvider errorProviderTlfn;
        private System.Windows.Forms.ErrorProvider errorProviderCorreo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel Panel_cabecera;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
    }
}