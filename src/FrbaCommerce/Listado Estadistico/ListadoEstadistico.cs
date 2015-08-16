using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Listado_Estadistico
{
    public partial class ListadoEstadistico : Form
    {
        DataTable t1 = new DataTable();
        DataTable t2 = new DataTable();
        DataTable t3 = new DataTable();
        DataTable t4 = new DataTable();

        public ListadoEstadistico()
        {
            InitializeComponent();
        }

        private void ListadoEstadistico_Load(object sender, EventArgs e)
        {
            fillYear();
            fillVisibility();
            t1.Columns.Add("Usuario");
            t1.Columns.Add("Stock");
            t2.Columns.Add("Usuario");
            t2.Columns.Add("Facturacion");
            t3.Columns.Add("Usuario");
            t3.Columns.Add("Promedio");
            t4.Columns.Add("Usuario");
            t4.Columns.Add("Publicaciones");
        }

        private void fillYear()
        {
            string connectionString = DAL.LoginDAL.Conectar();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT DISTINCT YEAR(fecha_inicio) anio FROM EBAY.publicacion";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbYear.Items.Add(dr["anio"].ToString());
                    }
                    dr.Close();
                }
            }
        }

        private void fillVisibility()
        {
            string connectionString = DAL.LoginDAL.Conectar();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT descripcion FROM EBAY.visibilidad";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbVisibility.Items.Add(dr["descripcion"].ToString());
                    }
                    dr.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbQuarter.Text = String.Empty;
            cmbVisibility.Text = String.Empty;
            cmbYear.Text = String.Empty;
            rbtTop5_1.Checked = false;
            rbtTop5_2.Checked = false;
            rbtTop5_3.Checked = false;
            rbtTop5_4.Checked = false;
            dataGridView.DataSource = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            fillDataGridView();
        }

        private void rbtTop5_1_CheckedChanged(object sender, EventArgs e)
        {
            if(((RadioButton)sender).Checked)
            {
                cmbVisibility.Visible = true;
                label3.Visible = true;
            }
            else
            {
                cmbVisibility.Visible = false;
                cmbVisibility.Text = String.Empty;
                label3.Visible = false;
            }
        }

        private void fillDataGridView()
        {
            string error = String.Empty;

            if(cmbYear.Text == String.Empty)
            {
                error += "Debe especificar el año a consultar.\n";
            }

            if (cmbQuarter.Text == String.Empty)
            {
                error += "Debe especificar el cuatrimestre a consultar.\n";
            }

            if (cmbVisibility.Text == String.Empty && rbtTop5_1.Checked)
            {
                error += "Debe especificar una visibilidad.";
            }

            if (error == String.Empty)
            {
                if (rbtTop5_1.Checked)
                {
                    fillTop5_1();
                }
                else if (rbtTop5_2.Checked)
                {
                    fillTop5_2();
                }
                else if (rbtTop5_3.Checked)
                {
                    fillTop5_3();
                }
                else if (rbtTop5_4.Checked)
                {
                    fillTop5_4();
                }
            }
            else
            {
                MessageBox.Show(error);
            }
        }

        private void fillTop5_1() 
        {
            string connectionString = DAL.LoginDAL.Conectar();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.listado_estadistico_top5_1";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@anio", SqlDbType.Int).Value = cmbYear.Text;
                    cmd.Parameters.Add("@trimestre", SqlDbType.Int).Value = cmbQuarter.Text;
                    cmd.Parameters.Add("@filtro_visibilidad", SqlDbType.VarChar).Value = cmbVisibility.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView.DataSource = dt;
                }
            }
        }

        private void fillTop5_2() 
        {
            string connectionString = DAL.LoginDAL.Conectar();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.listado_estadistico_top5_2";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@anio", SqlDbType.Int).Value = cmbYear.Text;
                    cmd.Parameters.Add("@trimestre", SqlDbType.Int).Value = cmbQuarter.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView.DataSource = dt;
                }
            }
        }

        private void fillTop5_3() 
        {
            string connectionString = DAL.LoginDAL.Conectar();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.listado_estadistico_top5_3";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@anio", SqlDbType.Int).Value = cmbYear.Text;
                    cmd.Parameters.Add("@trimestre", SqlDbType.Int).Value = cmbQuarter.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView.DataSource = dt;
                }
            }
        }

        private void fillTop5_4() 
        {
            string connectionString = DAL.LoginDAL.Conectar();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.listado_estadistico_top5_4";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@anio", SqlDbType.Int).Value = cmbYear.Text;
                    cmd.Parameters.Add("@trimestre", SqlDbType.Int).Value = cmbQuarter.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView.DataSource = dt;
                }
            }
        }

        private void rbtTop5_1_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = t1;
        }

        private void rbtTop5_2_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = t2;
        }

        private void rbtTop5_3_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = t3;
        }

        private void rbtTop5_4_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = t4;
        }

    }
}
