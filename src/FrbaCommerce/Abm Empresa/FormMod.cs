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
    public partial class FormMod : Form
    {
        Usuario user;
        SqlConnection connection;

        public FormMod(DataRow UserMod1, string rol)
        {
            user = new Usuario();
            InitializeComponent();
            if (rol == "Empresa")
            {
            label14.Visible = false;
            textEstado.Visible = false;
            }

            try
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    using (SqlCommand cmd = new SqlCommand("EBAY.cargarVentanaModE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@razon_social", UserMod1[0].ToString()));

                        connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            textRazSoc.Text = Convert.ToString(dr.GetValue(1));
                            textNomCon.Text = Convert.ToString(dr.GetValue(5));
                            textCUIT.Text = Convert.ToString(dr.GetValue(2));
                            textMail.Text = Convert.ToString(dr.GetValue(3));
                            textTel.Text = dr.GetValue(4).ToString();
                            textFech.Text = Convert.ToString(dr.GetValue(6));
                            textLoc.Text = Convert.ToString(dr.GetValue(9));
                            textCalle.Text = Convert.ToString(dr.GetValue(11));
                            textNro.Text = dr.GetValue(10).ToString();
                            textPiso.Text = dr.GetValue(12).ToString();
                            textDepto.Text = Convert.ToString(dr.GetValue(13));
                            textCiudad.Text = Convert.ToString(dr.GetValue(8));
                            textCP.Text = dr.GetValue(14).ToString();
                            textEstado.Text = dr.GetValue(20).ToString();
                            user.Cod_usuario = Convert.ToInt32(dr.GetValue(0));

                        }
                        else
                            MessageBox.Show("no hay datos");



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

        private void FormMod_Load(object sender, EventArgs e)
        {

        }

        private void textNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void FormMod_Load_1(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            int mod = 0;
                        try
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    using (cmd = new SqlCommand("EBAY.modificarEmpresa1", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        //[Instanciando parámetro de Salida.]
                        SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;


                        cmd.Parameters.Add(new SqlParameter("@razon_social", textRazSoc.Text));
                        cmd.Parameters.Add(new SqlParameter("@mail", textMail.Text));
                        cmd.Parameters.Add(new SqlParameter("@tel", Convert.ToInt64(textTel.Text)));
                        cmd.Parameters.Add(new SqlParameter("@dom_calle", textCalle.Text));
                        cmd.Parameters.Add(new SqlParameter("@num_calle", Convert.ToInt32(textNro.Text)));
                        cmd.Parameters.Add(new SqlParameter("@piso", Convert.ToInt32(textPiso.Text)));
                        cmd.Parameters.Add(new SqlParameter("@dpto", Convert.ToChar(textDepto.Text)));
                        cmd.Parameters.Add(new SqlParameter("@localidad", textLoc.Text));
                        cmd.Parameters.Add(new SqlParameter("@cod_postal", Convert.ToInt32(textCP.Text)));
                        cmd.Parameters.Add(new SqlParameter("@ciudad", textCiudad.Text));
                        cmd.Parameters.Add(new SqlParameter("@fecha", Convert.ToDateTime(textFech.Text).Date));
                        cmd.Parameters.Add(new SqlParameter("@nombre_contacto", textNomCon.Text));
                        cmd.Parameters.Add(new SqlParameter("@cod_usuario", user.Cod_usuario));
                        cmd.Parameters.Add(new SqlParameter("@estado", Convert.ToInt32(textEstado.Text)));
                        cmd.Parameters.Add(VariableRetorno);

                        connection.Open();
                        cmd.ExecuteNonQuery();

                        
                        switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                        {
                            case 0:
                                MessageBox.Show("La razon social ya pertenece a una empresa existente");
                                break;

                            case 1:
                                mod = 1;
                                break;
                        }
                    }

                    using (cmd = new SqlCommand("EBAY.modificarEmpresa2", connection))
                    {


                        SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);

                        cmd.CommandType = CommandType.StoredProcedure;
                        VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@cuit", textCUIT.Text));
                        cmd.Parameters.Add(new SqlParameter("@cod_usuario", user.Cod_usuario));
                        cmd.Parameters.Add(VariableRetorno);

                        cmd.ExecuteNonQuery();
                        switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                        {
                            case 0:
                                MessageBox.Show("El CUIT pertenece a una empresa existente");
                                break;

                            case 1:
                                if (mod == 1)
                                    MessageBox.Show("Los datos se modificaron correctamente");
                                break;
                        }
                        this.Hide();
                        connection.Close();
                    }
           
                }
            }

                        catch (Exception ex)
                        {
                            throw new Exception("Error: " + ex.Message);
                        }

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
            user.validarNum(sender, e);
        }

        private void textNro_KeyPress(object sender, KeyPressEventArgs e)
        {
            user.validarNum(sender, e);
        }

        private void textPiso_KeyPress(object sender, KeyPressEventArgs e)
        {
            user.validarNum(sender, e);
        }

        private void textCP_KeyPress(object sender, KeyPressEventArgs e)
        {
            user.validarNum(sender, e);
        }

        private void textDepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            user.validarLetra(sender, e);
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
    }
}
