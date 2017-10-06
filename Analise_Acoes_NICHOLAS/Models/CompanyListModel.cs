using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Analise_Acoes_NICHOLAS.Models
{
    public class CompanyListModel
    {
        public List<Company> CompanyList;
        public SelectList SList;
        public string companySymbol { get; set; }
        public string Sigla;
    }
}
