using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_and_oredres_sql_project
{
    class suppliers
    {
        public int supid;
        public string Company { get; set; }
        
        public string UserName { get; set; }
        public string Password { get; set; }
        public suppliers()
        {

        }
        public void InsertTDataBase(SqlCommand com)
        {
            try
            {
                com.CommandText = $"insert into suppliers (username,password,company) values ('{this.UserName}','{this.Password}','{this.Company}' )";
                com.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.Write("the user name already exist ");
                return;
            }
        }
        public suppliers supplierLogin(SqlCommand com, string user, string pass, suppliers s)
        {
            com.CommandText = ($"SELECT * FROM  suppliers  WHERE username={user} and password={pass};");
            SqlDataReader reader1 = com.ExecuteReader(CommandBehavior.Default);
            while (reader1.Read() == true)
            {
                    s.UserName = user;
                    s.Password = pass;
                    s.Company = (string)reader1[3];
                    supid = (int)reader1[0];
                    return s;
                
            }
            return s;
        }
    }
}
