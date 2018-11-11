using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FinancasCasal.Controllers
{
    public class TransacaosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}