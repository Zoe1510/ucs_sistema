namespace UCS_NODO_FGC
{
    partial class Buscar_cliente
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpbData = new System.Windows.Forms.GroupBox();
            this.dgvAreasEmpresa = new System.Windows.Forms.DataGridView();
            this.nombre_empresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_contacto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlfn_contacto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.correo_contacto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fee_empresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpbDatos = new System.Windows.Forms.GroupBox();
            this.cmbxEmpresa = new System.Windows.Forms.ComboBox();
            this.btnBuscarAreas = new System.Windows.Forms.Button();
            this.grpbOpciones = new System.Windows.Forms.GroupBox();
            this.btnModificarArea = new System.Windows.Forms.Button();
            this.btnEliminarEmpresa = new System.Windows.Forms.Button();
            this.btnEliminarArea = new System.Windows.Forms.Button();
            this.btnModificarEmpresa = new System.Windows.Forms.Button();
            this.btnModificarContacto = new System.Windows.Forms.Button();
            this.btnVerTodo = new System.Windows.Forms.Button();
            this.errorProviderSelecEmpresa = new System.Windows.Forms.ErrorProvider(this.components);
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.grpbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAreasEmpresa)).BeginInit();
            this.grpbDatos.SuspendLayout();
            this.grpbOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderSelecEmpresa)).BeginInit();
            this.Panel_cabecera.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbData
            // 
            this.grpbData.Controls.Add(this.dgvAreasEmpresa);
            this.grpbData.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbData.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbData.Location = new System.Drawing.Point(27, 119);
            this.grpbData.Name = "grpbData";
            this.grpbData.Size = new System.Drawing.Size(766, 579);
            this.grpbData.TabIndex = 3;
            this.grpbData.TabStop = false;
            this.grpbData.Text = "Áreas registradas";
            // 
            // dgvAreasEmpresa
            // 
            this.dgvAreasEmpresa.AllowUserToAddRows = false;
            this.dgvAreasEmpresa.AllowUserToResizeColumns = false;
            this.dgvAreasEmpresa.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = null;
            this.dgvAreasEmpresa.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAreasEmpresa.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvAreasEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAreasEmpresa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAreasEmpresa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre_empresa,
            this.nombre_area,
            this.nombre_contacto,
            this.tlfn_contacto,
            this.correo_contacto,
            this.fee_empresa});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAreasEmpresa.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAreasEmpresa.Location = new System.Drawing.Point(22, 34);
            this.dgvAreasEmpresa.Name = "dgvAreasEmpresa";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAreasEmpresa.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAreasEmpresa.RowHeadersVisible = false;
            this.dgvAreasEmpresa.Size = new System.Drawing.Size(724, 525);
            this.dgvAreasEmpresa.TabIndex = 0;
            this.dgvAreasEmpresa.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvAreasEmpresa_MouseClick);
            // 
            // nombre_empresa
            // 
            this.nombre_empresa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nombre_empresa.HeaderText = "Empresa";
            this.nombre_empresa.MaxInputLength = 100;
            this.nombre_empresa.Name = "nombre_empresa";
            this.nombre_empresa.ReadOnly = true;
            this.nombre_empresa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // nombre_area
            // 
            this.nombre_area.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nombre_area.HeaderText = "Área";
            this.nombre_area.MaxInputLength = 100;
            this.nombre_area.Name = "nombre_area";
            this.nombre_area.ReadOnly = true;
            this.nombre_area.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // nombre_contacto
            // 
            this.nombre_contacto.HeaderText = "Contacto de área";
            this.nombre_contacto.MaxInputLength = 100;
            this.nombre_contacto.Name = "nombre_contacto";
            this.nombre_contacto.ReadOnly = true;
            this.nombre_contacto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.nombre_contacto.Width = 150;
            // 
            // tlfn_contacto
            // 
            this.tlfn_contacto.HeaderText = "Teléfono de contacto";
            this.tlfn_contacto.MaxInputLength = 11;
            this.tlfn_contacto.Name = "tlfn_contacto";
            this.tlfn_contacto.ReadOnly = true;
            this.tlfn_contacto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tlfn_contacto.Width = 170;
            // 
            // correo_contacto
            // 
            this.correo_contacto.HeaderText = "Correo de contacto";
            this.correo_contacto.MaxInputLength = 254;
            this.correo_contacto.Name = "correo_contacto";
            this.correo_contacto.ReadOnly = true;
            this.correo_contacto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.correo_contacto.Width = 200;
            // 
            // fee_empresa
            // 
            this.fee_empresa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.fee_empresa.HeaderText = "FEE";
            this.fee_empresa.MaxInputLength = 3;
            this.fee_empresa.Name = "fee_empresa";
            this.fee_empresa.ReadOnly = true;
            this.fee_empresa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.fee_empresa.Width = 55;
            // 
            // grpbDatos
            // 
            this.grpbDatos.Controls.Add(this.cmbxEmpresa);
            this.grpbDatos.Controls.Add(this.btnBuscarAreas);
            this.grpbDatos.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbDatos.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbDatos.Location = new System.Drawing.Point(840, 119);
            this.grpbDatos.Name = "grpbDatos";
            this.grpbDatos.Size = new System.Drawing.Size(240, 172);
            this.grpbDatos.TabIndex = 7;
            this.grpbDatos.TabStop = false;
            this.grpbDatos.Text = "Empresas";
            // 
            // cmbxEmpresa
            // 
            this.cmbxEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxEmpresa.FormattingEnabled = true;
            this.cmbxEmpresa.Location = new System.Drawing.Point(31, 34);
            this.cmbxEmpresa.MaxDropDownItems = 3;
            this.cmbxEmpresa.Name = "cmbxEmpresa";
            this.cmbxEmpresa.Size = new System.Drawing.Size(176, 27);
            this.cmbxEmpresa.TabIndex = 0;
            this.cmbxEmpresa.SelectedIndexChanged += new System.EventHandler(this.cmbxEmpresa_SelectedIndexChanged);
            this.cmbxEmpresa.SelectionChangeCommitted += new System.EventHandler(this.cmbxEmpresa_SelectionChangeCommitted);
            // 
            // btnBuscarAreas
            // 
            this.btnBuscarAreas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnBuscarAreas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarAreas.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnBuscarAreas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarAreas.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarAreas.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBuscarAreas.Location = new System.Drawing.Point(31, 89);
            this.btnBuscarAreas.Name = "btnBuscarAreas";
            this.btnBuscarAreas.Size = new System.Drawing.Size(176, 47);
            this.btnBuscarAreas.TabIndex = 1;
            this.btnBuscarAreas.Text = "Buscar";
            this.btnBuscarAreas.UseVisualStyleBackColor = false;
            this.btnBuscarAreas.Click += new System.EventHandler(this.btnBuscarAreas_Click);
            // 
            // grpbOpciones
            // 
            this.grpbOpciones.Controls.Add(this.btnModificarArea);
            this.grpbOpciones.Controls.Add(this.btnEliminarEmpresa);
            this.grpbOpciones.Controls.Add(this.btnEliminarArea);
            this.grpbOpciones.Controls.Add(this.btnModificarEmpresa);
            this.grpbOpciones.Controls.Add(this.btnModificarContacto);
            this.grpbOpciones.Controls.Add(this.btnVerTodo);
            this.grpbOpciones.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbOpciones.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbOpciones.Location = new System.Drawing.Point(840, 311);
            this.grpbOpciones.Name = "grpbOpciones";
            this.grpbOpciones.Size = new System.Drawing.Size(240, 387);
            this.grpbOpciones.TabIndex = 8;
            this.grpbOpciones.TabStop = false;
            this.grpbOpciones.Text = "Opciones";
            this.grpbOpciones.Visible = false;
            // 
            // btnModificarArea
            // 
            this.btnModificarArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(115)))), ((int)(((byte)(177)))));
            this.btnModificarArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarArea.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnModificarArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarArea.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificarArea.Location = new System.Drawing.Point(32, 213);
            this.btnModificarArea.Name = "btnModificarArea";
            this.btnModificarArea.Size = new System.Drawing.Size(176, 38);
            this.btnModificarArea.TabIndex = 10;
            this.btnModificarArea.Text = "Modificar área";
            this.btnModificarArea.UseVisualStyleBackColor = false;
            this.btnModificarArea.Click += new System.EventHandler(this.btnModificarArea_Click);
            // 
            // btnEliminarEmpresa
            // 
            this.btnEliminarEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnEliminarEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarEmpresa.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarEmpresa.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEliminarEmpresa.Location = new System.Drawing.Point(32, 329);
            this.btnEliminarEmpresa.Name = "btnEliminarEmpresa";
            this.btnEliminarEmpresa.Size = new System.Drawing.Size(176, 38);
            this.btnEliminarEmpresa.TabIndex = 8;
            this.btnEliminarEmpresa.Text = "Eliminar empresa";
            this.btnEliminarEmpresa.UseVisualStyleBackColor = false;
            this.btnEliminarEmpresa.Click += new System.EventHandler(this.btnEliminarEmpresa_Click);
            // 
            // btnEliminarArea
            // 
            this.btnEliminarArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(31)))), ((int)(((byte)(30)))));
            this.btnEliminarArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarArea.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarArea.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEliminarArea.Location = new System.Drawing.Point(31, 272);
            this.btnEliminarArea.Name = "btnEliminarArea";
            this.btnEliminarArea.Size = new System.Drawing.Size(176, 38);
            this.btnEliminarArea.TabIndex = 6;
            this.btnEliminarArea.Text = "Eliminar área";
            this.btnEliminarArea.UseVisualStyleBackColor = false;
            this.btnEliminarArea.Click += new System.EventHandler(this.btnEliminarArea_Click);
            // 
            // btnModificarEmpresa
            // 
            this.btnModificarEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.btnModificarEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarEmpresa.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnModificarEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarEmpresa.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificarEmpresa.Location = new System.Drawing.Point(32, 155);
            this.btnModificarEmpresa.Name = "btnModificarEmpresa";
            this.btnModificarEmpresa.Size = new System.Drawing.Size(176, 38);
            this.btnModificarEmpresa.TabIndex = 9;
            this.btnModificarEmpresa.Text = "Modificar empresa";
            this.btnModificarEmpresa.UseVisualStyleBackColor = false;
            this.btnModificarEmpresa.Click += new System.EventHandler(this.btnModificarEmpresa_Click);
            // 
            // btnModificarContacto
            // 
            this.btnModificarContacto.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnModificarContacto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarContacto.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnModificarContacto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarContacto.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarContacto.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificarContacto.Location = new System.Drawing.Point(31, 97);
            this.btnModificarContacto.Name = "btnModificarContacto";
            this.btnModificarContacto.Size = new System.Drawing.Size(176, 38);
            this.btnModificarContacto.TabIndex = 5;
            this.btnModificarContacto.Text = "Modificar contacto";
            this.btnModificarContacto.UseVisualStyleBackColor = false;
            this.btnModificarContacto.Click += new System.EventHandler(this.btnModificarContacto_Click);
            // 
            // btnVerTodo
            // 
            this.btnVerTodo.BackColor = System.Drawing.Color.SlateBlue;
            this.btnVerTodo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerTodo.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnVerTodo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerTodo.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerTodo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnVerTodo.Location = new System.Drawing.Point(31, 42);
            this.btnVerTodo.Name = "btnVerTodo";
            this.btnVerTodo.Size = new System.Drawing.Size(176, 38);
            this.btnVerTodo.TabIndex = 7;
            this.btnVerTodo.Text = "Refrescar";
            this.btnVerTodo.UseVisualStyleBackColor = false;
            this.btnVerTodo.Click += new System.EventHandler(this.btnVerTodo_Click);
            // 
            // errorProviderSelecEmpresa
            // 
            this.errorProviderSelecEmpresa.ContainerControl = this;
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
            this.Panel_cabecera.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(266, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(432, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Busqueda de clientes";
            // 
            // rectangleShape7
            // 
            this.rectangleShape7.BorderColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape7.Location = new System.Drawing.Point(1, 93);
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
            this.shapeContainer1.Size = new System.Drawing.Size(1122, 96);
            this.shapeContainer1.TabIndex = 2;
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
            this.panel8.TabIndex = 59;
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
            // Buscar_cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1122, 739);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.Panel_cabecera);
            this.Controls.Add(this.grpbOpciones);
            this.Controls.Add(this.grpbDatos);
            this.Controls.Add(this.grpbData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Buscar_cliente";
            this.Text = "Buscar_cliente";
            this.Load += new System.EventHandler(this.Buscar_cliente_Load);
            this.grpbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAreasEmpresa)).EndInit();
            this.grpbDatos.ResumeLayout(false);
            this.grpbOpciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderSelecEmpresa)).EndInit();
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbData;
        public System.Windows.Forms.DataGridView dgvAreasEmpresa;
        private System.Windows.Forms.GroupBox grpbDatos;
        private System.Windows.Forms.Button btnBuscarAreas;
        private System.Windows.Forms.GroupBox grpbOpciones;
        private System.Windows.Forms.Button btnEliminarEmpresa;
        private System.Windows.Forms.Button btnVerTodo;
        private System.Windows.Forms.Button btnEliminarArea;
        private System.Windows.Forms.Button btnModificarContacto;
        private System.Windows.Forms.ComboBox cmbxEmpresa;
        private System.Windows.Forms.Button btnModificarArea;
        private System.Windows.Forms.Button btnModificarEmpresa;
        private System.Windows.Forms.ErrorProvider errorProviderSelecEmpresa;
        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_empresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_area;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_contacto;
        private System.Windows.Forms.DataGridViewTextBoxColumn tlfn_contacto;
        private System.Windows.Forms.DataGridViewTextBoxColumn correo_contacto;
        private System.Windows.Forms.DataGridViewTextBoxColumn fee_empresa;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
    }
}