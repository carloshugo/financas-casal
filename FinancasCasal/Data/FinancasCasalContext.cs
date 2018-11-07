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

        public DbSet<Conta> Conta { get; set; }
        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<Fundo> Fundo { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Transacao> Transacao { get; set; }
    }
}
