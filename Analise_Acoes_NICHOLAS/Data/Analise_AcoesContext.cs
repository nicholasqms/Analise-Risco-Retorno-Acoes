using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Analise_Acoes_NICHOLAS.Data
{
    public class Analise_AcoesContext : DbContext
    {
        public Analise_AcoesContext (DbContextOptions<Analise_AcoesContext> options)
            : base(options)
        {
        }

        public DbSet<Analise_Acoes_NICHOLAS.Models.Company> Company{ get; set; }
        public DbSet<Analise_Acoes_NICHOLAS.Models.Company> CompanyListModel { get; set; }

    }
}
