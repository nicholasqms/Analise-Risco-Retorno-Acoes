using System;
using System.Collections.Generic;
using System.Linq;

using AnaliseAcoesNicholas.YahooFinance;

namespace AnaliseAcoesNicholas.Operations
{
    public class OperationsF
    {
        public static List<Double> GetStockFeedback(string symbol, DateTime start, DateTime end)
        {
            List<double> values = YahooFinanceF.YahooLoadData(symbol, start, end);
            List<double> feedback = new List<double>();

            feedback.Add(0);
            for (int i = 1; i < values.Count();)
            {
                feedback.Add((values[i] / values[i - 1]) - 1);
                i++;
            }

            return feedback;
        }

        public static Dictionary<Double, int> GenerateFeedbackHistogram(List<Double> feedback)
        {
            var binSize = 0.025;
            var min = Math.Round(feedback.Min() * (1 / binSize)) / (1 / binSize);

            var dictHistogram = new Dictionary<Double, int>();
            dictHistogram.Add(min, 0);

            var matchCounter = feedback.Count();
            var bin = min; //set the bin to minimum
            while (matchCounter != 0)
            {
                for (int i = 0; i < feedback.Count(); i++)
                {
                    if ((feedback[i] < (bin + binSize / 2)) && (feedback[i] > (bin - binSize / 2))) //Found a Match
                    {
                        dictHistogram[bin]++; //Increase the value for the matching bin 
                        matchCounter--; //Decrease the Counter                        
                    }
                    else //do nothing
                    {
                    }
                }
                bin = bin + binSize; //Move to the next bin and reset the loop                
                dictHistogram.Add(bin, 0); //Add the next bin to the dictionary
            }
            return dictHistogram;
        }

        public static List<Double> GetLogFeedback(string symbol, DateTime start, DateTime end)
        {
            List<double> values = GetStockFeedback(symbol, start, end);
            List<double> logFeedback = new List<double>();

            for (int i = 1; i < values.Count() - 1;)
            {
                logFeedback.Add(Math.Log(values[i] + 1));
                i++;
            }

            return logFeedback;

        }

    }
}
