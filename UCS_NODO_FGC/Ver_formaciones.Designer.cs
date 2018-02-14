namespace UCS_NODO_FGC
{
    partial class Ver_formaciones
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.grpbData = new System.Windows.Forms.GroupBox();
            this.dgvFormaciones = new System.Windows.Forms.DataGridView();
            this.nombre_formacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo_formacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.solicitud_curso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duracion_curso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estatus_formacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etapa_formacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creado_por = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpbDatos = new System.Windows.Forms.GroupBox();
            this.txtBuscarNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbxEstatus = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.grpbOpciones = new System.Windows.Forms.GroupBox();
            this.btnCambiarStatus = new System.Windows.Forms.Button();
            this.btnVerFormacion = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.errorProviderCmbxNombre = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCmbxEstatus = new System.Windows.Forms.ErrorProvider(this.components);
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.panel8.SuspendLayout();
            this.grpbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormaciones)).BeginInit();
            this.grpbDatos.SuspendLayout();
            this.grpbOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxEstatus)).BeginInit();
            this.Panel_cabecera.SuspendLayout();
            this.SuspendLayout();
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
            this.panel8.TabIndex = 60;
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
            // grpbData
            // 
            this.grpbData.Controls.Add(this.dgvFormaciones);
            this.grpbData.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbData.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbData.Location = new System.Drawing.Point(29, 119);
            this.grpbData.Name = "grpbData";
            this.grpbData.Size = new System.Drawing.Size(766, 579);
            this.grpbData.TabIndex = 61;
            this.grpbData.TabStop = false;
            this.grpbData.Text = "Formaciones registradas";
            // 
            // dgvFormaciones
            // 
            this.dgvFormaciones.AllowUserToAddRows = false;
            this.dgvFormaciones.AllowUserToResizeColumns = false;
            this.dgvFormaciones.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.dgvFormaciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFormaciones.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvFormaciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFormaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre_formacion,
            this.tipo_formacion,
            this.solicitud_curso,
            this.duracion_curso,
            this.estatus_formacion,
            this.etapa_formacion,
            this.creado_por});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFormaciones.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFormaciones.Location = new System.Drawing.Point(22, 34);
            this.dgvFormaciones.Name = "dgvFormaciones";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFormaciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFormaciones.RowHeadersVisible = false;
            this.dgvFormaciones.Size = new System.Drawing.Size(724, 525);
            this.dgvFormaciones.TabIndex = 0;
            this.dgvFormaciones.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvFormaciones_MouseClick);
            // 
            // nombre_formacion
            // 
            this.nombre_formacion.HeaderText = "Formación";
            this.nombre_formacion.Name = "nombre_formacion";
            this.nombre_formacion.ReadOnly = true;
            // 
            // tipo_formacion
            // 
            this.tipo_formacion.HeaderText = "Tipo";
            this.tipo_formacion.Name = "tipo_formacion";
            this.tipo_formacion.ReadOnly = true;
            // 
            // solicitud_curso
            // 
            this.solicitud_curso.HeaderText = "Solicitado por";
            this.solicitud_curso.MaxInputLength = 100;
            this.solicitud_curso.Name = "solicitud_curso";
            this.solicitud_curso.ReadOnly = true;
            this.solicitud_curso.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // duracion_curso
            // 
            this.duracion_curso.HeaderText = "Duración";
            this.duracion_curso.MaxInputLength = 9;
            this.duracion_curso.MinimumWidth = 100;
            this.duracion_curso.Name = "duracion_curso";
            this.duracion_curso.ReadOnly = true;
            this.duracion_curso.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // estatus_formacion
            // 
            this.estatus_formacion.HeaderText = "Estatus";
            this.estatus_formacion.Name = "estatus_formacion";
            this.estatus_formacion.ReadOnly = true;
            // 
            // etapa_formacion
            // 
            this.etapa_formacion.HeaderText = "Etapa actual";
            this.etapa_formacion.Name = "etapa_formacion";
            this.etapa_formacion.ReadOnly = true;
            // 
            // creado_por
            // 
            this.creado_por.HeaderText = "Creador";
            this.creado_por.Name = "creado_por";
            this.creado_por.ReadOnly = true;
            // 
            // grpbDatos
            // 
            this.grpbDatos.Controls.Add(this.txtBuscarNombre);
            this.grpbDatos.Controls.Add(this.label3);
            this.grpbDatos.Controls.Add(this.label2);
            this.grpbDatos.Controls.Add(this.cmbxEstatus);
            this.grpbDatos.Controls.Add(this.btnBuscar);
            this.grpbDatos.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbDatos.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbDatos.Location = new System.Drawing.Point(824, 119);
            this.grpbDatos.Name = "grpbDatos";
            this.grpbDatos.Size = new System.Drawing.Size(252, 201);
            this.grpbDatos.TabIndex = 64;
            this.grpbDatos.TabStop = false;
            this.grpbDatos.Text = "Curso";
            // 
            // txtBuscarNombre
            // 
            this.txtBuscarNombre.Font = new System.Drawing.Font("Rockwell", 8F);
            this.txtBuscarNombre.Location = new System.Drawing.Point(79, 48);
            this.txtBuscarNombre.Name = "txtBuscarNombre";
            this.txtBuscarNombre.Size = new System.Drawing.Size(154, 20);
            this.txtBuscarNombre.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 10F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(21, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Estatus:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 10F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nombre:";
            // 
            // cmbxEstatus
            // 
            this.cmbxEstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEstatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxEstatus.Font = new System.Drawing.Font("Rockwell", 8F);
            this.cmbxEstatus.FormattingEnabled = true;
            this.cmbxEstatus.Items.AddRange(new object[] {
            "En curso",
            "Reprogramado",
            "Suspendido",
            "Finalizado"});
            this.cmbxEstatus.Location = new System.Drawing.Point(79, 91);
            this.cmbxEstatus.Name = "cmbxEstatus";
            this.cmbxEstatus.Size = new System.Drawing.Size(158, 21);
            this.cmbxEstatus.TabIndex = 5;
            this.cmbxEstatus.SelectionChangeCommitted += new System.EventHandler(this.cmbxEstatus_SelectionChangeCommitted);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBuscar.Location = new System.Drawing.Point(27, 137);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(199, 47);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // grpbOpciones
            // 
            this.grpbOpciones.Controls.Add(this.btnCambiarStatus);
            this.grpbOpciones.Controls.Add(this.btnVerFormacion);
            this.grpbOpciones.Controls.Add(this.btnModificar);
            this.grpbOpciones.Controls.Add(this.btnRefrescar);
            this.grpbOpciones.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbOpciones.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbOpciones.Location = new System.Drawing.Point(824, 335);
            this.grpbOpciones.Name = "grpbOpciones";
            this.grpbOpciones.Size = new System.Drawing.Size(252, 363);
            this.grpbOpciones.TabIndex = 65;
            this.grpbOpciones.TabStop = false;
            this.grpbOpciones.Text = "Opciones";
            // 
            // btnCambiarStatus
            // 
            this.btnCambiarStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(187)))), ((int)(((byte)(54)))));
            this.btnCambiarStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCambiarStatus.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnCambiarStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarStatus.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold);
            this.btnCambiarStatus.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCambiarStatus.Location = new System.Drawing.Point(27, 132);
            this.btnCambiarStatus.Name = "btnCambiarStatus";
            this.btnCambiarStatus.Size = new System.Drawing.Size(199, 47);
            this.btnCambiarStatus.TabIndex = 9;
            this.btnCambiarStatus.Text = "Cambiar estatus";
            this.btnCambiarStatus.UseVisualStyleBackColor = false;
            this.btnCambiarStatus.Click += new System.EventHandler(this.btnCambiarStatus_Click);
            // 
            // btnVerFormacion
            // 
            this.btnVerFormacion.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnVerFormacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerFormacion.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnVerFormacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerFormacion.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold);
            this.btnVerFormacion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnVerFormacion.Location = new System.Drawing.Point(27, 296);
            this.btnVerFormacion.Name = "btnVerFormacion";
            this.btnVerFormacion.Size = new System.Drawing.Size(199, 47);
            this.btnVerFormacion.TabIndex = 5;
            this.btnVerFormacion.Text = "Ver formación";
            this.btnVerFormacion.UseVisualStyleBackColor = false;
            this.btnVerFormacion.Click += new System.EventHandler(this.btnVerFormacion_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold);
            this.btnModificar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificar.Location = new System.Drawing.Point(27, 216);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(199, 47);
            this.btnModificar.TabIndex = 8;
            this.btnModificar.Text = "Modificar formación";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.SlateBlue;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefrescar.Location = new System.Drawing.Point(27, 48);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(199, 47);
            this.btnRefrescar.TabIndex = 7;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // errorProviderCmbxNombre
            // 
            this.errorProviderCmbxNombre.ContainerControl = this;
            // 
            // errorProviderCmbxEstatus
            // 
            this.errorProviderCmbxEstatus.ContainerControl = this;
            // 
            // Panel_cabecera
            // 
            this.Panel_cabecera.BackColor = System.Drawing.Color.MidnightBlue;
            this.Panel_cabecera.BackgroundImage = global::UCS_NODO_FGC.Properties.Resources.panel_cabecera;
            this.Panel_cabecera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel_cabecera.Controls.Add(this.label1);
            this.Panel_cabecera.Controls.Add(this.shapeContainer1);
            this.Panel_cabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_cabecera.Location = new System.Drawing.Point(0, 0);
            this.Panel_cabecera.Name = "Panel_cabecera";
            this.Panel_cabecera.Size = new System.Drawing.Size(1122, 96);
            this.Panel_cabecera.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(316, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ver formaciones";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape7});
            this.shapeContainer1.Size = new System.Drawing.Size(1122, 96);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape7
            // 
            this.rectangleShape7.BorderColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape7.Location = new System.Drawing.Point(1, 93);
            this.rectangleShape7.Name = "rectangleShape7";
            this.rectangleShape7.Size = new System.Drawing.Size(1121, 1);
            // 
            // Ver_formaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1122, 739);
            this.Controls.Add(this.grpbOpciones);
            this.Controls.Add(this.grpbDatos);
            this.Controls.Add(this.grpbData);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.Panel_cabecera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Ver_formaciones";
            this.Load += new System.EventHandler(this.Ver_formaciones_Load);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.grpbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormaciones)).EndInit();
            this.grpbDatos.ResumeLayout(false);
            this.grpbDatos.PerformLayout();
            this.grpbOpciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxEstatus)).EndInit();
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private System.Windows.Forms.GroupBox grpbData;
        public System.Windows.Forms.DataGridView dgvFormaciones;
        private System.Windows.Forms.GroupBox grpbDatos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbxEstatus;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox grpbOpciones;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnVerFormacion;
        private System.Windows.Forms.Button btnCambiarStatus;
        private System.Windows.Forms.ErrorProvider errorProviderCmbxNombre;
        private System.Windows.Forms.ErrorProvider errorProviderCmbxEstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_formacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo_formacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn solicitud_curso;
        private System.Windows.Forms.DataGridViewTextBoxColumn duracion_curso;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatus_formacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn etapa_formacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn creado_por;
        private System.Windows.Forms.TextBox txtBuscarNombre;
    }
}