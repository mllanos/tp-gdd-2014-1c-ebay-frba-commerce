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

namespace FrbaCommerce.Calificar_Vendedor
{
    public partial class AltaCalificacion : Form
    {
        string cod_usuarioC;
        CalificarVendedor calificarVendedor;
        DataTable dt;

        public AltaCalificacion(string cod, string desc, string cod_us, CalificarVendedor ca)
        {
            InitializeComponent();
            cod_usuarioC = cod_us;
            calificarVendedor = ca;
            tbxCode.Text = cod;
            tbxDescription.Text = desc;
        }

        private void AltaCalificacion_FormClosed(object sender, FormClosedEventArgs e)
        {
            calificarVendedor.Show();
        }

        private void AltaCalificacion_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.calificar_vendedor_calificaciones";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    nudStarRating.Minimum = Convert.ToInt32(dt.Rows[0]["cant_estrellas"]);
                    nudStarRating.Maximum = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["cant_estrellas"]);
                }
            }

        }

        private void cbbStars_SelectedIndexChanged(object sender, EventArgs e)
        {
            var senderCbb = (ComboBox) sender;
            tbxStarDesc.Text = dt.Rows[senderCbb.SelectedIndex]["descripcion"].ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.calificar_vendedor_alta_calificacion";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cant_estrellas", SqlDbType.Int).Value = nudStarRating.Value;
                    cmd.Parameters.Add("@cod_publicacion", SqlDbType.Decimal).Value = tbxCode.Text;
                    cmd.Parameters.Add("@cod_usuarioC", SqlDbType.Decimal).Value = cod_usuarioC;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Calificación aceptada.");
                    this.Close();
                }
            }
        }

        private void nudStarRating_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            tbxStarDesc.Text = dt.Rows[Convert.ToInt32(nud.Value - 1)]["descripcion"].ToString();
        }

    }
}
