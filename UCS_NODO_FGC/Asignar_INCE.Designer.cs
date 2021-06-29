namespace UCS_NODO_FGC
{
    partial class Asignar_INCE
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbxCurso = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbxFacilitador = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.errorProvidercurso = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderfacilitador = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer4 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvidercurso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderfacilitador)).BeginInit();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbxCurso);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbxFacilitador);
            this.groupBox1.Location = new System.Drawing.Point(31, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 158);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            // 
            // cmbxCurso
            // 
            this.cmbxCurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCurso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxCurso.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxCurso.FormattingEnabled = true;
            this.cmbxCurso.Location = new System.Drawing.Point(182, 35);
            this.cmbxCurso.Name = "cmbxCurso";
            this.cmbxCurso.Size = new System.Drawing.Size(232, 25);
            this.cmbxCurso.TabIndex = 79;
            this.cmbxCurso.SelectionChangeCommitted += new System.EventHandler(this.cmbxCurso_SelectionChangeCommitted);
            this.cmbxCurso.Leave += new System.EventHandler(this.cmbxCurso_Leave);
            this.cmbxCurso.Validating += new System.ComponentModel.CancelEventHandler(this.cmbxCurso_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(84, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 19);
            this.label2.TabIndex = 78;
            this.label2.Text = "Facilitador:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(30, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 19);
            this.label1.TabIndex = 77;
            this.label1.Text = "Nombre del curso:";
            // 
            // cmbxFacilitador
            // 
            this.cmbxFacilitador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxFacilitador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxFacilitador.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxFacilitador.FormattingEnabled = true;
            this.cmbxFacilitador.Location = new System.Drawing.Point(182, 102);
            this.cmbxFacilitador.Name = "cmbxFacilitador";
            this.cmbxFacilitador.Size = new System.Drawing.Size(232, 25);
            this.cmbxFacilitador.TabIndex = 76;
            this.cmbxFacilitador.SelectionChangeCommitted += new System.EventHandler(this.cmbxFacilitador_SelectionChangeCommitted);
            this.cmbxFacilitador.Validating += new System.ComponentModel.CancelEventHandler(this.cmbxFacilitador_Validating);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(37)))), ((int)(((byte)(31)))));
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Location = new System.Drawing.Point(286, 200);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(132, 46);
            this.btnCancelar.TabIndex = 82;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(176)))), ((int)(((byte)(26)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGuardar.Location = new System.Drawing.Point(75, 200);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(132, 46);
            this.btnGuardar.TabIndex = 81;
            this.btnGuardar.Text = "Asignar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // errorProvidercurso
            // 
            this.errorProvidercurso.ContainerControl = this;
            // 
            // errorProviderfacilitador
            // 
            this.errorProviderfacilitador.ContainerControl = this;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.shapeContainer4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 271);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(500, 21);
            this.panel8.TabIndex = 84;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 6.75F);
            this.label9.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label9.Location = new System.Drawing.Point(4, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(493, 15);
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
            this.shapeContainer4.Size = new System.Drawing.Size(500, 21);
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
            // Asignar_INCE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(500, 292);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Asignar_INCE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar facilitador";
            this.Load += new System.EventHandler(this.Asignar_INCE_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvidercurso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderfacilitador)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbxCurso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbxFacilitador;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ErrorProvider errorProvidercurso;
        private System.Windows.Forms.ErrorProvider errorProviderfacilitador;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
    }
}