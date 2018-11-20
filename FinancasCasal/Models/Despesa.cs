using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancasCasal.Models
{

    public class Despesa
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Valor R$")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Valor { get; set; }

        [Display(Name = "Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Inicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
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
