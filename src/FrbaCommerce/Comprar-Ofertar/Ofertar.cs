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
    public partial class Ofertar : Form
    {
        string cod_usuario;
        string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
        DataTable dt;
        public Ofertar(string cod_publ, string cod_us)
        {
            InitializeComponent();
            tbxPublCode.Text = cod_publ;
            cod_usuario = cod_us;
        }

        private void Ofertar_Load(object sender, EventArgs e)
        {
            setearMaximaOferta();
            verificarEstadoSubasta();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorString = String.Empty;

            if (tbxBid.Text == String.Empty)
            {
                errorString = "Necesita especificar un monto a pujar.";
            }
            else
            {
                decimal i;
                decimal i2;
                bool result = decimal.TryParse(tbxBid.Text, out i);
                bool result2 = decimal.TryParse((Convert.ToString(dt.Rows[0]["maximo"])), out i2);

                if (result == false)
                {
                    errorString = "Error de tipo. Debe especificar un valor numérico para la puja.";
                }
                else
                {
                    if (result2)
                    {
                        if (Convert.ToDecimal(tbxBid.Text.Replace(".", ",")) <= Convert.ToDecimal((Convert.ToString(dt.Rows[0]["maximo"]))))
                        {
                            errorString = "Se debe especificar una puja mayor a la puja máxima.";
                        }
                    }

                }

                if (errorString == String.Empty)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string sql = "EBAY.comprar_ofertar_ofertar_subasta";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@cod_publicacion", SqlDbType.Decimal).Value = tbxPublCode.Text;
                            cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = cod_usuario;
                            cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = tbxBid.Text.Replace(".", ",");
                            cmd.Parameters.Add("@currentDate", SqlDbType.Date).Value = ConfigurationManager.AppSettings["fecha"];
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Puja aceptada.");
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(errorString);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxBid.Text = String.Empty;
        }

        private void setearMaximaOferta()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.comprar_ofertar_obtener_mayor_oferta";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_publicacion", SqlDbType.Decimal).Value = tbxPublCode.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    tbxMaxBid.Text = dt.Rows[0]["maximo"].ToString();
                }
            }
        }

        private void verificarEstadoSubasta()
        {
        
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.comprar_ofertar_estado_subasta";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_publicacion", SqlDbType.Decimal).Value = tbxPublCode.Text;
                    SqlParameter retval = new SqlParameter("@status", SqlDbType.Bit);
                    retval.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(retval);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    if(Convert.ToBoolean(retval.Value))
                    {
                        tbxBid.Text = "-- SUBASTA TERMINADA --";
                        tbxBid.Enabled = false;
                        btnSave.Enabled = false;
                        btnClear.Enabled = false;
                    }
                }
            }
        }

        private void tbxMaxBid_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
