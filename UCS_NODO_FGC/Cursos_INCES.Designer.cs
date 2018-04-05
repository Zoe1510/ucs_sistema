namespace UCS_NODO_FGC
{
    partial class Cursos_INCES
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
            this.grpbData = new System.Windows.Forms.GroupBox();
            this.dgvInce = new System.Windows.Forms.DataGridView();
            this.Nombre_curso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellido_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cedula_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpbOpciones = new System.Windows.Forms.GroupBox();
            this.btnEliminarCurso = new System.Windows.Forms.Button();
            this.btnAsignarCurso = new System.Windows.Forms.Button();
            this.btnModificarCurso = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnEliminarAsignacion = new System.Windows.Forms.Button();
            this.grpbDatos = new System.Windows.Forms.GroupBox();
            this.cmbxCurso = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.errorProvidercmbx = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.grpbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInce)).BeginInit();
            this.grpbOpciones.SuspendLayout();
            this.grpbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvidercmbx)).BeginInit();
            this.panel8.SuspendLayout();
            this.Panel_cabecera.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbData
            // 
            this.grpbData.Controls.Add(this.dgvInce);
            this.grpbData.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.grpbData.Location = new System.Drawing.Point(24, 133);
            this.grpbData.Name = "grpbData";
            this.grpbData.Size = new System.Drawing.Size(832, 570);
            this.grpbData.TabIndex = 56;
            this.grpbData.TabStop = false;
            this.grpbData.Text = "Cursos actuales";
            // 
            // dgvInce
            // 
            this.dgvInce.AllowUserToAddRows = false;
            this.dgvInce.AllowUserToResizeColumns = false;
            this.dgvInce.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.dgvInce.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInce.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvInce.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInce.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInce.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInce.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre_curso,
            this.nombre_fa,
            this.apellido_fa,
            this.cedula_fa});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInce.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvInce.Location = new System.Drawing.Point(20, 34);
            this.dgvInce.MultiSelect = false;
            this.dgvInce.Name = "dgvInce";
            this.dgvInce.ReadOnly = true;
            this.dgvInce.RowHeadersVisible = false;
            this.dgvInce.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvInce.Size = new System.Drawing.Size(790, 518);
            this.dgvInce.TabIndex = 0;
            this.dgvInce.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvince_MouseClick);
            // 
            // Nombre_curso
            // 
            this.Nombre_curso.HeaderText = "Nombre curso";
            this.Nombre_curso.MaxInputLength = 250;
            this.Nombre_curso.MinimumWidth = 200;
            this.Nombre_curso.Name = "Nombre_curso";
            this.Nombre_curso.ReadOnly = true;
            this.Nombre_curso.Width = 300;
            // 
            // nombre_fa
            // 
            this.nombre_fa.HeaderText = "Nombre facilitador";
            this.nombre_fa.MinimumWidth = 100;
            this.nombre_fa.Name = "nombre_fa";
            this.nombre_fa.ReadOnly = true;
            this.nombre_fa.Width = 185;
            // 
            // apellido_fa
            // 
            this.apellido_fa.HeaderText = "Apellido";
            this.apellido_fa.MinimumWidth = 100;
            this.apellido_fa.Name = "apellido_fa";
            this.apellido_fa.ReadOnly = true;
            this.apellido_fa.Width = 150;
            // 
            // cedula_fa
            // 
            this.cedula_fa.HeaderText = "Cédula";
            this.cedula_fa.MinimumWidth = 100;
            this.cedula_fa.Name = "cedula_fa";
            this.cedula_fa.ReadOnly = true;
            this.cedula_fa.Width = 150;
            // 
            // grpbOpciones
            // 
            this.grpbOpciones.Controls.Add(this.btnEliminarCurso);
            this.grpbOpciones.Controls.Add(this.btnAsignarCurso);
            this.grpbOpciones.Controls.Add(this.btnModificarCurso);
            this.grpbOpciones.Controls.Add(this.btnAgregar);
            this.grpbOpciones.Controls.Add(this.btnRefrescar);
            this.grpbOpciones.Controls.Add(this.btnEliminarAsignacion);
            this.grpbOpciones.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbOpciones.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbOpciones.Location = new System.Drawing.Point(874, 290);
            this.grpbOpciones.Name = "grpbOpciones";
            this.grpbOpciones.Size = new System.Drawing.Size(222, 413);
            this.grpbOpciones.TabIndex = 58;
            this.grpbOpciones.TabStop = false;
            this.grpbOpciones.Text = "Opciones";
            // 
            // btnEliminarCurso
            // 
            this.btnEliminarCurso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnEliminarCurso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarCurso.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarCurso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarCurso.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnEliminarCurso.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEliminarCurso.Location = new System.Drawing.Point(22, 286);
            this.btnEliminarCurso.Name = "btnEliminarCurso";
            this.btnEliminarCurso.Size = new System.Drawing.Size(176, 43);
            this.btnEliminarCurso.TabIndex = 9;
            this.btnEliminarCurso.Text = "Eliminar curso";
            this.btnEliminarCurso.UseVisualStyleBackColor = false;
            this.btnEliminarCurso.Click += new System.EventHandler(this.btnEliminarCurso_Click);
            // 
            // btnAsignarCurso
            // 
            this.btnAsignarCurso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(70)))), ((int)(((byte)(226)))));
            this.btnAsignarCurso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsignarCurso.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnAsignarCurso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsignarCurso.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarCurso.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAsignarCurso.Location = new System.Drawing.Point(22, 226);
            this.btnAsignarCurso.Name = "btnAsignarCurso";
            this.btnAsignarCurso.Size = new System.Drawing.Size(176, 43);
            this.btnAsignarCurso.TabIndex = 5;
            this.btnAsignarCurso.Text = "Asignar facilitador";
            this.btnAsignarCurso.UseVisualStyleBackColor = false;
            this.btnAsignarCurso.Click += new System.EventHandler(this.btnAsignarCurso_Click);
            // 
            // btnModificarCurso
            // 
            this.btnModificarCurso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(101)))), ((int)(((byte)(24)))));
            this.btnModificarCurso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarCurso.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrange;
            this.btnModificarCurso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarCurso.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarCurso.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificarCurso.Location = new System.Drawing.Point(22, 165);
            this.btnModificarCurso.Name = "btnModificarCurso";
            this.btnModificarCurso.Size = new System.Drawing.Size(176, 43);
            this.btnModificarCurso.TabIndex = 8;
            this.btnModificarCurso.Text = "Modificar curso";
            this.btnModificarCurso.UseVisualStyleBackColor = false;
            this.btnModificarCurso.Click += new System.EventHandler(this.btnModificarCurso_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(187)))), ((int)(((byte)(54)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAgregar.Location = new System.Drawing.Point(22, 105);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(176, 43);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Añadir curso";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.SlateBlue;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.FlatAppearance.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefrescar.Location = new System.Drawing.Point(22, 41);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(176, 43);
            this.btnRefrescar.TabIndex = 7;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnEliminarAsignacion
            // 
            this.btnEliminarAsignacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnEliminarAsignacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarAsignacion.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarAsignacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarAsignacion.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnEliminarAsignacion.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEliminarAsignacion.Location = new System.Drawing.Point(22, 348);
            this.btnEliminarAsignacion.Name = "btnEliminarAsignacion";
            this.btnEliminarAsignacion.Size = new System.Drawing.Size(176, 43);
            this.btnEliminarAsignacion.TabIndex = 6;
            this.btnEliminarAsignacion.Text = "Eliminar asignación";
            this.btnEliminarAsignacion.UseVisualStyleBackColor = false;
            this.btnEliminarAsignacion.Click += new System.EventHandler(this.btnEliminarAsignacion_Click);
            // 
            // grpbDatos
            // 
            this.grpbDatos.Controls.Add(this.cmbxCurso);
            this.grpbDatos.Controls.Add(this.btnBuscar);
            this.grpbDatos.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbDatos.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbDatos.Location = new System.Drawing.Point(874, 133);
            this.grpbDatos.Name = "grpbDatos";
            this.grpbDatos.Size = new System.Drawing.Size(222, 151);
            this.grpbDatos.TabIndex = 57;
            this.grpbDatos.TabStop = false;
            this.grpbDatos.Text = "Buscar por curso:";
            // 
            // cmbxCurso
            // 
            this.cmbxCurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCurso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxCurso.FormattingEnabled = true;
            this.cmbxCurso.Location = new System.Drawing.Point(22, 36);
            this.cmbxCurso.Name = "cmbxCurso";
            this.cmbxCurso.Size = new System.Drawing.Size(176, 27);
            this.cmbxCurso.TabIndex = 4;
            this.cmbxCurso.SelectionChangeCommitted += new System.EventHandler(this.cmbxCurso_SelectionChangeCommitted);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBuscar.Location = new System.Drawing.Point(22, 79);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(176, 47);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // errorProvidercmbx
            // 
            this.errorProvidercmbx.ContainerControl = this;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.shapeContainer4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 714);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1121, 25);
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
            this.shapeContainer4.Size = new System.Drawing.Size(1121, 25);
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
            this.Panel_cabecera.Size = new System.Drawing.Size(1121, 96);
            this.Panel_cabecera.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(379, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cursos INCES";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape7});
            this.shapeContainer1.Size = new System.Drawing.Size(1121, 96);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape7
            // 
            this.rectangleShape7.BorderColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape7.Location = new System.Drawing.Point(-7, 93);
            this.rectangleShape7.Name = "rectangleShape7";
            this.rectangleShape7.Size = new System.Drawing.Size(1135, 1);
            // 
            // Cursos_INCES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1121, 739);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.grpbOpciones);
            this.Controls.Add(this.grpbDatos);
            this.Controls.Add(this.grpbData);
            this.Controls.Add(this.Panel_cabecera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Cursos_INCES";
            this.Load += new System.EventHandler(this.Cursos_INCES_Load);
            this.grpbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInce)).EndInit();
            this.grpbOpciones.ResumeLayout(false);
            this.grpbDatos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvidercmbx)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
        private System.Windows.Forms.GroupBox grpbData;
        public System.Windows.Forms.DataGridView dgvInce;
        private System.Windows.Forms.GroupBox grpbOpciones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnEliminarAsignacion;
        private System.Windows.Forms.Button btnAsignarCurso;
        private System.Windows.Forms.GroupBox grpbDatos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnModificarCurso;
        private System.Windows.Forms.Button btnEliminarCurso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_curso;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_fa;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellido_fa;
        private System.Windows.Forms.DataGridViewTextBoxColumn cedula_fa;
        private System.Windows.Forms.ErrorProvider errorProvidercmbx;
        private System.Windows.Forms.ComboBox cmbxCurso;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
    }
}