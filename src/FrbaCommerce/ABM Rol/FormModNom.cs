using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace FrbaCommerce.ABM_Rol
{
    public partial class FormModNom : Form
    {
        string nombre;
        public FormModNom(string nombreP)
        {
            nombre= nombreP;
            InitializeComponent();
            textBox1.Text = nombre;
        }

        private void FormModNom_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            using (SqlConnection connection = new SqlConnection())
            {
            connection.ConnectionString = DAL.LoginDAL.Conectar();
            connection.Open();
            using (SqlCommand cmd = new SqlCommand("EBAY.modificarNombreRol", connection))
            {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@nombreNuevo", textBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));

            cmd.ExecuteNonQuery();
            connection.Close();
            this.Hide();
            MessageBox.Show("El nombre ha sido modificado correctamente");
            }
            }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

        }
    }
}
