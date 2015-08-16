using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace FrbaCommerce.DAL
{
    class RegistroDAL
    {
        public static int verificarUsuario(string pass, string user)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = DAL.LoginDAL.Conectar();
            string queryString = "SELECT usuario FROM EBAY.usuario WHERE usuario=@usuario;";
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("usuario", user);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read()==true)
            {
                return 1; //ya existe el usuario
            }
            else
            {
               SqlCommand comando = new SqlCommand(string.Format("INSERT INTO EBAY.usuario (usuario,password,pub_finalizadas,cod_factura,estado,cant_fallos) VALUES ('{0}','{1}',0,0,1,0)",user,pass),connection);
               reader.Close();
                comando.ExecuteNonQuery();
                connection.Close();
                return 0; //usuario creado
            }

        }
}
}