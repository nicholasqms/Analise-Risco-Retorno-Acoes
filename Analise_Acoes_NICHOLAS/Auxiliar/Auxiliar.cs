using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Analise_Acoes_NICHOLAS.Models;
using YahooFinanceAPI;

namespace Analise_Acoes_NICHOLAS.Auxiliar
{            
    static class list_of_companies
    {
        public const string filename = "companylist.csv";
    }
    public class Auxiliares
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
                //{ 
                //    ID = i,
                //    Sigla = lista_siglas[i],
                //    Nome = lista_Nomes[i]
                //}
                //);

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


        //public List<Company> CarregaListaPath(string filename)
        //{
        //    List<string> lista_siglas = new List<string>();
        //    List<string> lista_Nomes = new List<string>();


        //    //lista_siglas = ObtainFirstColumn(list_of_companies.filename, 0);
        //    //lista_Nomes = ObtainFirstColumn(list_of_companies.filename, 1);
        //    lista_siglas = ObtainFirstColumn(filename, 0);
        //    lista_Nomes = ObtainFirstColumn(filename, 1);

        //    List<Company> CompanyList = new List<Company> { };
        //    for (int i = 1; i < lista_siglas.Count; i++)
        //    {
        //        string path = lista_siglas[i] + "start=2017-08-01&end=2017-10-06";
        //        CompanyList.Add(                     
        //        new Company(i, lista_siglas[i], lista_Nomes[i],path));
        //        //{ 
        //        //    ID = i,
        //        //    Sigla = lista_siglas[i],
        //        //    Nome = lista_Nomes[i]
        //        //}
        //        //);

        //    }
        //    lista_Nomes.Clear();
        //    lista_siglas.Clear();

        //    return CompanyList;
        //}

        //public List<String> CarregaListaPath(string filename)
        //{
        //    List<string> lista_siglas = new List<string>();
        //    lista_siglas = ObtainFirstColumn(filename, 0);
        //    List <string> CompanyListPath = new List<string>();
        //    for (int i = 1; i < lista_siglas.Count; i++)
        //    {
        //        string path = lista_siglas[i] + "start=2017-08-01&end=2017-10-06";
        //        CompanyListPath.Add((string)path);
            
        //}
        //    lista_siglas.Clear();

        //    return CompanyListPath;
        //}

public List<Double> CarregaDadosYahoo(string symbol)
        {
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> hps = Historical.Get(symbol, DateTime.Now.AddMonths(-1), DateTime.Now);
            List<Double> Eod = new List<double> { };
            for (int i = 0; i < hps.Count; i++)
            {
                Eod.Add(hps[i].Close);
            }
            
            return Eod;


        }
        //public List<Double> CarregaFechamentoYahooPeriod(string symbol, int months)
        //{
        //    while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
        //    {
        //        Token.Refresh();
        //    }

        //    List<HistoryPrice> hps = Historical.Get(symbol, DateTime.Now.AddMonths(-(months)), DateTime.Now);
        //    List<Double> Eod = new List<double> { };
            
        //    for (int i = 0; i < hps.Count; i++)
        //    {
        //        Eod.Add(hps[i].Close);
        //    }

        //    return Eod;


        //}
        public List<DateTime> CarregaDatasYahooPeriod(string symbol, int months)
        {
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> hps = Historical.Get(symbol, DateTime.Now.AddMonths(-(months)), DateTime.Now);            
            List<DateTime> Dates = new List<DateTime> { };

            for (int i = 0; i < hps.Count; i++)
            {
                Dates.Add(hps[i].Date);
                //Dates.Add(hps[i].Date.ToString("yyyy,MM,dd");
            }

            
            return Dates;


        }

        public List<Double> CarregaDadosYahooP(string symbol, DateTime start, DateTime end)
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

        public List<DateTime> CarregaDatasYahooP(string symbol, DateTime start, DateTime end)
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
                //Dates.Add(hps[i].Date.ToString("yyyy,MM,dd");
            }


            return Dates;


        }



        //        private string[] companyListNames = ObtainFirstColumn("companylist1.csv");

        //public string[] CompanyListNames { get => CompanyListNames; set {ObtainFirstColumn("companylist1.csv"); } }
    }
}