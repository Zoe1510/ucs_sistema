namespace UCS_NODO_FGC
{
    partial class Ver_refrigerios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpbData = new System.Windows.Forms.GroupBox();
            this.dgvRef = new System.Windows.Forms.DataGridView();
            this.nombre_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpbDatos = new System.Windows.Forms.GroupBox();
            this.txtBuscarTodo = new System.Windows.Forms.TextBox();
            this.btnBuscarFa = new System.Windows.Forms.Button();
            this.grpbOpciones = new System.Windows.Forms.GroupBox();
            this.btnAgregar = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvRef)).BeginInit();
            this.grpbDatos.SuspendLayout();
            this.grpbOpciones.SuspendLayout();
            this.Panel_cabecera.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbData
            // 
            this.grpbData.Controls.Add(this.dgvRef);
            this.grpbData.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbData.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbData.Location = new System.Drawing.Point(25, 121);
            this.grpbData.Name = "grpbData";
            this.grpbData.Size = new System.Drawing.Size(832, 570);
            this.grpbData.TabIndex = 48;
            this.grpbData.TabStop = false;
            this.grpbData.Text = "Refrigerios actuales";
            // 
            // dgvRef
            // 
            this.dgvRef.AllowUserToAddRows = false;
            this.dgvRef.AllowUserToResizeColumns = false;
            this.dgvRef.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.dgvRef.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRef.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvRef.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRef.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRef.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRef.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre_ref,
            this.descripcion_ref});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRef.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRef.Location = new System.Drawing.Point(20, 34);
            this.dgvRef.MultiSelect = false;
            this.dgvRef.Name = "dgvRef";
            this.dgvRef.ReadOnly = true;
            this.dgvRef.RowHeadersVisible = false;
            this.dgvRef.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvRef.Size = new System.Drawing.Size(790, 510);
            this.dgvRef.TabIndex = 0;
            this.dgvRef.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvRef_MouseClick);
            // 
            // nombre_ref
            // 
            this.nombre_ref.HeaderText = "Nombre";
            this.nombre_ref.MaxInputLength = 50;
            this.nombre_ref.MinimumWidth = 100;
            this.nombre_ref.Name = "nombre_ref";
            this.nombre_ref.ReadOnly = true;
            this.nombre_ref.Width = 200;
            // 
            // descripcion_ref
            // 
            this.descripcion_ref.HeaderText = "Descripcion contenido";
            this.descripcion_ref.MaxInputLength = 250;
            this.descripcion_ref.MinimumWidth = 300;
            this.descripcion_ref.Name = "descripcion_ref";
            this.descripcion_ref.ReadOnly = true;
            this.descripcion_ref.Width = 587;
            // 
            // grpbDatos
            // 
            this.grpbDatos.Controls.Add(this.txtBuscarTodo);
            this.grpbDatos.Controls.Add(this.btnBuscarFa);
            this.grpbDatos.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbDatos.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbDatos.Location = new System.Drawing.Point(874, 136);
            this.grpbDatos.Name = "grpbDatos";
            this.grpbDatos.Size = new System.Drawing.Size(222, 172);
            this.grpbDatos.TabIndex = 49;
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
            this.grpbOpciones.Controls.Add(this.btnAgregar);
            this.grpbOpciones.Controls.Add(this.btnRefrescar);
            this.grpbOpciones.Controls.Add(this.btnEliminarFa);
            this.grpbOpciones.Controls.Add(this.btnModificarFa);
            this.grpbOpciones.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbOpciones.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbOpciones.Location = new System.Drawing.Point(874, 328);
            this.grpbOpciones.Name = "grpbOpciones";
            this.grpbOpciones.Size = new System.Drawing.Size(222, 363);
            this.grpbOpciones.TabIndex = 50;
            this.grpbOpciones.TabStop = false;
            this.grpbOpciones.Text = "Opciones";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAgregar.Location = new System.Drawing.Point(22, 132);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(176, 47);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Añadir";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
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
            this.btnEliminarFa.Location = new System.Drawing.Point(22, 288);
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
            this.btnModificarFa.Location = new System.Drawing.Point(22, 212);
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
            this.Panel_cabecera.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(424, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Refrigerios";
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
            // Ver_refrigerios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1121, 739);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.grpbOpciones);
            this.Controls.Add(this.grpbDatos);
            this.Controls.Add(this.grpbData);
            this.Controls.Add(this.Panel_cabecera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Ver_refrigerios";
            this.Text = "Ver_refrigerios";
            this.Load += new System.EventHandler(this.Ver_refrigerios_Load);
            this.grpbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRef)).EndInit();
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

        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
        private System.Windows.Forms.GroupBox grpbData;
        public System.Windows.Forms.DataGridView dgvRef;
        private System.Windows.Forms.GroupBox grpbDatos;
        private System.Windows.Forms.TextBox txtBuscarTodo;
        private System.Windows.Forms.Button btnBuscarFa;
        private System.Windows.Forms.GroupBox grpbOpciones;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnEliminarFa;
        private System.Windows.Forms.Button btnModificarFa;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion_ref;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
    }
}