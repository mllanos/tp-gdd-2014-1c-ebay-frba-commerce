namespace FrbaCommerce.Abm_Empresa
{
    partial class FormFiltros
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
            this.textCUIT = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textMail = new System.Windows.Forms.TextBox();
            this.textRazSoc = new System.Windows.Forms.TextBox();
            this.dataGridViewFiltro = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiltro)).BeginInit();
            this.SuspendLayout();
            // 
            // textCUIT
            // 
            this.textCUIT.Location = new System.Drawing.Point(294, 15);
            this.textCUIT.MaxLength = 15;
            this.textCUIT.Name = "textCUIT";
            this.textCUIT.Size = new System.Drawing.Size(100, 20);
            this.textCUIT.TabIndex = 32;
            this.textCUIT.TextChanged += new System.EventHandler(this.textCUIT_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(472, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Email";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "CUIT";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Razon Social";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textMail
            // 
            this.textMail.Location = new System.Drawing.Point(524, 15);
            this.textMail.Name = "textMail";
            this.textMail.Size = new System.Drawing.Size(100, 20);
            this.textMail.TabIndex = 27;
            this.textMail.TextChanged += new System.EventHandler(this.textMail_TextChanged);
            // 
            // textRazSoc
            // 
            this.textRazSoc.Location = new System.Drawing.Point(90, 14);
            this.textRazSoc.Name = "textRazSoc";
            this.textRazSoc.Size = new System.Drawing.Size(100, 20);
            this.textRazSoc.TabIndex = 26;
            this.textRazSoc.TextChanged += new System.EventHandler(this.textRazSoc_TextChanged);
            // 
            // dataGridViewFiltro
            // 
            this.dataGridViewFiltro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFiltro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridViewFiltro.Location = new System.Drawing.Point(9, 97);
            this.dataGridViewFiltro.Name = "dataGridViewFiltro";
            this.dataGridViewFiltro.Size = new System.Drawing.Size(642, 152);
            this.dataGridViewFiltro.TabIndex = 25;
            this.dataGridViewFiltro.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFiltro_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Seleccionar";
            this.Column1.Name = "Column1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(475, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 23);
            this.button2.TabIndex = 33;
            this.button2.Text = "Limpiar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormFiltros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textCUIT);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textMail);
            this.Controls.Add(this.textRazSoc);
            this.Controls.Add(this.dataGridViewFiltro);
            this.Name = "FormFiltros";
            this.Text = "Filtros de busqueda";
            this.Load += new System.EventHandler(this.FormFiltros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiltro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textCUIT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textMail;
        private System.Windows.Forms.TextBox textRazSoc;
        private System.Windows.Forms.DataGridView dataGridViewFiltro;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.Button button2;
    }
}