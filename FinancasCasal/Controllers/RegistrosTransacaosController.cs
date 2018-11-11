using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancasCasal.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancasCasal.Controllers
{
    public class RegistrosTransacaosController : Controller
    {
        private readonly RegistrosTransacaoService _registrosTransacaoService;

        public RegistrosTransacaosController(RegistrosTransacaoService registrosTransacaoService)
        {
            _registrosTransacaoService = registrosTransacaoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BuscaSimples(DateTime? inicio, DateTime? fim)
        {
            if (!inicio.HasValue)
            {
                inicio = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!fim.HasValue)
            {
                fim = DateTime.Now;
            }
            ViewData["inicio"] = inicio.Value.ToString("yyyy-MM-dd");
            ViewData["fim"] = fim.Value.ToString("yyyy-MM-dd");
            var result = await _registrosTransacaoService.ObterPorDataAsync(inicio, fim);
            return View(result);
        }

        public IActionResult BuscaPorGrupo()
        {
            return View();
        }
    }
}