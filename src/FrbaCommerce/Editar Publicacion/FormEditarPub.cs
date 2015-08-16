using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Editar_Publicacion
{
    public partial class FormEditarPub : Form
    {
        Usuario usuario5 = new Usuario();
        int cod_user1, cod_pub1, durac, tipo1, stockPri, cantRubros;
        SqlConnection con;
        public FormEditarPub(int cod_user, int cod_pub, int tipo)
        {
            cod_user1 = cod_user;
            cod_pub1 = cod_pub;
            tipo1 = tipo;
            InitializeComponent();

            //try
           // {
                using (con = new SqlConnection())
                {
                    con.ConnectionString = DAL.LoginDAL.Conectar();
                    using (SqlCommand cmd = new SqlCommand("EBAY.getVisibilidad", con))
                    {//obtiene listado de visibilidades
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {

                            comboVis.Items.Add(Convert.ToString(row[0]));
                        }

                        cmd.CommandText = "EBAY.getRubros"; //obtiene listado de rubros

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataTable dtR = new DataTable();
                        SqlDataAdapter daR = new SqlDataAdapter(cmd);

                        daR.Fill(dtR);
                        cantRubros = dtR.Rows.Count;
                        foreach (DataRow row in dtR.Rows)
                        {

                            checkedListBox1.Items.Add(Convert.ToString(row[0]));
                        }






                        cmd.CommandText = "EBAY.cargarVentanaModPub"; //obtiene datos para cargar la ventana de modificacion

                        cmd.Parameters.Add(new SqlParameter("@cod_publicacion", cod_pub1));

                        con.Open();
                        cmd.ExecuteNonQuery();

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {

                            DateTime fecha = Convert.ToDateTime(dr.GetValue(4));
                            durac = Convert.ToInt32(dr.GetValue(7));
                            fecha = fecha.AddDays(durac);
                            textDesc.Text = Convert.ToString(dr.GetValue(0));
                            textPrecio.Text = Convert.ToString(dr.GetValue(1));

                            comboVis.Text = Convert.ToString(dr.GetValue(6));


                            if (dr.GetInt32(2) == 0)
                            {
                                comboTipo.Text = "Inmediata";
                            }
                            if (dr.GetInt32(2) == 1)
                            {
                                comboTipo.Text = "Subasta";
                            }

                            if (dr.GetInt32(3) == 0)
                            {
                                comboEstado.Text = "Borrador";
                            }
                            if (dr.GetInt32(3) == 1)
                            {
                                comboEstado.Text = "Publicada";
                            }
                            if (dr.GetInt32(3) == 2)
                            {
                                comboEstado.Text = "Pausada";
                            }
                            if (dr.GetInt32(3) == 3)
                            {
                                comboEstado.Text = "Finalizada";
                            }
                            if (dr.GetSqlBoolean(5) == false)
                            {
                                comboPreg.Text = "No";
                            }
                            if (dr.GetSqlBoolean(5) == true)
                            {
                                comboPreg.Text = "Si";
                            }

                            textStock.Text = Convert.ToString(dr.GetValue(8));
                            stockPri = Convert.ToInt32(dr.GetValue(8));

                            con.Close();
                        }
                        else
                            con.Close();
                        dr.Dispose();
                        dr.Close();
                    }
                }
            //}
            //catch (Exception ex)
           // {
             //   throw new Exception("Error: " + ex.Message);
           // }
        }

      


        private void comboVis_SelectedIndexChanged(object sender, EventArgs e) //cambiar la fecha final de la publicacion segun cambia la visibilidad
        {
            

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string rubros = "";
            int i;
            for (i = 0; i < cantRubros; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    rubros = rubros + checkedListBox1.GetItemText(checkedListBox1.Items[i]) + ",";
                }
            }

             try
             {
            using (con = new SqlConnection())
            {
                con.ConnectionString = DAL.LoginDAL.Conectar();

                using (SqlCommand cmd = new SqlCommand("EBAY.updateRubrosXPublicacion", con))
                {//update de publicacion sin stock
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@rubros", rubros));
                    cmd.Parameters.Add(new SqlParameter("@cod_publicacion", cod_pub1));
                    cmd.Parameters.Add(new SqlParameter("@cod_usuario", cod_user1));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmd.CommandText = "EBAY.updatePub";
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add(new SqlParameter("@cod_publicacion", cod_pub1));
                    cmd.Parameters.Add(new SqlParameter("@descripcion", textDesc.Text));
                    cmd.Parameters.Add(new SqlParameter("@precio", Convert.ToDecimal(textPrecio.Text)));

                    if (Convert.ToString(comboTipo.SelectedItem) == "Inmediata")
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", '0'));

                    }

                    else if (Convert.ToString(comboTipo.SelectedItem) == "Subasta")
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", '1'));

                    }


                    if (Convert.ToString(comboEstado.SelectedItem) == "Borrador")
                    {
                        cmd.Parameters.Add(new SqlParameter("@estado", '0'));
                    }
                    else if (Convert.ToString(comboEstado.SelectedItem) == "Publicada")
                    {
                        cmd.Parameters.Add(new SqlParameter("@estado", '1'));
                    }
                    else if (Convert.ToString(comboEstado.SelectedItem) == "Pausada")
                    {
                        cmd.Parameters.Add(new SqlParameter("@estado", '2'));
                    }
                    else if (Convert.ToString(comboEstado.SelectedItem) == "Finalizada")
                    {
                        cmd.Parameters.Add(new SqlParameter("@estado", '3'));
                    }



                    if (Convert.ToString(comboPreg.SelectedItem) == "No")
                    {
                        cmd.Parameters.Add(new SqlParameter("@permitir_preguntas", '0'));
                    }
                    else if (Convert.ToString(comboPreg.SelectedItem) == "Si")
                    {
                        cmd.Parameters.Add(new SqlParameter("@permitir_preguntas", '1'));
                    }


                    cmd.Parameters.Add(new SqlParameter("@stockIni", Convert.ToInt32(textStock.Text)));
                    cmd.Parameters.Add(new SqlParameter("@stockAct", Convert.ToInt32(textStock.Text)));


                    con.Open();
                    cmd.ExecuteNonQuery();


                    cmd.CommandText = "EBAY.updateVisPub"; //update de visibilidad
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add(new SqlParameter("@visibilidad", Convert.ToString(comboVis.SelectedItem)));
                    cmd.Parameters.Add(new SqlParameter("@cod_publicacion", cod_pub1));
                    cmd.Parameters.Add(new SqlParameter("@cod_usuario", cod_user1));
                    SqlParameter VariableRetorno1 = new SqlParameter("@a", SqlDbType.Int);
                    VariableRetorno1.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(VariableRetorno1);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    if (Convert.ToInt32(cmd.Parameters["@a"].Value) == 0) //verificar si excede o no la cant de pub gratis
                    {
                        MessageBox.Show("Cantidad maxima de publicaciones con visibilidad Gratis alcanzada. Ingrese otra visibilidad");

                    }
                    else MessageBox.Show("Publicacion actualizada correctamente");
                }
            }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
              }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void FormEditarPub_Load(object sender, EventArgs e)
        {

        }

        private void textPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }

        private void textStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }
    }
}
