using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace FrbaCommerce.Generar_Publicacion
{
    public partial class Form2 : Form
    {
        Usuario usuario;
        SqlConnection connection;
        int cantRubros;


        public Form2(Usuario user)
        {
            usuario = user;
            InitializeComponent();
            button1.Enabled = false;
            try
            {

            using (connection = new SqlConnection())
            {
            connection.ConnectionString = DAL.LoginDAL.Conectar();
                SqlCommand cmd;
            using (cmd = new SqlCommand("EBAY.getRubros", connection))
            {
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            DataTable dtR = new DataTable();
            SqlDataAdapter daR = new SqlDataAdapter(cmd);

            daR.Fill(dtR);
            cantRubros = dtR.Rows.Count;
            foreach (DataRow row in dtR.Rows)
            {

                checkedListBox1.Items.Add(Convert.ToString(row[0]));
            }
            }

            using (cmd = new SqlCommand("EBAY.getVisibilidad", connection))
            {
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            DataTable dtV = new DataTable();
            SqlDataAdapter daV = new SqlDataAdapter(cmd);

            daV.Fill(dtV);

            foreach (DataRow row in dtV.Rows)
            {

                comboVisi.Items.Add(Convert.ToString(row[0]));
            }
            }
            }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rubros="";
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
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    using (SqlCommand cmd = new SqlCommand("EBAY.generarPublicacion", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@rubros", rubros));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", textDesc.Text));
                        cmd.Parameters.Add(new SqlParameter("@stock", textStock.Text));
                        cmd.Parameters.Add(new SqlParameter("@precio", textPrecio.Text));
                        cmd.Parameters.Add(new SqlParameter("@visibilidad", comboVisi.SelectedItem));
                        if (string.Compare(Convert.ToString(comboTipo.SelectedItem), "Inmediata") == 0)
                            cmd.Parameters.Add(new SqlParameter("@tipo", '0'));
                        else
                            cmd.Parameters.Add(new SqlParameter("@tipo", '1'));

                        if (string.Compare(Convert.ToString(comboEst.SelectedItem), "Activa") == 0)
                            cmd.Parameters.Add(new SqlParameter("@estado", '1'));
                        if (string.Compare(Convert.ToString(comboEst.SelectedItem), "Borrador") == 0)
                            cmd.Parameters.Add(new SqlParameter("@estado", '0'));
                        if (string.Compare(Convert.ToString(comboEst.SelectedItem), "Pausada") == 0)
                            cmd.Parameters.Add(new SqlParameter("@estado", '2'));

                        cmd.Parameters.Add(new SqlParameter("@cod_usuario", usuario.Cod_usuario));
                        cmd.Parameters.Add(new SqlParameter("@fecha_inicio", ConfigurationManager.AppSettings["fecha"]));


                        if (radioButton1.Checked)
                            cmd.Parameters.Add(new SqlParameter("@preguntas", '1'));
                        else
                            cmd.Parameters.Add(new SqlParameter("@preguntas", '0'));
                        cmd.Parameters.Add(VariableRetorno);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                        {
                            case 1:
                                MessageBox.Show("Publicacion generada con éxito");
                                this.Hide();
                                break;
                            case 0:
                                MessageBox.Show("La descripción de la publicación ya existe");
                                break;
                            case 2:
                                MessageBox.Show("El usuario ya posee 3 publicaciones Gratis, cambie la visibilidad");
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

        private void comboEst_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario.validarNum(sender, e);
        }

        private void textStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario.validarNum(sender, e);
        }
        public int HabilitarVentana() //garantiza la creacion de uan visibilidad con los datos necesarios
        {
            if (textStock.Text != "" && textDesc.Text != "" && textPrecio.Text != "" && comboEst.SelectedIndex!=-1 && comboTipo.SelectedIndex!=-1 && comboVisi.SelectedIndex!=-1)
                button1.Enabled = true;
            else button1.Enabled = false; 
            return 0;
        }

        private void textDesc_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void textPrecio_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void comboVisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }
    }
}
