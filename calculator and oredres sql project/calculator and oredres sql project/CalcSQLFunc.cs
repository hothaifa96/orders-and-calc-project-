using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_and_oredres_sql_project
{
    class CalcSQLFunc
    {
        
        public void CreateTable (string tableName, SqlCommand cmd,string Column)
        {
            cmd.CommandText = $"CREATE  TABLE {tableName} ({Column});";
            cmd.ExecuteNonQuery();
        }
        public void InsertToTable(string tableName, SqlCommand cmd, string  value)
        {
            cmd.CommandText = $"INSERT INTO {tableName} VALUES ({value});";
            cmd.ExecuteNonQuery();
        }
        public List<FormulaDAO> CrossJoin(SqlCommand cmd )
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Results select X.x , O.op,Y.y,R.res from Xs X cross join Operator O cross join Ys Y cross join res R; ";
            cmd.ExecuteNonQuery();
            cmd.CommandText = ("SELECT * FROM  Results;");
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

            List<FormulaDAO> list = new List<FormulaDAO>();
            while (reader.Read() == true)
            {
                FormulaDAO f =new FormulaDAO
                {
                    x = (double )reader["x"],
                    op = (string)reader["op"],
                    y=(double)reader["y"],
                    result=(double)reader["result"]
                };
                switch(f.op)
                {
                    case "p":
                        f.result = f.x + f.y;
                        break;
                    case "m":
                        f.result = f.x - f.y;
                        break;
                    case "t":
                        f.result = f.x * f.y;
                        break;
                    case "d":
                        f.result = f.x / f.y;
                        break;
                }
               cmd.CommandText = $"UPDATE Results SET result ={f.result} where Resluts.X = {f.x} and Resluts.Y{f.y} and Resluts.op='{f.op}' ;";
               cmd.ExecuteNonQuery();
               list.Add(f);
            }
            return list ;
        }
    }
}
