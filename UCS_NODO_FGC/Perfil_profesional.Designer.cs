namespace UCS_NODO_FGC
{
    partial class Perfil_profesional
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Perfil_profesional));
            this.txtNuevoPass = new System.Windows.Forms.TextBox();
            this.txtConfirPass = new System.Windows.Forms.TextBox();
            this.txtNombreUser = new System.Windows.Forms.TextBox();
            this.txtApellidoUser = new System.Windows.Forms.TextBox();
            this.txtTlfnUser = new System.Windows.Forms.TextBox();
            this.txtCorreoUser = new System.Windows.Forms.TextBox();
            this.btnActualizarDatos = new System.Windows.Forms.Button();
            this.lblNombreImg = new System.Windows.Forms.Label();
            this.lblCargo = new System.Windows.Forms.Label();
            this.lblCedula = new System.Windows.Forms.Label();
            this.btnCambioPic = new System.Windows.Forms.Button();
            this.picFotoUser = new System.Windows.Forms.PictureBox();
            this.lblInfoPic = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMensajeErrorPass = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.errorProviderNombre = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderApellido = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderCorreo = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderTlfn = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picFotoUser)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderApellido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNuevoPass
            // 
            this.txtNuevoPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtNuevoPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNuevoPass.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNuevoPass.Location = new System.Drawing.Point(670, 493);
            this.txtNuevoPass.Name = "txtNuevoPass";
            this.txtNuevoPass.Size = new System.Drawing.Size(167, 19);
            this.txtNuevoPass.TabIndex = 0;
            this.txtNuevoPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNuevoPass.Enter += new System.EventHandler(this.txtNuevoPass_Enter);
            // 
            // txtConfirPass
            // 
            this.txtConfirPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtConfirPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfirPass.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirPass.Location = new System.Drawing.Point(670, 552);
            this.txtConfirPass.Name = "txtConfirPass";
            this.txtConfirPass.Size = new System.Drawing.Size(167, 19);
            this.txtConfirPass.TabIndex = 1;
            this.txtConfirPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConfirPass.TextChanged += new System.EventHandler(this.txtConfirPass_TextChanged);
            this.txtConfirPass.Enter += new System.EventHandler(this.txtConfirPass_Enter);
            this.txtConfirPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConfirPass_KeyPress);
            // 
            // txtNombreUser
            // 
            this.txtNombreUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtNombreUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombreUser.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreUser.Location = new System.Drawing.Point(644, 119);
            this.txtNombreUser.Multiline = true;
            this.txtNombreUser.Name = "txtNombreUser";
            this.txtNombreUser.Size = new System.Drawing.Size(181, 26);
            this.txtNombreUser.TabIndex = 3;
            this.txtNombreUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreUser_KeyPress);
            // 
            // txtApellidoUser
            // 
            this.txtApellidoUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtApellidoUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtApellidoUser.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidoUser.Location = new System.Drawing.Point(644, 168);
            this.txtApellidoUser.Multiline = true;
            this.txtApellidoUser.Name = "txtApellidoUser";
            this.txtApellidoUser.Size = new System.Drawing.Size(181, 26);
            this.txtApellidoUser.TabIndex = 4;
            this.txtApellidoUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtApellidoUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApellidoUser_KeyPress);
            // 
            // txtTlfnUser
            // 
            this.txtTlfnUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtTlfnUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTlfnUser.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTlfnUser.Location = new System.Drawing.Point(644, 265);
            this.txtTlfnUser.MaxLength = 11;
            this.txtTlfnUser.Multiline = true;
            this.txtTlfnUser.Name = "txtTlfnUser";
            this.txtTlfnUser.Size = new System.Drawing.Size(181, 26);
            this.txtTlfnUser.TabIndex = 6;
            this.txtTlfnUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTlfnUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTlfnUser_KeyPress);
            this.txtTlfnUser.Validating += new System.ComponentModel.CancelEventHandler(this.txtTlfnUser_Validating);
            // 
            // txtCorreoUser
            // 
            this.txtCorreoUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtCorreoUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCorreoUser.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoUser.Location = new System.Drawing.Point(644, 216);
            this.txtCorreoUser.Multiline = true;
            this.txtCorreoUser.Name = "txtCorreoUser";
            this.txtCorreoUser.Size = new System.Drawing.Size(181, 26);
            this.txtCorreoUser.TabIndex = 5;
            this.txtCorreoUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCorreoUser.Validating += new System.ComponentModel.CancelEventHandler(this.txtCorreoUser_Validating);
            // 
            // btnActualizarDatos
            // 
            this.btnActualizarDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(182)))), ((int)(((byte)(36)))));
            this.btnActualizarDatos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(170)))), ((int)(((byte)(31)))));
            this.btnActualizarDatos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(170)))), ((int)(((byte)(31)))));
            this.btnActualizarDatos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(163)))), ((int)(((byte)(36)))));
            this.btnActualizarDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarDatos.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarDatos.ForeColor = System.Drawing.Color.White;
            this.btnActualizarDatos.Location = new System.Drawing.Point(570, 646);
            this.btnActualizarDatos.Name = "btnActualizarDatos";
            this.btnActualizarDatos.Size = new System.Drawing.Size(267, 44);
            this.btnActualizarDatos.TabIndex = 2;
            this.btnActualizarDatos.Text = "Siguiente";
            this.btnActualizarDatos.UseVisualStyleBackColor = false;
            this.btnActualizarDatos.Click += new System.EventHandler(this.btnActualizarDatos_Click);
            // 
            // lblNombreImg
            // 
            this.lblNombreImg.AutoSize = true;
            this.lblNombreImg.BackColor = System.Drawing.Color.Transparent;
            this.lblNombreImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNombreImg.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreImg.Location = new System.Drawing.Point(623, 71);
            this.lblNombreImg.MaximumSize = new System.Drawing.Size(170, 0);
            this.lblNombreImg.Name = "lblNombreImg";
            this.lblNombreImg.Size = new System.Drawing.Size(139, 14);
            this.lblNombreImg.TabIndex = 7;
            this.lblNombreImg.Text = "Img-predeterminada.png";
            // 
            // lblCargo
            // 
            this.lblCargo.AutoSize = true;
            this.lblCargo.BackColor = System.Drawing.Color.Transparent;
            this.lblCargo.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCargo.Location = new System.Drawing.Point(689, 370);
            this.lblCargo.Name = "lblCargo";
            this.lblCargo.Size = new System.Drawing.Size(0, 19);
            this.lblCargo.TabIndex = 8;
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.BackColor = System.Drawing.Color.Transparent;
            this.lblCedula.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCedula.Location = new System.Drawing.Point(689, 431);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(0, 19);
            this.lblCedula.TabIndex = 9;
            // 
            // btnCambioPic
            // 
            this.btnCambioPic.BackColor = System.Drawing.Color.Transparent;
            this.btnCambioPic.BackgroundImage = global::UCS_NODO_FGC.Properties.Resources.icon_camara;
            this.btnCambioPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCambioPic.Location = new System.Drawing.Point(799, 64);
            this.btnCambioPic.Name = "btnCambioPic";
            this.btnCambioPic.Size = new System.Drawing.Size(33, 29);
            this.btnCambioPic.TabIndex = 10;
            this.btnCambioPic.UseVisualStyleBackColor = false;
            this.btnCambioPic.Click += new System.EventHandler(this.btnCambioPic_Click);
            // 
            // picFotoUser
            // 
            this.picFotoUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picFotoUser.Location = new System.Drawing.Point(49, 19);
            this.picFotoUser.Name = "picFotoUser";
            this.picFotoUser.Size = new System.Drawing.Size(129, 114);
            this.picFotoUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFotoUser.TabIndex = 11;
            this.picFotoUser.TabStop = false;
            // 
            // lblInfoPic
            // 
            this.lblInfoPic.AutoSize = true;
            this.lblInfoPic.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoPic.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoPic.Location = new System.Drawing.Point(75, 136);
            this.lblInfoPic.Name = "lblInfoPic";
            this.lblInfoPic.Size = new System.Drawing.Size(71, 16);
            this.lblInfoPic.TabIndex = 12;
            this.lblInfoPic.Text = "Foto actual";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lblInfoPic);
            this.groupBox1.Controls.Add(this.picFotoUser);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(268, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 162);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // lblMensajeErrorPass
            // 
            this.lblMensajeErrorPass.AutoSize = true;
            this.lblMensajeErrorPass.BackColor = System.Drawing.Color.Transparent;
            this.lblMensajeErrorPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeErrorPass.ForeColor = System.Drawing.Color.Red;
            this.lblMensajeErrorPass.Location = new System.Drawing.Point(520, 605);
            this.lblMensajeErrorPass.Name = "lblMensajeErrorPass";
            this.lblMensajeErrorPass.Size = new System.Drawing.Size(353, 16);
            this.lblMensajeErrorPass.TabIndex = 14;
            this.lblMensajeErrorPass.Text = "Las contraseñas no coinciden. Inténtelo de nuevo.";
            this.lblMensajeErrorPass.Visible = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.Controls.Add(this.label9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 704);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1286, 25);
            this.panel8.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label9.Location = new System.Drawing.Point(325, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(663, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Derechos reservados Universidad de Margarita © 2017. Proyecto de Pasantías Elabor" +
    "ado por Br. Zoyla Bermúdez";
            // 
            // errorProviderNombre
            // 
            this.errorProviderNombre.ContainerControl = this;
            // 
            // errorProviderApellido
            // 
            this.errorProviderApellido.ContainerControl = this;
            // 
            // errorProviderCorreo
            // 
            this.errorProviderCorreo.ContainerControl = this;
            // 
            // errorProviderTlfn
            // 
            this.errorProviderTlfn.ContainerControl = this;
            // 
            // Perfil_profesional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1286, 729);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.lblMensajeErrorPass);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCambioPic);
            this.Controls.Add(this.lblCedula);
            this.Controls.Add(this.lblCargo);
            this.Controls.Add(this.lblNombreImg);
            this.Controls.Add(this.btnActualizarDatos);
            this.Controls.Add(this.txtCorreoUser);
            this.Controls.Add(this.txtTlfnUser);
            this.Controls.Add(this.txtApellidoUser);
            this.Controls.Add(this.txtNombreUser);
            this.Controls.Add(this.txtConfirPass);
            this.Controls.Add(this.txtNuevoPass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Perfil_profesional";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.Perfil_profesional_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFotoUser)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderApellido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCorreo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTlfn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNuevoPass;
        private System.Windows.Forms.TextBox txtConfirPass;
        private System.Windows.Forms.TextBox txtNombreUser;
        private System.Windows.Forms.TextBox txtApellidoUser;
        private System.Windows.Forms.TextBox txtTlfnUser;
        private System.Windows.Forms.TextBox txtCorreoUser;
        private System.Windows.Forms.Button btnActualizarDatos;
        private System.Windows.Forms.Label lblNombreImg;
        private System.Windows.Forms.Label lblCargo;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.Button btnCambioPic;
        public System.Windows.Forms.PictureBox picFotoUser;
        private System.Windows.Forms.Label lblInfoPic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMensajeErrorPass;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ErrorProvider errorProviderNombre;
        private System.Windows.Forms.ErrorProvider errorProviderApellido;
        private System.Windows.Forms.ErrorProvider errorProviderCorreo;
        private System.Windows.Forms.ErrorProvider errorProviderTlfn;
    }
}