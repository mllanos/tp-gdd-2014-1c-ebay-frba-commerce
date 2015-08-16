using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace FrbaCommerce.Registro_de_Usuario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled=false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string string1 = comboRol.SelectedItem.ToString();
            int i = String.Compare(string1, "Cliente", StringComparison.Ordinal);
            if (i == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = DAL.LoginDAL.Conectar();
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("EBAY.estadoRol", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@rol", "Cliente"));

                            SqlParameter VariableRetorno = new SqlParameter("@b", SqlDbType.Int);
                            VariableRetorno.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(VariableRetorno);
                            cmd.ExecuteNonQuery();

                            if (Convert.ToInt32(cmd.Parameters["@b"].Value) == 0)
                            {
                                MessageBox.Show("El rol se encuentra inhabilitado");
                            }
                            else
                            {
                                Abm_Cliente.Form1 formCliente = new Abm_Cliente.Form1();
                                this.Hide();
                                formCliente.ShowDialog();
                            }
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            this.Hide();

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }




            }
            else
            {

                try
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = DAL.LoginDAL.Conectar();
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("EBAY.estadoRol", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@rol", "Empresa"));

                            SqlParameter VariableRetorno = new SqlParameter("@b", SqlDbType.Int);
                            VariableRetorno.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(VariableRetorno);
                            cmd.ExecuteNonQuery();

                            if (Convert.ToInt32(cmd.Parameters["@b"].Value) == 0)
                            {
                                MessageBox.Show("El rol se encuentra inhabilitado");
                            }
                            else
                            {
                                Abm_Empresa.FormCrearE formEmpresa = new Abm_Empresa.FormCrearE();
                                this.Hide();
                                formEmpresa.ShowDialog();
                            }
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            this.Hide();

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboRol.SelectedIndex != -1)
                button1.Enabled = true;
            else button1.Enabled = false;
        }


    }
}
