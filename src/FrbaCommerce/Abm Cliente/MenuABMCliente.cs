using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Abm_Cliente
{
    public partial class MenuABMCliente : Form
    {
        public MenuABMCliente()
        {
            InitializeComponent();
        }

        private void btmCrearCli_Click(object sender, EventArgs e)
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

                            Abm_Cliente.Form1 crearCliente = new Abm_Cliente.Form1();
                            crearCliente.ShowDialog();
                            this.Hide();
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

        private void btnModCli_Click(object sender, EventArgs e)
        {
            int modificacion = 1;
            Abm_Cliente.VentanaFiltros ventanaFiltros = new Abm_Cliente.VentanaFiltros(modificacion);
            ventanaFiltros.Show();
            this.Hide();
        }

        private void btnEliminarCli_Click(object sender, EventArgs e)
        {
            int eliminacion = 0;
            Abm_Cliente.VentanaFiltros ventanaFiltros = new Abm_Cliente.VentanaFiltros(eliminacion);
            ventanaFiltros.Show();
            this.Hide();
        }

        private void MenuABMCliente_Load(object sender, EventArgs e)
        {

        }
    }
}
