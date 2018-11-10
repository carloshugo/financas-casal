using FinancasCasal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasCasal.Services
{
    public class PessoaService
    {
        private readonly FinancasCasalContext _context;

        public PessoaService(FinancasCasalContext context)
        {
            _context = context;
        }

        public async Task<List<Pessoa>> ObterTodosAsync()
        {
            return await _context.Pessoa.OrderBy(p => p.Nome).ToListAsync();
        }
    }
}
