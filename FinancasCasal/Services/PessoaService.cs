using FinancasCasal.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinancasCasal.Services
{
    public class PessoaService
    {
        private readonly FinancasCasalContext _context;

        public PessoaService(FinancasCasalContext context)
        {
            _context = context;
        }

        public List<Pessoa> ObterTodos()
        {
            return _context.Pessoa.OrderBy(p => p.Nome).ToList();
        }
    }
}
