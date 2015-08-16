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
    public partial class FormModificacion : Form
    {
        string rol;
        public FormModificacion(string rolP)
        {
            rol = rolP;
            InitializeComponent();
        }

        private void FormMod2_Load(object sender, EventArgs e)
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

                        cmd.Parameters.Add(new SqlParameter("@rol", rol));

                        SqlParameter VariableRetorno = new SqlParameter("@b", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(VariableRetorno);
                        cmd.ExecuteNonQuery();
                        
                        if (Convert.ToInt32(cmd.Parameters["@b"].Value) == 0)
                        {
                            BtnAlta.Visible = true;
                        }
                        else
                        {
                            BtnAlta.Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormModNom ModNombre = new ABM_Rol.FormModNom(rol);
            this.Hide();
            ModNombre.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormModAddFunc AgregarFunc = new ABM_Rol.FormModAddFunc(rol);
            AgregarFunc.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormModDelFunc DelFunc = new ABM_Rol.FormModDelFunc(rol);
            DelFunc.ShowDialog();
        }

        private void BtnAlta_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("EBAY.altaRol", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@rol", rol));

                        cmd.ExecuteNonQuery();
                        connection.Close();

                    }
                    MessageBox.Show("Rol dado de alta correctamente");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
