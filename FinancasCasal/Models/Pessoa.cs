using System.Collections.Generic;
using System.Linq;
using System;

namespace FinancasCasal.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Fundo> Fundos { get; set; } = new List<Fundo>();

        public Pessoa()
        {
        }

        public Pessoa(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void AdicionarFundo(Fundo fundo)
        {
            Fundos.Add(fundo);
        }

        public void RemoverFundo(Fundo fundo)
        {
            Fundos.Remove(fundo);
        }

        public double TotalFundos()
        {
            return Fundos.Sum(fundo => fundo.Saldo);
        }

        public double TotalFundos(DateTime inicio, DateTime fim)
        {
            return Fundos.Sum(fundo => fundo.TotalTransacoes(inicio, fim));
        }
    }
}
