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
    public partial class FormCambiarEstadoPub : Form
    {
        Usuario usuario5 = new Usuario();
        int codigoPub, codigoUs,stockIni;
        string desc;
        public FormCambiarEstadoPub(int cod_user, int cod_pub)
        {
            codigoPub = cod_pub;
            codigoUs = cod_user;
            InitializeComponent();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = DAL.LoginDAL.Conectar();
            SqlCommand cmd = new SqlCommand("EBAY.getDescYStock", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cod_publicacion",codigoPub));
            con.Open();
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                desc = dr.GetValue(0).ToString();
                textDesc.Text = desc;
                stockIni = dr.GetInt32(1);
                textStock.Text =Convert.ToString( stockIni);

            }
            con.Close();
        }

        private void FormCambiarEstadoPub_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e) //pausa la publicacion
        {
            try
            {
            using (SqlConnection con = new SqlConnection())
            {
            con.ConnectionString = DAL.LoginDAL.Conectar();
            using (SqlCommand cmd = new SqlCommand("EBAY.pausePublication", con))
            {
            cmd.CommandType = CommandType.StoredProcedure;
            decimal num = (decimal)codigoPub;
            cmd.Parameters.Add(new SqlParameter("@cod_user",Convert.ToDecimal( codigoUs)));
            cmd.Parameters.Add(new SqlParameter("@cod_pub",num));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Publicacion pausada");
            button2.Enabled = false;
                    button1.Enabled = false;
                    textStock.Enabled = false;
                    textDesc.Enabled = false;
                    button3.Enabled = false;

            }
            }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e) //finaliza la publicacion
        {
            try
            {
            using(SqlConnection con = new SqlConnection())
            {
            con.ConnectionString = DAL.LoginDAL.Conectar();
            using (SqlCommand cmd = new SqlCommand("EBAY.endPublication", con))
            {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cod_user",Convert.ToDecimal( codigoUs)));
            cmd.Parameters.Add(new SqlParameter("@cod_pub",Convert.ToDecimal( codigoPub)));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Publicacion finalizada");
            button1.Enabled = false;
            button2.Enabled = false;
            textStock.Enabled = false;
            textDesc.Enabled = false;
            button3.Enabled = false;
            }
            }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = DAL.LoginDAL.Conectar();

                    using (SqlCommand cmd = new SqlCommand("EBAY.updatePubDesc", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.Add(new SqlParameter("@cod_publicacion", codigoPub));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", textDesc.Text));
                        if (stockIni < (Convert.ToInt32(textStock.Text) + 1))
                        {
                            cmd.CommandText = "EBAY.updatePubStockYDesc";
                            cmd.Parameters.Add(new SqlParameter("@stock", Convert.ToInt32(textStock.Text)));

                            MessageBox.Show("Datos actualizados correctamente");
                        }
                        else
                        {
                            MessageBox.Show("Descripcion actualizada correctamente. El stock ingresado debe ser superior al stock anterior");
                        }
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            } 
        }

        private void textStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            usuario5.validarNum(sender,e);
        }

    }
}
