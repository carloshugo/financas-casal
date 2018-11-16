using FinancasCasal.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FinancasCasal.Services.Exceptions;
using System.Threading.Tasks;

namespace FinancasCasal.Services
{
    public class FundoService
    {
        private readonly FinancasCasalContext _context;

        public FundoService(FinancasCasalContext context)
        {
            _context = context;
        }

        public async Task<List<Fundo>> ObterTodosAsync()
        {
            return await _context.Fundo.Include(obj => obj.Pessoa).ToListAsync();
        }

        public async Task InserirAsync(Fundo fundo)
        {
            _context.Add(fundo);
            await _context.SaveChangesAsync();
        }

        public async Task<Fundo> ObterPorIdAsync(int id)
        {
            return await _context.Fundo.Include(obj => obj.Pessoa).Include(obj => obj.Conta).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<Fundo> ObterPorIdGastosAsync(int id)
        {
            return await _context.Fundo
                .Include(obj => obj.Pessoa)
                .Include(obj => obj.Conta)
                .Include(obj => obj.Transacoes)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoverAsync(int id)
        {
            var obj = _context.Fundo.Find(id);
            _context.Fundo.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Fundo obj)
        {
            bool temAlgum = await _context.Fundo.AnyAsync(x => x.Id == obj.Id);
            if (!temAlgum)
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
