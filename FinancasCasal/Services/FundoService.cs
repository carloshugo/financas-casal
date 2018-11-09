using FinancasCasal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasCasal.Services
{
    public class FundoService
    {
        private readonly FinancasCasalContext _context;

        public FundoService(FinancasCasalContext context)
        {
            _context = context;
        }

        public List<Fundo> ObterTodos()
        {
            return _context.Fundo.ToList();
        }

        public void Inserir(Fundo fundo)
        {
            _context.Add(fundo);
            _context.SaveChanges();
        }
    }
}
