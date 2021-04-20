using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintaxis1
{
    class Sintaxis:Lexico
    {
        public Sintaxis()
        {
            Console.WriteLine("Inicia analisis sintactico");
            NextToken();
        }
        public Sintaxis(string nombre) : base(nombre)
        {
            Console.WriteLine("Inicia analisis sintactico");
            NextToken();
        }
        protected void match(string espera)
        {
            if (espera == getContenido())
            {
                NextToken();
            }
            else
            {
                throw new Exception("Error de Sintaxis: Se espera un " + espera);
            }
        }
        protected void match(Clasificaciones espera)
        {
            if (espera == getClasificacion())
            {
                NextToken();
            }
            else
            {
                throw new Exception("Error de Sintaxis: Se espera un " + espera);
            }
        }
    }
}
