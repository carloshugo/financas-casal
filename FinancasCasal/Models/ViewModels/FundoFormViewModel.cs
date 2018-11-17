using System.Collections.Generic;

namespace FinancasCasal.Models.ViewModels
{
    public class FundoFormViewModel
    {
        public Fundo Fundo { get; set; }
        public ICollection<Pessoa> Pessoas { get; set; }
        public ICollection<Conta> Contas { get; set; }
    }
}
