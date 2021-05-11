using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintaxis2
{
    // Requerimiento 1: Poder inicializar variables con una expresion matematica
    // Requerimiento 2: La Condicion -> Expresion OperadorRelacional Expresion
    // Requerimiento 3: Implementar la sintaxis del ciclo for
    // Requerimeinto 4: Implementar la sintaxis del ciclo while
    // Requerimiento 5: Implementar la sintaxis del ciclo do while
    class Lenguaje : Sintaxis
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
        /*
        Main		->	void main()
                        {
                            Instrucciones
                        }
        */
        private void Main()
        {
            match(Clasificaciones.TipoDato);
            match("main");
            match("(");
            match(")");
            Bloque_Instrucciones();
        }
        /*
        Bloque_Instrucciones -> {
                                    Intrucciones
                                }
        */
        private void Bloque_Instrucciones()
        {
            match("{");
            Instruccion();
            match("}");
        }
        //Lista__IDs -> Identificador(, Lista_IDs)?
        private void Lista_IDs()
        {
            match(Clasificaciones.Identificador);
            // Requerimiento 1
            if (getContenido() == ",")
            {
                match(",");
                Lista_IDs();
            }
        }
        //Variables -> TipoDato Lista_IDs;
        private void Variables()
        {
            match(Clasificaciones.TipoDato);
            Lista_IDs();
            match(";");
        }
        /*
        Instruccion -> printf(numero | cadena | identificador) | Identificador = numero | cadena | identificador ; 
                       Instruccion?
        */
        private void Instruccion()
        {
            if (getContenido() == "cout")
            {
                match("cout");
                Flujo_Salida();
                match(";");
            }
            else if (getContenido() == "cin")
            {
                match("cin");
                match(Clasificaciones.FlujoEntrada);//>>
                match(Clasificaciones.Identificador);
                match(";");
            }
            else if (getContenido() == "if")
            {
                If();
            }
            else if (getContenido() == "const")
            {
                Constante();
            }
            else if (getClasificacion() == Clasificaciones.TipoDato)
            {
                Variables();
            }
            else
            {
                match(Clasificaciones.Identificador);
                match("=");
                if (getClasificacion() == Clasificaciones.Cadena)
                {
                    match(Clasificaciones.Cadena);
                }
                else
                {
                    Expresion();
                }
                match(";");
            }
            if (getClasificacion() != Clasificaciones.FinBloque)
            {
                Instruccion();
            }
        }
        //Constante -> const TipoDato Identificador = Numero | Cadena
        private void Constante()
        {
            match("const");
            match(Clasificaciones.TipoDato);
            match(Clasificaciones.Identificador);
            match("=");
            if (getClasificacion() == Clasificaciones.Numero)
            {
                match(Clasificaciones.Numero);
            }
            else
            {
                match(Clasificaciones.Cadena);
            }
            match(";");
        }
        //Flujo_Salida -> << Identificador | Numero | Cadena Flujo_Salida?
        private void Flujo_Salida()
        {
            match(Clasificaciones.FlujoSalida);//<<
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
            if (getClasificacion() == Clasificaciones.FlujoSalida)//if(getClasificion()==Calsificaciones.FlujoSalida)
            {
                Flujo_Salida();
            }
        }
        // If -> if(Condicion) Bloque_Instrucciones (else Bloque_Instrucciones)?
        private void If()

        {
            match("if");
            match("(");
            Condicion();
            match(")");
            Bloque_Instrucciones();
            if (getContenido() == "else")
            {
                match("else");
                Bloque_Instrucciones();
            }
        }
        // Condicion -> Identificador OperadorRelacional Identificador
        private void Condicion()
        {
            match(Clasificaciones.Identificador);
            match(Clasificaciones.OperadorRelacional);
            match(Clasificaciones.Identificador);
        }
        // x26 = (3+5)*8 - (10-4)/2
        // Expresion -> Termino MasTermino
        private void Expresion()
        {
            Termino();
            MasTermino();
        }
        // MasTermino -> (OperadorTermino Termino)?
        private void MasTermino()
        {
            if (getClasificacion() == Clasificaciones.OperadorTermino)
            {
                match(Clasificaciones.OperadorTermino);
                Termino();
            }
        }
        // Termino -> Factor PorFactor
        private void Termino()
        {
            Factor();
            PorFactor();
        }
        // PorFactor -> (OperadorFactor Factor)?
        private void PorFactor()
        {
            if (getClasificacion() == Clasificaciones.OperadorFactor)
            {
                match(Clasificaciones.OperadorFactor);
                Factor();
            }
        }
        // Factor -> Identificador | Numero | (Expresion)
        private void Factor()
        {
            if (getClasificacion() == Clasificaciones.Identificador)
            {
                match(Clasificaciones.Identificador);
            }
            else if (getClasificacion() == Clasificaciones.Numero)
            {
                match(Clasificaciones.Numero);
            }
            else
            {
                match("(");
                Expresion();
                match(")");
            }
        }
        // For -> for(Identificador = Expresion; Condicion; Identificador IncrementoTermino) BloqueInstrucciones
        // While -> while(Condicion) BloqueInstrucciones
        // DoWhile -> do BloqueInstrucciones while(Condicion);
    }
}
