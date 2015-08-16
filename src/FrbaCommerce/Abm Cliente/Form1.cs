using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace FrbaCommerce.Abm_Cliente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            SqlConnection connection;

            try
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    using (cmd = new SqlCommand("EBAY.registrarUsuario", connection))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        //[Instanciando parámetro de Salida.]
                        SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;

                        string pass = sha256encrypt(textTel.Text);

                        cmd.Parameters.Add(new SqlParameter("@nombre", textNombre.Text));
                        cmd.Parameters.Add(new SqlParameter("@apellido", textApellido.Text));
                        cmd.Parameters.Add(new SqlParameter("@usuario", textNombre.Text + textTel.Text));
                        cmd.Parameters.Add(new SqlParameter("@password", pass));
                        cmd.Parameters.Add(new SqlParameter("@dni", textDNI.Text));

                        if (tipoDoc.SelectedItem.ToString() == "DNI")
                        {
                            cmd.Parameters.Add(new SqlParameter("@tipo_dni", '0'));
                        }
                        if (tipoDoc.SelectedItem.ToString() == "LE")
                        {
                            cmd.Parameters.Add(new SqlParameter("@tipo_dni", '1'));
                        }
                        if (tipoDoc.SelectedItem.ToString() == "PASAPORTE")
                        {
                            cmd.Parameters.Add(new SqlParameter("@tipo_dni", '2'));
                        }
                        if (tipoDoc.SelectedItem.ToString() == "LC")
                        {
                            cmd.Parameters.Add(new SqlParameter("@tipo_dni", '3'));
                        }

                        cmd.Parameters.Add(new SqlParameter("@mail", textMail.Text));
                        cmd.Parameters.Add(new SqlParameter("@tel", Convert.ToInt64(textTel.Text)));
                        cmd.Parameters.Add(new SqlParameter("@dom_calle", textCalle.Text));
                        if (textNro.TextLength == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@num_calle", '0'));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@num_calle", Convert.ToInt32(textNro.Text)));
                        }
                        if (textPiso.TextLength == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@piso", '0'));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@piso", Convert.ToInt32(textPiso.Text)));
                        }

                        if (textDpto.TextLength == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@dpto", '0'));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@dpto", Convert.ToChar(textDpto.Text)));
                        }
                        cmd.Parameters.Add(new SqlParameter("@localidad", textLoc.Text));

                        if (textCodPost.TextLength == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@cod_postal", '0'));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@cod_postal", Convert.ToInt32(textCodPost.Text)));
                        }

                        cmd.Parameters.Add(new SqlParameter("@fecha", Convert.ToDateTime(textFecha.Text).Date));

                        cmd.Parameters.Add(new SqlParameter("@rol", "Cliente"));
                        cmd.Parameters.Add(VariableRetorno);

                        connection.Open();
                        cmd.ExecuteNonQuery();

                        switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                        {
                            // case 0:
                            //   MessageBox.Show("La direccion ingresada ya pertenece a un usuario registrado en la Base de Datos");
                            // connection.Close();
                            //break;
                            case 1:
                                MessageBox.Show("El dni ingresado ya pertenece a un usuario registrado en la Base de Datos");
                                connection.Close();
                                break;
                            case 2:
                                MessageBox.Show("El telefono ingresado ya pertenece a un usuario registrado en la Base de Datos");
                                connection.Close();
                                break;
                            case 3:
                                MessageBox.Show("¡Cliente registrado correctamente! USUARIO:" + textNombre.Text + textTel.Text + " CONTRASEÑA:" + textTel.Text);
                                connection.Close();
                                this.Hide();
                                break;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        public static string sha256encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(phrase));
            return byteArrayToString(hashedDataBytes);
        }
        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             textNombre.Clear() ;
             textApellido.Clear() ;
             textTel.Clear();
             tipoDoc.ResetText();
             textDNI.Clear();
             textMail.Clear();
             textCalle.Clear();
             textNro.Clear();
             textPiso.Clear();
             textDpto.Clear();
             textLoc.Clear();
             textCodPost.Clear();
             textFecha.Clear();
        }

        private void monthcalendar_text(object sender, DateRangeEventArgs e)
        {
            textFecha.Text = e.Start.ToString().Substring(0, e.Start.ToString().Length - 5);
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.textFecha.Text = e.Start.ToShortDateString();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);

        }

        private void textTel_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
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

        private void textDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }

        private void textTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }

        private void textNro_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textNro_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }

        private void textPiso_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }

        private void textCodPost_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textCodPost_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNum(sender, e);
        }

        public int HabilitarVentana() //garantiza la creacion de un cliente con los datos necesarios
        {
            if (textApellido.Text != "" && textNombre.Text != "" && textNro.Text != "" && textMail.Text != "" && textTel.Text != "" && textCodPost.Text != "" && textFecha.Text != ""
                && textDNI.Text != "" && textPiso.Text != "" && textCalle.Text != "" && textNro.Text != "" && tipoDoc.SelectedItem != null)
                button2.Enabled = true;
            else button2.Enabled = false;
            return 0;
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

        private void textNombre_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textDNI_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textMail_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textCalle_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textPiso_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textDpto_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textLoc_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textFecha_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }
    }
}
