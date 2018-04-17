namespace UCS_NODO_FGC
{
    partial class Inicio_Sesion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio_Sesion));
            this.btn_cerrar = new System.Windows.Forms.Button();
            this.btn_minimizar = new System.Windows.Forms.Button();
            this.lblOlvidarContraseña = new System.Windows.Forms.Label();
            this.Txt_id_user = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.Txt_pass_user = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.BackColor = System.Drawing.Color.Transparent;
            this.btn_cerrar.BackgroundImage = global::UCS_NODO_FGC.Properties.Resources.icon_close;
            this.btn_cerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cerrar.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btn_cerrar.FlatAppearance.BorderSize = 0;
            this.btn_cerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_cerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btn_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cerrar.Location = new System.Drawing.Point(1320, 3);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(25, 21);
            this.btn_cerrar.TabIndex = 3;
            this.btn_cerrar.UseVisualStyleBackColor = false;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // btn_minimizar
            // 
            this.btn_minimizar.BackColor = System.Drawing.Color.Transparent;
            this.btn_minimizar.BackgroundImage = global::UCS_NODO_FGC.Properties.Resources.icon_minimizar;
            this.btn_minimizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_minimizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(40)))), ((int)(((byte)(90)))));
            this.btn_minimizar.FlatAppearance.BorderSize = 0;
            this.btn_minimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_minimizar.Location = new System.Drawing.Point(1283, 3);
            this.btn_minimizar.Name = "btn_minimizar";
            this.btn_minimizar.Size = new System.Drawing.Size(31, 21);
            this.btn_minimizar.TabIndex = 3;
            this.btn_minimizar.UseVisualStyleBackColor = false;
            this.btn_minimizar.Click += new System.EventHandler(this.btn_minimizar_Click);
            // 
            // lblOlvidarContraseña
            // 
            this.lblOlvidarContraseña.AutoSize = true;
            this.lblOlvidarContraseña.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(88)))), ((int)(((byte)(105)))));
            this.lblOlvidarContraseña.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblOlvidarContraseña.Font = new System.Drawing.Font("Rockwell", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOlvidarContraseña.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblOlvidarContraseña.Location = new System.Drawing.Point(540, 561);
            this.lblOlvidarContraseña.Name = "lblOlvidarContraseña";
            this.lblOlvidarContraseña.Size = new System.Drawing.Size(283, 23);
            this.lblOlvidarContraseña.TabIndex = 3;
            this.lblOlvidarContraseña.Text = "¿Ha olvidado su contraseña?";
            this.lblOlvidarContraseña.Click += new System.EventHandler(this.lblOlvidarContraseña_Click);
            // 
            // Txt_id_user
            // 
            this.Txt_id_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(214)))), ((int)(((byte)(218)))));
            this.Txt_id_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Txt_id_user.Font = new System.Drawing.Font("Rockwell", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_id_user.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Txt_id_user.Location = new System.Drawing.Point(617, 297);
            this.Txt_id_user.MaxLength = 8;
            this.Txt_id_user.Name = "Txt_id_user";
            this.Txt_id_user.Size = new System.Drawing.Size(173, 23);
            this.Txt_id_user.TabIndex = 0;
            this.Txt_id_user.Text = "Cédula";
            this.Txt_id_user.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Txt_id_user.Click += new System.EventHandler(this.Txt_id_user_Click);
            this.Txt_id_user.TextChanged += new System.EventHandler(this.Txt_id_user_TextChanged);
            this.Txt_id_user.Enter += new System.EventHandler(this.Txt_id_user_Enter);
            this.Txt_id_user.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_id_user_KeyDown);
            this.Txt_id_user.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_id_user_KeyPress);
            this.Txt_id_user.Leave += new System.EventHandler(this.Txt_id_user_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(144)))), ((int)(((byte)(201)))));
            this.panel1.Controls.Add(this.btn_cerrar);
            this.panel1.Controls.Add(this.btn_minimizar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1378, 25);
            this.panel1.TabIndex = 6;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(182)))), ((int)(((byte)(36)))));
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(170)))), ((int)(((byte)(31)))));
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(163)))), ((int)(((byte)(36)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Rockwell", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(567, 448);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(232, 59);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "ACEPTAR";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // Txt_pass_user
            // 
            this.Txt_pass_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(214)))), ((int)(((byte)(218)))));
            this.Txt_pass_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Txt_pass_user.Font = new System.Drawing.Font("Rockwell", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_pass_user.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Txt_pass_user.Location = new System.Drawing.Point(617, 374);
            this.Txt_pass_user.MaxLength = 250;
            this.Txt_pass_user.Name = "Txt_pass_user";
            this.Txt_pass_user.Size = new System.Drawing.Size(173, 23);
            this.Txt_pass_user.TabIndex = 1;
            this.Txt_pass_user.Text = "Contraseña";
            this.Txt_pass_user.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Txt_pass_user.Click += new System.EventHandler(this.Txt_pass_user_Click);
            this.Txt_pass_user.Enter += new System.EventHandler(this.Txt_pass_user_Enter);
            this.Txt_pass_user.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_pass_user_KeyDown);
            this.Txt_pass_user.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_pass_user_KeyPress);
            this.Txt_pass_user.Leave += new System.EventHandler(this.Txt_pass_user_Leave);
            // 
            // Inicio_Sesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BackgroundImage = global::UCS_NODO_FGC.Properties.Resources.background_login;
            this.ClientSize = new System.Drawing.Size(1378, 780);
            this.Controls.Add(this.Txt_pass_user);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Txt_id_user);
            this.Controls.Add(this.lblOlvidarContraseña);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Inicio_Sesion";
            this.Text = "Iniciar sesión";
            this.Load += new System.EventHandler(this.Inicio_Sesion_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_cerrar;
        private System.Windows.Forms.Button btn_minimizar;
        private System.Windows.Forms.Label lblOlvidarContraseña;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox Txt_id_user;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox Txt_pass_user;
    }
}