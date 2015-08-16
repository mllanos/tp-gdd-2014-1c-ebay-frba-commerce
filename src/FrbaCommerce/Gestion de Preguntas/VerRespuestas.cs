using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace FrbaCommerce.Gestion_de_Preguntas
{
    public partial class VerRespuestas : Form
    {
        GestionDePreguntas selector;
        string cod_usuario;

        public VerRespuestas(GestionDePreguntas s, string cod_us)
        {
            InitializeComponent();
            selector = s;
            cod_usuario = cod_us;
        }

        private void VerRespuestas_Closing(object sender, FormClosingEventArgs e)
        {
            selector.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxLikeFilter.Text = String.Empty;
            tbxEqualsFilter.Text = String.Empty;
            tbxDateFrom.Text = String.Empty;
            tbxDateTo.Text = String.Empty;
            dataGridView.DataSource = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString)) {
                string sql = "EBAY.gestion_de_preguntas_ver_respuestas";
                using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = 14;
                    cmd.Parameters.Add("@filtro1", SqlDbType.VarChar).Value = tbxLikeFilter.Text;
                    cmd.Parameters.Add("@filtro2", SqlDbType.VarChar).Value = tbxEqualsFilter.Text;
                    cmd.Parameters.Add("@filtro3", SqlDbType.VarChar).Value = tbxDateFrom.Text;
                    cmd.Parameters.Add("@filtro4", SqlDbType.VarChar).Value = tbxDateTo.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView.DataSource = dt;
                }
            }
        }

        private void btnSelectFrom_Click(object sender, EventArgs e)
        {
            mcalFrom.Visible = mcalFrom.Visible ? false : true;
        }

        private void btnSelectTo_Click(object sender, EventArgs e)
        {
            mcalTo.Visible = mcalTo.Visible ? false : true;
        }


        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            tbxDateFrom.Text = e.Start.ToString().Substring(0, e.Start.ToString().Length - 5); // le sacamos el a.m./p.m.
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            tbxDateTo.Text = e.Start.ToString().Substring(0, e.Start.ToString().Length - 5); // le sacamos el a.m./p.m.
        }
    }
}
