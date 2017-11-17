namespace UCS_NODO_FGC
{
    partial class Buscar_facilitadores
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpbData = new System.Windows.Forms.GroupBox();
            this.dgvFa = new System.Windows.Forms.DataGridView();
            this.nacionalidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellido_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.correo_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.especialidad_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlfn_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requerimiento_inces = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ubicacion_fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpbDatos = new System.Windows.Forms.GroupBox();
            this.txtBuscarTodo = new System.Windows.Forms.TextBox();
            this.btnBuscarFa = new System.Windows.Forms.Button();
            this.grpbOpciones = new System.Windows.Forms.GroupBox();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnEliminarFa = new System.Windows.Forms.Button();
            this.btnModificarFa = new System.Windows.Forms.Button();
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.grpbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFa)).BeginInit();
            this.grpbDatos.SuspendLayout();
            this.grpbOpciones.SuspendLayout();
            this.Panel_cabecera.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbData
            // 
            this.grpbData.Controls.Add(this.dgvFa);
            this.grpbData.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbData.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbData.Location = new System.Drawing.Point(12, 117);
            this.grpbData.Name = "grpbData";
            this.grpbData.Size = new System.Drawing.Size(860, 576);
            this.grpbData.TabIndex = 3;
            this.grpbData.TabStop = false;
            this.grpbData.Text = "Facilitadores registrados";
            // 
            // dgvFa
            // 
            this.dgvFa.AllowUserToAddRows = false;
            this.dgvFa.AllowUserToResizeColumns = false;
            this.dgvFa.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.dgvFa.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFa.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvFa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFa.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nacionalidad,
            this.id_fa,
            this.nombre_fa,
            this.apellido_fa,
            this.correo_fa,
            this.especialidad_fa,
            this.tlfn_fa,
            this.requerimiento_inces,
            this.ubicacion_fa});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFa.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFa.Location = new System.Drawing.Point(20, 34);
            this.dgvFa.MultiSelect = false;
            this.dgvFa.Name = "dgvFa";
            this.dgvFa.ReadOnly = true;
            this.dgvFa.RowHeadersVisible = false;
            this.dgvFa.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvFa.Size = new System.Drawing.Size(821, 522);
            this.dgvFa.TabIndex = 0;
            this.dgvFa.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvFa_MouseClick);
            // 
            // nacionalidad
            // 
            this.nacionalidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nacionalidad.HeaderText = "";
            this.nacionalidad.MaxInputLength = 1;
            this.nacionalidad.Name = "nacionalidad";
            this.nacionalidad.ReadOnly = true;
            this.nacionalidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.nacionalidad.Width = 40;
            // 
            // id_fa
            // 
            this.id_fa.HeaderText = "Cédula";
            this.id_fa.MaxInputLength = 8;
            this.id_fa.Name = "id_fa";
            this.id_fa.ReadOnly = true;
            this.id_fa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.id_fa.Width = 150;
            // 
            // nombre_fa
            // 
            this.nombre_fa.HeaderText = "Nombre";
            this.nombre_fa.MaxInputLength = 45;
            this.nombre_fa.Name = "nombre_fa";
            this.nombre_fa.ReadOnly = true;
            this.nombre_fa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.nombre_fa.Width = 150;
            // 
            // apellido_fa
            // 
            this.apellido_fa.HeaderText = "Apellido";
            this.apellido_fa.MaxInputLength = 45;
            this.apellido_fa.Name = "apellido_fa";
            this.apellido_fa.ReadOnly = true;
            this.apellido_fa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.apellido_fa.Width = 150;
            // 
            // correo_fa
            // 
            this.correo_fa.HeaderText = "Correo";
            this.correo_fa.MaxInputLength = 255;
            this.correo_fa.MinimumWidth = 100;
            this.correo_fa.Name = "correo_fa";
            this.correo_fa.ReadOnly = true;
            this.correo_fa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.correo_fa.Width = 140;
            // 
            // especialidad_fa
            // 
            this.especialidad_fa.HeaderText = "Especialidad";
            this.especialidad_fa.MaxInputLength = 45;
            this.especialidad_fa.Name = "especialidad_fa";
            this.especialidad_fa.ReadOnly = true;
            this.especialidad_fa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.especialidad_fa.Width = 130;
            // 
            // tlfn_fa
            // 
            this.tlfn_fa.HeaderText = "Teléfono";
            this.tlfn_fa.MaxInputLength = 11;
            this.tlfn_fa.Name = "tlfn_fa";
            this.tlfn_fa.ReadOnly = true;
            this.tlfn_fa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // requerimiento_inces
            // 
            this.requerimiento_inces.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.requerimiento_inces.HeaderText = "Inces";
            this.requerimiento_inces.MaxInputLength = 3;
            this.requerimiento_inces.Name = "requerimiento_inces";
            this.requerimiento_inces.ReadOnly = true;
            this.requerimiento_inces.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.requerimiento_inces.Width = 55;
            // 
            // ubicacion_fa
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ubicacion_fa.DefaultCellStyle = dataGridViewCellStyle3;
            this.ubicacion_fa.HeaderText = "Ubicación";
            this.ubicacion_fa.Name = "ubicacion_fa";
            this.ubicacion_fa.ReadOnly = true;
            this.ubicacion_fa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ubicacion_fa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // grpbDatos
            // 
            this.grpbDatos.Controls.Add(this.txtBuscarTodo);
            this.grpbDatos.Controls.Add(this.btnBuscarFa);
            this.grpbDatos.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbDatos.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbDatos.Location = new System.Drawing.Point(881, 117);
            this.grpbDatos.Name = "grpbDatos";
            this.grpbDatos.Size = new System.Drawing.Size(222, 172);
            this.grpbDatos.TabIndex = 6;
            this.grpbDatos.TabStop = false;
            this.grpbDatos.Text = "Datos";
            // 
            // txtBuscarTodo
            // 
            this.txtBuscarTodo.Location = new System.Drawing.Point(22, 34);
            this.txtBuscarTodo.Multiline = true;
            this.txtBuscarTodo.Name = "txtBuscarTodo";
            this.txtBuscarTodo.Size = new System.Drawing.Size(176, 28);
            this.txtBuscarTodo.TabIndex = 4;
            this.txtBuscarTodo.Text = "Escriba aquí";
            this.txtBuscarTodo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscarTodo.Click += new System.EventHandler(this.txtBuscarTodo_Click);
            this.txtBuscarTodo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarTodo_KeyDown);
            this.txtBuscarTodo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarTodo_KeyPress);
            this.txtBuscarTodo.Leave += new System.EventHandler(this.txtBuscarTodo_Leave);
            // 
            // btnBuscarFa
            // 
            this.btnBuscarFa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnBuscarFa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarFa.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnBuscarFa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarFa.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBuscarFa.Location = new System.Drawing.Point(22, 89);
            this.btnBuscarFa.Name = "btnBuscarFa";
            this.btnBuscarFa.Size = new System.Drawing.Size(176, 47);
            this.btnBuscarFa.TabIndex = 3;
            this.btnBuscarFa.Text = "Buscar";
            this.btnBuscarFa.UseVisualStyleBackColor = false;
            this.btnBuscarFa.Click += new System.EventHandler(this.btnBuscarFa_Click);
            // 
            // grpbOpciones
            // 
            this.grpbOpciones.Controls.Add(this.btnRefrescar);
            this.grpbOpciones.Controls.Add(this.btnEliminarFa);
            this.grpbOpciones.Controls.Add(this.btnModificarFa);
            this.grpbOpciones.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbOpciones.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbOpciones.Location = new System.Drawing.Point(881, 330);
            this.grpbOpciones.Name = "grpbOpciones";
            this.grpbOpciones.Size = new System.Drawing.Size(222, 300);
            this.grpbOpciones.TabIndex = 7;
            this.grpbOpciones.TabStop = false;
            this.grpbOpciones.Text = "Opciones";
            this.grpbOpciones.Visible = false;
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.SlateBlue;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefrescar.Location = new System.Drawing.Point(22, 54);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(176, 47);
            this.btnRefrescar.TabIndex = 7;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnEliminarFa
            // 
            this.btnEliminarFa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnEliminarFa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarFa.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarFa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarFa.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEliminarFa.Location = new System.Drawing.Point(22, 225);
            this.btnEliminarFa.Name = "btnEliminarFa";
            this.btnEliminarFa.Size = new System.Drawing.Size(176, 47);
            this.btnEliminarFa.TabIndex = 6;
            this.btnEliminarFa.Text = "Eliminar";
            this.btnEliminarFa.UseVisualStyleBackColor = false;
            this.btnEliminarFa.Click += new System.EventHandler(this.btnEliminarFa_Click);
            // 
            // btnModificarFa
            // 
            this.btnModificarFa.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnModificarFa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarFa.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnModificarFa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarFa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarFa.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificarFa.Location = new System.Drawing.Point(22, 138);
            this.btnModificarFa.Name = "btnModificarFa";
            this.btnModificarFa.Size = new System.Drawing.Size(176, 47);
            this.btnModificarFa.TabIndex = 5;
            this.btnModificarFa.Text = "Modificar";
            this.btnModificarFa.UseVisualStyleBackColor = false;
            this.btnModificarFa.Click += new System.EventHandler(this.btnModificarFa_Click);
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
            this.Panel_cabecera.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(266, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(526, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Busqueda de facilitadores";
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
            // Buscar_facilitadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1121, 739);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.Panel_cabecera);
            this.Controls.Add(this.grpbOpciones);
            this.Controls.Add(this.grpbDatos);
            this.Controls.Add(this.grpbData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Buscar_facilitadores";
            this.Text = "Buscar_facilitadores";
            this.Load += new System.EventHandler(this.Buscar_facilitadores_Load);
            this.grpbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFa)).EndInit();
            this.grpbDatos.ResumeLayout(false);
            this.grpbDatos.PerformLayout();
            this.grpbOpciones.ResumeLayout(false);
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbData;
        public System.Windows.Forms.DataGridView dgvFa;
        private System.Windows.Forms.GroupBox grpbDatos;
        private System.Windows.Forms.Button btnBuscarFa;
        private System.Windows.Forms.GroupBox grpbOpciones;
        private System.Windows.Forms.Button btnEliminarFa;
        private System.Windows.Forms.Button btnModificarFa;
        private System.Windows.Forms.TextBox txtBuscarTodo;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nacionalidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_fa;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_fa;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellido_fa;
        private System.Windows.Forms.DataGridViewTextBoxColumn correo_fa;
        private System.Windows.Forms.DataGridViewTextBoxColumn especialidad_fa;
        private System.Windows.Forms.DataGridViewTextBoxColumn tlfn_fa;
        private System.Windows.Forms.DataGridViewTextBoxColumn requerimiento_inces;
        private System.Windows.Forms.DataGridViewTextBoxColumn ubicacion_fa;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
    }
}