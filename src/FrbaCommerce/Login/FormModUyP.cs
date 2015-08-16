using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace FrbaCommerce.Login
{
    public partial class FormModUyP : Form
    {
        Usuario usuario;
        public FormModUyP(Usuario user)
        {
            usuario = user;

            InitializeComponent();
            button1.Enabled = false;
        }

        private void FormModUyP_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            using (SqlConnection connection = new SqlConnection())
            {
            connection.ConnectionString = DAL.LoginDAL.Conectar();
            using (SqlCommand cmd = new SqlCommand("EBAY.modificarUyP", connection))
            {

            cmd.CommandType = CommandType.StoredProcedure;

            string pass = sha256encrypt(textPass.Text);

            cmd.Parameters.Add(new SqlParameter("@contraseña", pass));
            cmd.Parameters.Add(new SqlParameter("@cod_usuario", usuario.Cod_usuario));

            connection.Open();
            cmd.ExecuteNonQuery();

            connection.Close();
            MessageBox.Show("Modificación correcta, ingrese al sistema con su nueva contraseña");
            this.Hide();
            }
            }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

            }

        public static string sha256encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(phrase));
            return byteArrayToString(hashedDataBytes);
        }
        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");

            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        private void textPass_TextChanged(object sender, EventArgs e)
        {
            if (textPass.Text != "")
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }



    }
}
