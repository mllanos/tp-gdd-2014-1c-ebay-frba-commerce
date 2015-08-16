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
    public partial class FormModDelFunc : Form
    {
        string rol;
        SqlConnection connection;

        public FormModDelFunc(string rolP)
        {
            rol = rolP;
            InitializeComponent();

            try
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("EBAY.getFuncRol", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@rol", rol));
                        cmd.ExecuteNonQuery();

                        DataTable tablaFuncRol = new DataTable();
                        SqlDataAdapter daR = new SqlDataAdapter(cmd);

                        daR.Fill(tablaFuncRol);

                        connection.Close();

                        foreach (DataRow row in tablaFuncRol.Rows)
                        {
                            comboBox1.Items.Add(Convert.ToString(row[0]));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

                
        }

        private void FormModDelFunc_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("EBAY.DelFunc", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@func", Convert.ToString(comboBox1.SelectedItem)));
                        cmd.Parameters.Add(new SqlParameter("@rol", rol));


                        cmd.ExecuteNonQuery();
                        connection.Close();
                        this.Hide();
                        MessageBox.Show("Funcionalidad Eliminada correctamente");

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
