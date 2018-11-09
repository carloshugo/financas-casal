using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancasCasal.Models
{

    public class Fundo {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public double Saldo { get; set; }
        public Pessoa Pessoa { get; set; }
        public int PessoaId { get; set; }
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public Fundo()
        {

        }

        public Fundo(int id, string nome, double saldo, Pessoa dono)
        {
            Id = id;
            Nome = nome;
            Saldo = saldo;
            Pessoa = dono;
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }

        public double TotalTransacoes(DateTime inicio, DateTime fim)
        {
            return Transacoes.Where(ts => ts.Data >= inicio && ts.Data <= fim).Sum(ts => ts.Valor);
        }
    }
}
