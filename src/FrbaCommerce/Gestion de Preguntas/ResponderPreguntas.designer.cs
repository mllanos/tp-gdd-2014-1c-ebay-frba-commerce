namespace FrbaCommerce.Gestion_de_Preguntas
{
    partial class ResponderPreguntas
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
            this.mcalTo = new System.Windows.Forms.MonthCalendar();
            this.mcalFrom = new System.Windows.Forms.MonthCalendar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectTo = new System.Windows.Forms.Button();
            this.tbxDateTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxDateFrom = new System.Windows.Forms.TextBox();
            this.tbxEqualsFilter = new System.Windows.Forms.TextBox();
            this.tbxLikeFilter = new System.Windows.Forms.TextBox();
            this.btnSelectFrom = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mcalTo
            // 
            this.mcalTo.Location = new System.Drawing.Point(296, 100);
            this.mcalTo.Name = "mcalTo";
            this.mcalTo.TabIndex = 11;
            this.mcalTo.Visible = false;
            this.mcalTo.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.mcalTo_DateSelected);
            // 
            // mcalFrom
            // 
            this.mcalFrom.Location = new System.Drawing.Point(296, 65);
            this.mcalFrom.Name = "mcalFrom";
            this.mcalFrom.TabIndex = 10;
            this.mcalFrom.Visible = false;
            this.mcalFrom.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.mcalFrom_DateSelected);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectTo);
            this.groupBox1.Controls.Add(this.tbxDateTo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxDateFrom);
            this.groupBox1.Controls.Add(this.tbxEqualsFilter);
            this.groupBox1.Controls.Add(this.tbxLikeFilter);
            this.groupBox1.Controls.Add(this.btnSelectFrom);
            this.groupBox1.Location = new System.Drawing.Point(33, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 100);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de búsqueda";
            // 
            // btnSelectTo
            // 
            this.btnSelectTo.Location = new System.Drawing.Point(380, 53);
            this.btnSelectTo.Name = "btnSelectTo";
            this.btnSelectTo.Size = new System.Drawing.Size(75, 23);
            this.btnSelectTo.TabIndex = 10;
            this.btnSelectTo.Text = "Seleccionar";
            this.btnSelectTo.UseVisualStyleBackColor = true;
            this.btnSelectTo.Click += new System.EventHandler(this.btnSelectTo_Click);
            // 
            // tbxDateTo
            // 
            this.tbxDateTo.Enabled = false;
            this.tbxDateTo.Location = new System.Drawing.Point(261, 57);
            this.tbxDateTo.Name = "tbxDateTo";
            this.tbxDateTo.ReadOnly = true;
            this.tbxDateTo.Size = new System.Drawing.Size(100, 20);
            this.tbxDateTo.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(209, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Hasta:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Desde:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Es igual a:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Contiene:";
            // 
            // tbxDateFrom
            // 
            this.tbxDateFrom.Enabled = false;
            this.tbxDateFrom.Location = new System.Drawing.Point(261, 27);
            this.tbxDateFrom.Name = "tbxDateFrom";
            this.tbxDateFrom.ReadOnly = true;
            this.tbxDateFrom.Size = new System.Drawing.Size(100, 20);
            this.tbxDateFrom.TabIndex = 4;
            // 
            // tbxEqualsFilter
            // 
            this.tbxEqualsFilter.Location = new System.Drawing.Point(82, 57);
            this.tbxEqualsFilter.Name = "tbxEqualsFilter";
            this.tbxEqualsFilter.Size = new System.Drawing.Size(100, 20);
            this.tbxEqualsFilter.TabIndex = 2;
            // 
            // tbxLikeFilter
            // 
            this.tbxLikeFilter.Location = new System.Drawing.Point(82, 23);
            this.tbxLikeFilter.Name = "tbxLikeFilter";
            this.tbxLikeFilter.Size = new System.Drawing.Size(100, 20);
            this.tbxLikeFilter.TabIndex = 1;
            // 
            // btnSelectFrom
            // 
            this.btnSelectFrom.Location = new System.Drawing.Point(380, 22);
            this.btnSelectFrom.Name = "btnSelectFrom";
            this.btnSelectFrom.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFrom.TabIndex = 0;
            this.btnSelectFrom.Text = "Seleccionar";
            this.btnSelectFrom.UseVisualStyleBackColor = true;
            this.btnSelectFrom.Click += new System.EventHandler(this.btnSelectFrom_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(435, 135);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(33, 135);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Limpiar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 178);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(517, 190);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // ResponderPreguntas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 380);
            this.Controls.Add(this.mcalTo);
            this.Controls.Add(this.mcalFrom);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ResponderPreguntas";
            this.Text = "Responder Preguntas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResponderPreguntas_Closing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar mcalTo;
        private System.Windows.Forms.MonthCalendar mcalFrom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectTo;
        private System.Windows.Forms.TextBox tbxDateTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxDateFrom;
        private System.Windows.Forms.TextBox tbxEqualsFilter;
        private System.Windows.Forms.TextBox tbxLikeFilter;
        private System.Windows.Forms.Button btnSelectFrom;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}