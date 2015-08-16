using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;


namespace FrbaCommerce.Login
{
   
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            btnAcept.Enabled = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAcept_Click(object sender, EventArgs e)
        {
            if (textUser.Text != null && textPass.Text != null)
            {
               // try 
               // {
                using (SqlConnection connection = new SqlConnection())
                {
                connection.ConnectionString = DAL.LoginDAL.Conectar();
                using (SqlCommand cmd = new SqlCommand("EBAY.login", connection))
                {

                cmd.CommandType = CommandType.StoredProcedure;

                //[Instanciando parámetro de Salida.]
                SqlParameter VariableRetorno = new SqlParameter("@a", SqlDbType.Int);
                VariableRetorno.Direction = ParameterDirection.Output;

                string pass = sha256encrypt(textPass.Text);

                cmd.Parameters.Add(new SqlParameter("@usuario", textUser.Text));
                cmd.Parameters.Add(new SqlParameter("@password", pass));
                cmd.Parameters.Add(VariableRetorno);

                connection.Open();
                cmd.ExecuteNonQuery();

                switch (Convert.ToInt32(cmd.Parameters["@a"].Value))
                    {
                        case 4:
                            {
                                MessageBox.Show("No Existe el usuario");
                                connection.Close();
                                break;
                            }
                        case 2:
                            {
                                MessageBox.Show("Contraseña incorrecta");
                                connection.Close();
                                break;
                            }
                        case 3:
                            {
                                connection.Close();
                                MessageBox.Show("Se ingreso correctamente");
                                Usuario user = new Usuario();


                                user.Cod_usuario = DAL.LoginDAL.getCodUser(textUser.Text, connection);

                            
                                //STORED PROCEDURE QUE ME DE LOS ROLES DE EL USUARIO
                                DataTable result = new DataTable();
                                using (SqlCommand command = new SqlCommand("SELECT r.nombre, r.cod_rol FROM (SELECT cod_rol FROM EBAY.rol_x_usuario WHERE cod_usuario=@cod_usuario) as u, EBAY.rol as r WHERE r.cod_rol = u.cod_rol", connection))
                                {
                                command.Parameters.Add(new SqlParameter("@cod_usuario", user.Cod_usuario));
                                command.CommandType = CommandType.Text;
                                connection.Open();
                                SqlDataReader dr = command.ExecuteReader();
                                int cant = dr.FieldCount;
                                DataTable dtSchema = dr.GetSchemaTable();
                                if (dtSchema != null)
                                {
                                    for (int i = 0; i < dtSchema.Rows.Count; i++)
                                    {

                                        DataRow dataRow = dtSchema.Rows[i];
                                        DataColumn column = new DataColumn(dataRow["ColumnName"].ToString(), (Type)dataRow["DataType"]);
                                        result.Columns.Add(column);
                                    }
                                }

                                while (dr.Read())
                                {
                                    DataRow dataRow2 = result.NewRow();
                                    for (int i = 0; i < dr.FieldCount; i++)
                                    {
                                        dataRow2[i] = dr.GetValue(i);


                                    }
                                    result.Rows.Add(dataRow2);
                                }

                                connection.Close();

                                if (result.Rows.Count == 1)
                                {
                                    //CONSULTAS DE LA FUNCIONALIDADES DE ESTE ROLL.
                                    user.Rol = Convert.ToString(result.Rows[0][0]);
                                    user.IdRol = Convert.ToInt32(result.Rows[0][1]);
                                    Login.FormFuncionalidades formFunc = new Login.FormFuncionalidades(Convert.ToString(result.Rows[0][0]), user);
                                    this.Hide();
                                    formFunc.ShowDialog();
                                    
                                }
                                else
                                {
                                    if (result.Rows.Count == 0)
                                    {
                                        MessageBox.Show("ningun Rol Asignado");
                                    }
                                    else
                                    {
                                        //Formulario para elegir el rol para ingresar FALTA CORROBORRAR QUE NO HAYA ROLES ASIGNADOS 
                                        Login.FormRol FormRol = new Login.FormRol(result, user); //USER SIN CARGAR EL ROL PORQUE TODAVIA NO LO ELIGIO
                                        this.Hide();
                                        FormRol.ShowDialog();
                                       
                                    }
                                }

                                }
                                break;
                            }
                        case 1:
                            {
                                MessageBox.Show("Cantidad limite de fallos alcanzada");
                                connection.Close();
                                break;
                            }
                        case 0:
                            {
                                MessageBox.Show("Usuario inhabilitado");
                                connection.Close();
                                break;
                            }
                        case 5:
                            {
                                connection.Close();
                                Usuario user = new Usuario();

                                user.Cod_usuario = DAL.LoginDAL.getCodUser(textUser.Text, connection);

                                FormModUyP modUserYCont = new Login.FormModUyP(user);
                                modUserYCont.ShowDialog();
                                this.Hide();
                                //Form de modificacion de usuario y contraseña
                                break;
                            }

                    }
                }
                }
               // }
               // catch (Exception ex)
               // {
                //    throw new Exception("Error: " + ex.Message);
                //}

                }
            }
        

        private void textUser_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
  
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {    
            this.Close();             
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

        private void textPass_TextChanged(object sender, EventArgs e)
        {
            HabilitarVentana();
        }

        public int HabilitarVentana() //garantiza la creacion de uan visibilidad con los datos necesarios
        {
            if (textUser.Text != "" && textPass.Text != "")
                btnAcept.Enabled = true;
            else btnAcept.Enabled = false;
            return 0;
        }
        }

    }
