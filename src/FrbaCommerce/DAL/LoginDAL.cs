using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FrbaCommerce.DAL
{
    class LoginDAL
    {

        public static string Conectar()
        {
            return (ConfigurationManager.AppSettings["ConexionBD"].ToString());  
        }

        public static int getCodUser(string user,SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand("EBAY.getCodUser",connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add( new SqlParameter("@usuario", user));
            SqlParameter VariableRetorno = new SqlParameter("@b", SqlDbType.Int);
            VariableRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(VariableRetorno);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            return (Convert.ToInt32(cmd.Parameters["@b"].Value));
            
        }
    }
}