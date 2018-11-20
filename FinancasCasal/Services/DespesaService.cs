using FinancasCasal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasCasal.Services
{
    public class DespesaService
    {
        private readonly FinancasCasalContext _context;

        public DespesaService(FinancasCasalContext context)
        {
            _context = context;
        }

        public async Task<List<Despesa>> ObterTodosAsync()
        {
            return await _context.Despesa.ToListAsync();
        }

        public async Task<Despesa> ObterPorIdAsync(int id)
        {
            return await _context.Despesa.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<Despesa> ObterPorIdGastosAsync(int id)
        {
            return await _context.Despesa
                .Include(obj => obj.Transacoes)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

    }
}
