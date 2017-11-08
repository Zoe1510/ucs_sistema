namespace UCS_NODO_FGC
{
    partial class Registrar_area
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
            this.txtNombreArea = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtNombreContactoArea = new System.Windows.Forms.TextBox();
            this.btnGuardarArea = new System.Windows.Forms.Button();
            this.txtTelefonoCliArea = new System.Windows.Forms.TextBox();
            this.txtCorreoCliArea = new System.Windows.Forms.TextBox();
            this.cmbxEmpresa = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNombreEmpresa = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.errorProviderTxtNCli = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCMBxNCli = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderNomArea = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderPersContacto = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderTlfn = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCorreo = new System.Windows.Forms.ErrorProvider(this.components);
            this.Panel_cabecera = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rectangleShape7 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTxtNCli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCMBxNCli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNomArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPersContacto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).BeginInit();
            this.Panel_cabecera.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNombreArea
            // 
            this.txtNombreArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreArea.Location = new System.Drawing.Point(59, 30);
            this.txtNombreArea.MaxLength = 99;
            this.txtNombreArea.Multiline = true;
            this.txtNombreArea.Name = "txtNombreArea";
            this.txtNombreArea.Size = new System.Drawing.Size(310, 30);
            this.txtNombreArea.TabIndex = 1;
            this.txtNombreArea.Text = "Nombre del área";
            this.txtNombreArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreArea.Click += new System.EventHandler(this.txtNombreArea_Click);
            this.txtNombreArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreArea_KeyDown);
            this.txtNombreArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreArea_KeyPress);
            this.txtNombreArea.Leave += new System.EventHandler(this.txtNombreArea_Leave);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Location = new System.Drawing.Point(573, 619);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(147, 47);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtNombreContactoArea
            // 
            this.txtNombreContactoArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreContactoArea.Location = new System.Drawing.Point(59, 31);
            this.txtNombreContactoArea.MaxLength = 99;
            this.txtNombreContactoArea.Multiline = true;
            this.txtNombreContactoArea.Name = "txtNombreContactoArea";
            this.txtNombreContactoArea.Size = new System.Drawing.Size(310, 30);
            this.txtNombreContactoArea.TabIndex = 2;
            this.txtNombreContactoArea.Text = "Persona contacto del área";
            this.txtNombreContactoArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreContactoArea.Click += new System.EventHandler(this.txtNombreContacto_Click);
            this.txtNombreContactoArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreContacto_KeyDown);
            this.txtNombreContactoArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreContacto_KeyPress);
            this.txtNombreContactoArea.Leave += new System.EventHandler(this.txtNombreContacto_Leave);
            // 
            // btnGuardarArea
            // 
            this.btnGuardarArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnGuardarArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarArea.FlatAppearance.BorderColor = System.Drawing.Color.DarkGreen;
            this.btnGuardarArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarArea.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGuardarArea.Location = new System.Drawing.Point(399, 619);
            this.btnGuardarArea.Name = "btnGuardarArea";
            this.btnGuardarArea.Size = new System.Drawing.Size(147, 47);
            this.btnGuardarArea.TabIndex = 5;
            this.btnGuardarArea.Text = "Registrar";
            this.btnGuardarArea.UseVisualStyleBackColor = false;
            this.btnGuardarArea.Click += new System.EventHandler(this.btnGuardarArea_Click);
            // 
            // txtTelefonoCliArea
            // 
            this.txtTelefonoCliArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefonoCliArea.Location = new System.Drawing.Point(59, 93);
            this.txtTelefonoCliArea.MaxLength = 11;
            this.txtTelefonoCliArea.Multiline = true;
            this.txtTelefonoCliArea.Name = "txtTelefonoCliArea";
            this.txtTelefonoCliArea.Size = new System.Drawing.Size(310, 30);
            this.txtTelefonoCliArea.TabIndex = 3;
            this.txtTelefonoCliArea.Text = "Teléfono o celular";
            this.txtTelefonoCliArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTelefonoCliArea.Click += new System.EventHandler(this.txtTelefonoCli_Click);
            this.txtTelefonoCliArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelefonoCli_KeyDown);
            this.txtTelefonoCliArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefonoCli_KeyPress);
            this.txtTelefonoCliArea.Leave += new System.EventHandler(this.txtTelefonoCli_Leave);
            this.txtTelefonoCliArea.Validating += new System.ComponentModel.CancelEventHandler(this.txtTelefonoCliArea_Validating);
            // 
            // txtCorreoCliArea
            // 
            this.txtCorreoCliArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoCliArea.Location = new System.Drawing.Point(59, 158);
            this.txtCorreoCliArea.MaxLength = 255;
            this.txtCorreoCliArea.Multiline = true;
            this.txtCorreoCliArea.Name = "txtCorreoCliArea";
            this.txtCorreoCliArea.Size = new System.Drawing.Size(310, 30);
            this.txtCorreoCliArea.TabIndex = 4;
            this.txtCorreoCliArea.Text = "correo@ejemplo.com";
            this.txtCorreoCliArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCorreoCliArea.Click += new System.EventHandler(this.txtCorreoCli_Click);
            this.txtCorreoCliArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCorreoCli_KeyDown);
            this.txtCorreoCliArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCorreoCliArea_KeyPress);
            this.txtCorreoCliArea.Leave += new System.EventHandler(this.txtCorreoCli_Leave);
            this.txtCorreoCliArea.Validating += new System.ComponentModel.CancelEventHandler(this.txtCorreoCliArea_Validating);
            // 
            // cmbxEmpresa
            // 
            this.cmbxEmpresa.BackColor = System.Drawing.SystemColors.Control;
            this.cmbxEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxEmpresa.FormattingEnabled = true;
            this.cmbxEmpresa.Location = new System.Drawing.Point(59, 35);
            this.cmbxEmpresa.Name = "cmbxEmpresa";
            this.cmbxEmpresa.Size = new System.Drawing.Size(310, 27);
            this.cmbxEmpresa.TabIndex = 0;
            this.cmbxEmpresa.SelectionChangeCommitted += new System.EventHandler(this.cmbxEmpresa_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNombreEmpresa);
            this.groupBox1.Controls.Add(this.cmbxEmpresa);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(351, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 87);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Empresa";
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.Cursor = System.Windows.Forms.Cursors.No;
            this.txtNombreEmpresa.Enabled = false;
            this.txtNombreEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreEmpresa.Location = new System.Drawing.Point(59, 35);
            this.txtNombreEmpresa.MaxLength = 99;
            this.txtNombreEmpresa.Multiline = true;
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.Size = new System.Drawing.Size(310, 30);
            this.txtNombreEmpresa.TabIndex = 17;
            this.txtNombreEmpresa.Text = "Nombre empresa";
            this.txtNombreEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreEmpresa.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNombreArea);
            this.groupBox2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox2.Location = new System.Drawing.Point(351, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 81);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Área";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtNombreContactoArea);
            this.groupBox3.Controls.Add(this.txtCorreoCliArea);
            this.groupBox3.Controls.Add(this.txtTelefonoCliArea);
            this.groupBox3.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox3.Location = new System.Drawing.Point(351, 351);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(427, 214);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Persona";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 714);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1122, 25);
            this.panel8.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label9.Location = new System.Drawing.Point(238, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(663, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Derechos reservados Universidad de Margarita © 2017. Proyecto de Pasantías Elabor" +
    "ado por Br. Zoyla Bermúdez";
            // 
            // errorProviderTxtNCli
            // 
            this.errorProviderTxtNCli.ContainerControl = this;
            // 
            // errorProviderCMBxNCli
            // 
            this.errorProviderCMBxNCli.ContainerControl = this;
            // 
            // errorProviderNomArea
            // 
            this.errorProviderNomArea.ContainerControl = this;
            // 
            // errorProviderPersContacto
            // 
            this.errorProviderPersContacto.ContainerControl = this;
            // 
            // errorProviderTlfn
            // 
            this.errorProviderTlfn.ContainerControl = this;
            // 
            // errorProviderCorreo
            // 
            this.errorProviderCorreo.ContainerControl = this;
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
            this.label1.Location = new System.Drawing.Point(300, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(538, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Registro área de empresas";
            // 
            // rectangleShape7
            // 
            this.rectangleShape7.BorderColor = System.Drawing.Color.MidnightBlue;
            this.rectangleShape7.Location = new System.Drawing.Point(0, 96);
            this.rectangleShape7.Name = "rectangleShape7";
            this.rectangleShape7.Size = new System.Drawing.Size(1119, 1);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape7});
            this.shapeContainer1.Size = new System.Drawing.Size(1122, 739);
            this.shapeContainer1.TabIndex = 46;
            this.shapeContainer1.TabStop = false;
            // 
            // Registrar_area
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1122, 739);
            this.Controls.Add(this.Panel_cabecera);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardarArea);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Registrar_area";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar área";
            this.Load += new System.EventHandler(this.Registrar_area_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTxtNCli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCMBxNCli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNomArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPersContacto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).EndInit();
            this.Panel_cabecera.ResumeLayout(false);
            this.Panel_cabecera.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombreArea;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtNombreContactoArea;
        private System.Windows.Forms.Button btnGuardarArea;
        private System.Windows.Forms.TextBox txtTelefonoCliArea;
        private System.Windows.Forms.TextBox txtCorreoCliArea;
        private System.Windows.Forms.ComboBox cmbxEmpresa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtNombreEmpresa;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ErrorProvider errorProviderTxtNCli;
        private System.Windows.Forms.ErrorProvider errorProviderCMBxNCli;
        private System.Windows.Forms.ErrorProvider errorProviderNomArea;
        private System.Windows.Forms.ErrorProvider errorProviderPersContacto;
        private System.Windows.Forms.ErrorProvider errorProviderTlfn;
        private System.Windows.Forms.ErrorProvider errorProviderCorreo;
        private System.Windows.Forms.Panel Panel_cabecera;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape7;
    }
}