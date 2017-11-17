namespace UCS_NODO_FGC
{
    partial class Buscar_usuarios
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
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.nacionalidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellido_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cargo_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.correo_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlfn_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtApellidoUsuario = new System.Windows.Forms.TextBox();
            this.btnBuscarUsuario = new System.Windows.Forms.Button();
            this.grpbData = new System.Windows.Forms.GroupBox();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.txtCedulaUsuario = new System.Windows.Forms.TextBox();
            this.grpbDatos = new System.Windows.Forms.GroupBox();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.cmbxCargoUsuario = new System.Windows.Forms.ComboBox();
            this.txtCargoUsuario = new System.Windows.Forms.TextBox();
            this.grpbOpciones = new System.Windows.Forms.GroupBox();
            this.btnEliminarUsuario = new System.Windows.Forms.Button();
            this.btnModificarUsuario = new System.Windows.Forms.Button();
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.grpbData.SuspendLayout();
            this.grpbDatos.SuspendLayout();
            this.grpbOpciones.SuspendLayout();
            this.Panel_cabecera.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.AllowUserToResizeColumns = false;
            this.dgvUsuarios.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.dgvUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsuarios.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nacionalidad,
            this.id_user,
            this.nombre_user,
            this.apellido_user,
            this.cargo_user,
            this.correo_user,
            this.tlfn_user});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsuarios.Location = new System.Drawing.Point(22, 34);
            this.dgvUsuarios.Name = "dgvUsuarios";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.Size = new System.Drawing.Size(724, 510);
            this.dgvUsuarios.TabIndex = 0;
            this.dgvUsuarios.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvUsuarios_MouseClick);
            // 
            // nacionalidad
            // 
            this.nacionalidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nacionalidad.HeaderText = "";
            this.nacionalidad.MaxInputLength = 1;
            this.nacionalidad.Name = "nacionalidad";
            this.nacionalidad.ReadOnly = true;
            this.nacionalidad.Width = 40;
            // 
            // id_user
            // 
            this.id_user.HeaderText = "Cédula";
            this.id_user.MaxInputLength = 8;
            this.id_user.Name = "id_user";
            this.id_user.ReadOnly = true;
            // 
            // nombre_user
            // 
            this.nombre_user.HeaderText = "Nombre";
            this.nombre_user.Name = "nombre_user";
            this.nombre_user.ReadOnly = true;
            // 
            // apellido_user
            // 
            this.apellido_user.HeaderText = "Apellido";
            this.apellido_user.Name = "apellido_user";
            this.apellido_user.ReadOnly = true;
            // 
            // cargo_user
            // 
            this.cargo_user.HeaderText = "Cargo";
            this.cargo_user.Name = "cargo_user";
            this.cargo_user.ReadOnly = true;
            // 
            // correo_user
            // 
            this.correo_user.HeaderText = "Correo";
            this.correo_user.Name = "correo_user";
            this.correo_user.ReadOnly = true;
            // 
            // tlfn_user
            // 
            this.tlfn_user.HeaderText = "Teléfono";
            this.tlfn_user.Name = "tlfn_user";
            this.tlfn_user.ReadOnly = true;
            // 
            // txtApellidoUsuario
            // 
            this.txtApellidoUsuario.Location = new System.Drawing.Point(31, 148);
            this.txtApellidoUsuario.Multiline = true;
            this.txtApellidoUsuario.Name = "txtApellidoUsuario";
            this.txtApellidoUsuario.Size = new System.Drawing.Size(176, 28);
            this.txtApellidoUsuario.TabIndex = 2;
            this.txtApellidoUsuario.Text = "Apellido";
            this.txtApellidoUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtApellidoUsuario.Click += new System.EventHandler(this.txtApellidoUsuario_Click);
            this.txtApellidoUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtApellidoUsuario_KeyDown);
            this.txtApellidoUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApellidoUsuario_KeyPress);
            this.txtApellidoUsuario.Leave += new System.EventHandler(this.txtApellidoUsuario_Leave);
            // 
            // btnBuscarUsuario
            // 
            this.btnBuscarUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnBuscarUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarUsuario.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnBuscarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarUsuario.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarUsuario.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBuscarUsuario.Location = new System.Drawing.Point(31, 259);
            this.btnBuscarUsuario.Name = "btnBuscarUsuario";
            this.btnBuscarUsuario.Size = new System.Drawing.Size(176, 47);
            this.btnBuscarUsuario.TabIndex = 4;
            this.btnBuscarUsuario.Text = "Buscar";
            this.btnBuscarUsuario.UseVisualStyleBackColor = false;
            this.btnBuscarUsuario.Click += new System.EventHandler(this.btnBuscarUsuario_Click);
            // 
            // grpbData
            // 
            this.grpbData.Controls.Add(this.dgvUsuarios);
            this.grpbData.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbData.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbData.Location = new System.Drawing.Point(28, 122);
            this.grpbData.Name = "grpbData";
            this.grpbData.Size = new System.Drawing.Size(766, 564);
            this.grpbData.TabIndex = 2;
            this.grpbData.TabStop = false;
            this.grpbData.Text = "Usuarios registrados";
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Location = new System.Drawing.Point(31, 91);
            this.txtNombreUsuario.Multiline = true;
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(176, 28);
            this.txtNombreUsuario.TabIndex = 1;
            this.txtNombreUsuario.Text = "Nombre";
            this.txtNombreUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreUsuario.Click += new System.EventHandler(this.txtNombreUsuario_Click);
            this.txtNombreUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreUsuario_KeyDown);
            this.txtNombreUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreUsuario_KeyPress);
            this.txtNombreUsuario.Leave += new System.EventHandler(this.txtNombreUsuario_Leave);
            // 
            // txtCedulaUsuario
            // 
            this.txtCedulaUsuario.Cursor = System.Windows.Forms.Cursors.No;
            this.txtCedulaUsuario.Location = new System.Drawing.Point(31, 38);
            this.txtCedulaUsuario.MaxLength = 8;
            this.txtCedulaUsuario.Multiline = true;
            this.txtCedulaUsuario.Name = "txtCedulaUsuario";
            this.txtCedulaUsuario.ReadOnly = true;
            this.txtCedulaUsuario.Size = new System.Drawing.Size(176, 28);
            this.txtCedulaUsuario.TabIndex = 0;
            this.txtCedulaUsuario.Text = "Cédula";
            this.txtCedulaUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCedulaUsuario.Click += new System.EventHandler(this.txtCedulaUsuario_Click);
            this.txtCedulaUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCedulaUsuario_KeyDown);
            this.txtCedulaUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCedulaUsuario_KeyPress);
            this.txtCedulaUsuario.Leave += new System.EventHandler(this.txtCedulaUsuario_Leave);
            // 
            // grpbDatos
            // 
            this.grpbDatos.Controls.Add(this.btnRefrescar);
            this.grpbDatos.Controls.Add(this.cmbxCargoUsuario);
            this.grpbDatos.Controls.Add(this.txtCargoUsuario);
            this.grpbDatos.Controls.Add(this.btnBuscarUsuario);
            this.grpbDatos.Controls.Add(this.txtCedulaUsuario);
            this.grpbDatos.Controls.Add(this.txtApellidoUsuario);
            this.grpbDatos.Controls.Add(this.txtNombreUsuario);
            this.grpbDatos.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbDatos.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbDatos.Location = new System.Drawing.Point(840, 122);
            this.grpbDatos.Name = "grpbDatos";
            this.grpbDatos.Size = new System.Drawing.Size(240, 387);
            this.grpbDatos.TabIndex = 5;
            this.grpbDatos.TabStop = false;
            this.grpbDatos.Text = "Datos";
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRefrescar.Location = new System.Drawing.Point(31, 321);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(176, 47);
            this.btnRefrescar.TabIndex = 5;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // cmbxCargoUsuario
            // 
            this.cmbxCargoUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCargoUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxCargoUsuario.FormattingEnabled = true;
            this.cmbxCargoUsuario.Items.AddRange(new object[] {
            "Asistente",
            "Coordinador",
            "Líder"});
            this.cmbxCargoUsuario.Location = new System.Drawing.Point(31, 207);
            this.cmbxCargoUsuario.Name = "cmbxCargoUsuario";
            this.cmbxCargoUsuario.Size = new System.Drawing.Size(176, 27);
            this.cmbxCargoUsuario.TabIndex = 3;
            // 
            // txtCargoUsuario
            // 
            this.txtCargoUsuario.Location = new System.Drawing.Point(31, 206);
            this.txtCargoUsuario.Multiline = true;
            this.txtCargoUsuario.Name = "txtCargoUsuario";
            this.txtCargoUsuario.Size = new System.Drawing.Size(176, 28);
            this.txtCargoUsuario.TabIndex = 5;
            this.txtCargoUsuario.Text = "Cargo";
            this.txtCargoUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCargoUsuario.Click += new System.EventHandler(this.txtCargoUsuario_Click);
            this.txtCargoUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCargoUsuario_KeyDown);
            this.txtCargoUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCargoUsuario_KeyPress);
            this.txtCargoUsuario.Leave += new System.EventHandler(this.txtCargoUsuario_Leave);
            // 
            // grpbOpciones
            // 
            this.grpbOpciones.Controls.Add(this.btnEliminarUsuario);
            this.grpbOpciones.Controls.Add(this.btnModificarUsuario);
            this.grpbOpciones.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbOpciones.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbOpciones.Location = new System.Drawing.Point(840, 523);
            this.grpbOpciones.Name = "grpbOpciones";
            this.grpbOpciones.Size = new System.Drawing.Size(240, 163);
            this.grpbOpciones.TabIndex = 6;
            this.grpbOpciones.TabStop = false;
            this.grpbOpciones.Text = "Opciones";
            this.grpbOpciones.Visible = false;
            // 
            // btnEliminarUsuario
            // 
            this.btnEliminarUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnEliminarUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarUsuario.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarUsuario.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarUsuario.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEliminarUsuario.Location = new System.Drawing.Point(31, 102);
            this.btnEliminarUsuario.Name = "btnEliminarUsuario";
            this.btnEliminarUsuario.Size = new System.Drawing.Size(176, 41);
            this.btnEliminarUsuario.TabIndex = 7;
            this.btnEliminarUsuario.Text = "Eliminar";
            this.btnEliminarUsuario.UseVisualStyleBackColor = false;
            this.btnEliminarUsuario.Click += new System.EventHandler(this.btnEliminarUsuario_Click);
            // 
            // btnModificarUsuario
            // 
            this.btnModificarUsuario.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnModificarUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarUsuario.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnModificarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarUsuario.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarUsuario.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificarUsuario.Location = new System.Drawing.Point(31, 40);
            this.btnModificarUsuario.Name = "btnModificarUsuario";
            this.btnModificarUsuario.Size = new System.Drawing.Size(176, 41);
            this.btnModificarUsuario.TabIndex = 6;
            this.btnModificarUsuario.Text = "Modificar";
            this.btnModificarUsuario.UseVisualStyleBackColor = false;
            this.btnModificarUsuario.Click += new System.EventHandler(this.btnModificarUsuario_Click);
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
            this.Panel_cabecera.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(266, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(448, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Busqueda de usuarios";
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
            this.shapeContainer1.Size = new System.Drawing.Size(1122, 735);
            this.shapeContainer1.TabIndex = 46;
            this.shapeContainer1.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.shapeContainer4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 710);
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
            // Buscar_usuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1122, 735);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.Panel_cabecera);
            this.Controls.Add(this.grpbOpciones);
            this.Controls.Add(this.grpbDatos);
            this.Controls.Add(this.grpbData);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "Buscar_usuarios";
            this.Text = "Buscar_usuarios";
            this.Load += new System.EventHandler(this.Buscar_usuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.grpbData.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtApellidoUsuario;
        private System.Windows.Forms.Button btnBuscarUsuario;
        private System.Windows.Forms.GroupBox grpbData;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.TextBox txtCedulaUsuario;
        private System.Windows.Forms.GroupBox grpbDatos;
        private System.Windows.Forms.GroupBox grpbOpciones;
        private System.Windows.Forms.Button btnEliminarUsuario;
        private System.Windows.Forms.Button btnModificarUsuario;
        public System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.ComboBox cmbxCargoUsuario;
        private System.Windows.Forms.TextBox txtCargoUsuario;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.DataGridViewTextBoxColumn nacionalidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellido_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn cargo_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn correo_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn tlfn_user;
        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
    }
}