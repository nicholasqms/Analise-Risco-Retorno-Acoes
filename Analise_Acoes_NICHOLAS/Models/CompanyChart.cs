using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Analise_Acoes_NICHOLAS.Models
{
    public class CompanyChart
    {
        public List<Company> LoadedList { get; set; }
        public string GraphPath { get; set; }

        //public List<string> ReturnList (List<Company> LoadedList, string GraphPath)
        //{
        //    List<string> CompanyCList = new List<string>();

        //    for (int i = 1; i < LoadeList.Count; i++)
        //    {
        //        ClosingList.Add(
        //            new Closings(Eod[i], Dates[i]));
        //    }

        //    return CompanyCList;
        //}

    }
}
