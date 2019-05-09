using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_and_oredres_sql_project
{
    class orders
    {
        public Customer customer { get; set; }
        public Product product { get; set; }

        public List<Product> ShowMeAllorders(SqlCommand com, Customer cust)
        {
            List<Product> list = new List<Product>();
            com.CommandText = $"SELECT * FROM orders WHERE custID={cust.custId} ; ";
            SqlDataReader reader1 = com.ExecuteReader(CommandBehavior.Default);
            while (reader1.Read() == true)
            {
               
                Product p = new Product
                {
                    name = (string)reader1[2],
                    price = (double)reader1[3],
                    quantity = 0
                };
                list.Add(p);
                Console.WriteLine();
            }
            return list;
        }
        public void NewOrder(SqlCommand com,string productname , int c,Customer cust )
        {
            com.CommandText = $"SELECT * FROM products WHERE productName={productname} ; ";
            SqlDataReader reader1 = com.ExecuteReader(CommandBehavior.Default);
            Product p = new Product();
            while (reader1.Read() == true)
            {
                p.name =(string)reader1[1];
                p.price = (int)reader1[3];
                p.quantity = (int )reader1[4];
            }
            if(p==null)
            {
                Console.WriteLine("not found !!!!!!!!!!");
                return;
            }
            else
            {
                com.CommandText = $"INSERT INTO orders (custID ,item ,totoalcheck) Values ({cust.custId},{p.name},{p.price})";
            }

        }
    }
}

