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
    public partial class FormFiltros : Form
    {

        DataTable dt;
        int accion;
        SqlConnection connection;
        


        public FormFiltros(int accionP)
        {
            accion = accionP;
            InitializeComponent();
        }

        private void FormFiltros_Load(object sender, EventArgs e)
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
                    using (SqlCommand cmd = new SqlCommand("EBAY.CargarGrillaModE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@razon_social", Convert.ToString(textRazSoc.Text)));
                        cmd.Parameters.Add(new SqlParameter("@cuit", Convert.ToString(textCUIT.Text)));
                        cmd.Parameters.Add(new SqlParameter("@mail", Convert.ToString(textMail.Text)));
                        cmd.ExecuteNonQuery();


                        dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(dt);

                        dataGridViewFiltro.DataSource = dt;
                        dataGridViewFiltro.MultiSelect = true;


                        connection.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        private void textCUIT_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textMail_TextChanged(object sender, EventArgs e)
        {

        }

        private void textRazSoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewFiltro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
      
    
                if (accion == 1)
                {
                    FormMod FormMoficacion = new FormMod(dt.Rows[e.RowIndex], "Administrador");
                    this.Hide();
                    FormMoficacion.ShowDialog();
                }
                else
                {
                    //ELIMINACION
                    try
                    {

                        using (connection = new SqlConnection())
                        {
                            connection.ConnectionString = DAL.LoginDAL.Conectar();

                            using (SqlCommand cmd = new SqlCommand("EBAY.bajaEmpresa", connection))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;

                                //[Instanciando parámetro de Salida.]
                                cmd.Parameters.Add(new SqlParameter("@razon_social", dt.Rows[e.RowIndex][0]));
                                cmd.Parameters.Add(new SqlParameter("@cuit", dt.Rows[e.RowIndex][1]));
                                cmd.Parameters.Add(new SqlParameter("@mail", dt.Rows[e.RowIndex][2]));


                                connection.Open();
                                cmd.ExecuteNonQuery();
                                connection.Close();
                                MessageBox.Show("Empresa:  " + dt.Rows[e.RowIndex][0] + " Dada de baja");
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
            // setear los campos a null
            textCUIT.Clear();
            textRazSoc.Clear();
            textMail.Clear();
            dataGridViewFiltro.DataSource = null;
        }
    }
}