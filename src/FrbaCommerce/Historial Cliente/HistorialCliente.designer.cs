namespace FrbaCommerce.Historial_Cliente
{
    partial class HistorialCliente
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
            this.btnHCompras = new System.Windows.Forms.Button();
            this.btnHOfertas = new System.Windows.Forms.Button();
            this.btnHCalificaciones = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHCompras
            // 
            this.btnHCompras.Location = new System.Drawing.Point(34, 29);
            this.btnHCompras.Name = "btnHCompras";
            this.btnHCompras.Size = new System.Drawing.Size(138, 23);
            this.btnHCompras.TabIndex = 0;
            this.btnHCompras.Text = "Historial de Compras";
            this.btnHCompras.UseVisualStyleBackColor = true;
            this.btnHCompras.Click += new System.EventHandler(this.btnHCompras_Click);
            // 
            // btnHOfertas
            // 
            this.btnHOfertas.Location = new System.Drawing.Point(34, 67);
            this.btnHOfertas.Name = "btnHOfertas";
            this.btnHOfertas.Size = new System.Drawing.Size(138, 23);
            this.btnHOfertas.TabIndex = 1;
            this.btnHOfertas.Text = "Historial de Ofertas";
            this.btnHOfertas.UseVisualStyleBackColor = true;
            this.btnHOfertas.Click += new System.EventHandler(this.btnHOfertas_Click);
            // 
            // btnHCalificaciones
            // 
            this.btnHCalificaciones.Location = new System.Drawing.Point(34, 105);
            this.btnHCalificaciones.Name = "btnHCalificaciones";
            this.btnHCalificaciones.Size = new System.Drawing.Size(138, 23);
            this.btnHCalificaciones.TabIndex = 2;
            this.btnHCalificaciones.Text = "Hist. Calificaciones";
            this.btnHCalificaciones.UseVisualStyleBackColor = true;
            this.btnHCalificaciones.Click += new System.EventHandler(this.btnHCalificaciones_Click);
            // 
            // HistorialCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 173);
            this.Controls.Add(this.btnHCalificaciones);
            this.Controls.Add(this.btnHOfertas);
            this.Controls.Add(this.btnHCompras);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "HistorialCliente";
            this.Text = "Historial de Cliente";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHCompras;
        private System.Windows.Forms.Button btnHOfertas;
        private System.Windows.Forms.Button btnHCalificaciones;


    }
}