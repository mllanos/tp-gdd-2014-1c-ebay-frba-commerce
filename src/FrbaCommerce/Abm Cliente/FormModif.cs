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
    public partial class FormModif : Form
    {
        Usuario user = new Usuario();
        SqlConnection connection;


        public FormModif(DataRow UserMod, string rol)
        {
            
            DataRow UserMod1 = UserMod;
            InitializeComponent();
            if (rol == "Cliente")
            {
                label15.Visible = false;
                textEstado.Visible = false;
            }

            try
            {
                string conS = DAL.LoginDAL.Conectar();
                using (connection = new SqlConnection(conS))
                {

                    using (SqlCommand cmd = new SqlCommand("EBAY.cargarVentanaMod", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@dni", UserMod1[3].ToString()));

                        connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            textNombre.Text = Convert.ToString(dr.GetValue(4));
                            textApellido.Text = Convert.ToString(dr.GetValue(3));
                            textDNI.Text = Convert.ToString(dr.GetValue(1));
                            textMail.Text = Convert.ToString(dr.GetValue(5));
                            textTel.Text = dr.GetValue(6).ToString();

                            if (dr.GetInt32(2) == 0)
                            {
                                tipoDoc.Text = "DNI";
                            }
                            if (dr.GetInt32(2) == 1)
                            {
                                tipoDoc.Text = "LE";
                            }
                            if (dr.GetInt32(2) == 2)
                            {
                                tipoDoc.Text = "PASAPORTE";
                            }
                            if (dr.GetInt32(2) == 3)
                            {
                                tipoDoc.Text = "LC";
                            }


                            textFecha.Text = Convert.ToString(dr.GetValue(7));
                            textLoc.Text = Convert.ToString(dr.GetValue(10));
                            textCalle.Text = Convert.ToString(dr.GetValue(12));
                            textNro.Text = dr.GetValue(11).ToString();
                            textPiso.Text = dr.GetValue(13).ToString();
                            textDpto.Text = Convert.ToString(dr.GetValue(14));
                            textCodPost.Text = dr.GetValue(15).ToString();
                            textEstado.Text = dr.GetValue(21).ToString();
                            user.Cod_usuario = Convert.ToInt32(dr.GetValue(0));


                        }



                        dr.Close();
                        connection.Close();
                    }
                }
            

                        }

            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        private void FormModif_Load(object sender, EventArgs e)
        {

        }

        private void FormModif_Load_1(object sender, EventArgs e)
        {

        }
        private void monthcalendar_text(object sender, DateRangeEventArgs e)
        {
            textFecha.Text = e.Start.ToString().Substring(0, e.Start.ToString().Length - 5);
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.textFecha.Text = e.Start.ToShortDateString();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {

                string conS = DAL.LoginDAL.Conectar();
                using (connection = new SqlConnection(conS))
                {
                    using (SqlCommand cmd = new SqlCommand("EBAY.modificarCliente1", connection))
                    {


                        cmd.CommandType = CommandType.StoredProcedure;


                        //[Instanciando parámetro de Salida.]
                        SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;


                        cmd.Parameters.Add(new SqlParameter("@nombre", textNombre.Text));
                        cmd.Parameters.Add(new SqlParameter("@apellido", textApellido.Text));
                        cmd.Parameters.Add(new SqlParameter("@dni", Convert.ToInt64(textDNI.Text)));
                        if (tipoDoc.SelectedItem.ToString() == "DNI")
                        {
                            cmd.Parameters.Add(new SqlParameter("@tipo_dni", Convert.ToInt32(0)));
                        }
                        if (tipoDoc.SelectedItem.ToString() == "LE")
                        {
                            cmd.Parameters.Add(new SqlParameter("@tipo_dni", Convert.ToInt32(1)));
                        }
                        if (tipoDoc.SelectedItem.ToString() == "PASAPORTE")
                        {
                            cmd.Parameters.Add(new SqlParameter("@tipo_dni", Convert.ToInt32(2)));
                        }
                        if (tipoDoc.SelectedItem.ToString() == "LC")
                        {
                            cmd.Parameters.Add(new SqlParameter("@tipo_dni", Convert.ToInt32(3)));
                        }
                        cmd.Parameters.Add(new SqlParameter("@mail", textMail.Text));
                        cmd.Parameters.Add(new SqlParameter("@tel", Convert.ToInt64(textTel.Text)));
                        cmd.Parameters.Add(new SqlParameter("@dom_calle", textCalle.Text));
                        cmd.Parameters.Add(new SqlParameter("@num_calle", Convert.ToInt32(textNro.Text)));
                        cmd.Parameters.Add(new SqlParameter("@piso", Convert.ToInt32(textPiso.Text)));
                        cmd.Parameters.Add(new SqlParameter("@dpto", Convert.ToChar(textDpto.Text)));
                        cmd.Parameters.Add(new SqlParameter("@localidad", textLoc.Text));
                        cmd.Parameters.Add(new SqlParameter("@cod_postal", Convert.ToInt32(textCodPost.Text)));
                        cmd.Parameters.Add(new SqlParameter("@fecha", Convert.ToDateTime(textFecha.Text).Date));
                        cmd.Parameters.Add(new SqlParameter("@cod_usuario", user.Cod_usuario));
                        cmd.Parameters.Add(new SqlParameter("@estado", Convert.ToInt32(textEstado.Text)));
                        cmd.Parameters.Add(VariableRetorno);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        int mod = 0;
                        switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                        {
                            case 0:
                                MessageBox.Show("El telefono pertenece a un cliente existente");
                                break;

                            case 1:
                                mod = 1;
                                break;
                        }

                        cmd.CommandText = "EBAY.modificarCliente2";
                        cmd.ExecuteNonQuery();
                        switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                        {
                            case 0:
                                MessageBox.Show("El DNI - tipo DNI pertenece a un cliente existente");
                                break;

                            case 1:
                                if (mod == 1)
                                    MessageBox.Show("Los datos se modificaron correctamente");
                                break;
                        }
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        private void monthCalendar1_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);

        }

        private void textFecha_TextChanged(object sender, EventArgs e)
        {

        }

        private void textDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }
        private void validarNum(object sender, KeyPressEventArgs e)
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
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                    MessageBox.Show("solo numeros", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

        }

        private void textTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }

        private void textNro_TextChanged(object sender, EventArgs e)
        {

        }

        private void textNro_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }

        private void textPiso_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPiso_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }

        private void textCodPost_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }

        private void textDpto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                    MessageBox.Show("solo letras", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }
    }
}


