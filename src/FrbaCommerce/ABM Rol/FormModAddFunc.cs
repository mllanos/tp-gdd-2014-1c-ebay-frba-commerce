using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace FrbaCommerce.ABM_Rol
{
    public partial class FormModAddFunc : Form
    {
        string rol;
        SqlConnection connection;

        public FormModAddFunc(string rolP)
        {
            rol = rolP;
            InitializeComponent();
            try
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    connection.Open();
                    SqlCommand cmd;
                    DataTable tablaFunc;
                    using (cmd = new SqlCommand("EBAY.obtenerFuncionalidades", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                        tablaFunc = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tablaFunc);
                    }
                        using (cmd = new SqlCommand("EBAY.getFuncRol", connection))
                        {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@rol", rol));
                        cmd.ExecuteNonQuery();

                        DataTable tablaFuncRol = new DataTable();
                        SqlDataAdapter daR = new SqlDataAdapter(cmd);

                        daR.Fill(tablaFuncRol);

                        connection.Close();
                        int yaExiste;
                        foreach (DataRow row in tablaFunc.Rows)
                        {
                            yaExiste = 0;
                            foreach (DataRow row2 in tablaFuncRol.Rows)
                            {

                                if ((String.Compare(Convert.ToString(row[0]), Convert.ToString(row2[0]))) == 0)
                                {
                                 //   MessageBox.Show("YA ESTA EN EL ROL " + Convert.ToString(row[0]));
                                    yaExiste = 1;
                                    break;
                                }

                            }
                            if (yaExiste == 0)
                            {
                               // MessageBox.Show("NO ESTA EN el rol " + Convert.ToString(row[0]));

                                comboBox1.Items.Add(Convert.ToString(row[0]));
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("EBAY.addFunc", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@func", comboBox1.SelectedItem));
                        cmd.Parameters.Add(new SqlParameter("@rol", rol));


                        cmd.ExecuteNonQuery();
                        this.Hide();
                        MessageBox.Show("Funcionalidad Agregada correctamente");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        private void FormModFunc_Load(object sender, EventArgs e)
        {

        }
    }
}
