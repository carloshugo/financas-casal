using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancasCasal.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancasCasal.Controllers
{
    public class FundosController : Controller
    {
        private readonly FundoService _fundoService;

        public FundosController(FundoService fundoService)
        {
            _fundoService = fundoService;
        }

        public IActionResult Index()
        {
            var list = _fundoService.ObterTodos();
            return View(list);
        }
    }
}