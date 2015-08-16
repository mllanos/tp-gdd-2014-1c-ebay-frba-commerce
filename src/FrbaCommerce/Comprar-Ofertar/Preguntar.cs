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
    public partial class Preguntar : Form
    {
        public Preguntar(string cod_publ)
        {
            InitializeComponent();
            tbxPublCode.Text = cod_publ;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxQuestion.Text = String.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorString = String.Empty;

            if (tbxQuestion.Text == String.Empty) 
            { 
                errorString = "Necesita especificar una pregunta."; 
            }

            if (tbxQuestion.Text.Length > 255) { 
                errorString = "No se puede realizar una pregunta de más de 255 caracteres."; 
            }

            if (errorString == String.Empty)
            {
                string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = "EBAY.comprar_ofertar_realizar_pregunta";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@cod_publicacion", SqlDbType.Decimal).Value = tbxPublCode.Text;
                        cmd.Parameters.Add("@pregunta", SqlDbType.VarChar).Value = tbxQuestion.Text;
                        cmd.Parameters.Add("@currentDate", SqlDbType.Date).Value = ConfigurationManager.AppSettings["fecha"];
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Pregunta aceptada.");
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
}
