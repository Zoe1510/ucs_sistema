namespace UCS_NODO_FGC
{
    partial class Modificar_contacto_area
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
            this.btnModificarContacto = new System.Windows.Forms.Button();
            this.txtTelefonoCliArea = new System.Windows.Forms.TextBox();
            this.txtCorreoCliArea = new System.Windows.Forms.TextBox();
            this.txtNombreEmpresa = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProviderNCliente = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderNArea = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderNContacto = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderTlfn = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCorreo = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNContacto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombreArea
            // 
            this.txtNombreArea.Cursor = System.Windows.Forms.Cursors.No;
            this.txtNombreArea.Enabled = false;
            this.txtNombreArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreArea.Location = new System.Drawing.Point(33, 84);
            this.txtNombreArea.MaxLength = 99;
            this.txtNombreArea.Multiline = true;
            this.txtNombreArea.Name = "txtNombreArea";
            this.txtNombreArea.Size = new System.Drawing.Size(310, 34);
            this.txtNombreArea.TabIndex = 8;
            this.txtNombreArea.Text = "Nombre del área";
            this.txtNombreArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Location = new System.Drawing.Point(366, 456);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(147, 46);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtNombreContactoArea
            // 
            this.txtNombreContactoArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreContactoArea.Location = new System.Drawing.Point(33, 32);
            this.txtNombreContactoArea.MaxLength = 99;
            this.txtNombreContactoArea.Multiline = true;
            this.txtNombreContactoArea.Name = "txtNombreContactoArea";
            this.txtNombreContactoArea.Size = new System.Drawing.Size(310, 34);
            this.txtNombreContactoArea.TabIndex = 9;
            this.txtNombreContactoArea.Text = "Persona contacto del área";
            this.txtNombreContactoArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreContactoArea.Click += new System.EventHandler(this.txtNombreContacto_Click);
            this.txtNombreContactoArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreContacto_KeyDown);
            this.txtNombreContactoArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreContacto_KeyPress);
            this.txtNombreContactoArea.Leave += new System.EventHandler(this.txtNombreContacto_Leave);
            // 
            // btnModificarContacto
            // 
            this.btnModificarContacto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnModificarContacto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarContacto.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnModificarContacto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarContacto.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarContacto.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificarContacto.Location = new System.Drawing.Point(192, 456);
            this.btnModificarContacto.Name = "btnModificarContacto";
            this.btnModificarContacto.Size = new System.Drawing.Size(147, 46);
            this.btnModificarContacto.TabIndex = 12;
            this.btnModificarContacto.Text = "Actualizar";
            this.btnModificarContacto.UseVisualStyleBackColor = false;
            this.btnModificarContacto.Click += new System.EventHandler(this.btnModificarContacto_Click);
            // 
            // txtTelefonoCliArea
            // 
            this.txtTelefonoCliArea.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefonoCliArea.Location = new System.Drawing.Point(33, 90);
            this.txtTelefonoCliArea.MaxLength = 11;
            this.txtTelefonoCliArea.Multiline = true;
            this.txtTelefonoCliArea.Name = "txtTelefonoCliArea";
            this.txtTelefonoCliArea.Size = new System.Drawing.Size(310, 34);
            this.txtTelefonoCliArea.TabIndex = 10;
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
            this.txtCorreoCliArea.Location = new System.Drawing.Point(33, 151);
            this.txtCorreoCliArea.MaxLength = 255;
            this.txtCorreoCliArea.Multiline = true;
            this.txtCorreoCliArea.Name = "txtCorreoCliArea";
            this.txtCorreoCliArea.Size = new System.Drawing.Size(310, 34);
            this.txtCorreoCliArea.TabIndex = 11;
            this.txtCorreoCliArea.Text = "correo@ejemplo.com";
            this.txtCorreoCliArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCorreoCliArea.Click += new System.EventHandler(this.txtCorreoCli_Click);
            this.txtCorreoCliArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCorreoCli_KeyDown);
            this.txtCorreoCliArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCorreoCliArea_KeyPress);
            this.txtCorreoCliArea.Leave += new System.EventHandler(this.txtCorreoCli_Leave);
            this.txtCorreoCliArea.Validating += new System.ComponentModel.CancelEventHandler(this.txtCorreoCliArea_Validating);
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.Cursor = System.Windows.Forms.Cursors.No;
            this.txtNombreEmpresa.Enabled = false;
            this.txtNombreEmpresa.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreEmpresa.Location = new System.Drawing.Point(33, 31);
            this.txtNombreEmpresa.MaxLength = 99;
            this.txtNombreEmpresa.Multiline = true;
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.Size = new System.Drawing.Size(310, 34);
            this.txtNombreEmpresa.TabIndex = 7;
            this.txtNombreEmpresa.Text = "Nombre empresa ";
            this.txtNombreEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNombreEmpresa);
            this.groupBox1.Controls.Add(this.txtNombreArea);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(160, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 145);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos empresa";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNombreContactoArea);
            this.groupBox2.Controls.Add(this.txtTelefonoCliArea);
            this.groupBox2.Controls.Add(this.txtCorreoCliArea);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox2.Location = new System.Drawing.Point(160, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 213);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos persona contacto";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 556);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(695, 25);
            this.panel5.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label1.Location = new System.Drawing.Point(14, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(663, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Derechos reservados Universidad de Margarita © 2017. Proyecto de Pasantías Elabor" +
    "ado por Br. Zoyla Bermúdez";
            // 
            // errorProviderNCliente
            // 
            this.errorProviderNCliente.ContainerControl = this;
            // 
            // errorProviderNArea
            // 
            this.errorProviderNArea.ContainerControl = this;
            // 
            // errorProviderNContacto
            // 
            this.errorProviderNContacto.ContainerControl = this;
            // 
            // errorProviderTlfn
            // 
            this.errorProviderTlfn.ContainerControl = this;
            // 
            // errorProviderCorreo
            // 
            this.errorProviderCorreo.ContainerControl = this;
            // 
            // Modificar_contacto_area
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(695, 581);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnModificarContacto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Modificar_contacto_area";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modificar contacto de área";
            this.Load += new System.EventHandler(this.Modificar_contacto_area_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNContacto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombreArea;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtNombreContactoArea;
        private System.Windows.Forms.Button btnModificarContacto;
        private System.Windows.Forms.TextBox txtTelefonoCliArea;
        private System.Windows.Forms.TextBox txtCorreoCliArea;
        private System.Windows.Forms.TextBox txtNombreEmpresa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProviderNCliente;
        private System.Windows.Forms.ErrorProvider errorProviderNArea;
        private System.Windows.Forms.ErrorProvider errorProviderNContacto;
        private System.Windows.Forms.ErrorProvider errorProviderTlfn;
        private System.Windows.Forms.ErrorProvider errorProviderCorreo;
    }
}