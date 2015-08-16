namespace FrbaCommerce.Gestion_de_Preguntas
{
    partial class GestionDePreguntas
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
            this.btnRespPreg = new System.Windows.Forms.Button();
            this.btnVerResp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRespPreg
            // 
            this.btnRespPreg.Location = new System.Drawing.Point(65, 32);
            this.btnRespPreg.Name = "btnRespPreg";
            this.btnRespPreg.Size = new System.Drawing.Size(149, 23);
            this.btnRespPreg.TabIndex = 0;
            this.btnRespPreg.Text = "Responder preguntas";
            this.btnRespPreg.UseVisualStyleBackColor = true;
            this.btnRespPreg.Click += new System.EventHandler(this.btnRespPreg_Click);
            // 
            // btnVerResp
            // 
            this.btnVerResp.Location = new System.Drawing.Point(65, 83);
            this.btnVerResp.Name = "btnVerResp";
            this.btnVerResp.Size = new System.Drawing.Size(149, 22);
            this.btnVerResp.TabIndex = 1;
            this.btnVerResp.Text = "Ver respuestas";
            this.btnVerResp.UseVisualStyleBackColor = true;
            this.btnVerResp.Click += new System.EventHandler(this.btnVerResp_Click);
            // 
            // GestionDePreguntas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 135);
            this.Controls.Add(this.btnVerResp);
            this.Controls.Add(this.btnRespPreg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GestionDePreguntas";
            this.Text = "Gestión de Preguntas";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRespPreg;
        private System.Windows.Forms.Button btnVerResp;
    }
}