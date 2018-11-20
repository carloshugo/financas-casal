using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancasCasal.Models;
using FinancasCasal.Services;
using FinancasCasal.Models.ViewModels;
using System.Diagnostics;

namespace FinancasCasal.Controllers
{
    public class DespesasController : Controller
    {
        private readonly FinancasCasalContext _context;
        private readonly DespesaService _despesaService;
        private readonly TransacaoService _transacaoService;
        private readonly ContaService _contaService;

        public DespesasController(FinancasCasalContext context, TransacaoService transacaoService, ContaService contaService, DespesaService despesaService)
        {
            _context = context;
            _transacaoService = transacaoService;
            _contaService = contaService;
            _despesaService = despesaService;
        }

        // GET: Despesas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Despesa.ToListAsync());
        }

        // GET: Despesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (despesa == null)
            {
                return NotFound();
            }

            return View(despesa);
        }

        // GET: Despesas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Despesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Valor,Inicio,Fim")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(despesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(despesa);
        }

        // GET: Despesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesa.FindAsync(id);
            if (despesa == null)
            {
                return NotFound();
            }
            return View(despesa);
        }

        // POST: Despesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Valor,Inicio,Fim")] Despesa despesa)
        {
            if (id != despesa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(despesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesaExists(despesa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(despesa);
        }

        // GET: Despesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (despesa == null)
            {
                return NotFound();
            }

            return View(despesa);
        }

        // POST: Despesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var despesa = await _context.Despesa.FindAsync(id);
            _context.Despesa.Remove(despesa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Gastos(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não provido (null)" });
            }
            var obj = await _despesaService.ObterPorIdGastosAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            Transacao transacao = _transacaoService.ObterInstanciaGastoDespesa(obj);

            var contas = await _contaService.ObterTodosAsync();

            var viewModel = new TransacaoFormViewModel { Transacao = transacao, Contas = contas };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gastos(Transacao transacao)
        {
            transacao.Id = 0;
            Despesa despesa = null;
            if (!ModelState.IsValid)
            {
                var obj = await _despesaService.ObterPorIdAsync(transacao.FundoId.Value);
                transacao = _transacaoService.ObterInstanciaGastoDespesa(transacao.Despesa);
                List<Conta> contas = await _contaService.ObterTodosAsync();
                TransacaoFormViewModel viewModel = new TransacaoFormViewModel { Transacao = transacao, Contas = contas };
                return View(viewModel);
            }
            despesa = await _despesaService.ObterPorIdAsync(transacao.DespesaId.Value);
            var conta = await _contaService.ObterPorIdAsync(transacao.ContaId);
            transacao.Despesa = despesa;
            transacao.Conta = conta;
            await _transacaoService.InserirAsync(transacao);
            return RedirectToAction(nameof(Gastos), despesa.Id);
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

        private bool DespesaExists(int id)
        {
            return _context.Despesa.Any(e => e.Id == id);
        }


    }
}
