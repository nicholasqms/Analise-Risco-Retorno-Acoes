﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Analise_Acoes_NICHOLAS.Models
{
    public class Company
    {
        //public Company{}

        public Company(int i, string symbol, string name)
        {
            ID = i;
            Sigla = symbol;
            Nome = name;
        }

        public int ID { get; set; }

//        [Display(Name = "Nome do Arquivo")]
//        public string filename { get { return filename; } set => "companylist1.csv"; }
        
        [Display(Name = "Sigla")]
        public string Sigla { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

    }
}
