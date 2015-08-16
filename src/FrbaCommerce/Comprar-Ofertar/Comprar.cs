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

namespace FrbaCommerce.Comprar_Ofertar
{
    public partial class Comprar : Form
    {
        string cod_publicacion;
        string cod_usuario;

        public Comprar(string cod_publ, string cod_us)
        {
            InitializeComponent();
            cod_publicacion = cod_publ;
            cod_usuario = cod_us;
        }

        private void Comprar_Load(object sender, EventArgs e)
        {
            fillSellerData();
            fillProductMaxCount();
            btnBuy.Enabled = false;
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EBAY.comprar_ofertar_comprar_producto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = nudCount.Value;
                    cmd.Parameters.Add("@cod_publicacion", SqlDbType.Decimal).Value = cod_publicacion;
                    cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = cod_usuario;
                    cmd.Parameters.Add("@currentDate", SqlDbType.Date).Value = ConfigurationManager.AppSettings["fecha"];
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Compra realizada.");
                    this.Close();
                }
            }
        }

        private void fillSellerData()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EBAY.comprar_ofertar_info_vendedor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_publicacion", SqlDbType.Decimal).Value = cod_publicacion;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tbxName.Text = Convert.ToString(dt.Rows[0]["nombre_contacto"]);
                    tbxEmail.Text = Convert.ToString(dt.Rows[0]["mail"]);
                    tbxPhone.Text = Convert.ToString(dt.Rows[0]["tel"]);

                }
            }
        }

        private void fillProductMaxCount()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EBAY.comprar_ofertar_disponibilidad_producto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_publicacion", SqlDbType.Decimal).Value = cod_publicacion;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tbxMaxStock.Text = Convert.ToString(dt.Rows[0]["stock_actual"]);
                    nudCount.Maximum = Convert.ToInt32(dt.Rows[0]["stock_actual"]);
                }
            }
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {
            if (((NumericUpDown)sender).Value == 0)
            {
                btnBuy.Enabled = false;
            }
            else
            {
                btnBuy.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            nudCount.Value = 0;
        }

    }
}