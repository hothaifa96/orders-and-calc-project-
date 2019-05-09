using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_and_oredres_sql_project
{
    class Customer
    {
        public  int custId;
        public string FirstName { get; set; }
        public string  Lastname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public  int CCNumber { get; set; }
        public Customer()
        {

        }
        public void InsertTDataBase(SqlCommand com)
        {
            try
            {
                com.CommandText = $"insert into customers (username,password,firstName,lastName,creditCardNum) values ('{this.UserName}','{this.Password}','{this.FirstName}','{this.Lastname}',{this.CCNumber} )";
                com.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.Write("the user name already exist ");
                return;
            }
        }
        public Customer CustomerLogin(SqlCommand com, string user,string pass,Customer s)
        {
            com.CommandText = ($"SELECT * FROM  customers WHERE username={user} and password={pass};");
            SqlDataReader reader1 = com.ExecuteReader(CommandBehavior.Default);
            while (reader1.Read() == true)
            {
                
                
                    s.UserName = user;
                    s.Password = pass;
                    s.FirstName = (string)reader1[5];
                    s.Lastname = (string)reader1[6];
                    s.CCNumber = (int)reader1[7];
                    custId = (int)reader1[2];
                    return s;
                
            }
            return s; 
        }

    }
}
