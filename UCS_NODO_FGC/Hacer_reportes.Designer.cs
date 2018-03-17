namespace UCS_NODO_FGC
{
    partial class Hacer_reportes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gpbItems = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbxPeriodoTiempo = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.btnRpt_TimeEtapas = new System.Windows.Forms.Button();
            this.btnRpt_TimeTipos = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpbData = new System.Windows.Forms.GroupBox();
            this.dgvFormaciones = new System.Windows.Forms.DataGridView();
            this.nombre_formacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo_formacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.solicitud_curso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duracion_curso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estatus_formacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creado_por = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProviderPeriodo = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpbDatos = new System.Windows.Forms.GroupBox();
            this.txtBuscarNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbxEstatus = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.errorProviderCmbxNombre = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCmbxEstatus = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.btnRpt_TimeDur = new System.Windows.Forms.Button();
            this.btnRpt_TipoDur = new System.Windows.Forms.Button();
            this.gpbItems.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPeriodo)).BeginInit();
            this.grpbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxEstatus)).BeginInit();
            this.Panel_cabecera.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbItems
            // 
            this.gpbItems.Controls.Add(this.btnRpt_TipoDur);
            this.gpbItems.Controls.Add(this.btnRpt_TimeDur);
            this.gpbItems.Controls.Add(this.btnRpt_TimeTipos);
            this.gpbItems.Controls.Add(this.label6);
            this.gpbItems.Controls.Add(this.cmbxPeriodoTiempo);
            this.gpbItems.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbItems.Location = new System.Drawing.Point(759, 459);
            this.gpbItems.Name = "gpbItems";
            this.gpbItems.Size = new System.Drawing.Size(354, 238);
            this.gpbItems.TabIndex = 49;
            this.gpbItems.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Rockwell", 11F);
            this.label6.Location = new System.Drawing.Point(16, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 17);
            this.label6.TabIndex = 80;
            this.label6.Text = "Periodo de tiempo:";
            // 
            // cmbxPeriodoTiempo
            // 
            this.cmbxPeriodoTiempo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxPeriodoTiempo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxPeriodoTiempo.FormattingEnabled = true;
            this.cmbxPeriodoTiempo.Items.AddRange(new object[] {
            "Último mes",
            "Último trimestre"});
            this.cmbxPeriodoTiempo.Location = new System.Drawing.Point(166, 25);
            this.cmbxPeriodoTiempo.Name = "cmbxPeriodoTiempo";
            this.cmbxPeriodoTiempo.Size = new System.Drawing.Size(164, 27);
            this.cmbxPeriodoTiempo.TabIndex = 79;
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
            // btnRpt_TimeEtapas
            // 
            this.btnRpt_TimeEtapas.Location = new System.Drawing.Point(9, 31);
            this.btnRpt_TimeEtapas.Name = "btnRpt_TimeEtapas";
            this.btnRpt_TimeEtapas.Size = new System.Drawing.Size(336, 36);
            this.btnRpt_TimeEtapas.TabIndex = 62;
            this.btnRpt_TimeEtapas.Text = "Relación tiempo/etapas de una formacion";
            this.btnRpt_TimeEtapas.UseVisualStyleBackColor = true;
            this.btnRpt_TimeEtapas.Click += new System.EventHandler(this.btnReporte1_Click);
            // 
            // btnRpt_TimeTipos
            // 
            this.btnRpt_TimeTipos.Location = new System.Drawing.Point(9, 73);
            this.btnRpt_TimeTipos.Name = "btnRpt_TimeTipos";
            this.btnRpt_TimeTipos.Size = new System.Drawing.Size(336, 36);
            this.btnRpt_TimeTipos.TabIndex = 63;
            this.btnRpt_TimeTipos.Text = "Relacion tiempo / tipos de formaciones";
            this.btnRpt_TimeTipos.UseVisualStyleBackColor = true;
            this.btnRpt_TimeTipos.Click += new System.EventHandler(this.btnReporte2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRpt_TimeEtapas);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(759, 375);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 83);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reportes";
            // 
            // grpbData
            // 
            this.grpbData.Controls.Add(this.dgvFormaciones);
            this.grpbData.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grpbData.Location = new System.Drawing.Point(12, 129);
            this.grpbData.Name = "grpbData";
            this.grpbData.Size = new System.Drawing.Size(738, 568);
            this.grpbData.TabIndex = 78;
            this.grpbData.TabStop = false;
            this.grpbData.Text = "Formaciones registradas";
            // 
            // dgvFormaciones
            // 
            this.dgvFormaciones.AllowUserToAddRows = false;
            this.dgvFormaciones.AllowUserToResizeColumns = false;
            this.dgvFormaciones.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.NullValue = null;
            this.dgvFormaciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvFormaciones.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvFormaciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFormaciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvFormaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre_formacion,
            this.tipo_formacion,
            this.solicitud_curso,
            this.duracion_curso,
            this.estatus_formacion,
            this.creado_por});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFormaciones.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvFormaciones.Location = new System.Drawing.Point(22, 25);
            this.dgvFormaciones.Name = "dgvFormaciones";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFormaciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvFormaciones.RowHeadersVisible = false;
            this.dgvFormaciones.Size = new System.Drawing.Size(695, 525);
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
            this.solicitud_curso.HeaderText = "Solicitud";
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
            // creado_por
            // 
            this.creado_por.HeaderText = "Creador";
            this.creado_por.Name = "creado_por";
            this.creado_por.ReadOnly = true;
            // 
            // errorProviderPeriodo
            // 
            this.errorProviderPeriodo.ContainerControl = this;
            // 
            // grpbDatos
            // 
            this.grpbDatos.Controls.Add(this.btnRefrescar);
            this.grpbDatos.Controls.Add(this.txtBuscarNombre);
            this.grpbDatos.Controls.Add(this.label3);
            this.grpbDatos.Controls.Add(this.label2);
            this.grpbDatos.Controls.Add(this.cmbxEstatus);
            this.grpbDatos.Controls.Add(this.btnBuscar);
            this.grpbDatos.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbDatos.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbDatos.Location = new System.Drawing.Point(768, 138);
            this.grpbDatos.Name = "grpbDatos";
            this.grpbDatos.Size = new System.Drawing.Size(336, 231);
            this.grpbDatos.TabIndex = 79;
            this.grpbDatos.TabStop = false;
            this.grpbDatos.Text = "Curso";
            // 
            // txtBuscarNombre
            // 
            this.txtBuscarNombre.Font = new System.Drawing.Font("Rockwell", 10F);
            this.txtBuscarNombre.Location = new System.Drawing.Point(99, 30);
            this.txtBuscarNombre.MaxLength = 44;
            this.txtBuscarNombre.Name = "txtBuscarNombre";
            this.txtBuscarNombre.Size = new System.Drawing.Size(202, 23);
            this.txtBuscarNombre.TabIndex = 8;
            this.txtBuscarNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombrePart_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 11F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(28, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Estatus:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 11F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(20, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
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
            this.cmbxEstatus.Location = new System.Drawing.Point(99, 66);
            this.cmbxEstatus.Name = "cmbxEstatus";
            this.cmbxEstatus.Size = new System.Drawing.Size(202, 21);
            this.cmbxEstatus.TabIndex = 5;
            this.cmbxEstatus.SelectionChangeCommitted += new System.EventHandler(this.cmbxEstatus_SelectionChangeCommitted);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Teal;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBuscar.Location = new System.Drawing.Point(42, 117);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(246, 37);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // errorProviderCmbxNombre
            // 
            this.errorProviderCmbxNombre.ContainerControl = this;
            // 
            // errorProviderCmbxEstatus
            // 
            this.errorProviderCmbxEstatus.ContainerControl = this;
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(173)))), ((int)(((byte)(224)))));
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.FlatAppearance.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRefrescar.Location = new System.Drawing.Point(42, 173);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(246, 37);
            this.btnRefrescar.TabIndex = 9;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
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
            this.Panel_cabecera.Size = new System.Drawing.Size(1122, 111);
            this.Panel_cabecera.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(315, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(414, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Crear nuevo reporte";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape7});
            this.shapeContainer1.Size = new System.Drawing.Size(1122, 111);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape7
            // 
            this.rectangleShape7.BorderColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape7.Location = new System.Drawing.Point(1, 108);
            this.rectangleShape7.Name = "rectangleShape7";
            this.rectangleShape7.Size = new System.Drawing.Size(1121, 1);
            // 
            // btnRpt_TimeDur
            // 
            this.btnRpt_TimeDur.Location = new System.Drawing.Point(9, 129);
            this.btnRpt_TimeDur.Name = "btnRpt_TimeDur";
            this.btnRpt_TimeDur.Size = new System.Drawing.Size(336, 36);
            this.btnRpt_TimeDur.TabIndex = 81;
            this.btnRpt_TimeDur.Text = "Relacion tiempo / duración de formaciones";
            this.btnRpt_TimeDur.UseVisualStyleBackColor = true;
            this.btnRpt_TimeDur.Click += new System.EventHandler(this.btnRpt_TimeDur_Click);
            // 
            // btnRpt_TipoDur
            // 
            this.btnRpt_TipoDur.Location = new System.Drawing.Point(9, 184);
            this.btnRpt_TipoDur.Name = "btnRpt_TipoDur";
            this.btnRpt_TipoDur.Size = new System.Drawing.Size(336, 36);
            this.btnRpt_TipoDur.TabIndex = 82;
            this.btnRpt_TipoDur.Text = "Relacion tipo / duración de formaciones";
            this.btnRpt_TipoDur.UseVisualStyleBackColor = true;
            this.btnRpt_TipoDur.Click += new System.EventHandler(this.btnRpt_TipoDur_Click);
            // 
            // Hacer_reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1122, 739);
            this.Controls.Add(this.grpbDatos);
            this.Controls.Add(this.grpbData);
            this.Controls.Add(this.gpbItems);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.Panel_cabecera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Hacer_reportes";
            this.Load += new System.EventHandler(this.Hacer_reportes_Load);
            this.gpbItems.ResumeLayout(false);
            this.gpbItems.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.grpbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPeriodo)).EndInit();
            this.grpbDatos.ResumeLayout(false);
            this.grpbDatos.PerformLayout();
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
        private System.Windows.Forms.GroupBox gpbItems;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private System.Windows.Forms.Button btnRpt_TimeEtapas;
        private System.Windows.Forms.Button btnRpt_TimeTipos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbxPeriodoTiempo;
        private System.Windows.Forms.GroupBox grpbData;
        public System.Windows.Forms.DataGridView dgvFormaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_formacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo_formacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn solicitud_curso;
        private System.Windows.Forms.DataGridViewTextBoxColumn duracion_curso;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatus_formacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn creado_por;
        private System.Windows.Forms.ErrorProvider errorProviderPeriodo;
        private System.Windows.Forms.GroupBox grpbDatos;
        private System.Windows.Forms.TextBox txtBuscarNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbxEstatus;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ErrorProvider errorProviderCmbxNombre;
        private System.Windows.Forms.ErrorProvider errorProviderCmbxEstatus;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnRpt_TipoDur;
        private System.Windows.Forms.Button btnRpt_TimeDur;
    }
}