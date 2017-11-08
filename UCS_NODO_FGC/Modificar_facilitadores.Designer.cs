namespace UCS_NODO_FGC
{
    partial class Modificar_facilitadores
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
            this.cmbxINCE = new System.Windows.Forms.ComboBox();
            this.cmbUbicacionEdo = new System.Windows.Forms.ComboBox();
            this.txtEspecialidadFa = new System.Windows.Forms.TextBox();
            this.cmbNacionalidad = new System.Windows.Forms.ComboBox();
            this.txtCedulaFa = new System.Windows.Forms.TextBox();
            this.txtNombreFa = new System.Windows.Forms.TextBox();
            this.txtApellidoFa = new System.Windows.Forms.TextBox();
            this.txtCorreoFa = new System.Windows.Forms.TextBox();
            this.txtTelefonoFa = new System.Windows.Forms.TextBox();
            this.btnGuardarFa = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.errorProviderCI = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderNombre = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderApellido = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCorreo = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderTlfn = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderEspec = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderUbicacion = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCmbINCE = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderApellido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEspec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderUbicacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbINCE)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbxINCE);
            this.groupBox1.Controls.Add(this.cmbUbicacionEdo);
            this.groupBox1.Controls.Add(this.txtEspecialidadFa);
            this.groupBox1.Controls.Add(this.cmbNacionalidad);
            this.groupBox1.Controls.Add(this.txtCedulaFa);
            this.groupBox1.Controls.Add(this.txtNombreFa);
            this.groupBox1.Controls.Add(this.txtApellidoFa);
            this.groupBox1.Controls.Add(this.txtCorreoFa);
            this.groupBox1.Controls.Add(this.txtTelefonoFa);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(153, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 432);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del facilitador";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cmbxINCE
            // 
            this.cmbxINCE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxINCE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxINCE.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxINCE.FormattingEnabled = true;
            this.cmbxINCE.IntegralHeight = false;
            this.cmbxINCE.ItemHeight = 19;
            this.cmbxINCE.Items.AddRange(new object[] {
            "INCES",
            "NO INCES"});
            this.cmbxINCE.Location = new System.Drawing.Point(46, 329);
            this.cmbxINCE.MaxDropDownItems = 5;
            this.cmbxINCE.Name = "cmbxINCE";
            this.cmbxINCE.Size = new System.Drawing.Size(257, 27);
            this.cmbxINCE.TabIndex = 11;
            // 
            // cmbUbicacionEdo
            // 
            this.cmbUbicacionEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUbicacionEdo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbUbicacionEdo.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUbicacionEdo.FormattingEnabled = true;
            this.cmbUbicacionEdo.IntegralHeight = false;
            this.cmbUbicacionEdo.ItemHeight = 19;
            this.cmbUbicacionEdo.Items.AddRange(new object[] {
            "Amazonas",
            "Anzoátegui",
            "Apure",
            "Aragua",
            "Barinas",
            "Bolívar",
            "Carabobo",
            "Cojedes",
            "Delta Amacuro",
            "Distrito Capital",
            "Falcón",
            "Guárico",
            "Lara",
            "Mérida",
            "Miranda",
            "Monagas",
            "Nueva Esparta",
            "Portuguesa",
            "Sucre",
            "Táchira",
            "Trujillo",
            "Vargas",
            "Yaracuy",
            "Zulia"});
            this.cmbUbicacionEdo.Location = new System.Drawing.Point(46, 378);
            this.cmbUbicacionEdo.MaxDropDownItems = 5;
            this.cmbUbicacionEdo.Name = "cmbUbicacionEdo";
            this.cmbUbicacionEdo.Size = new System.Drawing.Size(257, 27);
            this.cmbUbicacionEdo.TabIndex = 10;
            // 
            // txtEspecialidadFa
            // 
            this.txtEspecialidadFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEspecialidadFa.Location = new System.Drawing.Point(46, 278);
            this.txtEspecialidadFa.MaxLength = 45;
            this.txtEspecialidadFa.Multiline = true;
            this.txtEspecialidadFa.Name = "txtEspecialidadFa";
            this.txtEspecialidadFa.Size = new System.Drawing.Size(257, 28);
            this.txtEspecialidadFa.TabIndex = 9;
            this.txtEspecialidadFa.Text = "Especialidad";
            this.txtEspecialidadFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEspecialidadFa.Click += new System.EventHandler(this.txtEspecialidadFa_Click);
            this.txtEspecialidadFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEspecialidadFa_KeyDown);
            this.txtEspecialidadFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEspecialidadFa_KeyPress);
            this.txtEspecialidadFa.Leave += new System.EventHandler(this.txtEspecialidadFa_Leave);
            // 
            // cmbNacionalidad
            // 
            this.cmbNacionalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNacionalidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNacionalidad.Font = new System.Drawing.Font("Rockwell", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNacionalidad.FormattingEnabled = true;
            this.cmbNacionalidad.ItemHeight = 21;
            this.cmbNacionalidad.Items.AddRange(new object[] {
            "V",
            "E"});
            this.cmbNacionalidad.Location = new System.Drawing.Point(46, 39);
            this.cmbNacionalidad.Name = "cmbNacionalidad";
            this.cmbNacionalidad.Size = new System.Drawing.Size(42, 29);
            this.cmbNacionalidad.TabIndex = 0;
            this.cmbNacionalidad.Validating += new System.ComponentModel.CancelEventHandler(this.cmbNacionalidad_Validating);
            // 
            // txtCedulaFa
            // 
            this.txtCedulaFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCedulaFa.Location = new System.Drawing.Point(94, 41);
            this.txtCedulaFa.MaxLength = 8;
            this.txtCedulaFa.Multiline = true;
            this.txtCedulaFa.Name = "txtCedulaFa";
            this.txtCedulaFa.Size = new System.Drawing.Size(209, 28);
            this.txtCedulaFa.TabIndex = 1;
            this.txtCedulaFa.Text = "Cédula";
            this.txtCedulaFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCedulaFa.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtCedulaFa_MouseClick);
            this.txtCedulaFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCedulaFa_KeyDown);
            this.txtCedulaFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCedulaFa_KeyPress);
            this.txtCedulaFa.Leave += new System.EventHandler(this.txtCedulaFa_Leave);
            this.txtCedulaFa.Validating += new System.ComponentModel.CancelEventHandler(this.txtCedulaFa_Validating);
            // 
            // txtNombreFa
            // 
            this.txtNombreFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreFa.Location = new System.Drawing.Point(46, 90);
            this.txtNombreFa.MaxLength = 45;
            this.txtNombreFa.Multiline = true;
            this.txtNombreFa.Name = "txtNombreFa";
            this.txtNombreFa.Size = new System.Drawing.Size(257, 28);
            this.txtNombreFa.TabIndex = 2;
            this.txtNombreFa.Text = "Nombre";
            this.txtNombreFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreFa.Click += new System.EventHandler(this.txtNombreFa_Click);
            this.txtNombreFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreFa_KeyDown);
            this.txtNombreFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreFa_KeyPress);
            this.txtNombreFa.Leave += new System.EventHandler(this.txtNombreFa_Leave);
            // 
            // txtApellidoFa
            // 
            this.txtApellidoFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidoFa.Location = new System.Drawing.Point(46, 137);
            this.txtApellidoFa.MaxLength = 45;
            this.txtApellidoFa.Multiline = true;
            this.txtApellidoFa.Name = "txtApellidoFa";
            this.txtApellidoFa.Size = new System.Drawing.Size(257, 28);
            this.txtApellidoFa.TabIndex = 3;
            this.txtApellidoFa.Text = "Apellido";
            this.txtApellidoFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtApellidoFa.Click += new System.EventHandler(this.txtApellidoFa_Click);
            this.txtApellidoFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtApellidoFa_KeyDown);
            this.txtApellidoFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApellidoFa_KeyPress);
            this.txtApellidoFa.Leave += new System.EventHandler(this.txtApellidoFa_Leave);
            // 
            // txtCorreoFa
            // 
            this.txtCorreoFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoFa.Location = new System.Drawing.Point(46, 184);
            this.txtCorreoFa.MaxLength = 75;
            this.txtCorreoFa.Multiline = true;
            this.txtCorreoFa.Name = "txtCorreoFa";
            this.txtCorreoFa.Size = new System.Drawing.Size(257, 28);
            this.txtCorreoFa.TabIndex = 4;
            this.txtCorreoFa.Text = "correo@ejemplo.com";
            this.txtCorreoFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCorreoFa.Click += new System.EventHandler(this.txtCorreoFa_Click);
            this.txtCorreoFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCorreoFa_KeyDown);
            this.txtCorreoFa.Leave += new System.EventHandler(this.txtCorreoFa_Leave);
            this.txtCorreoFa.Validating += new System.ComponentModel.CancelEventHandler(this.txtCorreoFa_Validating);
            // 
            // txtTelefonoFa
            // 
            this.txtTelefonoFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefonoFa.Location = new System.Drawing.Point(46, 231);
            this.txtTelefonoFa.MaxLength = 11;
            this.txtTelefonoFa.Multiline = true;
            this.txtTelefonoFa.Name = "txtTelefonoFa";
            this.txtTelefonoFa.Size = new System.Drawing.Size(257, 28);
            this.txtTelefonoFa.TabIndex = 8;
            this.txtTelefonoFa.Text = "Teléfono o celular";
            this.txtTelefonoFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTelefonoFa.Click += new System.EventHandler(this.txtTelefonoFa_Click);
            this.txtTelefonoFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelefonoFa_KeyDown);
            this.txtTelefonoFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefonoFa_KeyPress);
            this.txtTelefonoFa.Leave += new System.EventHandler(this.txtTelefonoFa_Leave);
            this.txtTelefonoFa.Validating += new System.ComponentModel.CancelEventHandler(this.txtTelefonoFa_Validating);
            // 
            // btnGuardarFa
            // 
            this.btnGuardarFa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnGuardarFa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarFa.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnGuardarFa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarFa.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGuardarFa.Location = new System.Drawing.Point(165, 484);
            this.btnGuardarFa.Name = "btnGuardarFa";
            this.btnGuardarFa.Size = new System.Drawing.Size(147, 46);
            this.btnGuardarFa.TabIndex = 5;
            this.btnGuardarFa.Text = "Actualizar";
            this.btnGuardarFa.UseVisualStyleBackColor = false;
            this.btnGuardarFa.Click += new System.EventHandler(this.btnGuardarFa_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Location = new System.Drawing.Point(335, 484);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(147, 46);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 556);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(669, 25);
            this.panel8.TabIndex = 38;
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label9.Location = new System.Drawing.Point(3, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(663, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Derechos reservados Universidad de Margarita © 2017. Proyecto de Pasantías Elabor" +
    "ado por Br. Zoyla Bermúdez";
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
            // errorProviderCorreo
            // 
            this.errorProviderCorreo.ContainerControl = this;
            // 
            // errorProviderTlfn
            // 
            this.errorProviderTlfn.ContainerControl = this;
            // 
            // errorProviderEspec
            // 
            this.errorProviderEspec.ContainerControl = this;
            // 
            // errorProviderUbicacion
            // 
            this.errorProviderUbicacion.ContainerControl = this;
            // 
            // errorProviderCmbINCE
            // 
            this.errorProviderCmbINCE.ContainerControl = this;
            // 
            // Modificar_facilitadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(669, 581);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardarFa);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Modificar_facilitadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificar datos del facilitador";
            this.Load += new System.EventHandler(this.Modificar_facilitadores_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderApellido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEspec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderUbicacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbINCE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbNacionalidad;
        private System.Windows.Forms.TextBox txtCorreoFa;
        private System.Windows.Forms.TextBox txtApellidoFa;
        private System.Windows.Forms.TextBox txtCedulaFa;
        private System.Windows.Forms.TextBox txtNombreFa;
        private System.Windows.Forms.ComboBox cmbUbicacionEdo;
        private System.Windows.Forms.TextBox txtEspecialidadFa;
        private System.Windows.Forms.TextBox txtTelefonoFa;
        private System.Windows.Forms.Button btnGuardarFa;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ErrorProvider errorProviderCI;
        private System.Windows.Forms.ErrorProvider errorProviderNombre;
        private System.Windows.Forms.ErrorProvider errorProviderApellido;
        private System.Windows.Forms.ErrorProvider errorProviderCorreo;
        private System.Windows.Forms.ErrorProvider errorProviderTlfn;
        private System.Windows.Forms.ErrorProvider errorProviderEspec;
        private System.Windows.Forms.ErrorProvider errorProviderUbicacion;
        private System.Windows.Forms.ComboBox cmbxINCE;
        private System.Windows.Forms.ErrorProvider errorProviderCmbINCE;
    }
}