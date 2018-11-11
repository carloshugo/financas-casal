using FinancasCasal.Models;
using FinancasCasal.Services.Exceptions;
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

        public async Task RemoverAsync(int id)
        {
            try
            {
                var obj = _context.Pessoa.Find(id);
                _context.Pessoa.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não pode deletar esta pessoa porque tem fundo vinculado.");
            }
        }
    }
}
