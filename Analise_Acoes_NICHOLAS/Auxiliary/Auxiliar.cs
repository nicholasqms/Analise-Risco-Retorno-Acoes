using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Analise_Acoes_NICHOLAS.Models;
using YahooFinanceAPI;

namespace Analise_Acoes_NICHOLAS.AuxiliaryF
{            
    static class list_of_companies
    {
        public const string filename = "companylist.csv";
    }
    public class Auxiliary
    {

        public List<Company> ListInitializer(string filename)
           {
            List<string> symbolList = new List<string>();
            List<string> nameList = new List<string>();

            symbolList = ObtainFirstColumn(filename, 0);
            nameList = ObtainFirstColumn(filename, 1);

            List<Company> CompanyList = new List<Company> { };
            for (int i = 1; i < symbolList.Count; i++)
            {
                CompanyList.Add(
                    new Company(symbolList[i], nameList[i]));
            }
            nameList.Clear();
            symbolList.Clear();

            return CompanyList;
        }


        public List<string> ObtainFirstColumn(string filename, int i)
        {
            List<string> lista = new List<string>();
            using (var reader = new StreamReader(@filename))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine(); // Lê a linha do .CSV
                var values = line.Split(','); // Lê cada entrada do .CSV separada por vírgula
              
                lista.Add(values[i].Trim(new Char[] { '"' })); // Adiciona o termo lido, sem as aspas do padrão .CSV
               
            }
        }
        return lista;
        }
    

        public List<string> CarregaSiglas (string filename)
        {
            List<string> symbolList = new List<string>();
            
            symbolList = ObtainFirstColumn(filename, 0);
                        
            return symbolList;
        }

         public List<Company> CarregaLista (string filename)
            {
                List<string> symbolList = new List<string>();
                List<string> nameList = new List<string>();

                symbolList = ObtainFirstColumn(filename, 0);
                nameList = ObtainFirstColumn(filename, 1);

                List<Company> CompanyList = new List<Company> { };
                for (int i = 1; i < symbolList.Count; i++)
                {
                    CompanyList.Add(
                        new Company(symbolList[i], nameList[i]));                    
                }
                nameList.Clear();
                symbolList.Clear();
            
                return CompanyList;
         }


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
            List<Double> VolumeSold = new List<double> { };
            for (int i = 0; i < hps.Count; i++)
            {
               VolumeSold.Add(hps[i].Volume);
            }

            return VolumeSold;


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

        public List<Double> GetStockFeedback(string symbol, DateTime start, DateTime end)
        {            
            List<double> Values = YahooLoadData(symbol, start, end);

            List<double> Feedback = new List<double>();
            Feedback.Add(0);
            for (int i = 1; i < Values.Count();)
            {
                Feedback.Add((Values[i]/Values[i-1]) - 1);
                i++;
            }

            return Feedback;
        }

        public List<Double> GetLogFeedback (string symbol, DateTime start, DateTime end)
        {
            List<double> Values = GetStockFeedback(symbol, start, end);
            List<double> logFeedback = new List<double>();
            
            for (int i = 1; i < Values.Count();)
            {
                logFeedback.Add(Math.Log(Values[i]+1));
                i++;
            }

            return logFeedback;

        }

        public Task<IEnumerable<DataPoint>> GetDatapointsAsync (string symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                List<Double> Eod = YahooLoadData(symbol, start, end);
                List<DateTime> Dates = YahooLoadDates(symbol, start, end);


                List<DataPoint> dataPointsL = new List<DataPoint> { };
                for (int i = 0; i < Eod.Count; i++)
                {
                    dataPointsL.Add(
                        new DataPoint(Dates[i].ToString("yyyy-MM-dd"), Eod[i]));
                }
                IEnumerable<DataPoint> dataPoints = dataPointsL.AsEnumerable();

                return dataPoints;
            });            
        }


        public Task<IEnumerable<DataPoint>> GetFeedbackAsync(string symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                List<Double> Feedback = GetStockFeedback(symbol, start, end);
                List<DateTime> Dates = YahooLoadDates(symbol, start, end);


                List<DataPoint> dataPointsL = new List<DataPoint> { };
                for (int i = 0; i < Feedback.Count; i++)
                {
                    dataPointsL.Add(
                        new DataPoint(Dates[i].ToString("yyyy-MM-dd"), Feedback[i]));
                }
                IEnumerable<DataPoint> dataPoints = dataPointsL.AsEnumerable();

                return dataPoints;
            });
        }

        public Task<IEnumerable<DataPoint>> GetVolumeAsync(string symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                List<Double> Volume = YahooLoadVolume(symbol, start, end);
                List<DateTime> Dates = YahooLoadDates(symbol, start, end);


                List<DataPoint> dataPointsL = new List<DataPoint> { };
                for (int i = 0; i < Volume.Count; i++)
                {
                    dataPointsL.Add(
                        new DataPoint(Dates[i].ToString("yyyy-MM-dd"), Volume[i]));
                }
                IEnumerable<DataPoint> dataPoints = dataPointsL.AsEnumerable();

                return dataPoints;
            });
        }

    }
}