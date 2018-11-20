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
            if (transacao.Efetivada)
            {
                transacao.Efetivar();
                _context.Update(transacao.Conta);
                if (transacao.Fundo != null)
                {
                    _context.Update(transacao.Fundo);
                }
            }
            _context.Add(transacao);
            await _context.SaveChangesAsync();
        }

        public Transacao ObterInstanciaGastoFundo(Fundo fundo)
        {
            Transacao transacao = new Transacao()
            {
                Fundo = fundo,
                FundoId = fundo.Id,
                Conta = fundo.Conta,
                ContaId = fundo.ContaId,
                Debito = true,
                Efetivada = true,
                Data = DateTime.Now
            };
            return transacao;
        }

        public Transacao ObterInstanciaGastoConta(Conta conta)
        {
            Transacao transacao = new Transacao()
            {
                Conta = conta,
                ContaId = conta.Id,
                Debito = true,
                Efetivada = true,
                Data = DateTime.Now
            };
            return transacao;
        }

    }
}
