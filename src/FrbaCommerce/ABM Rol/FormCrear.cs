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
    public partial class FormCrear : Form
    {
        SqlConnection connection;

        public FormCrear()
        {
            try
            {
                InitializeComponent();
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("EBAY.obtenerFuncionalidades", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        connection.Close();




                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {

                            checkedListBox1.Items.Add(Convert.ToString(row[0]));
                        }

                        connection.Close();
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
            SqlCommand cmd;
            try
            {
                                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();
                    using (cmd = new SqlCommand("EBAY.crearRol", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;


                        cmd.Parameters.Add(new SqlParameter("@nombre", textNom.Text));

                        if (checkedListBox1.GetItemChecked(0))
                            cmd.Parameters.Add(new SqlParameter("@func0", Convert.ToString(checkedListBox1.Items[0])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func0", ""));

                        if (checkedListBox1.GetItemChecked(1))
                            cmd.Parameters.Add(new SqlParameter("@func1", Convert.ToString(checkedListBox1.Items[1])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func1", ""));

                        if (checkedListBox1.GetItemChecked(2))
                            cmd.Parameters.Add(new SqlParameter("@func2", Convert.ToString(checkedListBox1.Items[2])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func2", ""));

                        if (checkedListBox1.GetItemChecked(3))
                            cmd.Parameters.Add(new SqlParameter("@func3", Convert.ToString(checkedListBox1.Items[3])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func3", ""));

                        if (checkedListBox1.GetItemChecked(4))
                            cmd.Parameters.Add(new SqlParameter("@func4", Convert.ToString(checkedListBox1.Items[4])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func4", ""));

                        if (checkedListBox1.GetItemChecked(5))
                            cmd.Parameters.Add(new SqlParameter("@func5", Convert.ToString(checkedListBox1.Items[5])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func5", ""));

                        if (checkedListBox1.GetItemChecked(6))
                            cmd.Parameters.Add(new SqlParameter("@func6", Convert.ToString(checkedListBox1.Items[6])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func6", ""));

                        if (checkedListBox1.GetItemChecked(7))
                            cmd.Parameters.Add(new SqlParameter("@func7", Convert.ToString(checkedListBox1.Items[7])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func7", ""));

                        if (checkedListBox1.GetItemChecked(8))
                            cmd.Parameters.Add(new SqlParameter("@func8", Convert.ToString(checkedListBox1.Items[8])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func8", ""));

                        if (checkedListBox1.GetItemChecked(9))
                            cmd.Parameters.Add(new SqlParameter("@func9", Convert.ToString(checkedListBox1.Items[9])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func9", ""));

                        if (checkedListBox1.GetItemChecked(10))
                            cmd.Parameters.Add(new SqlParameter("@func10", Convert.ToString(checkedListBox1.Items[10])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func10", ""));

                        if (checkedListBox1.GetItemChecked(11))
                            cmd.Parameters.Add(new SqlParameter("@func11", Convert.ToString(checkedListBox1.Items[11])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func11", ""));

                        if (checkedListBox1.GetItemChecked(12))
                            cmd.Parameters.Add(new SqlParameter("@func12", Convert.ToString(checkedListBox1.Items[12])));
                        else
                            cmd.Parameters.Add(new SqlParameter("@func12", ""));


                        cmd.Parameters.Add(VariableRetorno);

                        connection.Open();
                        cmd.ExecuteNonQuery();

                        connection.Close();

                        switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                        {
                            case 0:
                                MessageBox.Show("El nombre del rol ya existe");
                                this.Hide();
                                break;
                            case 1:
                                MessageBox.Show("El rol ha sido creado correctamente");
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

        private void FormCrear_Load(object sender, EventArgs e)
        {

        }
    }
}
