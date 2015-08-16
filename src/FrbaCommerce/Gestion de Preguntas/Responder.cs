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
    public partial class Responder : Form
    {
        string cod_usuario;

        public Responder(string cod_pu, string question, string description, string cod_us)
        {
            InitializeComponent();
            tbxQuestion.Text = question;
            tbxCode.Text = cod_pu;
            tbxDescription.Text = description;
            cod_usuario = cod_us;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxReply.Text = String.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorString = String.Empty;
            if (tbxReply.Text.Length == 0) 
            {
                errorString = "Necesita especificar una respuesta."; 
            }
            if (tbxReply.Text.Length > 255) 
            { 
                errorString = "No se puede dar una respuesta de más de 255 caracteres."; 
            }
            if (errorString == String.Empty)
            {
                string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = "EBAY.gestion_de_preguntas_responder_pregunta";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = cod_usuario;
                        cmd.Parameters.Add("@cod_publicacion", SqlDbType.Decimal).Value = tbxCode.Text;
                        cmd.Parameters.Add("@pregunta", SqlDbType.VarChar).Value = tbxQuestion.Text;
                        cmd.Parameters.Add("@respuesta", SqlDbType.VarChar).Value = tbxReply.Text;
                        cmd.Parameters.Add("@currentDate", SqlDbType.Date).Value = ConfigurationManager.AppSettings["fecha"];
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Respuesta aceptada.");
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