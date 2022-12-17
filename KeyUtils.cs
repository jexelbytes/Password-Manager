using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager
{
    internal class KeyUtils
    {
        public string generateKey(int longitudContraseña, bool letras, bool numero, bool signo, bool operador)
        {
            Random rdn = new Random();
            
            string letrass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numeros = "1234567890";
            string signos = "!¡?¿.,()";
            string operadores = "%$#@&=+-*";

            string caracteres = "";

            if (letras)
                caracteres += letrass;
            
            if (numero)
                caracteres += numeros;
            
            if (signo)
                caracteres += signos;

            if (operador)
                caracteres += operadores;


            int longitud = caracteres.Length;
            
            char letra;
            
            string contraseniaAleatoria = string.Empty;
            
            for (int i = 0; i < longitudContraseña; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                
                contraseniaAleatoria += letra.ToString();
            }

            return contraseniaAleatoria;
        }
    }
}
