using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analise_Acoes_NICHOLAS.AuxiliaryF
{
    public class Operations
    {
        public List<Double> GetStockFeedback ( List <double> Values)
        {
            List<double> Feedback = new List<double>();

            for (int i=1; i < Values.Count(); i++)
            {
                Feedback[i] = (Values[i] / Values[i - 1]) - 1;

            }

            return Feedback;
        }
    }
}
