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
    public partial class VentanaModVisibilidad : Form
    {
        Usuario usuario5 = new Usuario();
        DataGridViewRow row2; //row que se usa en el update de los datos
        public VentanaModVisibilidad(DataGridViewRow row1)
        {
            DataGridViewRow row = row1;
            InitializeComponent();
            // llenar los campos con los datos actuales
            textDesc.Text = row.Cells[2].Value.ToString();  
            textPrecio.Text =row.Cells[3].Value.ToString();
            decimal a = Convert.ToDecimal(row.Cells[4].Value) * 100;
            textPorcent.Text = Convert.ToInt32(a).ToString();
            textDurac.Text = row.Cells[5].Value.ToString();

            row2 = row;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            using (SqlConnection con = new SqlConnection())
            {
            con.ConnectionString = DAL.LoginDAL.Conectar();

            using (SqlCommand cmd = new SqlCommand("EBAY.modificarVisibilidad", con))
            {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cod_visibilidad", row2.Cells[1].Value));
            if ((String.Compare(textDesc.Text, row2.Cells[2].Value.ToString()) == 0))
                cmd.Parameters.Add(new SqlParameter("@descripcion", ""));
            else
                cmd.Parameters.Add(new SqlParameter("@descripcion", textDesc.Text));
            cmd.Parameters.Add(new SqlParameter("@precio", Convert.ToDecimal (textPrecio.Text)));
            decimal a = Convert.ToDecimal(textPorcent.Text) / 100;
            cmd.Parameters.Add(new SqlParameter("@porcentaje", a));
            cmd.Parameters.Add(new SqlParameter("@duracion", Convert.ToInt32(textDurac.Text)));
            SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
            VariableRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(VariableRetorno);
            con.Open();

            cmd.ExecuteNonQuery();

            switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
            {
                case 0:
                    MessageBox.Show("La descripción especificada ya existe. Introduzca una nueva");
                    con.Close();
                    break;
                case 1:
                    MessageBox.Show("se actualizo la visibilidad correctamente");
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

        private void VentanaModVisibilidad_Load(object sender, EventArgs e)
        {

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

        private void textDurac_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }

        private void textDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
