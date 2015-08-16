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

namespace FrbaCommerce.Facturar_Publicaciones
{
    public partial class FacturarPublicaciones : Form
    {
        Usuario usuario;

        public FacturarPublicaciones(Usuario us)
        {
            InitializeComponent();
            usuario = us;
        }

        private void FacturarPublicaciones_Load(object sender, EventArgs e)
        {
            fillFormaDePago();
            fillDataGridViewCompra();
            fillDataGridViewCV();
            nudSalesCount.Maximum = dataGridViewCompra.RowCount;
            nudVCCount.Maximum = dataGridViewCV.RowCount;
        }

        private void fillFormaDePago()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT descripcion FROM EBAY.forma_de_pago";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cbbPaymForm.Items.Add(dr["descripcion"].ToString());
                    }
                    dr.Close();
                }
            }
        }

        private void fillDataGridViewCompra()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.facturar_publicaciones_compras_a_facturar";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = usuario.Cod_usuario;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewCompra.DataSource = dt;
                }
            }
        }

        private void fillDataGridViewCV()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.facturar_publicaciones_visibilidades_a_facturar";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = usuario.Cod_usuario;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewCV.DataSource = dt;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            nudSalesCount.Value = 0;
            nudVCCount.Value = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorString = String.Empty;

            if (cbbPaymForm.Text == String.Empty)
            {
                errorString += "Necesita especificar un método de pago.\n";
            }

            if (nudSalesCount.Value == 0 && nudVCCount.Value == 0)
            {
                errorString += "Necesita especificar una cantidad de items a facturar mayor a cero.";
            }

            if (errorString == String.Empty)
            {
                string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = "EBAY.facturar_publicaciones_generar_factura";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@forma_pago", SqlDbType.VarChar).Value = cbbPaymForm.Text;
                        cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = usuario.Cod_usuario;
                        cmd.Parameters.Add("@currentDate", SqlDbType.Date).Value = ConfigurationManager.AppSettings["fecha"];
                        cmd.Parameters.Add("@countSales", SqlDbType.Int).Value = nudSalesCount.Value;
                        cmd.Parameters.Add("@countVC", SqlDbType.Int).Value = nudVCCount.Value;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Facturación realizada.");
                        
                        if (nudSalesCount.Value != 0)
                        {
                            fillDataGridViewCompra();
                            nudSalesCount.Maximum = dataGridViewCompra.RowCount;
                        }

                        if (nudVCCount.Value != 0)
                        {
                            fillDataGridViewCV();
                            nudVCCount.Maximum = dataGridViewCV.RowCount;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(errorString);
            }
        }

    }
}
