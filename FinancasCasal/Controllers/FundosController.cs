using System;
using System.Collections.Generic;
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
                return NotFound();
            }
            var obj = _fundoService.ObterPorId(id.Value);
            if (obj == null)
            {
                return NotFound();
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
                return NotFound();
            }
            var obj = _fundoService.ObterPorId(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Edicao(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _fundoService.ObterPorId(id.Value);
            if (obj == null)
            {
                return NotFound();
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
                return BadRequest();
            }
            try
            {
                _fundoService.Atualizar(fundo);
                return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }


    }
}