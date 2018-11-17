using System;
using System.ComponentModel.DataAnnotations;

namespace FinancasCasal.Models
{
    public class Transacao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ser entre {2} e {1}")]
        public string Nome { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public double Valor { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        public Despesa Despesa { get; set; }
        [Display(Name = "Despesa")]
        public int? DespesaId { get; set; }

        public Fundo Fundo { get; set; }
        [Display(Name = "Fundo")]
        public int? FundoId { get; set; }

        public Conta Conta { get; set; }
        [Display(Name = "Conta")]
        public int ContaId { get; set; }

        [Display(Name = "Débito")]
        public bool Debito { get; set; }
        public bool Efetivada { get; set; }

        public Transacao()
        {
        }

        public Transacao(int id, string nome, double valor, DateTime data, Conta conta, bool debito, bool efetivada)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Data = data;
            Conta = conta;
            Efetivada = efetivada;
        }

        public Transacao(int id, string nome, double valor, DateTime data, Despesa despesa, Conta conta, bool debito, bool efetivada)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Data = data;
            Despesa = despesa;
            Conta = conta;
            Efetivada = efetivada;
        }

        public Transacao(int id, string nome, double valor, DateTime data, Fundo fundo, Conta conta, bool debito, bool efetivada)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Data = data;
            Fundo = fundo;
            Conta = conta;
            Efetivada = efetivada;
        }

        public Transacao(int id, string nome, double valor, DateTime data, Despesa despesa, Fundo fundo, Conta conta, bool debito, bool efetivada)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Data = data;
            Despesa = despesa;
            Fundo = fundo;
            Conta = conta;
            Efetivada = efetivada;
        }

        public void Efetivar()
        {
            if (Efetivada)
            {
                if (Fundo != null)
                {
                    if (Debito)
                    {
                        Fundo.Debitar(Valor);
                    }
                    else
                    {
                        Fundo.Creditar(Valor);
                    }
                }

                if (Debito)
                {
                    Conta.Debitar(Valor);
                }
                else
                {
                    Conta.Creditar(Valor);

                }
            }
        }
    }
}
