using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analise_Acoes_NICHOLAS.Models;
using YahooFinanceAPI;

namespace Analise_Acoes_NICHOLAS.AuxiliaryF
{
    public class YahooFinance
    {

        public List<Double> YahooLoadData(string symbol, DateTime start, DateTime end)
        {
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> hps = Historical.Get(symbol, start, end);
            List<Double> Eod = new List<double> { };
            for (int i = 0; i < hps.Count; i++)
            {
                Eod.Add(hps[i].Close);
            }

            return Eod;


        }

        public List<Double> YahooLoadVolume(string symbol, DateTime start, DateTime end)
        {
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> hps = Historical.Get(symbol, start, end);
            List<Double> Volume = new List<double> { };
            for (int i = 0; i < hps.Count; i++)
            {
                Volume.Add(hps[i].Close);
            }

            return Volume;


        }

        public List<DateTime> YahooLoadDates(string symbol, DateTime start, DateTime end)
        {
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> hps = Historical.Get(symbol, start, end);
            List<DateTime> Dates = new List<DateTime> { };

            for (int i = 0; i < hps.Count; i++)
            {
                Dates.Add(hps[i].Date);
            }

            return Dates;

        }
    }
}
