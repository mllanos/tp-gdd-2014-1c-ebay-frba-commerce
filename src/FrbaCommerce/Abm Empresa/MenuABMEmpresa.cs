using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Abm_Empresa
{
    public partial class MenuABMEmpresa : Form
    {
        public MenuABMEmpresa()
        {
            InitializeComponent();
        }

        private void btnCrearE_Click(object sender, EventArgs e)
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
                            FormCrearE CrearE = new FormCrearE();
                            CrearE.ShowDialog();
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

        private void btnModE_Click(object sender, EventArgs e)
        {
            int modificacion=1;
            FormFiltros FormFil = new FormFiltros(modificacion);
            FormFil.ShowDialog();
            this.Hide();
        }

        private void btnEliminarE_Click(object sender, EventArgs e)
        {
            int eliminacion = 0;
            FormFiltros FormFil = new FormFiltros(eliminacion);
            FormFil.ShowDialog();
            this.Hide();
        }

        private void MenuABMEmpresa_Load(object sender, EventArgs e)
        {

        }
    }
}
