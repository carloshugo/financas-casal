using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasCasal.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Pessoa(string nome)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        }

        public override string ToString()
        {
            return Id + " (Pessoa) \n"
                + Nome + "\n";
        }
    }
}
