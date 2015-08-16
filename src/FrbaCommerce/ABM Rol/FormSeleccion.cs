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
    public partial class FormSeleccion : Form
    {
       SqlConnection connection;
       int accion;
        public FormSeleccion(int accionP)
        {
            accion = accionP;
            InitializeComponent();
            try
            {
            using (connection = new SqlConnection())
            {
            connection.ConnectionString = DAL.LoginDAL.Conectar();
            connection.Open();
            using (SqlCommand cmd = new SqlCommand("EBAY.obtenerRoles", connection))
            {
            cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter ("@accion", accion));
            cmd.ExecuteNonQuery();
            connection.Close();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (accion == 1)
            {
                ABM_Rol.FormModificacion FormMod = new ABM_Rol.FormModificacion(Convert.ToString(comboBox1.SelectedItem));
                FormMod.ShowDialog();
                this.Hide();
            }
            else
            {
                try
                {
                    using (connection = new SqlConnection())
                    {
                        connection.ConnectionString = DAL.LoginDAL.Conectar();

                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("EBAY.bajaRol", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add (new SqlParameter ("@rol",Convert.ToString(comboBox1.SelectedItem)));
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            this.Hide();
                            MessageBox.Show("El rol ha sido eliminado correctamente");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }

            }
        }

        private void FormSeleccion_Load(object sender, EventArgs e)
        {

        }
    }
}
