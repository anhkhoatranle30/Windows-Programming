using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.MyUtils
{
    class SystemPathParser
    {
        public static string UriSourceParse(string Uri)
        {
            string[] seperator = { "file:///" };
            var tokens = Uri.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            var result = tokens[0];
            return result;
        }
    }
}
