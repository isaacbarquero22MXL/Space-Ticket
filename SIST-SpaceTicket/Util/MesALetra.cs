using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIST_SpaceTicket.Util
{
    public class MesALetra
    {
        private static readonly String [] Meses = {"ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL",
                                            "AGO", "SET", "OCT", "NOV", "DEC" }; 

        public static String GetMesAbreviadoByNumber(int mes)
        {
            return Meses[mes - 1];
        }
    }
}