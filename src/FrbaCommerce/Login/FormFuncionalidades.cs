using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.Collections;



namespace FrbaCommerce.Login
{
    public partial class FormFuncionalidades : Form
    {
        System.Windows.Forms.ComboBox comboFunc;
        DataTable result = new DataTable();
        Usuario usuario;
        SqlConnection connection;
        string Rol;

        public FormFuncionalidades(string RolP, Usuario user)
        {
            usuario = user;
            Rol = RolP;
            InitializeComponent();
            
            try
            {
                using (connection = new SqlConnection())
                {
                    connection.ConnectionString = DAL.LoginDAL.Conectar();

                    using (SqlCommand cmd = new SqlCommand("EBAY.getCodRol", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@nombreRol", Rol));
                        SqlParameter VariableRetorno = new SqlParameter("@b", SqlDbType.Int);
                        VariableRetorno.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(VariableRetorno);
                        connection.Open();
                        cmd.ExecuteNonQuery();


                        using (SqlCommand command = new SqlCommand("SELECT nombre FROM EBAY.funcion WHERE cod_funcion IN (SELECT cod_funcion FROM EBAY.rol_x_funcion WHERE cod_rol = @cod_rol)", connection))
                        {
                            command.CommandType = CommandType.Text;

                            command.Parameters.Add(new SqlParameter("@cod_rol", cmd.Parameters["@b"].Value));

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
                                    dataRow2[i] = dr.GetValue(i);
                                result.Rows.Add(dataRow2);
                            }

                            connection.Close();
                            InitializeComboBox();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            
        }

        private void InitializeComboBox()
        {
            comboFunc = new System.Windows.Forms.ComboBox();
            this.comboFunc.Location = new System.Drawing.Point(20, 60);
            this.comboFunc.Name = "comboBoxFunc";
            this.comboFunc.Size = new System.Drawing.Size(245, 25);
            this.comboFunc.BackColor = System.Drawing.Color.White;
            this.comboFunc.ForeColor = System.Drawing.Color.Black;
            this.Controls.Add(this.comboFunc);
            foreach (DataRow row in result.Rows)
            {
                this.comboFunc.Items.Add(Convert.ToString(row[0]));
            }

            if (Rol== "Cliente" || Rol== "Empresa")
            {
            this.comboFunc.Items.Add("Modificar mis Datos");
            }

      
        }
        private void comboFunc_selectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormFuncionalidades_Load(object sender, EventArgs e)
        {

        }

        private void btnSig_Click(object sender, EventArgs e)
        {
            
            switch (Convert.ToString(comboFunc.SelectedItem))
            {
                case "Generar Publicacion":
                    {

                        Generar_Publicacion.Form2 GenerarPub = new Generar_Publicacion.Form2(usuario);
                        GenerarPub.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "Editar Publicacion":
                    {
                        Editar_Publicacion.FormModificarPub EditarPub = new Editar_Publicacion.FormModificarPub(usuario.Cod_usuario);
                        EditarPub.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "Gestion de preguntas":
                    {
                        Gestion_de_Preguntas.GestionDePreguntas GestionDePreg = new Gestion_de_Preguntas.GestionDePreguntas(usuario);
                        GestionDePreg.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "Comprar/Ofertar":
                    {
                        Comprar_Ofertar.ComprarOfertar ComprarOfertar = new Comprar_Ofertar.ComprarOfertar(usuario);
                        ComprarOfertar.ShowDialog();
                        this.Hide();
                        break;
                    }

                case "ABM de cliente":
                    {
                        Abm_Cliente.MenuABMCliente AbmCliente = new Abm_Cliente.MenuABMCliente();
                        AbmCliente.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "ABM de empresa":
                    {
                        Abm_Empresa.MenuABMEmpresa AbmEmpresa = new Abm_Empresa.MenuABMEmpresa();
                        AbmEmpresa.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "ABM de rol":
                    {
                        ABM_Rol.Form1 AbmRol = new ABM_Rol.Form1();
                        AbmRol.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "Registro usuario":
                    {
                        Registro_de_Usuario.Form1 RegistroDeUsuario = new Registro_de_Usuario.Form1();
                        RegistroDeUsuario.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "ABM de rubro":
                    {
                        Abm_Rubro.Form1 AbmRubro = new Abm_Rubro.Form1();
                        AbmRubro.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "ABM de visibilidad de publicacion":
                    {
                        Abm_Visibilidad.VentanaMenuVisibilidad AbmVis = new Abm_Visibilidad.VentanaMenuVisibilidad();
                        AbmVis.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "Historial del cliente":
                    {
                        Historial_Cliente.HistorialCliente HistorialCliente = new Historial_Cliente.HistorialCliente(usuario);
                        HistorialCliente.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "Calificar vendedor":
                    {
                        Calificar_Vendedor.CalificarVendedor CalifVendedor = new Calificar_Vendedor.CalificarVendedor(usuario);
                        CalifVendedor.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "Facturar Publicaciones":
                    {
                        Facturar_Publicaciones.FacturarPublicaciones FacturarPub = new Facturar_Publicaciones.FacturarPublicaciones(usuario);
                        FacturarPub.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "Listado estadístico":
                    {
                        Listado_Estadistico.ListadoEstadistico ListEstadistico = new Listado_Estadistico.ListadoEstadistico();
                        ListEstadistico.ShowDialog();
                        this.Hide();
                        break;
                    }
                case "Modificar mis Datos":
                    {
                        try
                        {
                        using (connection = new SqlConnection())
                        {
                            connection.ConnectionString = DAL.LoginDAL.Conectar();
                            connection.Open();
                            SqlCommand cmd;
                            if (Rol == "Cliente")
                            {
                                cmd = new SqlCommand("SELECT nombre, apellido, mail,dni,tipo_dni FROM EBAY.cliente WHERE cod_usuario=@cod_usuario", connection);

                            }
                            else
                            {
                                cmd = new SqlCommand("SELECT razon_social, cuit, mail FROM EBAY.empresa WHERE cod_usuario=@cod_usuario", connection);

                            }

                            cmd.Parameters.AddWithValue("@cod_usuario", usuario.Cod_usuario);

                            cmd.CommandType = CommandType.Text;


                            cmd.ExecuteNonQuery();
                            connection.Close();

                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);

                            da.Fill(dt);

                            if (Rol == "Cliente")
                            {
                                Abm_Cliente.FormModif FormModCliente = new Abm_Cliente.FormModif(dt.Rows[0], "Cliente");
                                FormModCliente.ShowDialog();
                                this.Hide();
                            }
                            else
                            {
                                Abm_Empresa.FormMod FormModEmp = new Abm_Empresa.FormMod(dt.Rows[0], "Empresa");
                                FormModEmp.ShowDialog();
                                this.Hide();
                            }
                        }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error: " + ex.Message);
                        }
                        break;
                        
                    }
            }

        }
    }
}
