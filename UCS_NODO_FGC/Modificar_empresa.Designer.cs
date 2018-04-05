namespace UCS_NODO_FGC
{
    partial class Modificar_empresa
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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnModificarEmpresa = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbxFee = new System.Windows.Forms.ComboBox();
            this.txtNombreEmpresa = new System.Windows.Forms.TextBox();
            this.errorProviderEmpresa = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderFEE = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFEE)).BeginInit();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Location = new System.Drawing.Point(365, 248);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(147, 46);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnModificarEmpresa
            // 
            this.btnModificarEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnModificarEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarEmpresa.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnModificarEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarEmpresa.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificarEmpresa.Location = new System.Drawing.Point(119, 248);
            this.btnModificarEmpresa.Name = "btnModificarEmpresa";
            this.btnModificarEmpresa.Size = new System.Drawing.Size(147, 46);
            this.btnModificarEmpresa.TabIndex = 18;
            this.btnModificarEmpresa.Text = "Actualizar";
            this.btnModificarEmpresa.UseVisualStyleBackColor = false;
            this.btnModificarEmpresa.Click += new System.EventHandler(this.btnModificarEmpresa_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbxFee);
            this.groupBox1.Controls.Add(this.txtNombreEmpresa);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(41, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 148);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos empresa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(105, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "Recibe FEE:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(18, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nombre de la empresa:";
            // 
            // cmbxFee
            // 
            this.cmbxFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxFee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxFee.FormattingEnabled = true;
            this.cmbxFee.Items.AddRange(new object[] {
            "FEE",
            "NO FEE"});
            this.cmbxFee.Location = new System.Drawing.Point(208, 96);
            this.cmbxFee.Name = "cmbxFee";
            this.cmbxFee.Size = new System.Drawing.Size(310, 27);
            this.cmbxFee.TabIndex = 8;
            this.cmbxFee.SelectionChangeCommitted += new System.EventHandler(this.cmbxFee_SelectionChangeCommitted);
            this.cmbxFee.Validating += new System.ComponentModel.CancelEventHandler(this.cmbxFee_Validating);
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNombreEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreEmpresa.Location = new System.Drawing.Point(208, 34);
            this.txtNombreEmpresa.MaxLength = 99;
            this.txtNombreEmpresa.Multiline = true;
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.Size = new System.Drawing.Size(310, 31);
            this.txtNombreEmpresa.TabIndex = 7;
            this.txtNombreEmpresa.Text = "Nombre empresa ";
            this.txtNombreEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreEmpresa_KeyPress);
            this.txtNombreEmpresa.Validating += new System.ComponentModel.CancelEventHandler(this.txtNombreEmpresa_Validating);
            // 
            // errorProviderEmpresa
            // 
            this.errorProviderEmpresa.ContainerControl = this;
            // 
            // errorProviderFEE
            // 
            this.errorProviderFEE.ContainerControl = this;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.shapeContainer4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 345);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(629, 25);
            this.panel8.TabIndex = 58;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 8.75F);
            this.label9.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label9.Location = new System.Drawing.Point(1, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(628, 18);
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
            this.shapeContainer4.Size = new System.Drawing.Size(629, 25);
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
            // Modificar_empresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(629, 370);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnModificarEmpresa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Modificar_empresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificar datos de empresa";
            this.Load += new System.EventHandler(this.Modificar_empresa_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFEE)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnModificarEmpresa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNombreEmpresa;
        private System.Windows.Forms.ComboBox cmbxFee;
        private System.Windows.Forms.ErrorProvider errorProviderEmpresa;
        private System.Windows.Forms.ErrorProvider errorProviderFEE;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}