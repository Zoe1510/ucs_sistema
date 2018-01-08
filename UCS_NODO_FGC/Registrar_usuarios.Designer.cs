namespace UCS_NODO_FGC
{
    partial class Registrar_usuarios
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
            this.txtCedulaUsuario = new System.Windows.Forms.TextBox();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.txtApellidoUsuario = new System.Windows.Forms.TextBox();
            this.cmbxCargoUsuario = new System.Windows.Forms.ComboBox();
            this.btnGuardarUsuario = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbNacionalidad = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProviderCI = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderNombre = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderApellido = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCMBCargo = new System.Windows.Forms.ErrorProvider(this.components);
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderApellido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCMBCargo)).BeginInit();
            this.Panel_cabecera.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCedulaUsuario
            // 
            this.txtCedulaUsuario.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCedulaUsuario.Location = new System.Drawing.Point(216, 49);
            this.txtCedulaUsuario.MaxLength = 8;
            this.txtCedulaUsuario.Multiline = true;
            this.txtCedulaUsuario.Name = "txtCedulaUsuario";
            this.txtCedulaUsuario.Size = new System.Drawing.Size(226, 27);
            this.txtCedulaUsuario.TabIndex = 1;
            this.txtCedulaUsuario.Text = "Cédula";
            this.txtCedulaUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCedulaUsuario.Click += new System.EventHandler(this.txtCedulaUsuario_Click);
            this.txtCedulaUsuario.TextChanged += new System.EventHandler(this.txtCedulaUsuario_TextChanged);
            this.txtCedulaUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCedulaUsuario_KeyDown);
            this.txtCedulaUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCedulaUsuario_KeyPress);
            this.txtCedulaUsuario.Leave += new System.EventHandler(this.txtCedulaUsuario_Leave);
            this.txtCedulaUsuario.Validating += new System.ComponentModel.CancelEventHandler(this.txtCedulaUsuario_Validating);
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreUsuario.Location = new System.Drawing.Point(172, 120);
            this.txtNombreUsuario.MaxLength = 50;
            this.txtNombreUsuario.Multiline = true;
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(270, 28);
            this.txtNombreUsuario.TabIndex = 2;
            this.txtNombreUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreUsuario.Click += new System.EventHandler(this.txtNombreUsuario_Click);
            this.txtNombreUsuario.TextChanged += new System.EventHandler(this.txtNombreUsuario_TextChanged);
            this.txtNombreUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreUsuario_KeyDown);
            this.txtNombreUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreUsuario_KeyPress);
            this.txtNombreUsuario.Leave += new System.EventHandler(this.txtNombreUsuario_Leave);
            // 
            // txtApellidoUsuario
            // 
            this.txtApellidoUsuario.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidoUsuario.Location = new System.Drawing.Point(172, 182);
            this.txtApellidoUsuario.MaxLength = 50;
            this.txtApellidoUsuario.Multiline = true;
            this.txtApellidoUsuario.Name = "txtApellidoUsuario";
            this.txtApellidoUsuario.Size = new System.Drawing.Size(270, 28);
            this.txtApellidoUsuario.TabIndex = 3;
            this.txtApellidoUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtApellidoUsuario.Click += new System.EventHandler(this.txtApellidoUsuario_Click);
            this.txtApellidoUsuario.TextChanged += new System.EventHandler(this.txtApellidoUsuario_TextChanged);
            this.txtApellidoUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtApellidoUsuario_KeyDown);
            this.txtApellidoUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApellidoUsuario_KeyPress);
            this.txtApellidoUsuario.Leave += new System.EventHandler(this.txtApellidoUsuario_Leave);
            // 
            // cmbxCargoUsuario
            // 
            this.cmbxCargoUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCargoUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxCargoUsuario.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxCargoUsuario.FormattingEnabled = true;
            this.cmbxCargoUsuario.Location = new System.Drawing.Point(172, 253);
            this.cmbxCargoUsuario.Name = "cmbxCargoUsuario";
            this.cmbxCargoUsuario.Size = new System.Drawing.Size(270, 27);
            this.cmbxCargoUsuario.TabIndex = 4;
            this.cmbxCargoUsuario.SelectedIndexChanged += new System.EventHandler(this.cmbxCargoUsuario_SelectedIndexChanged);
            // 
            // btnGuardarUsuario
            // 
            this.btnGuardarUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnGuardarUsuario.FlatAppearance.BorderColor = System.Drawing.Color.ForestGreen;
            this.btnGuardarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarUsuario.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarUsuario.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGuardarUsuario.Location = new System.Drawing.Point(375, 523);
            this.btnGuardarUsuario.Name = "btnGuardarUsuario";
            this.btnGuardarUsuario.Size = new System.Drawing.Size(147, 46);
            this.btnGuardarUsuario.TabIndex = 5;
            this.btnGuardarUsuario.Text = "Registrar";
            this.btnGuardarUsuario.UseVisualStyleBackColor = false;
            this.btnGuardarUsuario.Click += new System.EventHandler(this.btnGuardarUsuario_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Firebrick;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Location = new System.Drawing.Point(607, 523);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(147, 46);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(395, 633);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "-La contraseña por defecto es: 12345678-";
            // 
            // cmbNacionalidad
            // 
            this.cmbNacionalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNacionalidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNacionalidad.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNacionalidad.FormattingEnabled = true;
            this.cmbNacionalidad.Items.AddRange(new object[] {
            "V",
            "E"});
            this.cmbNacionalidad.Location = new System.Drawing.Point(172, 49);
            this.cmbNacionalidad.Name = "cmbNacionalidad";
            this.cmbNacionalidad.Size = new System.Drawing.Size(38, 27);
            this.cmbNacionalidad.TabIndex = 0;
            this.cmbNacionalidad.Validating += new System.ComponentModel.CancelEventHandler(this.cmbNacionalidad_Validating);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbxCargoUsuario);
            this.groupBox1.Controls.Add(this.txtCedulaUsuario);
            this.groupBox1.Controls.Add(this.cmbNacionalidad);
            this.groupBox1.Controls.Add(this.txtNombreUsuario);
            this.groupBox1.Controls.Add(this.txtApellidoUsuario);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(297, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 313);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(97, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cargo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(83, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Apellido:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(83, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nombre:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(91, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cédula:";
            // 
            // errorProviderCI
            // 
            this.errorProviderCI.ContainerControl = this;
            // 
            // errorProviderNombre
            // 
            this.errorProviderNombre.ContainerControl = this;
            // 
            // errorProviderApellido
            // 
            this.errorProviderApellido.ContainerControl = this;
            // 
            // errorProviderCMBCargo
            // 
            this.errorProviderCMBCargo.ContainerControl = this;
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
            this.Panel_cabecera.Size = new System.Drawing.Size(1117, 96);
            this.Panel_cabecera.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(343, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(423, 43);
            this.label8.TabIndex = 1;
            this.label8.Text = "Registro de usuarios";
            // 
            // rectangleShape7
            // 
            this.rectangleShape7.BorderColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape7.Location = new System.Drawing.Point(-1, 96);
            this.rectangleShape7.Name = "rectangleShape7";
            this.rectangleShape7.Size = new System.Drawing.Size(1116, 1);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape7});
            this.shapeContainer1.Size = new System.Drawing.Size(1117, 742);
            this.shapeContainer1.TabIndex = 55;
            this.shapeContainer1.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.shapeContainer4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 717);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1117, 25);
            this.panel8.TabIndex = 61;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label9.Location = new System.Drawing.Point(227, 7);
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
            this.shapeContainer4.Size = new System.Drawing.Size(1117, 25);
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
            // Registrar_usuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1117, 742);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.Panel_cabecera);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardarUsuario);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Registrar_usuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.Registrar_usuarios_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderApellido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCMBCargo)).EndInit();
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCedulaUsuario;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.TextBox txtApellidoUsuario;
        private System.Windows.Forms.ComboBox cmbxCargoUsuario;
        private System.Windows.Forms.Button btnGuardarUsuario;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbNacionalidad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ErrorProvider errorProviderCI;
        private System.Windows.Forms.ErrorProvider errorProviderNombre;
        private System.Windows.Forms.ErrorProvider errorProviderApellido;
        private System.Windows.Forms.ErrorProvider errorProviderCMBCargo;
        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label8;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
    }
}