using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_and_oredres_sql_project
{
    class Product
    {
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public List < Product> GetAllProducts(SqlCommand com )
        {
            List<Product> list = new List<Product>();
            com.CommandText = $"SELECT * FROM products WHERE avalable> 0 ; ";
            SqlDataReader reader1 = com.ExecuteReader(CommandBehavior.Default);
            while (reader1.Read() == true)
            {

                Product p = new Product
                {
                    name = (string)reader1[2],
                    price = (double)reader1[3],
                    quantity = (int)reader1[4]
                };
                list.Add(p);
                return (list);
            }
            return list;

        }

    }
}
