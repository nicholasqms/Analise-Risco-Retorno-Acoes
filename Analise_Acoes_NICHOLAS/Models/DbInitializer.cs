using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Analise_Acoes_NICHOLAS.AuxiliaryF;
using Analise_Acoes_NICHOLAS.Data;
using System.Collections.Generic;

namespace Analise_Acoes_NICHOLAS.Models
{
    public static class DbInitializer
    {
        public static void Initialize(Analise_AcoesContext context)
        {
            //using (var context = new Analise_AcoesContext(
            //    serviceProvider.GetRequiredService<DbContextOptions<Analise_AcoesContext>>()))
            {
                // Look for any Company.
                if (context.Company.Any())
                {
                    return;   // DB has been seeded
                }
                Auxiliary auxiliar = new Auxiliary();
                
                List<Company> CompanyList = auxiliar.ListInitializer("companylist.csv");
                for (int i = 0; i < CompanyList.Count(); i++)
                {
                    context.Company.Add(CompanyList[i]);
                }

                //context.Company.AddRange(CompanyList);

                context.SaveChanges();
            }
        }
    }
}