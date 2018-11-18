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
        private readonly ContaService _contaService;

        public FundosController(FundoService fundoService, PessoaService pessoaService, TransacaoService transacaoService, ContaService contaService)
        {
            _fundoService = fundoService;
            _pessoaService = pessoaService;
            _transacaoService = transacaoService;
            _contaService = contaService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _fundoService.ObterTodosAsync();
            return View(list);
        }

        public async Task<IActionResult> Criacao()
        {
            var pessoas = await _pessoaService.ObterTodosAsync();
            var contas = await _contaService.ObterTodosAsync();
            var viewModel = new FundoFormViewModel { Pessoas = pessoas, Contas = contas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Fundo fundo)
        {
            if (!ModelState.IsValid)
            {
                List<Pessoa> pessoas = await _pessoaService.ObterTodosAsync();
                List<Conta> contas = await _contaService.ObterTodosAsync();
                FundoFormViewModel viewModel = new FundoFormViewModel { Fundo = fundo, Pessoas = pessoas, Contas = contas };
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
            Transacao transacao = _transacaoService.ObterInstanciaGastoFundo(obj);
            return View(transacao);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gastos(Transacao transacao)
        {
            transacao.Id = 0;

            if (!ModelState.IsValid)
            {
                var obj = await _fundoService.ObterPorIdAsync(transacao.FundoId.Value);
                transacao = _transacaoService.ObterInstanciaGastoFundo(obj);
                return View(transacao);
            }
            var fundo = await _fundoService.ObterPorIdAsync(transacao.FundoId.Value);
            var conta = await _contaService.ObterPorIdAsync(transacao.ContaId);
            transacao.Fundo = fundo;
            transacao.Conta = conta;
            await _transacaoService.InserirAsync(transacao);
            return RedirectToAction(nameof(Gastos), fundo.Id);
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