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
        //Programa	-> 	Librerias Main
        public void Programa()
        {
            Librerias();
            Main();
        }
        //Librerias	->	#include<identificador.h> Librerias?
        private void Librerias()//0 o más veces
        {
            if (getContenido() == "#")
            {
                match("#");
                match("include");
                match("<");
                match(Token.Clasificaciones.Identificador);
                match(".");
                match("h");
                match(">");
            
                Librerias();
            }           
        }
        private void Librerias2()//1 o más veces 
        {
            match("#");
            match("include");
            match("<");
            match(Token.Clasificaciones.Identificador);
            match(".");
            match("h");
            match(">");
            if (getContenido() == "#")
            {
                Librerias();
            }

        }
        /*
        Main		->	void main()
                        {
                            Variables?
                            Identificador = numero;
                        }
        */
        private void Main()
        {
            match("void");
            match("main");
            match("(");
            match(")");
            match("{");
            if (getClasificacion() == Clasificaciones.TipoDato)
            {
                Variables();
            }
            match(Clasificaciones.Identificador);
            match("=");
            match(Clasificaciones.Numero);
            match(";");
            match("}");
        }
        //Lista__IDs -> Identificador(, Lista_IDs)?
        private void Lista_IDs()
        {
            match(Clasificaciones.Identificador);
            if (getContenido()==",")
            {
                match(",");
                Lista_IDs();
            }
        }
        //Variables -> TipoDato Lista_IDs; Variables?
        private void Variables()
        {
            match(Clasificaciones.TipoDato);
            Lista_IDs();
            match(";");
            if (getClasificacion()==Clasificaciones.TipoDato)
            {
                Variables();
            }
        }
    }
}
