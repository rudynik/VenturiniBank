using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Banco.Models;
using Banco.repositorio;

namespace Banco.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContaRepository _repo;

        public HomeController(ILogger<HomeController> logger, IContaRepository _repo)
        {
            _logger = logger;
            this._repo = _repo;
        }

        public IActionResult Index()
        {
            var conta = new ContaCorrente()
            {
                Titular = new Cliente() { CPF = "228-435-554-09", Nome = "rudy", Profissao = "Desenvolvedor"},
                Agencia = 6471,
                Numero = 143063      
            };

            if (_repo.GetConta(conta) == null)
            {
                _repo.AddConta(conta);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(ContaCorrente conta)
        {
            if (_repo.GetConta(conta) != null)
            {
                return RedirectToAction("Conta", _repo.GetConta(conta));
            }

            ViewBag.error = true;
            return View();
        }

        public IActionResult Conta(ContaCorrente conta)
        {
            return View(conta);
        }

        [HttpGet]
        public JsonResult Sacar(double valor, int id)
        {
            if(new ContaCorrente().Sacar(valor))
            {
                return Json(_repo.BuscarConta(id));
            }
            else
            {
                return Json("Saldo Insuficiente");
            }

            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
