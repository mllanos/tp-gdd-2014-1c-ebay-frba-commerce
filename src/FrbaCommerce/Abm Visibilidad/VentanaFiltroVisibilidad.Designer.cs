namespace FrbaCommerce.Abm_Visibilidad
{
    partial class VentanaFiltroVisibilidad
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
            this.textPorcent = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textDurac = new System.Windows.Forms.TextBox();
            this.textPrecio = new System.Windows.Forms.TextBox();
            this.textDesc = new System.Windows.Forms.TextBox();
            this.dataGridViewFiltro = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiltro)).BeginInit();
            this.SuspendLayout();
            // 
            // textPorcent
            // 
            this.textPorcent.Location = new System.Drawing.Point(296, 12);
            this.textPorcent.MaxLength = 2;
            this.textPorcent.Name = "textPorcent";
            this.textPorcent.Size = new System.Drawing.Size(100, 20);
            this.textPorcent.TabIndex = 24;
            this.textPorcent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPorcent_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(544, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Duracion";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Porcentaje";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Precio";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Descripcion";
            // 
            // textDurac
            // 
            this.textDurac.Location = new System.Drawing.Point(296, 58);
            this.textDurac.Name = "textDurac";
            this.textDurac.Size = new System.Drawing.Size(100, 20);
            this.textDurac.TabIndex = 17;
            this.textDurac.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textDurac_KeyPress);
            // 
            // textPrecio
            // 
            this.textPrecio.Location = new System.Drawing.Point(68, 55);
            this.textPrecio.Name = "textPrecio";
            this.textPrecio.Size = new System.Drawing.Size(100, 20);
            this.textPrecio.TabIndex = 16;
            this.textPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPrecio_KeyPress);
            // 
            // textDesc
            // 
            this.textDesc.Location = new System.Drawing.Point(68, 12);
            this.textDesc.Name = "textDesc";
            this.textDesc.Size = new System.Drawing.Size(100, 20);
            this.textDesc.TabIndex = 15;
            // 
            // dataGridViewFiltro
            // 
            this.dataGridViewFiltro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFiltro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridViewFiltro.Location = new System.Drawing.Point(14, 100);
            this.dataGridViewFiltro.Name = "dataGridViewFiltro";
            this.dataGridViewFiltro.ReadOnly = true;
            this.dataGridViewFiltro.Size = new System.Drawing.Size(642, 152);
            this.dataGridViewFiltro.TabIndex = 14;
            this.dataGridViewFiltro.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFiltro_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Seleccionar";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(544, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Limpiar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // VentanaFiltroVisibilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 264);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textPorcent);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textDurac);
            this.Controls.Add(this.textPrecio);
            this.Controls.Add(this.textDesc);
            this.Controls.Add(this.dataGridViewFiltro);
            this.Name = "VentanaFiltroVisibilidad";
            this.Text = "VentanaModVisibilidad";
            this.Load += new System.EventHandler(this.VentanaFiltroVisibilidad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiltro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textPorcent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textDurac;
        private System.Windows.Forms.TextBox textPrecio;
        private System.Windows.Forms.TextBox textDesc;
        private System.Windows.Forms.DataGridView dataGridViewFiltro;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.Button button2;
    }
}