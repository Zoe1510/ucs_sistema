namespace UCS_NODO_FGC
{
    partial class Ver_participantes
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
            this.grpbOpciones = new System.Windows.Forms.GroupBox();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.grpbData = new System.Windows.Forms.GroupBox();
            this.dgvParticipantes = new System.Windows.Forms.DataGridView();
            this.ci_participante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_participante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellido_participante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.correo_par = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlfn_par = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empresa_asociada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.errorProviderCmbxNombre = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCmbxEstatus = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderFecha = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpbOpciones.SuspendLayout();
            this.grpbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipantes)).BeginInit();
            this.panel8.SuspendLayout();
            this.Panel_cabecera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxEstatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFecha)).BeginInit();
            this.SuspendLayout();
            // 
            // grpbOpciones
            // 
            this.grpbOpciones.Controls.Add(this.btnRefrescar);
            this.grpbOpciones.Controls.Add(this.btnModificar);
            this.grpbOpciones.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbOpciones.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbOpciones.Location = new System.Drawing.Point(862, 128);
            this.grpbOpciones.Name = "grpbOpciones";
            this.grpbOpciones.Size = new System.Drawing.Size(247, 181);
            this.grpbOpciones.TabIndex = 59;
            this.grpbOpciones.TabStop = false;
            this.grpbOpciones.Text = "Opciones";
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.SlateBlue;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefrescar.Location = new System.Drawing.Point(24, 40);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(199, 47);
            this.btnRefrescar.TabIndex = 7;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Font = new System.Drawing.Font("Rockwell", 11F, System.Drawing.FontStyle.Bold);
            this.btnModificar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificar.Location = new System.Drawing.Point(24, 108);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(199, 47);
            this.btnModificar.TabIndex = 5;
            this.btnModificar.Text = "Modificar datos participante";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // grpbData
            // 
            this.grpbData.Controls.Add(this.dgvParticipantes);
            this.grpbData.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbData.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpbData.Location = new System.Drawing.Point(22, 128);
            this.grpbData.Name = "grpbData";
            this.grpbData.Size = new System.Drawing.Size(822, 570);
            this.grpbData.TabIndex = 57;
            this.grpbData.TabStop = false;
            this.grpbData.Text = "Participantes registrados";
            // 
            // dgvParticipantes
            // 
            this.dgvParticipantes.AllowUserToAddRows = false;
            this.dgvParticipantes.AllowUserToDeleteRows = false;
            this.dgvParticipantes.AllowUserToResizeColumns = false;
            this.dgvParticipantes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.dgvParticipantes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvParticipantes.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvParticipantes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvParticipantes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvParticipantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParticipantes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ci_participante,
            this.nombre_participante,
            this.apellido_participante,
            this.correo_par,
            this.tlfn_par,
            this.empresa_asociada});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvParticipantes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvParticipantes.Location = new System.Drawing.Point(20, 34);
            this.dgvParticipantes.MultiSelect = false;
            this.dgvParticipantes.Name = "dgvParticipantes";
            this.dgvParticipantes.ReadOnly = true;
            this.dgvParticipantes.RowHeadersVisible = false;
            this.dgvParticipantes.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvParticipantes.Size = new System.Drawing.Size(783, 510);
            this.dgvParticipantes.TabIndex = 0;
            this.dgvParticipantes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvParticipantes_MouseClick);
            // 
            // ci_participante
            // 
            this.ci_participante.HeaderText = "Cédula";
            this.ci_participante.MaxInputLength = 10;
            this.ci_participante.MinimumWidth = 100;
            this.ci_participante.Name = "ci_participante";
            this.ci_participante.ReadOnly = true;
            this.ci_participante.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ci_participante.Width = 120;
            // 
            // nombre_participante
            // 
            this.nombre_participante.HeaderText = "Nombre participante";
            this.nombre_participante.MaxInputLength = 100;
            this.nombre_participante.MinimumWidth = 100;
            this.nombre_participante.Name = "nombre_participante";
            this.nombre_participante.ReadOnly = true;
            this.nombre_participante.Width = 180;
            // 
            // apellido_participante
            // 
            this.apellido_participante.HeaderText = "Apellido";
            this.apellido_participante.MaxInputLength = 100;
            this.apellido_participante.MinimumWidth = 100;
            this.apellido_participante.Name = "apellido_participante";
            this.apellido_participante.ReadOnly = true;
            this.apellido_participante.Width = 150;
            // 
            // correo_par
            // 
            this.correo_par.HeaderText = "Correo";
            this.correo_par.Name = "correo_par";
            this.correo_par.ReadOnly = true;
            // 
            // tlfn_par
            // 
            this.tlfn_par.HeaderText = "Teléfono";
            this.tlfn_par.MaxInputLength = 15;
            this.tlfn_par.Name = "tlfn_par";
            this.tlfn_par.ReadOnly = true;
            // 
            // empresa_asociada
            // 
            this.empresa_asociada.HeaderText = "Empresa asociada";
            this.empresa_asociada.Name = "empresa_asociada";
            this.empresa_asociada.ReadOnly = true;
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
            this.Panel_cabecera.TabIndex = 49;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(395, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Participantes";
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
            // errorProviderCmbxNombre
            // 
            this.errorProviderCmbxNombre.ContainerControl = this;
            // 
            // errorProviderCmbxEstatus
            // 
            this.errorProviderCmbxEstatus.ContainerControl = this;
            // 
            // errorProviderFecha
            // 
            this.errorProviderFecha.ContainerControl = this;
            // 
            // Ver_participantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1121, 739);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.grpbOpciones);
            this.Controls.Add(this.grpbData);
            this.Controls.Add(this.Panel_cabecera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Ver_participantes";
            this.Load += new System.EventHandler(this.Ver_participantes_Load);
            this.grpbOpciones.ResumeLayout(false);
            this.grpbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipantes)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCmbxEstatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFecha)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
        private System.Windows.Forms.GroupBox grpbOpciones;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.GroupBox grpbData;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private System.Windows.Forms.DataGridView dgvParticipantes;
        private System.Windows.Forms.ErrorProvider errorProviderCmbxNombre;
        private System.Windows.Forms.ErrorProvider errorProviderCmbxEstatus;
        private System.Windows.Forms.ErrorProvider errorProviderFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn ci_participante;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_participante;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellido_participante;
        private System.Windows.Forms.DataGridViewTextBoxColumn correo_par;
        private System.Windows.Forms.DataGridViewTextBoxColumn tlfn_par;
        private System.Windows.Forms.DataGridViewTextBoxColumn empresa_asociada;
    }
}