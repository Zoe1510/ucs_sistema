﻿namespace UCS_NODO_FGC
{
    partial class RPT_TIEMPO_TIPO
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.R_Formacion_DatosGeneralesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.R_Formacion_DatosGeneralesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsTodo";
            reportDataSource1.Value = this.R_Formacion_DatosGeneralesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "UCS_NODO_FGC.Reportes.rptRelacionTipoFTiempo.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(789, 750);
            this.reportViewer1.TabIndex = 0;
            // 
            // R_Formacion_DatosGeneralesBindingSource
            // 
            this.R_Formacion_DatosGeneralesBindingSource.DataSource = typeof(UCS_NODO_FGC.Clases.R_Formacion_DatosGenerales);
            // 
            // RPT_TIEMPO_TIPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 750);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RPT_TIEMPO_TIPO";
            this.Text = "REPORTE TIPOS DE FORMACIÓN";
            this.Load += new System.EventHandler(this.RPT_TIEMPO_TIPO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.R_Formacion_DatosGeneralesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource R_Formacion_DatosGeneralesBindingSource;
    }
}