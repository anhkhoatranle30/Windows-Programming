using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_project1
{
    //definition
    public class RecipesQuantity
    {
        public int Total { get; set; }
        public int Default { get; set; }
    }
    //abstract DAO
    public abstract class RecipesQuantityDAO
    {
        public abstract RecipesQuantity GetAll();
    }
    //DAO Text File
    public class RecipesQuantityDAOTextFile : RecipesQuantityDAO
    {
        public override RecipesQuantity GetAll()
        {
            var result = new RecipesQuantity();
            //
            //Read file txt
            //
            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);
            path.Append("Database_quantity.txt");

            var lines = File.ReadAllLines(path.ToString());
            //quantities
            result.Default = int.Parse(lines[0]);
            result.Total = int.Parse(lines[1]);
            return result;
        }
    }
}
