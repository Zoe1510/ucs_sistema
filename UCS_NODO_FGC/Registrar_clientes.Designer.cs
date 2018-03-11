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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbxFee = new System.Windows.Forms.ComboBox();
            this.txtNombreArea = new System.Windows.Forms.TextBox();
            this.txtNombreEmpresa = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtNombreContactoArea = new System.Windows.Forms.TextBox();
            this.btnGuardarCliente = new System.Windows.Forms.Button();
            this.txtTelefonoCliArea = new System.Windows.Forms.TextBox();
            this.txtCorreoCliArea = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderContacto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).BeginInit();
            this.Panel_cabecera.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbxFee);
            this.groupBox1.Controls.Add(this.txtNombreArea);
            this.groupBox1.Controls.Add(this.txtNombreEmpresa);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 12F);
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(261, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 217);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos empresa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(93, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nombre del área:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(136, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Recibe FEE:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(49, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nombre de la empresa:";
            // 
            // cmbxFee
            // 
            this.cmbxFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxFee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxFee.FormattingEnabled = true;
            this.cmbxFee.Items.AddRange(new object[] {
            "FEE",
            "NO FEE"});
            this.cmbxFee.Location = new System.Drawing.Point(239, 102);
            this.cmbxFee.Name = "cmbxFee";
            this.cmbxFee.Size = new System.Drawing.Size(310, 27);
            this.cmbxFee.TabIndex = 2;
            this.cmbxFee.Validating += new System.ComponentModel.CancelEventHandler(this.cmbxFee_Validating);
            // 
            // txtNombreArea
            // 
            this.txtNombreArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreArea.Location = new System.Drawing.Point(239, 158);
            this.txtNombreArea.MaxLength = 99;
            this.txtNombreArea.Multiline = true;
            this.txtNombreArea.Name = "txtNombreArea";
            this.txtNombreArea.Size = new System.Drawing.Size(310, 34);
            this.txtNombreArea.TabIndex = 3;
            this.txtNombreArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreArea.Click += new System.EventHandler(this.txtNombreArea_Click);
            this.txtNombreArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreArea_KeyDown);
            this.txtNombreArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreArea_KeyPress);
            this.txtNombreArea.Leave += new System.EventHandler(this.txtNombreArea_Leave);
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreEmpresa.Location = new System.Drawing.Point(239, 46);
            this.txtNombreEmpresa.MaxLength = 99;
            this.txtNombreEmpresa.Multiline = true;
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.Size = new System.Drawing.Size(310, 31);
            this.txtNombreEmpresa.TabIndex = 1;
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
            this.btnCancelar.Location = new System.Drawing.Point(642, 631);
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
            this.txtNombreContactoArea.Location = new System.Drawing.Point(239, 41);
            this.txtNombreContactoArea.MaxLength = 99;
            this.txtNombreContactoArea.Multiline = true;
            this.txtNombreContactoArea.Name = "txtNombreContactoArea";
            this.txtNombreContactoArea.Size = new System.Drawing.Size(310, 34);
            this.txtNombreContactoArea.TabIndex = 4;
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
            this.btnGuardarCliente.Location = new System.Drawing.Point(375, 631);
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
            this.txtTelefonoCliArea.Location = new System.Drawing.Point(239, 96);
            this.txtTelefonoCliArea.MaxLength = 11;
            this.txtTelefonoCliArea.Multiline = true;
            this.txtTelefonoCliArea.Name = "txtTelefonoCliArea";
            this.txtTelefonoCliArea.Size = new System.Drawing.Size(310, 34);
            this.txtTelefonoCliArea.TabIndex = 5;
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
            this.txtCorreoCliArea.Location = new System.Drawing.Point(239, 156);
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
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtNombreContactoArea);
            this.groupBox2.Controls.Add(this.txtCorreoCliArea);
            this.groupBox2.Controls.Add(this.txtTelefonoCliArea);
            this.groupBox2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox2.Location = new System.Drawing.Point(261, 378);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(602, 214);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos contacto";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(73, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 19);
            this.label6.TabIndex = 9;
            this.label6.Text = "Correo del contacto:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(17, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(216, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Persona contacto en el área:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(89, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Teléfono o celular:";
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
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.shapeContainer4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 716);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1122, 25);
            this.panel8.TabIndex = 58;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label9.Location = new System.Drawing.Point(221, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(663, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Derechos reservados Universidad de Margarita © 2017. Proyecto de Pasantías Elabor" +
    "ado por Br. Zoyla Bermúdez";
            // 
            // shapeContainer4
            // 
            this.shapeContainer4.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer4.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer4.Name = "shapeContainer4";
            this.shapeContainer4.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape2});
            this.shapeContainer4.Size = new System.Drawing.Size(1122, 25);
            this.shapeContainer4.TabIndex = 1;
            this.shapeContainer4.TabStop = false;
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BorderColor = System.Drawing.Color.LightSlateGray;
            this.rectangleShape2.Location = new System.Drawing.Point(0, 0);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.Size = new System.Drawing.Size(1118, 1);
            // 
            // Registrar_clientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1122, 741);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.Panel_cabecera);
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
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderContacto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).EndInit();
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
    }
}