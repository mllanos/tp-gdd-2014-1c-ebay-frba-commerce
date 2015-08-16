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
    public partial class VentanaFiltros : Form
    {
        DataTable dt;
        int accion;
        SqlConnection connection;

        public VentanaFiltros(int parametro)
        {
            accion = parametro;
            InitializeComponent();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (accion == 1)
            {

                FormModif FormMoficacion = new FormModif(dt.Rows[e.RowIndex], "Administrador");
                this.Hide();
                FormMoficacion.ShowDialog();
            }
            else
            {
                //ELIMINACION
                SqlConnection connection = new SqlConnection();
                
                connection.ConnectionString = DAL.LoginDAL.Conectar();

                SqlCommand cmd = new SqlCommand("EBAY.bajaCliente", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                //[Instanciando parámetro de Salida.]
                cmd.Parameters.Add(new SqlParameter("@nombre", dt.Rows[e.RowIndex][0]));
                cmd.Parameters.Add(new SqlParameter("@apellido", dt.Rows[e.RowIndex][1]));
                cmd.Parameters.Add(new SqlParameter("@mail", dt.Rows[e.RowIndex][2]));
                cmd.Parameters.Add(new SqlParameter("@dni", dt.Rows[e.RowIndex][3]));
                cmd.Parameters.Add(new SqlParameter("@tipo_dni", dt.Rows[e.RowIndex][4]));


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Usuario " + dt.Rows[e.RowIndex][0] + " Eliminado");
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        //int boton = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    connection.Open();
                    int tipoDoc = 5;
                    if (Convert.ToString(CombotipoDoc.SelectedItem) == "DNI")
                    {
                        tipoDoc = 0;
                    }
                    if (Convert.ToString(CombotipoDoc.SelectedItem) == "LE")
                    {
                        tipoDoc = 1;
                    }
                    if (Convert.ToString(CombotipoDoc.SelectedItem) == "PASAPORTE")
                    {
                        tipoDoc = 2;
                    }
                    if (Convert.ToString(CombotipoDoc.SelectedItem) == "LC")
                    {
                        tipoDoc = 3;
                    }

                    SqlCommand cmd;

                        cmd = new SqlCommand("EBAY.cargarGrillaModSinTipo", connection);


                    cmd.Parameters.Add(new SqlParameter("@nombre", Convert.ToString(textBoxNombre.Text)));
                    cmd.Parameters.Add(new SqlParameter("@apellido", Convert.ToString(textBoxApellido.Text)));
                    cmd.Parameters.Add(new SqlParameter("@mail", Convert.ToString(textBoxMail.Text)));
                    cmd.Parameters.Add(new SqlParameter("@doc", Convert.ToString(textBoxNDoc.Text)));
                    cmd.Parameters.Add(new SqlParameter("@tipo", tipoDoc));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                    dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(dt);
                    dataGridViewFiltro.DataSource = dt;
                    dataGridViewFiltro.MultiSelect = true;

                   // if (boton == 0)
                    //{
                      //  DataGridViewButtonColumn btngrid = new DataGridViewButtonColumn();
                        //btngrid.Name = "Seleccionar";
                       // btngrid.HeaderText = "Seleccionar";
                       // dataGridViewFiltro.Columns.Add(btngrid);
                       // boton = 1;

                    //}

                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

            
        }


        private void Seleccionar (object sender, EventArgs e)
        {
           
        }

        private void VentanaFiltros_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            textBoxApellido.Clear();
            textBoxNombre.Clear();
            textBoxNDoc.Clear();
            textBoxMail.Clear();
            dataGridViewFiltro.DataSource = null;
            CombotipoDoc.SelectedIndex=-1;
        
        }
    }
}
