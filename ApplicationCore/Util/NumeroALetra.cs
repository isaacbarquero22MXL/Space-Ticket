using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Util
{
    public static class NumeroALetra
    {
        private static readonly String[] Letras = { "A", "B", "C", "D", "E", "F", "G", "H",
                                                    "I", "J", "K", "L", "M", "N", "O", "P", "Q",
                                                    "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public static String GeLetterBynumber(int numero)
        {
            return Letras[numero - 1];
        }
    }
}
