using System.Collections.Generic;

namespace FinancasCasal.Models.ViewModels
{
    public class TransacaoFormViewModel
    {
        public Transacao Transacao { get; set; }
        public ICollection<Despesa> Despesas { get; set; }
        public ICollection<Fundo> Fundos { get; set; }
        public ICollection<Conta> Contas { get; set; }

    }
}
