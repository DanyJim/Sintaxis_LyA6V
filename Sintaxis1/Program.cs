using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintaxis1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (Lenguage l = new Lenguage())
                {
                    /*while (!l.FinArchivo())
                    {
                        l.NextToken();
                    }*/
                    l.match("#");
                    l.match("include");
                    l.match("<");
                    l.match(Token.Clasificaciones.Identificador);
                    l.match(".");
                    l.match("h");
                    l.match(">");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
