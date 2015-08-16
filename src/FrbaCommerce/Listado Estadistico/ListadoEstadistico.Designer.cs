namespace FrbaCommerce.Listado_Estadistico
{
    partial class ListadoEstadistico
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbVisibility = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbQuarter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtTop5_4 = new System.Windows.Forms.RadioButton();
            this.rbtTop5_3 = new System.Windows.Forms.RadioButton();
            this.rbtTop5_2 = new System.Windows.Forms.RadioButton();
            this.rbtTop5_1 = new System.Windows.Forms.RadioButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbVisibility);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbYear);
            this.groupBox1.Controls.Add(this.cmbQuarter);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rbtTop5_4);
            this.groupBox1.Controls.Add(this.rbtTop5_3);
            this.groupBox1.Controls.Add(this.rbtTop5_2);
            this.groupBox1.Controls.Add(this.rbtTop5_1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 176);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Top 5";
            // 
            // cmbVisibility
            // 
            this.cmbVisibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVisibility.FormattingEnabled = true;
            this.cmbVisibility.Location = new System.Drawing.Point(97, 112);
            this.cmbVisibility.Name = "cmbVisibility";
            this.cmbVisibility.Size = new System.Drawing.Size(121, 21);
            this.cmbVisibility.TabIndex = 33;
            this.cmbVisibility.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "* Visibilidad:";
            this.label3.Visible = false;
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(97, 75);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 31;
            // 
            // cmbQuarter
            // 
            this.cmbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarter.FormattingEnabled = true;
            this.cmbQuarter.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbQuarter.Location = new System.Drawing.Point(97, 38);
            this.cmbQuarter.Name = "cmbQuarter";
            this.cmbQuarter.Size = new System.Drawing.Size(121, 21);
            this.cmbQuarter.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "* Año:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "* Trimestre:";
            // 
            // rbtTop5_4
            // 
            this.rbtTop5_4.AutoSize = true;
            this.rbtTop5_4.Location = new System.Drawing.Point(280, 135);
            this.rbtTop5_4.Name = "rbtTop5_4";
            this.rbtTop5_4.Size = new System.Drawing.Size(182, 17);
            this.rbtTop5_4.TabIndex = 8;
            this.rbtTop5_4.TabStop = true;
            this.rbtTop5_4.Text = "Mayor Cant. de Publ. Sin Calificar";
            this.rbtTop5_4.UseVisualStyleBackColor = true;
            this.rbtTop5_4.Click += new System.EventHandler(this.rbtTop5_4_Click);
            // 
            // rbtTop5_3
            // 
            this.rbtTop5_3.AutoSize = true;
            this.rbtTop5_3.Location = new System.Drawing.Point(280, 99);
            this.rbtTop5_3.Name = "rbtTop5_3";
            this.rbtTop5_3.Size = new System.Drawing.Size(193, 17);
            this.rbtTop5_3.TabIndex = 7;
            this.rbtTop5_3.TabStop = true;
            this.rbtTop5_3.Text = "Mayores Calificaciones (Porcentaje)";
            this.rbtTop5_3.UseVisualStyleBackColor = true;
            this.rbtTop5_3.Click += new System.EventHandler(this.rbtTop5_3_Click);
            // 
            // rbtTop5_2
            // 
            this.rbtTop5_2.AutoSize = true;
            this.rbtTop5_2.Location = new System.Drawing.Point(280, 64);
            this.rbtTop5_2.Name = "rbtTop5_2";
            this.rbtTop5_2.Size = new System.Drawing.Size(173, 17);
            this.rbtTop5_2.TabIndex = 6;
            this.rbtTop5_2.TabStop = true;
            this.rbtTop5_2.Text = "Mayor Cantidad de Facturación";
            this.rbtTop5_2.UseVisualStyleBackColor = true;
            this.rbtTop5_2.Click += new System.EventHandler(this.rbtTop5_2_Click);
            // 
            // rbtTop5_1
            // 
            this.rbtTop5_1.AutoSize = true;
            this.rbtTop5_1.Location = new System.Drawing.Point(280, 30);
            this.rbtTop5_1.Name = "rbtTop5_1";
            this.rbtTop5_1.Size = new System.Drawing.Size(189, 17);
            this.rbtTop5_1.TabIndex = 5;
            this.rbtTop5_1.TabStop = true;
            this.rbtTop5_1.Text = "Mayor Cant. de Prod. No Vendidos";
            this.rbtTop5_1.UseVisualStyleBackColor = true;
            this.rbtTop5_1.Click += new System.EventHandler(this.rbtTop5_1_Click);
            this.rbtTop5_1.CheckedChanged += new System.EventHandler(this.rbtTop5_1_CheckedChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 253);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(495, 133);
            this.dataGridView.TabIndex = 5;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 206);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Limpiar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(432, 206);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ListadoEstadistico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 412);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListadoEstadistico";
            this.Text = "Listado Estadístico";
            this.Load += new System.EventHandler(this.ListadoEstadistico_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtTop5_1;
        private System.Windows.Forms.RadioButton rbtTop5_2;
        private System.Windows.Forms.RadioButton rbtTop5_4;
        private System.Windows.Forms.RadioButton rbtTop5_3;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbVisibility;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.ComboBox cmbQuarter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}