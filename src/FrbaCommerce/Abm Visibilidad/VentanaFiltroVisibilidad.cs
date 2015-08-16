using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Abm_Visibilidad
{
    public partial class VentanaFiltroVisibilidad : Form
    {
        Usuario usuario5 = new Usuario();
        int modificar;
        public VentanaFiltroVisibilidad(int borrarOmodif)
        {
            modificar = borrarOmodif;
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = DAL.LoginDAL.Conectar();

                   
                    int a;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;

                        if (String.Compare (textDurac.Text, "") != 0)
                        {
                            if (String.Compare (textPorcent.Text, "") != 0)
                            {
                                if (String.Compare(textPrecio.Text, "") != 0)
                                {
                                    a = Convert.ToInt32(textDurac.Text);
                                    decimal porcentaje = Convert.ToDecimal(Convert.ToInt32(textPorcent.Text));
                                    cmd.CommandText = ("SELECT cod_visibilidad, descripcion, precio, porcentaje,duracion FROM EBAY.visibilidad WHERE habilitada = '1' AND duracion = ('"+a+"') AND descripcion LIKE ('%"+textDesc.Text+"%') AND precio LIKE ('"+textPrecio.Text+"%') AND porcentaje LIKE ('%"+porcentaje+"%')  ");
                                }

                                else
                                {
                                    a = Convert.ToInt32(textDurac.Text);
                                    decimal porcentaje = Convert.ToDecimal(Convert.ToInt32(textPorcent.Text));
                                    cmd.CommandText = ("SELECT cod_visibilidad, descripcion, precio, porcentaje,duracion FROM EBAY.visibilidad WHERE habilitada = '1' AND duracion = ('"+a+"') AND descripcion LIKE ('%"+textDesc.Text+"%') AND porcentaje LIKE ('%"+porcentaje+"%') ");

                                }

                            }
                            else
                            {
                                if (String.Compare(textPrecio.Text, "") != 0)
                                {

                                    a = Convert.ToInt32(textDurac.Text);

                                    cmd.CommandText = ("SELECT cod_visibilidad, descripcion, precio, porcentaje,duracion FROM EBAY.visibilidad WHERE habilitada = '1' AND descripcion LIKE ('%"+textDesc.Text+"%') AND precio LIKE ('"+textPrecio.Text+"%') AND duracion = ( '"+a+"') ");
                                }

                                else
                                {

                                    a = Convert.ToInt32(textDurac.Text);

                                    cmd.CommandText = ("SELECT cod_visibilidad, descripcion, precio, porcentaje,duracion FROM EBAY.visibilidad WHERE habilitada = '1' AND descripcion LIKE ('%"+textDesc.Text+"%') AND duracion = ( '"+a+"') ");


                                }
                                
                                }
                        }

                        else
                        {
                            if (String.Compare (textPorcent.Text, "") != 0 )
                            {
                                if (String.Compare(textPrecio.Text, "") != 0)
                                {
                                    decimal porcentaje = Convert.ToDecimal(Convert.ToInt32(textPorcent.Text));
                                    cmd.CommandText = ("SELECT cod_visibilidad, descripcion, precio, porcentaje,duracion FROM EBAY.visibilidad WHERE habilitada = '1' AND descripcion LIKE ('%" + textDesc.Text + "%') AND precio LIKE ('" + textPrecio.Text + "%') AND porcentaje LIKE ('%" + porcentaje +"%')");
                                }
                                else
                                {
                                    decimal porcentaje = Convert.ToDecimal(Convert.ToInt32(textPorcent.Text));
                                    cmd.CommandText = ("SELECT cod_visibilidad, descripcion, precio, porcentaje,duracion FROM EBAY.visibilidad WHERE habilitada = '1' AND descripcion LIKE ('%" + textDesc.Text + "%') AND porcentaje LIKE ('%" + porcentaje + "%')");


                                }
                            }
                            else
                            {
                                if (String.Compare(textPrecio.Text, "") != 0)
                                {
                                    cmd.CommandText = ("SELECT cod_visibilidad, descripcion, precio, porcentaje,duracion FROM EBAY.visibilidad WHERE habilitada = '1' AND descripcion LIKE ('%" + textDesc.Text + "%') AND precio LIKE ('" + textPrecio.Text + "%')");
                                }
                                else
                                {
                                cmd.CommandText = ("SELECT cod_visibilidad, descripcion, precio, porcentaje,duracion FROM EBAY.visibilidad WHERE habilitada = '1' AND descripcion LIKE ('%" + textDesc.Text + "%')");
                                }
                                }
                        }

 
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(dt);

                        dataGridViewFiltro.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            

        }

        private void dataGridViewFiltro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (modificar == 0)
            {
                VentanaModVisibilidad winModVis = new VentanaModVisibilidad(dataGridViewFiltro.CurrentRow);
                winModVis.ShowDialog();
            }
            else
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = DAL.LoginDAL.Conectar();
                SqlCommand cmd = new SqlCommand("EBAY.modificarEstadoVisibilidad", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cod_visibilidad", dataGridViewFiltro.CurrentRow.Cells[1].Value));


                // stored procedure para eliminar la vista seleccionada
                cmd.Parameters.Add(new SqlParameter("@a", '0'));
                MessageBox.Show("Estado de la visibilidad: Inhabilitada");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                   
                
            }
        }
        

        private void button2_Click(object sender, EventArgs e)  //boton de limpieza de campos
        {
            textDesc.Clear();   
            textDurac.Clear();
            textPrecio.Clear();
            textPorcent.Clear();
            dataGridViewFiltro.DataSource = null;
        }

        private void VentanaFiltroVisibilidad_Load(object sender, EventArgs e)
        {

        }

        private void textDurac_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }

        private void textPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                    if (char.IsPunctuation(e.KeyChar)) //permitir puntuacion
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        //el resto de teclas pulsadas se desactivan 
                        e.Handled = true;
                        MessageBox.Show("solo numeros", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
        }

        private void textPorcent_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }
    }
}
