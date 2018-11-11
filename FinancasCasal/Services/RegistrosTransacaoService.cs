using FinancasCasal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinancasCasal.Services
{
    public class RegistrosTransacaoService
    {
        private readonly FinancasCasalContext _context;

        public RegistrosTransacaoService(FinancasCasalContext context)
        {
            _context = context;
        }

        public async Task<List<Transacao>> ObterPorDataAsync(DateTime? inicio, DateTime? fim)
        {
            var result = from obj in _context.Transacao select obj;
            if (inicio.HasValue)
            {
                result = result.Where(x => x.Data >= inicio.Value);
            }
            if (fim.HasValue)
            {
                result = result.Where(x => x.Data <= fim.Value);
            }
            return await result
                .Include(x => x.Conta)
                .Include(x => x.Despesa)
                .Include(x => x.Fundo)
                .Include(x => x.Fundo.Pessoa)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Conta, Transacao>>> ObterGrupoPorDataAsync(DateTime? inicio, DateTime? fim)
        {
            var result = from obj in _context.Transacao select obj;
            if (inicio.HasValue)
            {
                result = result.Where(x => x.Data >= inicio.Value);
            }
            if (fim.HasValue)
            {
                result = result.Where(x => x.Data <= fim.Value);
            }
            return await result
                .Include(x => x.Conta)
                .Include(x => x.Despesa)
                .Include(x => x.Fundo)
                .Include(x => x.Fundo.Pessoa)
                .OrderByDescending(x => x.Data)
                .GroupBy(x => x.Conta)
                .ToListAsync();
        }
    }
}
