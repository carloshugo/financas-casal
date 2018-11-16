using FinancasCasal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasCasal.Services
{
    public class TransacaoService
    {
        private readonly FinancasCasalContext _context;

        public TransacaoService(FinancasCasalContext context)
        {
            _context = context;
        }

        public async Task<List<Transacao>> ObterTodosNaoEfetivadosAsync()
        {
            return await _context.Transacao.Where(x => !x.Efetivada).ToListAsync();
        }

        public async Task InserirAsync(Transacao transacao)
        {
            _context.Add(transacao);
            await _context.SaveChangesAsync();
        }

    }
}
