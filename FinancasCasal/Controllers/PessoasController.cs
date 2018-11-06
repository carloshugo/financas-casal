using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinancasCasal.Models;

namespace FinancasCasal.Controllers
{
    public class PessoasController : Controller
    {
        
        public IActionResult Index()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            pessoas.Add(new Pessoa("Hugo") { Id = 1});
            pessoas.Add(new Pessoa("Maria") { Id = 1});

            return View(pessoas);
        }
    }
}