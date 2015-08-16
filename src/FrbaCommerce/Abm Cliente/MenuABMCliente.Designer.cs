namespace FrbaCommerce.Abm_Cliente
{
    partial class MenuABMCliente
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
            this.btmCrearCli = new System.Windows.Forms.Button();
            this.btnModCli = new System.Windows.Forms.Button();
            this.btnEliminarCli = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btmCrearCli
            // 
            this.btmCrearCli.Location = new System.Drawing.Point(80, 27);
            this.btmCrearCli.Name = "btmCrearCli";
            this.btmCrearCli.Size = new System.Drawing.Size(108, 23);
            this.btmCrearCli.TabIndex = 0;
            this.btmCrearCli.Text = "Crear cliente";
            this.btmCrearCli.UseVisualStyleBackColor = true;
            this.btmCrearCli.Click += new System.EventHandler(this.btmCrearCli_Click);
            // 
            // btnModCli
            // 
            this.btnModCli.Location = new System.Drawing.Point(80, 70);
            this.btnModCli.Name = "btnModCli";
            this.btnModCli.Size = new System.Drawing.Size(108, 23);
            this.btnModCli.TabIndex = 1;
            this.btnModCli.Text = "Modificar cliente";
            this.btnModCli.UseVisualStyleBackColor = true;
            this.btnModCli.Click += new System.EventHandler(this.btnModCli_Click);
            // 
            // btnEliminarCli
            // 
            this.btnEliminarCli.Location = new System.Drawing.Point(80, 118);
            this.btnEliminarCli.Name = "btnEliminarCli";
            this.btnEliminarCli.Size = new System.Drawing.Size(108, 23);
            this.btnEliminarCli.TabIndex = 2;
            this.btnEliminarCli.Text = "Eliminar cliente";
            this.btnEliminarCli.UseVisualStyleBackColor = true;
            this.btnEliminarCli.Click += new System.EventHandler(this.btnEliminarCli_Click);
            // 
            // MenuABMCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 168);
            this.Controls.Add(this.btnEliminarCli);
            this.Controls.Add(this.btnModCli);
            this.Controls.Add(this.btmCrearCli);
            this.Name = "MenuABMCliente";
            this.Text = "MenuABMCliente";
            this.Load += new System.EventHandler(this.MenuABMCliente_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btmCrearCli;
        private System.Windows.Forms.Button btnModCli;
        private System.Windows.Forms.Button btnEliminarCli;
    }
}