using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analise_Acoes_NICHOLAS.Models
{
    public class Closings
    {
        public Closings(double i, DateTime date)
            {
             CloseValue = i;
             Date = date;
            }

        public double CloseValue;
        public DateTime Date;

    }
}
