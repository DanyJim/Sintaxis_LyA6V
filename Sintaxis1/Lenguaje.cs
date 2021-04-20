using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintaxis1
{
    /*Requerimiento 1: Separar el nombre del archivo y el Path del archivo 
                       con el objeto Path y ajustar el contructor Lexico(string)*/
    class Lenguaje :Sintaxis
    {
        public Lenguaje()
        {
            Console.WriteLine("Inicia analisis gramatical");
        }
        public Lenguaje(string nombre):base(nombre)
        {
            Console.WriteLine("Inicia analisis gramatical");
        }
        /*
        Programa	-> 	Librerias Main
        Librerias	->	#include<identificador.h>
        Main		->	void main()
                        {
                            numero;
                        }
        */
        public void Programa()
        {
            Librerias();
            Main();
        }
        private void Librerias()
        {
            match("#");
            match("include");
            match("<");
            match(Token.Clasificaciones.Identificador);
            match(".");
            match("h");
            match(">");
        }
        private void Main()
        {

        }
        /*
        
        */
    }
}
