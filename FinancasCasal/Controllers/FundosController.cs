using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinancasCasal.Models;
using FinancasCasal.Models.ViewModels;
using FinancasCasal.Services;
using FinancasCasal.Services.Exceptions;
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
            var viewModel = new FundoFormViewModel { Pessoas = pessoas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Fundo fundo)
        {
            _fundoService.Inserir(fundo);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delecao(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = _fundoService.ObterPorId(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletar(int id)
        {
            _fundoService.Remover(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não provido (null)" });
            }
            var obj = _fundoService.ObterPorId(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        public IActionResult Edicao(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não provido (null)" });
            }
            var obj = _fundoService.ObterPorId(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            List<Pessoa> pessoas = _pessoaService.ObterTodos();
            FundoFormViewModel viewModel = new FundoFormViewModel { Fundo = obj, Pessoas = pessoas };
            return View(viewModel);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, Fundo fundo)
        {
            if (id != fundo.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id provido não casa com o Id do Objeto" });
            }
            try
            {
                _fundoService.Atualizar(fundo);
                return RedirectToAction(nameof(Index));

            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}