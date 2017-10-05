using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Analise_Acoes_NICHOLAS.Models
{
    public class CompanyListModel
    {
        public List<Company> companies;
        public SelectList company_names;
        public string companyName { get; set; }
    }
}
