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

        public List<Company> CarregaListaInitializer(string filename)
           {
            List<string> lista_siglas = new List<string>();
            List<string> lista_Nomes = new List<string>();


            //lista_siglas = ObtainFirstColumn(list_of_companies.filename, 0);
            //lista_Nomes = ObtainFirstColumn(list_of_companies.filename, 1);
            lista_siglas = ObtainFirstColumn(filename, 0);
            lista_Nomes = ObtainFirstColumn(filename, 1);

            List<Company> CompanyList = new List<Company> { };
            for (int i = 1; i < lista_siglas.Count; i++)
            {
                CompanyList.Add(
                    new Company(lista_siglas[i], lista_Nomes[i]));
            }
            lista_Nomes.Clear();
            lista_siglas.Clear();

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
            List<string> lista_siglas = new List<string>();
            
            lista_siglas = ObtainFirstColumn(filename, 0);
            
            
            return lista_siglas;
        }

     public List<Company> CarregaLista (string filename)
        {
            List<string> lista_siglas = new List<string>();
            List<string> lista_Nomes = new List<string>();


            //lista_siglas = ObtainFirstColumn(list_of_companies.filename, 0);
            //lista_Nomes = ObtainFirstColumn(list_of_companies.filename, 1);
            lista_siglas = ObtainFirstColumn(filename, 0);
            lista_Nomes = ObtainFirstColumn(filename, 1);

            List<Company> CompanyList = new List<Company> { };
            for (int i = 1; i < lista_siglas.Count; i++)
            {
                CompanyList.Add(
                    new Company(lista_siglas[i], lista_Nomes[i]));                    
            }
            lista_Nomes.Clear();
            lista_siglas.Clear();
            
            return CompanyList;
        }


        public List<Double> yahooLoadData(string symbol, DateTime start, DateTime end)
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

        public List<DateTime> yahooLoadDates(string symbol, DateTime start, DateTime end)
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

        public List<Double> getStockFeedback(string symbol, DateTime start, DateTime end)
        {            
            List<double> Values = yahooLoadData(symbol, start, end);

            List<double> Feedback = new List<double>();
            Feedback.Add(0);
            for (int i = 1; i < Values.Count();)
            {
                Feedback.Add((Values[i]/Values[i-1]) - 1);
                i++;
            }

            return Feedback;
        }

        public Task<IEnumerable<DataPoint>> GetDatapointsAsync (string symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                List<Double> Eod = yahooLoadData(symbol, start, end);
                List<DateTime> Dates = yahooLoadDates(symbol, start, end);


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

    }
}