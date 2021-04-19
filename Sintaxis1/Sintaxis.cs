using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintaxis1
{
    //Requerimiento 1: Agregar en el archivo log la fecha y la hora
    class Sintaxis:Lexico
    {
        public Sintaxis()
        {
            NextToken();
        }
        public void match(string espera)
        {
            if (espera == getContenido())
            {
                NextToken();
            }
            else
            {
                throw new Exception("Error de Sintaxis: Se espera un "+espera);
            }
        }
        public void match(Clasificaciones espera)
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
