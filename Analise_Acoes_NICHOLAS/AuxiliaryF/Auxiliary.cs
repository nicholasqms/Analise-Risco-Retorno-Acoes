using System;
using System.Collections.Generic;
using System.IO;
using AnaliseAcoesNicholas.Models;


namespace AnaliseAcoesNicholas.AuxiliaryF
{            

    public class Auxiliary
    {

        public static List<Company> ListInitializer(string filename)
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


        public static List<string> ObtainFirstColumn(string filename, int i)
        {
            List<string> list = new List<string>();
            using (var reader = new StreamReader(@filename))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine(); // Read each line from .CSV
                var values = line.Split(','); // Read each entry from .CSV split by ","
              
                list.Add(values[i].Trim(new Char[] { '"' })); // Add the entry, without the "" from .CSV               
            }
        }
        return list;
        }
    

         public static List<Company> ListLoader (string filename)
        {
            List<string> symbolList = new List<string>();
            List<string> nameList = new List<string>();

            symbolList = ObtainFirstColumn(filename, 0);
            nameList = ObtainFirstColumn(filename, 1);

            List<Company> companyList = new List<Company> { };
            for (int i = 1; i < symbolList.Count; i++)
            {
                companyList.Add(
                    new Company(symbolList[i], nameList[i]));                    
            }
            nameList.Clear();
            symbolList.Clear();
            
            return companyList;
         }


    }
}