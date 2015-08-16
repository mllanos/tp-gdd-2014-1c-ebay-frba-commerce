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


namespace FrbaCommerce.Abm_Empresa
{
    public partial class FormCrearE : Form
    {
        public FormCrearE()
        {
            InitializeComponent();
        }
Usuario usuario5 = new Usuario();
        private void btnSig_Click(object sender, EventArgs e)
        {
            
            SqlConnection connection;
            SqlCommand cmd;
            try
            {

                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    using (cmd = new SqlCommand("EBAY.registrarEmpresa", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        //[Instanciando parámetro de Salida.]
                        SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;

                        string pass = sha256encrypt(textCUIT.Text);

                        cmd.Parameters.Add(new SqlParameter("@usuario", textRazSoc.Text + textCUIT.Text));
                        cmd.Parameters.Add(new SqlParameter("@contraseña", pass));
                        cmd.Parameters.Add(new SqlParameter("@razon_social", textRazSoc.Text));
                        cmd.Parameters.Add(new SqlParameter("@mail", textMail.Text));
                        cmd.Parameters.Add(new SqlParameter("@tel", Convert.ToInt64(textTel.Text)));
                        cmd.Parameters.Add(new SqlParameter("@calle", textCalle.Text));

                        if (textNro.TextLength == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@numero", '0'));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@numero", Convert.ToInt32(textNro.Text)));
                        }
                        if (textPiso.TextLength == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@piso", '0'));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@piso", Convert.ToInt32(textPiso.Text)));
                        }

                        if (textDepto.TextLength == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@depto", '0'));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@depto", Convert.ToChar(textDepto.Text)));
                        }
                        cmd.Parameters.Add(new SqlParameter("@localidad", textLoc.Text));

                        if (textCP.TextLength == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@cod_postal", '0'));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@cod_postal", Convert.ToInt32(textCP.Text)));
                        }
                        cmd.Parameters.Add(new SqlParameter("@ciudad", textCiudad.Text));
                        cmd.Parameters.Add(new SqlParameter("@cuit", textCUIT.Text));
                        cmd.Parameters.Add(new SqlParameter("@nombre_contacto", textNomCon.Text));
                        cmd.Parameters.Add(new SqlParameter("@fecha", Convert.ToDateTime(textFech.Text).Date));
                        cmd.Parameters.Add(new SqlParameter("@rol", "Empresa"));
                        cmd.Parameters.Add(VariableRetorno);

                        connection.Open();
                        cmd.ExecuteNonQuery();

                        switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                        {

                            case 1:
                                MessageBox.Show("El CUIT ingresado ya pertenece a un usuario registrado en la Base de Datos");
                                connection.Close();
                                break;
                            case 2:
                                MessageBox.Show("La Razon Social ingresada ya pertenece a un usuario registrado en la Base de Datos");
                                connection.Close();
                                break;
                            case 3:
                                MessageBox.Show("¡Empresa registrada correctamente! USUARIO: " + textRazSoc.Text + textCUIT.Text + " CONTRASEÑA: " + textCUIT.Text); //FALTA DECIR CUAL ES EL NOMBRE DE USUARIO Y CONTRASEÑA
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

        private void btnLimp_Click(object sender, EventArgs e)
        {
            textTel.Clear();
            textMail.Clear();
            textCalle.Clear();
            textNro.Clear();
            textPiso.Clear();
            textDepto.Clear();
            textLoc.Clear();
            textCUIT.Clear();
            textNomCon.Clear();
            textRazSoc.Clear();
            textCP.Clear();
            textFech.Clear();
            textCiudad.Clear();
        }

        private void FormCrearE_Load(object sender, EventArgs e)
        {

        }

        private void textFech_TextChanged(object sender, EventArgs e)
        {

        }

        private void monthcalendar_text(object sender, DateRangeEventArgs e)
        {
            textFech.Text = e.Start.ToString().Substring(0, e.Start.ToString().Length - 5);
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.textFech.Text = e.Start.ToShortDateString();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);

        }
        
        private void textTel_KeyPress(object sender, KeyPressEventArgs e)
        {

            usuario5.validarNum(sender, e);
        }

        private void textPiso_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }

        private void textCP_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }

        private void textCUIT_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textDepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarLetra(sender, e);

        }

        private void textNro_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }


    }

}
