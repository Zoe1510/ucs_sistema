namespace UCS_NODO_FGC
{
    partial class RPT_HORAS_DEDICADAS
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.R_Formacion_DatosGeneralesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.R_EtapaFormacionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.R_Formacion_DatosGeneralesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.R_EtapaFormacionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsGeneral";
            reportDataSource1.Value = this.R_Formacion_DatosGeneralesBindingSource;
            reportDataSource2.Name = "dsEtapas";
            reportDataSource2.Value = this.R_EtapaFormacionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "UCS_NODO_FGC.Reportes.rptHorasDedicadas.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(789, 754);
            this.reportViewer1.TabIndex = 0;
            // 
            // R_Formacion_DatosGeneralesBindingSource
            // 
            this.R_Formacion_DatosGeneralesBindingSource.DataSource = typeof(UCS_NODO_FGC.Clases.R_Formacion_DatosGenerales);
            // 
            // R_EtapaFormacionBindingSource
            // 
            this.R_EtapaFormacionBindingSource.DataSource = typeof(UCS_NODO_FGC.Clases.R_EtapaFormacion);
            // 
            // RPT_HORAS_DEDICADAS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 754);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RPT_HORAS_DEDICADAS";
            this.Text = "Reporte horas dedicadas";
            this.Load += new System.EventHandler(this.RPT_HORAS_DEDICADAS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.R_Formacion_DatosGeneralesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.R_EtapaFormacionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource R_Formacion_DatosGeneralesBindingSource;
        private System.Windows.Forms.BindingSource R_EtapaFormacionBindingSource;
    }
}