using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinancasCasal.Models
{
    public class FinancasCasalContext : DbContext
    {
        public FinancasCasalContext (DbContextOptions<FinancasCasalContext> options)
            : base(options)
        {
        }

        public DbSet<FinancasCasal.Models.Pessoa> Pessoa { get; set; }
    }
}
