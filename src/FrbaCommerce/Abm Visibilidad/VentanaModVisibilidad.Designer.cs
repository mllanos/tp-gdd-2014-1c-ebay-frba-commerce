namespace FrbaCommerce.Abm_Visibilidad
{
    partial class VentanaModVisibilidad
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textDesc = new System.Windows.Forms.TextBox();
            this.textPrecio = new System.Windows.Forms.TextBox();
            this.textPorcent = new System.Windows.Forms.TextBox();
            this.textDurac = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(214, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Descripcion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Precio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Porcentaje";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Duracion";
            // 
            // textDesc
            // 
            this.textDesc.Location = new System.Drawing.Point(168, 40);
            this.textDesc.Name = "textDesc";
            this.textDesc.Size = new System.Drawing.Size(100, 20);
            this.textDesc.TabIndex = 5;
            this.textDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textDesc_KeyPress);
            // 
            // textPrecio
            // 
            this.textPrecio.Location = new System.Drawing.Point(168, 91);
            this.textPrecio.Name = "textPrecio";
            this.textPrecio.Size = new System.Drawing.Size(100, 20);
            this.textPrecio.TabIndex = 6;
            this.textPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPrecio_KeyPress);
            // 
            // textPorcent
            // 
            this.textPorcent.Location = new System.Drawing.Point(168, 143);
            this.textPorcent.MaxLength = 2;
            this.textPorcent.Name = "textPorcent";
            this.textPorcent.Size = new System.Drawing.Size(100, 20);
            this.textPorcent.TabIndex = 7;
            this.textPorcent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPorcent_KeyPress);
            // 
            // textDurac
            // 
            this.textDurac.Location = new System.Drawing.Point(168, 191);
            this.textDurac.Name = "textDurac";
            this.textDurac.Size = new System.Drawing.Size(100, 20);
            this.textDurac.TabIndex = 8;
            this.textDurac.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textDurac_KeyPress);
            // 
            // VentanaModVisibilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 286);
            this.Controls.Add(this.textDurac);
            this.Controls.Add(this.textPorcent);
            this.Controls.Add(this.textPrecio);
            this.Controls.Add(this.textDesc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "VentanaModVisibilidad";
            this.Text = "VentanaModVisibilidad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textDesc;
        private System.Windows.Forms.TextBox textPrecio;
        private System.Windows.Forms.TextBox textPorcent;
        private System.Windows.Forms.TextBox textDurac;
    }
}