using System;
using System.Collections.Generic;
using YahooFinanceAPI;

namespace Analise_Acoes_NICHOLAS.YahooFinance
{
    public class YahooFinanceF
    {

        public static List<Double> YahooLoadData(string symbol, DateTime start, DateTime end)
        {
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> hps = Historical.Get(symbol, start, end);
            List<Double> eod = new List<double> { };

            foreach (var item in hps)
            {
                eod.Add(item.Close);
            }

            return eod;
        }

        public static List<Double> YahooLoadVolume(string symbol, DateTime start, DateTime end)
        {
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> hps = Historical.Get(symbol, start, end);
            List<Double> volumeSold = new List<double> { };

            foreach (var item in hps)
            {
                volumeSold.Add(item.Volume);
            }

            return volumeSold;
        }

        public static List<DateTime> YahooLoadDates(string symbol, DateTime start, DateTime end)
        {
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> hps = Historical.Get(symbol, start, end);
            List<DateTime> dates = new List<DateTime> { };

            foreach (var item in hps)
            {
                dates.Add(item.Date);
            }
            return dates;
        }

    }
}
