using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sintaxis1
{
    class Lexico:Token,IDisposable
    {
        StreamReader archivo;
        StreamWriter bitacora;
        const int F = -1;
        const int E = -2;
        int linea, caracter;
        int[,] TRAND6V =
        {
            //WS,EF, L, D, ., E, +, -, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?,La, {, },#10
            {  0, F, 1, 2,29, 1,19,20, 8, 9,11,12,13,14,17,18,22,32,22,24,26,28,29,30,31, 0 },//Estado 0
            {  F, F, 1, 1, F, 1, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 1
            {  F, F, F, 2, 3, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 2
            {  E, E, E, 4, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, F, F, E },//Estado 3
            {  F, F, F, 4, F, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 4
            {  E, E, E, 7, E, E, 6, 6, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, F, F, F },//Estado 5
            {  E, E, E, 7, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, F, F, F },//Estado 6
            {  F, F, F, 7, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 7
            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 8
            {  F, F, F, F, F, F, F, F,10, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 9
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 10
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 11
            {  F, F, F, F, F, F, F, F, F, F, F,15, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 12
            {  F, F, F, F, F, F, F, F, F, F, F, F,15, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 13
            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 14
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 15
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 16
            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 17
            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F },//Estado 18
            {  F, F, F, F, F, F,21, F,21, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 19
            {  F, F, F, F, F, F, F,21,21, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 20
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 21
            {  F, F, F, F, F, F, F, F,23, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 22
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 23
            { 24, E,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,25,24,24,24, F, F,24 },//Estado 24
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 25
            { 26, E,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,27,26,26, F, F,26 },//Estado 26
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 27
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 28
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 29
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 30
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//Estado 31
            {  F, F, F, F, F, F, F, F,23, F, F, F, F, F, F, F,34,33, F, F, F, F, F, F, F, F },//Estado 32
            { 33, 0,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33, 0 },//Estado 33
            { 34, E,34,34,34,34,34,34,34,34,34,34,34,34,34,34,35,34,34,34,34,34,34,34,34,34 },//Estado 34
            { 34, E,34,34,34,34,34,34,34,34,34,34,34,34,34,34,35, 0,34,34,34,34,34,34,34,34 },//Estado 35
            //WS,EF, L, D, ., E, +, -, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?,La, {, },#10
        };
        public Lexico()
        {
            Console.WriteLine("Compilando el archivo Prueba.txt...");
            if (File.Exists("C:\\Archivos\\Prueba.txt"))
            {
                //string fecha = 
                //string hora = ;
                linea = caracter = 1;
                archivo = new StreamReader("C:\\Archivos\\Prueba.txt");
                bitacora = new StreamWriter("C:\\Archivos\\Prueba.log");
                bitacora.AutoFlush = true;
                bitacora.WriteLine("Archivo: Prueba.txt");
                bitacora.WriteLine("Directorio: C:\\Archivos");
                bitacora.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss"));
            }
            else
            {
                throw new Exception("El archivo Prueba.txt no existe");
            }
        }
        //~Lexico()
        public void Dispose()
        {
            CerrarArchivos();
            Console.WriteLine("Finaliza compilacion de Prueba.txt");
        }
        private void CerrarArchivos()
        {
            archivo.Close();
            bitacora.Close();
        }
        public void NextToken()
        {
            int estado = 0;
            char c;
            string palabra = "";

            while (estado >= 0)
            {
                c = (char)archivo.Peek();
                estado = TRAND6V[estado, Columna(c)];
                Clasifica(estado);
                if (estado >= 0)
                {
                    archivo.Read();
                    caracter++;
                    if (c == 10)
                    {
                        linea++;
                        caracter = 1;
                    }
                    if (estado > 0)
                        palabra += c;
                    else
                        palabra = "";
                }
            }
            setContenido(palabra);
            if (getClasificacion() == Clasificaciones.Identificador)
            {
                switch (getContenido())
                {
                    case "char":
                    case "int":
                    case "float":
                        setClasificacion(Clasificaciones.TipoDato);
                        break;
                    case "private":
                    case "protected":
                    case "public":
                        setClasificacion(Clasificaciones.Zona);
                        break;
                    case "if":
                    case "else":
                    case "switch":
                        setClasificacion(Clasificaciones.Condicion);
                        break;
                    case "for":
                    case "while":
                    case "do":
                        setClasificacion(Clasificaciones.Ciclo);
                        break;
                }
            }
            if (estado == E)
            {
                string mensaje = "Error Lexico Linea " + linea + " Caracter " + caracter + " : " + (mensaje = Error(palabra));
                bitacora.WriteLine(mensaje);
                throw new Exception(mensaje);
            }
            else if (palabra != "")
            {
                bitacora.WriteLine("Token = " + getContenido());
                bitacora.WriteLine("Clasificacion = " + getClasificacion());
            }
        }
        private int Columna(char t)
        {
            //WS,EF, L, D, ., E, +, -, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?,La, {, },#10
            if (FinArchivo())
                return 1;
            if (t == 10)
                return 25;
            if (char.IsWhiteSpace(t))
                return 0;
            if (char.ToLower(t) == 'e')
                return 5;
            if (char.IsLetter(t))
                return 2;
            if (char.IsDigit(t))
                return 3;
            if (t == '.')
                return 4;
            if (t == '+')
                return 6;
            if (t == '-')
                return 7;
            if (t == '=')
                return 8;
            if (t == ':')
                return 9;
            if (t == ';')
                return 10;
            if (t == '&')
                return 11;
            if (t == '|')
                return 12;
            if (t == '!')
                return 13;
            if (t == '>')
                return 14;
            if (t == '<')
                return 15;
            if (t == '*')
                return 16;
            if (t == '/')
                return 17;
            if (t == '%')
                return 18;
            if (t == '"')
                return 19;
            if (t == '\'')
                return 20;
            if (t == '?')
                return 21;
            if (t == '{')
                return 23;
            if (t == '}')
                return 24;
            return 22;
            //WS,EF, L, D, ., E, +, -, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?,La, {, },#10
        }
        private void Clasifica(int estado)
        {
            switch (estado)
            {
                case 1:
                    setClasificacion(Clasificaciones.Identificador);
                    break;
                case 2:
                    setClasificacion(Clasificaciones.Numero);
                    break;
                case 8:
                    setClasificacion(Clasificaciones.Asignacion);
                    break;
                case 9:
                case 12:
                case 13:
                case 29:
                    setClasificacion(Clasificaciones.Caracter);
                    break;
                case 10:
                    setClasificacion(Clasificaciones.Inicializacion);
                    break;
                case 11:
                    setClasificacion(Clasificaciones.FinSentencia);
                    break;
                case 14:
                case 15:
                    setClasificacion(Clasificaciones.OperadorLogico);
                    break;
                case 16:
                case 17:
                case 18:
                    setClasificacion(Clasificaciones.OperadorRelacional);
                    break;
                case 19:
                case 20:
                    setClasificacion(Clasificaciones.OperadorTermino);
                    break;
                case 21:
                    setClasificacion(Clasificaciones.IncrementoTermino);
                    break;
                case 22:
                case 32:
                    setClasificacion(Clasificaciones.OperadorFactor);
                    break;
                case 23:
                    setClasificacion(Clasificaciones.IncrementoFactor);
                    break;
                case 24:
                case 26:
                    setClasificacion(Clasificaciones.Cadena);
                    break;
                case 28:
                    setClasificacion(Clasificaciones.Ternario);
                    break;
                case 30:
                    setClasificacion(Clasificaciones.InicioBloque);
                    break;
                case 31:
                    setClasificacion(Clasificaciones.FinBloque);
                    break;
            }
        }
        public string Error(string palabra)
        {
            string mensaje = "";
            if (palabra.EndsWith("."))
                mensaje = "Sintaxis del numero incorrecta, Falta digito";
            if (palabra.EndsWith("E") || palabra.EndsWith("e") || palabra.EndsWith("+") || palabra.EndsWith("-"))
                mensaje = "Sintaxis del numero incorrecta, Falta parte exponencial";
            if (palabra.StartsWith("\"") || palabra.StartsWith("'"))
                mensaje = "Sintaxis de la cadena incorrecta, Cerrar Cadena";
            if (palabra.StartsWith("/"))
                mensaje = "Sintaxis incorrecta, Cerrar Comentario";
            return mensaje;
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}
