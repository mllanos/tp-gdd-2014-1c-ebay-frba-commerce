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
    public partial class FormModificarPub : Form
    {
        int codigoU;
        Usuario usuario5 = new Usuario();
        public FormModificarPub(int cod_usuario1)
        {

            codigoU = cod_usuario1;
            InitializeComponent();
            monthCalendar1.Visible = false;  
            textFecha.Enabled = false;

        }
        
        private void FormModificarPub_Load(object sender, EventArgs e)
        {
           
        }

        private void dataGridViewPub_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
 
            int codigo = Convert.ToInt32(dataGridViewPub.CurrentRow.Cells[1].Value);
            if (dataGridViewPub.CurrentRow.Cells[6].Value.Equals("Borrador")) //publicacion borrador
            {
                string tipo1 = dataGridViewPub.CurrentRow.Cells[4].Value.ToString();
                int tipo;
                if (tipo1 == "Subasta")
                    tipo = 1;
                else tipo = 0;
                // = Convert.ToInt32(dataGridViewPub.CurrentRow.Cells[4].Value.Equals(1));
                Editar_Publicacion.FormEditarPub winEditPub = new FormEditarPub(codigoU, codigo, tipo);
                winEditPub.ShowDialog();
            }

            if (dataGridViewPub.CurrentRow.Cells[6].Value.Equals("Publicada")) //publicacion publicada
            {
                if (dataGridViewPub.CurrentRow.Cells[4].Value.Equals("Subasta")) //publicacion subasta
                {
                    Editar_Publicacion.FormUpdateDescPausada winPubPau = new FormUpdateDescPausada(codigo);
                    winPubPau.ShowDialog();
                }
                else //publicacion inmediata
                {
                    Editar_Publicacion.FormCambiarEstadoPub winChangePub = new FormCambiarEstadoPub(codigoU, codigo);
                    winChangePub.ShowDialog();
                    this.Close();
                }
            }
            if (dataGridViewPub.CurrentRow.Cells[6].Value.Equals("Finalizada")) //publicacion finalizada
                MessageBox.Show("publicacion finalizada. No se puede modificar");

            if (dataGridViewPub.CurrentRow.Cells[6].Value.Equals("Pausada")) //publicacion pausada
            {
                MessageBox.Show("Publicacion pausada. No se puede modificar");
            }
           // dataGridViewPub.DataSource = null;
        }




        private void monthcalendar_text(object sender, DateRangeEventArgs e)
        {
            textFecha.Text = e.Start.ToString().Substring(0, e.Start.ToString().Length - 5);
        }

        
        private void buttonBuscar_Click(object sender, EventArgs e)
        {

        }
        
        private void buttonBuscar_Click_1(object sender, EventArgs e) //boton de filtrado
        {
            try
            {
            using (SqlConnection con = new SqlConnection())
            {
            con.ConnectionString = DAL.LoginDAL.Conectar();
           using (SqlCommand cmd = new SqlCommand("EBAY.filtrarPublicacion", con))
           {//procedure que filtra las publicaciones segun los parametros
            cmd.CommandType = CommandType.StoredProcedure;


            if (Convert.ToString(comboTipo.SelectedItem) == "Inmediata")
            {
                cmd.Parameters.Add(new SqlParameter("@tipo", '0'));
            }

            else if (Convert.ToString(comboTipo.SelectedItem) == "Subasta")
            {
                cmd.Parameters.Add(new SqlParameter("@tipo", '1'));
            }
            else cmd.Parameters.Add(new SqlParameter("@tipo",'9'));

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
            else cmd.Parameters.Add(new SqlParameter("@estado", '9'));

            cmd.Parameters.Add(new SqlParameter("@descripcion", textDesc.Text));
            cmd.Parameters.Add(new SqlParameter("@precio", textPrecio.Text));
            if (textFecha.TextLength == 0)
                cmd.CommandText = ("EBAY.filtrarPublicacionS");
            else
                cmd.Parameters.Add(new SqlParameter("@fecha_inicio", Convert.ToDateTime(textFecha.Text).Date));
            cmd.Parameters.Add(new SqlParameter("@cod_usuario", codigoU));
            
            con.Open();
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewPub.DataSource = dt;
           }
            }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;  //para actualizar la fecha en cada click
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            
        }
        private void monthCalendar1_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
    
            this.textFecha.Text = e.Start.ToShortDateString() ;
        }

        private void button2_Click(object sender, EventArgs e) //boton de limpieza
        {
            textFecha.Clear();
            textDesc.Clear();
            textPrecio.Clear();
            dataGridViewPub.DataSource = null;
            comboTipo.SelectedIndex = -1;
            comboEstado.SelectedIndex = -1;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void textPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender, e);
        }
    }
}
