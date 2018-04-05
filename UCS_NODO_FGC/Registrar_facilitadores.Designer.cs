namespace UCS_NODO_FGC
{
    partial class Registrar_facilitadores
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
            this.txtCedulaFa = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbNacionalidad = new System.Windows.Forms.ComboBox();
            this.txtApellidoFa = new System.Windows.Forms.TextBox();
            this.txtNombreFa = new System.Windows.Forms.TextBox();
            this.txtCorreoFa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbxINCE = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbUbicacionEdo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEspecialidadFa = new System.Windows.Forms.TextBox();
            this.txtTelefonoFa = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardarFacilitador = new System.Windows.Forms.Button();
            this.errorProviderCI = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderNombre = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderApellido = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCorreo = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderTlfn = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderEspec = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCmbINCE = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider8CmbUbi = new System.Windows.Forms.ErrorProvider(this.components);
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderApellido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEspec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbINCE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8CmbUbi)).BeginInit();
            this.Panel_cabecera.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCedulaFa
            // 
            this.txtCedulaFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCedulaFa.Location = new System.Drawing.Point(267, 34);
            this.txtCedulaFa.MaxLength = 8;
            this.txtCedulaFa.Multiline = true;
            this.txtCedulaFa.Name = "txtCedulaFa";
            this.txtCedulaFa.Size = new System.Drawing.Size(260, 29);
            this.txtCedulaFa.TabIndex = 1;
            this.txtCedulaFa.Text = "                     Cédula";
            this.txtCedulaFa.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtCedulaFa_MouseClick);
            this.txtCedulaFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCedulaFa_KeyDown);
            this.txtCedulaFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCedulaFa_KeyPress);
            this.txtCedulaFa.Leave += new System.EventHandler(this.txtCedulaFa_Leave);
            this.txtCedulaFa.Validating += new System.ComponentModel.CancelEventHandler(this.txtCedulaFa_Validating);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbNacionalidad);
            this.groupBox1.Controls.Add(this.txtApellidoFa);
            this.groupBox1.Controls.Add(this.txtNombreFa);
            this.groupBox1.Controls.Add(this.txtCorreoFa);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCedulaFa);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(269, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 246);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
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
            this.cmbNacionalidad.Location = new System.Drawing.Point(219, 34);
            this.cmbNacionalidad.Name = "cmbNacionalidad";
            this.cmbNacionalidad.Size = new System.Drawing.Size(42, 29);
            this.cmbNacionalidad.TabIndex = 0;
            this.cmbNacionalidad.Validating += new System.ComponentModel.CancelEventHandler(this.cmbNacionalidad_Validating);
            // 
            // txtApellidoFa
            // 
            this.txtApellidoFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidoFa.Location = new System.Drawing.Point(219, 137);
            this.txtApellidoFa.MaxLength = 45;
            this.txtApellidoFa.Multiline = true;
            this.txtApellidoFa.Name = "txtApellidoFa";
            this.txtApellidoFa.Size = new System.Drawing.Size(308, 29);
            this.txtApellidoFa.TabIndex = 3;
            this.txtApellidoFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtApellidoFa.Click += new System.EventHandler(this.txtApellidoFa_Click);
            this.txtApellidoFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtApellidoFa_KeyDown);
            this.txtApellidoFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApellidoFa_KeyPress);
            this.txtApellidoFa.Leave += new System.EventHandler(this.txtApellidoFa_Leave);
            // 
            // txtNombreFa
            // 
            this.txtNombreFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreFa.Location = new System.Drawing.Point(219, 86);
            this.txtNombreFa.MaxLength = 45;
            this.txtNombreFa.Multiline = true;
            this.txtNombreFa.Name = "txtNombreFa";
            this.txtNombreFa.Size = new System.Drawing.Size(308, 29);
            this.txtNombreFa.TabIndex = 2;
            this.txtNombreFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreFa.Click += new System.EventHandler(this.txtNombreFa_Click);
            this.txtNombreFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreFa_KeyDown);
            this.txtNombreFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreFa_KeyPress);
            this.txtNombreFa.Leave += new System.EventHandler(this.txtNombreFa_Leave);
            // 
            // txtCorreoFa
            // 
            this.txtCorreoFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoFa.Location = new System.Drawing.Point(219, 189);
            this.txtCorreoFa.MaxLength = 75;
            this.txtCorreoFa.Multiline = true;
            this.txtCorreoFa.Name = "txtCorreoFa";
            this.txtCorreoFa.Size = new System.Drawing.Size(308, 29);
            this.txtCorreoFa.TabIndex = 4;
            this.txtCorreoFa.Text = "correo@ejemplo.com";
            this.txtCorreoFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCorreoFa.Click += new System.EventHandler(this.txtCorreoFa_Click);
            this.txtCorreoFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCorreoFa_KeyDown);
            this.txtCorreoFa.Leave += new System.EventHandler(this.txtCorreoFa_Leave);
            this.txtCorreoFa.Validating += new System.ComponentModel.CancelEventHandler(this.txtCorreoFa_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(53, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 19);
            this.label4.TabIndex = 62;
            this.label4.Text = "Correo electrónico:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(129, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 19);
            this.label3.TabIndex = 61;
            this.label3.Text = "Apellido:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(129, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 60;
            this.label2.Text = "Nombre:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(137, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 19);
            this.label10.TabIndex = 59;
            this.label10.Text = "Cédula:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbxINCE);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmbUbicacionEdo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtEspecialidadFa);
            this.groupBox2.Controls.Add(this.txtTelefonoFa);
            this.groupBox2.Location = new System.Drawing.Point(269, 369);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 252);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(118, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 19);
            this.label8.TabIndex = 66;
            this.label8.Text = "Ubicación:";
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
            this.cmbxINCE.Location = new System.Drawing.Point(219, 142);
            this.cmbxINCE.MaxDropDownItems = 5;
            this.cmbxINCE.Name = "cmbxINCE";
            this.cmbxINCE.Size = new System.Drawing.Size(308, 27);
            this.cmbxINCE.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(31, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 19);
            this.label7.TabIndex = 65;
            this.label7.Text = "Requerimiento INCES:";
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
            this.cmbUbicacionEdo.Location = new System.Drawing.Point(219, 196);
            this.cmbUbicacionEdo.MaxDropDownItems = 5;
            this.cmbUbicacionEdo.Name = "cmbUbicacionEdo";
            this.cmbUbicacionEdo.Size = new System.Drawing.Size(308, 27);
            this.cmbUbicacionEdo.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(96, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 19);
            this.label6.TabIndex = 64;
            this.label6.Text = "Especialidad:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(127, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 19);
            this.label5.TabIndex = 63;
            this.label5.Text = "Teléfono:";
            // 
            // txtEspecialidadFa
            // 
            this.txtEspecialidadFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEspecialidadFa.Location = new System.Drawing.Point(219, 87);
            this.txtEspecialidadFa.MaxLength = 45;
            this.txtEspecialidadFa.Multiline = true;
            this.txtEspecialidadFa.Name = "txtEspecialidadFa";
            this.txtEspecialidadFa.Size = new System.Drawing.Size(308, 29);
            this.txtEspecialidadFa.TabIndex = 6;
            this.txtEspecialidadFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEspecialidadFa.Click += new System.EventHandler(this.txtEspecialidadFa_Click);
            this.txtEspecialidadFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEspecialidadFa_KeyDown);
            this.txtEspecialidadFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEspecialidadFa_KeyPress);
            this.txtEspecialidadFa.Leave += new System.EventHandler(this.txtEspecialidadFa_Leave);
            // 
            // txtTelefonoFa
            // 
            this.txtTelefonoFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefonoFa.Location = new System.Drawing.Point(219, 36);
            this.txtTelefonoFa.MaxLength = 11;
            this.txtTelefonoFa.Multiline = true;
            this.txtTelefonoFa.Name = "txtTelefonoFa";
            this.txtTelefonoFa.Size = new System.Drawing.Size(308, 29);
            this.txtTelefonoFa.TabIndex = 5;
            this.txtTelefonoFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTelefonoFa.Click += new System.EventHandler(this.txtTelefonoFa_Click);
            this.txtTelefonoFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelefonoFa_KeyDown);
            this.txtTelefonoFa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefonoFa_KeyPress);
            this.txtTelefonoFa.Leave += new System.EventHandler(this.txtTelefonoFa_Leave);
            this.txtTelefonoFa.Validating += new System.ComponentModel.CancelEventHandler(this.txtTelefonoFa_Validating);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Location = new System.Drawing.Point(665, 642);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(147, 47);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardarFacilitador
            // 
            this.btnGuardarFacilitador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnGuardarFacilitador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarFacilitador.FlatAppearance.BorderColor = System.Drawing.Color.ForestGreen;
            this.btnGuardarFacilitador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarFacilitador.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarFacilitador.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGuardarFacilitador.Location = new System.Drawing.Point(369, 642);
            this.btnGuardarFacilitador.Name = "btnGuardarFacilitador";
            this.btnGuardarFacilitador.Size = new System.Drawing.Size(147, 47);
            this.btnGuardarFacilitador.TabIndex = 8;
            this.btnGuardarFacilitador.Text = "Registrar";
            this.btnGuardarFacilitador.UseVisualStyleBackColor = false;
            this.btnGuardarFacilitador.Click += new System.EventHandler(this.btnGuardarFacilitador_Click);
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
            // errorProviderCmbINCE
            // 
            this.errorProviderCmbINCE.ContainerControl = this;
            // 
            // errorProvider8CmbUbi
            // 
            this.errorProvider8CmbUbi.ContainerControl = this;
            // 
            // Panel_cabecera
            // 
            this.Panel_cabecera.BackColor = System.Drawing.Color.MidnightBlue;
            this.Panel_cabecera.BackgroundImage = global::UCS_NODO_FGC.Properties.Resources.panel_cabecera;
            this.Panel_cabecera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel_cabecera.Controls.Add(this.label1);
            this.Panel_cabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_cabecera.Location = new System.Drawing.Point(0, 0);
            this.Panel_cabecera.Name = "Panel_cabecera";
            this.Panel_cabecera.Size = new System.Drawing.Size(1122, 96);
            this.Panel_cabecera.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(311, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(501, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Registro de facilitadores";
            // 
            // rectangleShape7
            // 
            this.rectangleShape7.BorderColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape7.Location = new System.Drawing.Point(0, 96);
            this.rectangleShape7.Name = "rectangleShape7";
            this.rectangleShape7.Size = new System.Drawing.Size(1121, 1);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape7});
            this.shapeContainer1.Size = new System.Drawing.Size(1122, 739);
            this.shapeContainer1.TabIndex = 47;
            this.shapeContainer1.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.shapeContainer4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 714);
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
            this.label9.Text = "Derechos reservados Universidad de Margarita © 2018. Proyecto de Pasantías Elabor" +
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
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::UCS_NODO_FGC.Properties.Resources.icon_cliente;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(16, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 20);
            this.pictureBox1.TabIndex = 73;
            this.pictureBox1.TabStop = false;
            // 
            // Registrar_facilitadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1122, 739);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.Panel_cabecera);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnGuardarFacilitador);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Registrar_facilitadores";
            this.Load += new System.EventHandler(this.Registrar_facilitadores_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderApellido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEspec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbINCE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8CmbUbi)).EndInit();
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCedulaFa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtApellidoFa;
        private System.Windows.Forms.TextBox txtNombreFa;
        private System.Windows.Forms.TextBox txtCorreoFa;
        private System.Windows.Forms.TextBox txtEspecialidadFa;
        private System.Windows.Forms.TextBox txtTelefonoFa;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardarFacilitador;
        private System.Windows.Forms.ComboBox cmbNacionalidad;
        private System.Windows.Forms.ComboBox cmbUbicacionEdo;
        private System.Windows.Forms.ComboBox cmbxINCE;
        private System.Windows.Forms.ErrorProvider errorProviderCI;
        private System.Windows.Forms.ErrorProvider errorProviderNombre;
        private System.Windows.Forms.ErrorProvider errorProviderApellido;
        private System.Windows.Forms.ErrorProvider errorProviderCorreo;
        private System.Windows.Forms.ErrorProvider errorProviderTlfn;
        private System.Windows.Forms.ErrorProvider errorProviderEspec;
        private System.Windows.Forms.ErrorProvider errorProviderCmbINCE;
        private System.Windows.Forms.ErrorProvider errorProvider8CmbUbi;
        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}