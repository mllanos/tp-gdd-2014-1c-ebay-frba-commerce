namespace FrbaCommerce.Abm_Cliente
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textDNI = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tipoDoc = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textMail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textTel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textCalle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textNro = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textDpto = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textLoc = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textCodPost = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textPiso = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.textFecha = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre*";
            // 
            // textNombre
            // 
            this.textNombre.Location = new System.Drawing.Point(105, 10);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(100, 20);
            this.textNombre.TabIndex = 1;
            this.textNombre.TextChanged += new System.EventHandler(this.textNombre_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Apellido";
            // 
            // textApellido
            // 
            this.textApellido.Location = new System.Drawing.Point(105, 47);
            this.textApellido.Name = "textApellido";
            this.textApellido.Size = new System.Drawing.Size(100, 20);
            this.textApellido.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = " Nº Documento *";
            // 
            // textDNI
            // 
            this.textDNI.Location = new System.Drawing.Point(105, 89);
            this.textDNI.Name = "textDNI";
            this.textDNI.Size = new System.Drawing.Size(100, 20);
            this.textDNI.TabIndex = 5;
            this.textDNI.TextChanged += new System.EventHandler(this.textDNI_TextChanged);
            this.textDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textDNI_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tipo *";
            // 
            // tipoDoc
            // 
            this.tipoDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipoDoc.FormattingEnabled = true;
            this.tipoDoc.Items.AddRange(new object[] {
            "DNI",
            "LE",
            "LC",
            "PASAPORTE"});
            this.tipoDoc.Location = new System.Drawing.Point(105, 122);
            this.tipoDoc.Name = "tipoDoc";
            this.tipoDoc.Size = new System.Drawing.Size(100, 21);
            this.tipoDoc.TabIndex = 7;
            this.tipoDoc.SelectedIndexChanged += new System.EventHandler(this.tipoDoc_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Mail";
            // 
            // textMail
            // 
            this.textMail.Location = new System.Drawing.Point(105, 165);
            this.textMail.Name = "textMail";
            this.textMail.Size = new System.Drawing.Size(100, 20);
            this.textMail.TabIndex = 9;
            this.textMail.TextChanged += new System.EventHandler(this.textMail_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Telefono *";
            // 
            // textTel
            // 
            this.textTel.Location = new System.Drawing.Point(105, 213);
            this.textTel.Name = "textTel";
            this.textTel.Size = new System.Drawing.Size(100, 20);
            this.textTel.TabIndex = 11;
            this.textTel.TextChanged += new System.EventHandler(this.textTel_TextChanged);
            this.textTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTel_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(281, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Calle";
            // 
            // textCalle
            // 
            this.textCalle.Location = new System.Drawing.Point(329, 10);
            this.textCalle.Name = "textCalle";
            this.textCalle.Size = new System.Drawing.Size(74, 20);
            this.textCalle.TabIndex = 13;
            this.textCalle.TextChanged += new System.EventHandler(this.textCalle_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(409, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Numero";
            // 
            // textNro
            // 
            this.textNro.Location = new System.Drawing.Point(459, 10);
            this.textNro.Name = "textNro";
            this.textNro.Size = new System.Drawing.Size(53, 20);
            this.textNro.TabIndex = 15;
            this.textNro.TextChanged += new System.EventHandler(this.textNro_TextChanged);
            this.textNro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textNro_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(281, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Dpto";
            // 
            // textDpto
            // 
            this.textDpto.Location = new System.Drawing.Point(393, 85);
            this.textDpto.MaxLength = 1;
            this.textDpto.Name = "textDpto";
            this.textDpto.Size = new System.Drawing.Size(100, 20);
            this.textDpto.TabIndex = 17;
            this.textDpto.TextChanged += new System.EventHandler(this.textDpto_TextChanged);
            this.textDpto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textDpto_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(279, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Localidad";
            // 
            // textLoc
            // 
            this.textLoc.Location = new System.Drawing.Point(393, 123);
            this.textLoc.Name = "textLoc";
            this.textLoc.Size = new System.Drawing.Size(100, 20);
            this.textLoc.TabIndex = 19;
            this.textLoc.TextChanged += new System.EventHandler(this.textLoc_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(279, 172);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Codigo Postal";
            // 
            // textCodPost
            // 
            this.textCodPost.Location = new System.Drawing.Point(393, 165);
            this.textCodPost.Name = "textCodPost";
            this.textCodPost.Size = new System.Drawing.Size(100, 20);
            this.textCodPost.TabIndex = 21;
            this.textCodPost.TextChanged += new System.EventHandler(this.textCodPost_TextChanged);
            this.textCodPost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textCodPost_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(266, 220);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Fecha de Nacimiento *";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(281, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Piso";
            // 
            // textPiso
            // 
            this.textPiso.Location = new System.Drawing.Point(393, 47);
            this.textPiso.Name = "textPiso";
            this.textPiso.Size = new System.Drawing.Size(100, 20);
            this.textPiso.TabIndex = 25;
            this.textPiso.TextChanged += new System.EventHandler(this.textPiso_TextChanged);
            this.textPiso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPiso_KeyPress);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(157, 287);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(16, 287);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 29;
            this.button3.Text = "Limpiar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(393, 213);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 30;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // textFecha
            // 
            this.textFecha.Enabled = false;
            this.textFecha.Location = new System.Drawing.Point(281, 249);
            this.textFecha.Name = "textFecha";
            this.textFecha.Size = new System.Drawing.Size(100, 20);
            this.textFecha.TabIndex = 31;
            this.textFecha.TextChanged += new System.EventHandler(this.textFecha_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 384);
            this.Controls.Add(this.textFecha);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textPiso);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textCodPost);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textLoc);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textDpto);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textNro);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textCalle);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textTel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textMail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tipoDoc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textDNI);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textApellido);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textNombre);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "ABM Cliente";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textDNI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox tipoDoc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textMail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textTel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textCalle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textNro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textDpto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textLoc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textCodPost;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textPiso;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.TextBox textFecha;
    }
}