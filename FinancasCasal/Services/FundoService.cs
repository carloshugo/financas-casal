﻿using FinancasCasal.Models;
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

        public Fundo ObterPorId(int id)
        {
            return _context.Fundo.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remover(int id)
        {
            var obj = _context.Fundo.Find(id);
            _context.Fundo.Remove(obj);
            _context.SaveChanges();
        }
    }
}
