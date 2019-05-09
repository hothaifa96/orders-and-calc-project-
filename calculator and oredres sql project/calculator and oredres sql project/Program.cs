using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_and_oredres_sql_project
{
    class Program
    {
        static void Main(string[] args)
        {
            //calculator();
            orders();
        }

        private static void orders()
        {
            bool flag ;
            int input=0;
            do
            {
                Console.WriteLine("exist customer ? (press 1)");
                Console.WriteLine("new customer ? (press 2)");
                Console.WriteLine(" exist supplier ? (press 3)");
                Console.WriteLine("new supplier ? (press 4)");
                int.TryParse(Console.ReadLine(), out input);
                flag = (input <5||input >1 ? false : true);
                
            } while (flag);
            SqlCommand com = sql_connections.ConnectTo("Orders");
            
            switch (input)
            {
                case (1):
                    ExistCust(com);
                    break;
                case (2):
                    customerRegister(com);
                    break;
                case (3):
                    ExistSup(com);
                    break;
                case (4):
                    supplierRegister(com);
                    break;
                
            }

            com.Connection.Close();
        }

        private static void ExistCust(SqlCommand com)
        {
            Console.WriteLine("Please enter your username :");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter your password :");
            string password = Console.ReadLine();
            Customer cust = new Customer();
            cust = cust.CustomerLogin(com, username, password,cust);
            if (cust!=null)
            {
                Console.WriteLine("Show me my porchacses (1)");
                Console.WriteLine("Show me all products (2)");
                Console.WriteLine("New Order(3)");
                int input =int.Parse(Console.ReadLine());
                switch (input)
                {
                    case (1):
                        orders o = new orders();
                        List<Product> list = o.ShowMeAllorders(com, cust);
                        foreach(Product s in list)
                        {
                            Console.WriteLine(s);
                        }
                        break;
                    case (2):
                        Product r = new Product();
                        List<Product> l =r.GetAllProducts(com);
                        foreach (Product t in l)
                        {
                            Console.WriteLine(t);
                        }
                        break;
                    case (3):
                        Console.WriteLine("please enter the item name and the counatity :");
                        string productname =Console.ReadLine();
                        int c =int.Parse(Console.ReadLine());
                        orders he = new orders();
                        he.NewOrder(com, productname, c, cust);

                        break;
                }
            }
            Console.WriteLine("the username  and the password dont match !!!!!!!!!");
            return;
        }
        private static void ExistSup(SqlCommand com)
        {
            Console.WriteLine("Please enter your username :");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter your password :");
            string password = Console.ReadLine();
            suppliers sup = new suppliers();
            sup = sup.supplierLogin(com, username, password, sup);
            if (sup != null)
            {
                Console.WriteLine("Add new product (1)");
                Console.WriteLine("Show me all avalabile items (2)");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case (1):
                        Product o = new Product();
                        Console.WriteLine("please enter the product name  : ");
                        o.name =(Console.ReadLine());
                        Console.WriteLine("please enter the product price  : ");
                        o.price = int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the product amounts  : ");
                        o.quantity =int.Parse(Console.ReadLine());
                        com.CommandText = $"INSERT INTO products (productName,supID,price,avalable) VALUES ({o.name},{sup.supid},{o.price},{o.quantity});";
                        break;
                    case (2):
                        com.CommandText = $"SELECT * FROM products WHERE avalable>0 and supID={sup.supid}; ";
                        SqlDataReader reader1 = com.ExecuteReader(CommandBehavior.Default);
                        Product p = new Product();
                        while (reader1.Read() == true)
                        {
                            p.name = (string)reader1[1];
                            p.price = (int)reader1[3];
                            p.quantity = (int)reader1[4];
                            Console.WriteLine(p);
                        }
                        break;
                   
                }
            }
            Console.WriteLine("the username  and the password dont match !!!!!!!!!");
            return;
        }
        private static void customerRegister(SqlCommand com)
        {
            Console.WriteLine("***********Regiusteration***********");
            Customer c = new Customer();
            Console.WriteLine("First name : ");
            c.FirstName =Console.ReadLine();
            Console.WriteLine("Last name : ");
            c.Lastname = Console.ReadLine();
            Console.WriteLine("Username : ");
            c.UserName = Console.ReadLine();
            Console.WriteLine(" Password : ");
            c.Password = Console.ReadLine();
            Console.WriteLine("Credit card number : ");
            c.CCNumber = int.Parse(Console.ReadLine());
            if (c.Password == null || c.UserName == null || c.FirstName == null || c.Lastname == null)
            {
                Console.WriteLine("illegal input ");
                return;
            }
            else
                c.InsertTDataBase(com);
        }
        private static void supplierRegister(SqlCommand com)
        {
            Console.WriteLine("***********Regiusteration***********");
            suppliers s = new suppliers();
            
            Console.WriteLine("Username : ");
            s.UserName = Console.ReadLine();
            Console.WriteLine(" Password : ");
            s.Password = Console.ReadLine();
            Console.WriteLine("Company : ");
            s.Company =Console.ReadLine();
            if (s.Password == null || s.UserName == null )
            {
                Console.WriteLine("illegal input ");
                return;
            }
            else
                s.InsertTDataBase(com);
        }

        private static void calculator()
        {
            #region variables
            string xTableName ="Xs", yTableName="Ys";
            bool flag = true;
            CalcSQLFunc csql = new CalcSQLFunc();
            SqlCommand cmd = sql_connections.ConnectTo("Calculator");
            #endregion
            
            #region Table_Creation
            csql.CreateTable("res", cmd, "res real");
            cmd.CommandText = "INSERT INTO res VALUES (null) ;";
            cmd.ExecuteNonQuery();
            csql.CreateTable("Results", cmd, "x real  , op text , y real  , result real ");
            csql.CreateTable(xTableName, cmd, "x real");
            csql.CreateTable(yTableName, cmd, "y real");
            #endregion
            while (flag)
            {
                Console.WriteLine("please enter the X : ");
                //int x =int.Parse(Console.ReadLine());
                double.TryParse(Console.ReadLine(), out double x);
                if (x > 0)
                {
                    csql.InsertToTable(xTableName, cmd, x.ToString());
                    Console.WriteLine("please enter the Y : ");
                    double.TryParse(Console.ReadLine(), out double y);
                    csql.InsertToTable(yTableName, cmd, y.ToString());
                }
                else flag = !flag;
            
            }// this while recive x and y from the user and insert them into the tables in the DB
            #region opersator's_Table
            csql.CreateTable("Operator", cmd, "Op text ");
            cmd.CommandText = "INSERT INTO Operator VALUES ('p');";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Operator VALUES ('m');";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Operator VALUES ('t');";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Operator VALUES ('d');";
            cmd.ExecuteNonQuery();

            #endregion

            List<FormulaDAO> list = csql.CrossJoin(cmd);
            foreach(FormulaDAO m in list )
            {
                Console.WriteLine(m.ToString());
            }
            cmd.Connection.Close();
            Console.ReadKey();
        }
    }
}
