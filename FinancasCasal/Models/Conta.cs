using System;
using FinancasCasal.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace FinancasCasal.Models
{
    public class Conta {
        public int Id { get; set; }

        [Required(ErrorMessage ="Informe o Nome do Banco")]
        public string Banco { get; set; }

        [Required(ErrorMessage = "Informe o Código da Agência")]
        [Display(Name = "Código da Agência")]
        public string CodigoAgencia { get; set; }

        [Required(ErrorMessage = "Informe o Código da Conta")]
        [Display(Name = "Código da Conta")]
        public string CodigoConta { get; set; }

        [Required(ErrorMessage = "Informe o Tipo da Conta")]
        [Display(Name = "Tipo da Conta")]
        public TipoConta TipoConta { get; set; }

        [Required(ErrorMessage = "Informe o Saldo Inicial da Conta")]
        public double Saldo { get; set; }

        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public Conta()
        {
        }

        public Conta(int id, string banco, string codigoAgencia, string codigoConta, TipoConta tipoConta)
        {
            Id = id;
            Banco = banco;
            CodigoAgencia = codigoAgencia;
            CodigoConta = codigoConta;
            TipoConta = tipoConta;
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }

        public void RemoverTransacao(Transacao transacao)
        {
            Transacoes.Remove(transacao);
        }

        public double TotalTransacoes(DateTime inicio, DateTime fim)
        {
            return Transacoes.Where(ts => ts.Data >= inicio && ts.Data <= fim).Sum(ts => ts.Valor);
        }
    }
}
