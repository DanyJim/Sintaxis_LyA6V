using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintaxis2
{
    class Lenguaje:Sintaxis
    {
        public Lenguaje()
        {
            Console.WriteLine("Inicia analisis gramatical");
        }
        public Lenguaje(string nombre) : base(nombre)
        {
            Console.WriteLine("Inicia analisis gramatical");
        }
        //Programa	-> 	Librerias Main
        public void Programa()
        {
            Librerias();
            Main();
        }
        //Librerias	->	#include<identificador(.h)?> Librerias?
        private void Librerias()//0 o más veces
        {
            if (getContenido() == "#")
            {
                match("#");
                match("include");
                match("<");
                match(Token.Clasificaciones.Identificador);
                if (getContenido() == ".")
                {
                    match(".");
                    match("h");
                }
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
            if (getContenido() == ".")
            {
                match(".");
                match("h");
            }
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
            Instruccion();
            match("}");
        }
        //Lista__IDs -> Identificador(, Lista_IDs)?
        private void Lista_IDs()
        {
            match(Clasificaciones.Identificador);
            if (getContenido() == ",")
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
            if (getClasificacion() == Clasificaciones.TipoDato)
            {
                Variables();
            }
        }
        /*
        Instruccion -> printf(numero | cadena | identificador) | Identificador = numero | cadena | identificador ; 
                       Instruccion?
        */
        private void Instruccion()
        {
            if (getContenido() == "printf")
            {
                match("printf");
                match("(");
                if (getClasificacion() == Clasificaciones.Numero)
                {
                    match(Clasificaciones.Numero);
                }
                else if (getClasificacion() == Clasificaciones.Cadena)
                {
                    match(Clasificaciones.Cadena);
                }
                else
                {
                    match(Clasificaciones.Identificador);
                }
                match(")");
            }
            else
            {
                match(Clasificaciones.Identificador);
                match("=");
                if (getClasificacion() == Clasificaciones.Numero)
                {
                    match(Clasificaciones.Numero);
                }
                else if (getClasificacion() == Clasificaciones.Cadena)
                {
                    match(Clasificaciones.Cadena);
                }
                else
                {
                    match(Clasificaciones.Identificador);
                }
            }
            match(";");
            if (getClasificacion() == Clasificaciones.Identificador)
            {
                Instruccion();
            }
        }
    }
}
