using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_and_oredres_sql_project
{
    public class FormulaDAO
    {
        public double x { get; set; }
        public double y { get; set; }
        public double result { get; set; }
        public string op { get; set; }
        public FormulaDAO()
        {

        }

        public FormulaDAO(int x, int y, int result, string op)
        {
            this.x = x;
            this.y = y;
            this.result = result;
            this.op = op;
        }

        public override string ToString()
        {
            return $"{this.x}{this.op}{this.y}={this.result}";
        }
        
    }
}
