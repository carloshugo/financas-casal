using System;
namespace FinancasCasal.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; private set; }
        public DateTime Data { get; set; }
        public Despesa Despesa { get; set; }
        public Fundo Fundo { get; set; }
        public Conta Conta { get; set; }
        public bool Efetivada { get; set; }

        public Transacao()
        {
        }

        public Transacao(int id, string nome, double valor, DateTime data, Conta conta)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Data = data;
            Conta = conta;
        }

        public Transacao(int id, string nome, double valor, DateTime data, Despesa despesa, Conta conta)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Data = data;
            Despesa = despesa;
            Conta = conta;
        }

        public Transacao(int id, string nome, double valor, DateTime data, Fundo fundo, Conta conta)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Data = data;
            Fundo = fundo;
            Conta = conta;
        }

        public Transacao(int id, string nome, double valor, DateTime data, Despesa despesa, Fundo fundo, Conta conta, bool efetivada)
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
            if (!Efetivada)
            {
                if (Fundo != null)
                {
                    Fundo.Saldo += Valor;
                }
                Conta.Saldo += Valor;
                Efetivada = true;
            }
        }
    }
}
