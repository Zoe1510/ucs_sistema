namespace UCS_NODO_FGC
{
    partial class aaa
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvMediosDifusion = new System.Windows.Forms.DataGridView();
            this.opcion_refrigerio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seleccionar_opcion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMediosDifusion)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvMediosDifusion);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(140, 177);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(643, 282);
            this.groupBox6.TabIndex = 48;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Refrigerios";
            // 
            // dgvMediosDifusion
            // 
            this.dgvMediosDifusion.AllowUserToAddRows = false;
            this.dgvMediosDifusion.AllowUserToDeleteRows = false;
            this.dgvMediosDifusion.AllowUserToResizeRows = false;
            this.dgvMediosDifusion.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvMediosDifusion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMediosDifusion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMediosDifusion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMediosDifusion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.opcion_refrigerio,
            this.seleccionar_opcion});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMediosDifusion.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMediosDifusion.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvMediosDifusion.Location = new System.Drawing.Point(32, 39);
            this.dgvMediosDifusion.Name = "dgvMediosDifusion";
            this.dgvMediosDifusion.RowHeadersVisible = false;
            this.dgvMediosDifusion.Size = new System.Drawing.Size(583, 213);
            this.dgvMediosDifusion.TabIndex = 48;
            // 
            // opcion_refrigerio
            // 
            this.opcion_refrigerio.HeaderText = "Opción refrigerio";
            this.opcion_refrigerio.MaxInputLength = 250;
            this.opcion_refrigerio.MinimumWidth = 100;
            this.opcion_refrigerio.Name = "opcion_refrigerio";
            this.opcion_refrigerio.Width = 530;
            // 
            // seleccionar_opcion
            // 
            this.seleccionar_opcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.seleccionar_opcion.HeaderText = "";
            this.seleccionar_opcion.MinimumWidth = 50;
            this.seleccionar_opcion.Name = "seleccionar_opcion";
            this.seleccionar_opcion.Width = 50;
            // 
            // aaa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(922, 637);
            this.Controls.Add(this.groupBox6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "aaa";
            this.Text = "aaa";
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMediosDifusion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvMediosDifusion;
        private System.Windows.Forms.DataGridViewTextBoxColumn opcion_refrigerio;
        private System.Windows.Forms.DataGridViewCheckBoxColumn seleccionar_opcion;
    }
}