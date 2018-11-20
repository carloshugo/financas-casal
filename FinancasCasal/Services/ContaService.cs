using FinancasCasal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasCasal.Services
{
    public class ContaService
    {
        private readonly FinancasCasalContext _context;

        public ContaService(FinancasCasalContext context)
        {
            _context = context;
        }

        public async Task<List<Conta>> ObterTodosAsync()
        {
            return await _context.Conta.OrderBy(p => p.Banco).ToListAsync();
        }

        public async Task<Conta> ObterPorIdAsync(int id)
        {
            return await _context.Conta.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<Conta> ObterPorIdGastosAsync(int id)
        {
            return await _context.Conta
                .Include(obj => obj.Transacoes)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

    }
}
