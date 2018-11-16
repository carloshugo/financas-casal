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
        private readonly TransacaoService _transacaoService;

        public FundosController(FundoService fundoService, PessoaService pessoaService, TransacaoService transacaoService)
        {
            _fundoService = fundoService;
            _pessoaService = pessoaService;
            _transacaoService = transacaoService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _fundoService.ObterTodosAsync();
            return View(list);
        }

        public async Task<IActionResult> Criacao()
        {
            var pessoas = await _pessoaService.ObterTodosAsync();
            var viewModel = new FundoFormViewModel { Pessoas = pessoas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Fundo fundo)
        {
            if (!ModelState.IsValid)
            {
                List<Pessoa> pessoas = await _pessoaService.ObterTodosAsync();
                FundoFormViewModel viewModel = new FundoFormViewModel { Fundo = fundo, Pessoas = pessoas };
                return View(viewModel);
            }
            await _fundoService.InserirAsync(fundo);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delecao(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = await _fundoService.ObterPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            await _fundoService.RemoverAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não provido (null)" });
            }
            var obj = await _fundoService.ObterPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edicao(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não provido (null)" });
            }
            var obj = await _fundoService.ObterPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            List<Pessoa> pessoas = await _pessoaService.ObterTodosAsync();
            FundoFormViewModel viewModel = new FundoFormViewModel { Fundo = obj, Pessoas = pessoas };
            return View(viewModel);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Fundo fundo)
        {
            if (!ModelState.IsValid)
            {
                List<Pessoa> pessoas = await _pessoaService.ObterTodosAsync();
                FundoFormViewModel viewModel = new FundoFormViewModel { Fundo = fundo, Pessoas = pessoas };
                return View(viewModel);
            }

            if (id != fundo.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id provido não casa com o Id do Objeto" });
            }
            try
            {
                await _fundoService.AtualizarAsync(fundo);
                return RedirectToAction(nameof(Index));

            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Gastos(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não provido (null)" });
            }
            var obj = await _fundoService.ObterPorIdGastosAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            Transacao transacao = new Transacao()
            {
                Fundo = obj,
                FundoId = obj.Id,
                Conta = obj.Conta,
                ContaId = obj.ContaId,
                Debito = true,
                Nome = "Jantar",
                Valor = 44,
                Data = DateTime.Now
            };
            FundoGastoViewModel viewModel = new FundoGastoViewModel { Fundo = obj, Transacao = transacao };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gastos(Transacao transacao)
        {
            FundoGastoViewModel viewModel;
            if (!ModelState.IsValid)
            {
                var obj = await _fundoService.ObterPorIdAsync(transacao.Fundo.Id);
                Transacao t = new Transacao()
                {
                    Fundo = obj,
                    FundoId = obj.Id,
                    Conta = obj.Conta,
                    ContaId = obj.ContaId,
                    Debito = true
                };
                viewModel = new FundoGastoViewModel { Fundo = obj, Transacao = transacao };
                return View(viewModel);
            }
            var fundo = await _fundoService.ObterPorIdGastosAsync(transacao.FundoId.Value);
            await _transacaoService.InserirAsync(transacao);
            transacao = new Transacao()
            {
                Fundo = fundo,
                FundoId = fundo.Id,
                Conta = fundo.Conta,
                ContaId = fundo.ContaId,
                Debito = true
            };
            viewModel = new FundoGastoViewModel { Fundo = fundo, Transacao = transacao };
            return View(viewModel);
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