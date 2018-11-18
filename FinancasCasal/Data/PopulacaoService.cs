using FinancasCasal.Models;
using FinancasCasal.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasCasal.Data
{
    public class PopulacaoService
    {
        private FinancasCasalContext _context;

        public PopulacaoService(FinancasCasalContext context)
        {
            _context = context;
        }

        public void Popular()
        {
            if (_context.Conta.Any()
                || _context.Despesa.Any()
                || _context.Fundo.Any()
                || _context.Pessoa.Any()
                || _context.Transacao.Any())
            {
                return; //Banco de dados já populado
            }

            Conta c1 = new Conta(1, "CC BP", "Banpará", "21", "22926", TipoConta.Corrente);
            Conta c2 = new Conta(2, "CC BB", "Banco do Brasil", "3074-0", "49850-5", TipoConta.Corrente);
            Conta c3 = new Conta(3, "PP BB", "Banco do Brasil", "3074-0", "49850-5", TipoConta.Poupanca);
            Conta c4 = new Conta(4, "PP BP", "Banpará", "21", "6071996", TipoConta.Poupanca);

            Despesa d1 = new Despesa(1, "Energia", 255.63, DateTime.Now, null);
            Despesa d2 = new Despesa(2, "Condomínio", 290.0, DateTime.Now, null);
            Despesa d3 = new Despesa(3, "Internet", 133.35, DateTime.Now, null);

            Pessoa p1 = new Pessoa(1, "Arianne Machado");
            Pessoa p2 = new Pessoa(2, "Carlos Hugo Lopes");

            Fundo f1 = new Fundo(1, "Lazer", 10.0, p1, c1);
            Fundo f2 = new Fundo(2, "Lazer", 10.0, p2, c2);


            _context.Conta.AddRange(c1, c2, c3, c4);
            _context.Despesa.AddRange(d1, d2, d3);
            _context.Pessoa.AddRange(p1, p2);
            _context.Fundo.AddRange(f1, f2);

            _context.SaveChanges();

        }
    }
}
