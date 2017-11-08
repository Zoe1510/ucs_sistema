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
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnModificarEmpresa = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbxFee = new System.Windows.Forms.ComboBox();
            this.txtNombreEmpresa = new System.Windows.Forms.TextBox();
            this.errorProviderEmpresa = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderFEE = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFEE)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 341);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(572, 22);
            this.panel5.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F);
            this.label1.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(572, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Derechos reservados Universidad de Margarita © 2017. Proyecto de Pasantías Elabor" +
    "ado por Br. Zoyla Bermúdez";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Location = new System.Drawing.Point(307, 248);
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
            this.btnModificarEmpresa.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBox1.Controls.Add(this.cmbxFee);
            this.groupBox1.Controls.Add(this.txtNombreEmpresa);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(98, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 148);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos empresa";
            // 
            // cmbxFee
            // 
            this.cmbxFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxFee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxFee.FormattingEnabled = true;
            this.cmbxFee.Items.AddRange(new object[] {
            "FEE",
            "NO FEE"});
            this.cmbxFee.Location = new System.Drawing.Point(33, 89);
            this.cmbxFee.Name = "cmbxFee";
            this.cmbxFee.Size = new System.Drawing.Size(310, 24);
            this.cmbxFee.TabIndex = 8;
            this.cmbxFee.SelectionChangeCommitted += new System.EventHandler(this.cmbxFee_SelectionChangeCommitted);
            this.cmbxFee.Validating += new System.ComponentModel.CancelEventHandler(this.cmbxFee_Validating);
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNombreEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreEmpresa.Location = new System.Drawing.Point(33, 31);
            this.txtNombreEmpresa.MaxLength = 99;
            this.txtNombreEmpresa.Multiline = true;
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.Size = new System.Drawing.Size(310, 34);
            this.txtNombreEmpresa.TabIndex = 7;
            this.txtNombreEmpresa.Text = "Nombre empresa ";
            this.txtNombreEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // Modificar_empresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(572, 363);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnModificarEmpresa);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Modificar_empresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificar datos de empresa";
            this.Load += new System.EventHandler(this.Modificar_empresa_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderFEE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnModificarEmpresa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNombreEmpresa;
        private System.Windows.Forms.ComboBox cmbxFee;
        private System.Windows.Forms.ErrorProvider errorProviderEmpresa;
        private System.Windows.Forms.ErrorProvider errorProviderFEE;
    }
}