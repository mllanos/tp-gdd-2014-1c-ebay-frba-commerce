namespace FrbaCommerce.Abm_Empresa
{
    partial class MenuABMEmpresa
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
            this.btnCrearE = new System.Windows.Forms.Button();
            this.btnModE = new System.Windows.Forms.Button();
            this.btnEliminarE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCrearE
            // 
            this.btnCrearE.Location = new System.Drawing.Point(75, 29);
            this.btnCrearE.Name = "btnCrearE";
            this.btnCrearE.Size = new System.Drawing.Size(115, 23);
            this.btnCrearE.TabIndex = 0;
            this.btnCrearE.Text = "Crear Empresa";
            this.btnCrearE.UseVisualStyleBackColor = true;
            this.btnCrearE.Click += new System.EventHandler(this.btnCrearE_Click);
            // 
            // btnModE
            // 
            this.btnModE.Location = new System.Drawing.Point(75, 74);
            this.btnModE.Name = "btnModE";
            this.btnModE.Size = new System.Drawing.Size(115, 23);
            this.btnModE.TabIndex = 1;
            this.btnModE.Text = "Modificar Empresa";
            this.btnModE.UseVisualStyleBackColor = true;
            this.btnModE.Click += new System.EventHandler(this.btnModE_Click);
            // 
            // btnEliminarE
            // 
            this.btnEliminarE.Location = new System.Drawing.Point(75, 125);
            this.btnEliminarE.Name = "btnEliminarE";
            this.btnEliminarE.Size = new System.Drawing.Size(115, 23);
            this.btnEliminarE.TabIndex = 2;
            this.btnEliminarE.Text = "Eliminar Empresa";
            this.btnEliminarE.UseVisualStyleBackColor = true;
            this.btnEliminarE.Click += new System.EventHandler(this.btnEliminarE_Click);
            // 
            // MenuABMEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 173);
            this.Controls.Add(this.btnEliminarE);
            this.Controls.Add(this.btnModE);
            this.Controls.Add(this.btnCrearE);
            this.Name = "MenuABMEmpresa";
            this.Text = "Menu ABM Empresa";
            this.Load += new System.EventHandler(this.MenuABMEmpresa_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCrearE;
        private System.Windows.Forms.Button btnModE;
        private System.Windows.Forms.Button btnEliminarE;
    }
}