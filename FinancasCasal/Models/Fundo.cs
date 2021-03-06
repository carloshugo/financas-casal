﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FinancasCasal.Models
{

    public class Fundo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ser entre {2} e {1}")]
        public string Nome { get; set; }

        [Display(Name = "Saldo R$")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public double Saldo { get; set; }

        [Display(Name = "Dono")]
        public Pessoa Pessoa { get; set; }

        [Display(Name = "Dono")]
        public int PessoaId { get; set; }

        public Conta Conta { get; set; }
        [Display(Name = "Conta")]
        public int ContaId { get; set; }

        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public Fundo()
        {

        }

        public Fundo(int id, string nome, double saldo, Pessoa dono, Conta conta)
        {
            Id = id;
            Nome = nome;
            Saldo = saldo;
            Pessoa = dono;
            Conta = conta;
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }

        public double TotalTransacoes(DateTime inicio, DateTime fim)
        {
            return Transacoes.Where(ts => ts.Data >= inicio && ts.Data <= fim).Sum(ts => ts.Valor);
        }

        public void Debitar(double valor)
        {
            Saldo -= valor;
        }

        public void Creditar(double valor)
        {
            Saldo += valor;
        }
    }
}
