using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_and_oredres_sql_project
{
    static class sql_connections
    {
        public static SqlCommand ConnectTo (string DBName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection($@"Data Source= DESKTOP-B8BE5NO ; Initial Catalog = {DBName}  ; Integrated Security = True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
    }
}
