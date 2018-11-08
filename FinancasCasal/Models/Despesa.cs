using System;
using System.Collections.Generic;
namespace FinancasCasal.Models
{

    public class Despesa
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public Despesa()
        {
        }

        public Despesa(int id, string nome, double valor, DateTime inicio, DateTime? fim)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Inicio = inicio;
            Fim = fim;
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }
        public void RemoverTransacao(Transacao transacao)
        {
            Transacoes.Remove(transacao);
        }
    }

}
