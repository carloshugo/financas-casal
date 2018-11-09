using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancasCasal.Models;
using FinancasCasal.Models.ViewModels;
using FinancasCasal.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancasCasal.Controllers
{
    public class FundosController : Controller
    {
        private readonly FundoService _fundoService;
        private readonly PessoaService _pessoaService;

        public FundosController(FundoService fundoService, PessoaService pessoaService)
        {
            _fundoService = fundoService;
            _pessoaService = pessoaService;
        }

        public IActionResult Index()
        {
            var list = _fundoService.ObterTodos();
            return View(list);
        }

        public IActionResult Criacao()
        {
            var pessoas = _pessoaService.ObterTodos();
            var viewModel = new FundoFormViewModel { Pessoas = pessoas};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Fundo fundo)
        {
            _fundoService.Inserir(fundo);
            return RedirectToAction(nameof(Index));
        }
        
    }
}