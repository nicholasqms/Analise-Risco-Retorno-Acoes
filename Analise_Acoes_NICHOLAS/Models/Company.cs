using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Analise_Acoes_NICHOLAS.Models
{
    public class Company
    {
        public Company(){}

        public Company(string s, string name)
        {            
            Symbol = s;
            Nome = name;
        }

//        [Display(Name = "Nome do Arquivo")]
//        public string filename { get { return filename; } set => "companylist1.csv"; }
        
        [Display(Name = "Symbol")]
        public string Symbol { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        //[Display(Name = "")]
        //public string GraphPath { get; set; }

    }
}
