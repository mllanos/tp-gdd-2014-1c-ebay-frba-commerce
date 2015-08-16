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
    public partial class FormUpdateDescPausada : Form
    {
        int cod_pub5;
        SqlConnection  con;
        public FormUpdateDescPausada(int cod_pub)
        {
            cod_pub5 = cod_pub;
            InitializeComponent();
            button1.Enabled = false;
            con = new SqlConnection();
            con.ConnectionString = DAL.LoginDAL.Conectar();
            SqlCommand cmd = new SqlCommand("EBAY.getDescYStock", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cod_publicacion", cod_pub5));
            con.Open();
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textDesc.Text= dr.GetValue(0).ToString();
               
            }
            con.Close();
        }

        private void FormUpdateDescPausada_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("EBAY.updatePubDesc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cod_publicacion", cod_pub5));
            cmd.Parameters.Add(new SqlParameter("@descripcion", textDesc.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Descripcion actualizada");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textDesc.Text != "")
                button1.Enabled = true;
            else button1.Enabled = false;
        }
    }
}
