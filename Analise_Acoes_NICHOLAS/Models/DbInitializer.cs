using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using AnaliseAcoesNicholas.AuxiliaryF;
using AnaliseAcoesNicholas.Data;
using System.Collections.Generic;

namespace AnaliseAcoesNicholas.Models
{
    public static class DbInitializer
    {
        public static void Initialize(Analise_AcoesContext context)
        {
            {
                // Look for any Company.
                if (context.Company.Any())
                {
                    return;   // DB has been seeded
                }
                
                List<Company> CompanyList = Auxiliary.ListInitializer("companylist.csv");
                for (int i = 0; i < CompanyList.Count(); i++)
                {
                    context.Company.Add(CompanyList[i]);
                }

                context.SaveChanges();
            }
        }
    }
}