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
            var contaPrincipal = new ContaCorrente()
            {
                Titular = new Cliente() { CPF = "228-435-554-09", Nome = "rudy", Profissao = "Desenvolvedor"},
                Agencia = 6471,
                Numero = 143063      
            };

            var contaDestinatario = new ContaCorrente()
            {
                Titular = new Cliente() { CPF = "265-589-254-12", Nome = "renan", Profissao = "Administrador" },
                Agencia = 6471,
                Numero = 132567
            };

            if (_repo.GetConta(contaPrincipal) == null)
            {
                _repo.AddConta(contaPrincipal);
            }

            if (_repo.GetConta(contaDestinatario) == null)
            {
                _repo.AddConta(contaDestinatario);
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
            ViewBag.cliente = _repo.GetCliente(conta.Id);
            return View(conta);
        }

        [HttpGet]
        public JsonResult Sacar(double valor, int id)
        {
            ContaCorrente conta = _repo.BuscarConta(id);


            if (conta.Sacar(valor))
            {
                _repo.AtualizarConta(conta);
                return Json(conta);
            }
            else
            {
                return Json("Saldo Insuficiente");
            }            
        }

        [HttpGet]
        public JsonResult Depositar(double valor, int id)
        {
            ContaCorrente conta = _repo.BuscarConta(id);


            if (conta.Depositar(valor))
            {
                _repo.AtualizarConta(conta);
                return Json(conta);
            }
            else
            {
                return Json("O valor deve ser maior que 0");
            }
        }

        public JsonResult Transferir(double valor, int id, int agencia, int conta)
        {
            ContaCorrente contaDestinatario = _repo.GetConta(new ContaCorrente()
            {
                 Agencia = agencia,
                  Numero = conta
            });

            var contaTitular = _repo.BuscarConta(id);

            if (contaDestinatario != null)
            {
                if(contaTitular.Transferir(valor, contaDestinatario))
                {
                    _repo.AtualizarConta(contaTitular);
                    _repo.AtualizarConta(contaDestinatario);

                    return Json(contaTitular);
                }
                else
                {
                    return Json("Saldo Insuficiente");
                }
            }
            else
            {
                return Json("agência e/ou conta inválido");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
