using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AnaliseAcoesNicholas.Data
{
    public class Analise_AcoesContext : DbContext
    {
        public Analise_AcoesContext (DbContextOptions<Analise_AcoesContext> options)
            : base(options)
        {
        }

        public DbSet<AnaliseAcoesNicholas.Models.Company> Company{ get; set; }
        public DbSet<AnaliseAcoesNicholas.Models.Company> CompanyListModel { get; set; }

    }
}
