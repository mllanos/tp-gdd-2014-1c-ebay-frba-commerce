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
    public partial class VentanaCrearVisibilidad : Form
    {
        Usuario usuario5=new Usuario();
        public VentanaCrearVisibilidad()
        {
            InitializeComponent();

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = DAL.LoginDAL.Conectar();
                    using (SqlCommand cmd = new SqlCommand("EBAY.insertarVisibilidad", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@codigo", Convert.ToInt32(textCodigo.Text)));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", textDesc.Text));
                        cmd.Parameters.Add(new SqlParameter("@precio", textPrecio.Text));
                        decimal a = Convert.ToDecimal(textPorcent.Text) / 100;
                        cmd.Parameters.Add(new SqlParameter("@porcentaje", a));
                        cmd.Parameters.Add(new SqlParameter("@duracion", Convert.ToInt32(textDuracion.Text)));
                        SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(VariableRetorno);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                        {

                            case 0:
                                MessageBox.Show(" Codigo o descripcion de visibilidad ya existente.");
                                con.Close();
                                break;
                            case 1: MessageBox.Show("Visibilidad creada correctamente");
                                con.Close();
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

        private void VentanaCrearVisibilidad_Load(object sender, EventArgs e)
        {
            buttonCrear.Enabled = false;
        }

        private void textDesc_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textPrecio_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textPorcent_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textDuracion_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textCodigo_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textCodigo.Clear();
            textDesc.Clear();
            textPrecio.Clear();
            textPorcent.Clear();
            textDuracion.Clear();
        }

        private void textPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }

        private void textPorcent_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }

        private void textDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }

        private void textCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }
        public int HabilitarVentana() //garantiza la creacion de uan visibilidad con los datos necesarios
        {
            if (textCodigo.Text != "" && textDesc.Text != "" && textPrecio.Text != "" && textPorcent.Text != "" && textDuracion.Text != "")
                buttonCrear.Enabled = true;
            else buttonCrear.Enabled = false;
            return 0;
        }
    }
}
