using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnFarmChallenge.Models;
using OnFarmChallenge.Models.DTO;
using OnFarmChallenge.Services;

namespace OnFarmChallenge.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(ILogger<ClientesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Cliente> Clientes = new Cliente().Todos();
            ViewBag.Clientes = Clientes;

            return View();
        }

        public IActionResult Novo()
        {
            return View();
        }

        public RedirectToActionResult Salvar(NovoClienteDataBinding novoCliente)
        {

            try
            {
                var NovoCliente = new Cliente
                {
                    Nome = novoCliente.Nome,
                    Cep = novoCliente.Cep,
                    Endereco = novoCliente.Endereco,
                    Telefone = novoCliente.Telefone
                };

                NovoCliente.Salvar();
            } catch(Exception) { }

            return RedirectToAction("", "Clientes");
        }

        public RedirectToActionResult Atualizar(EditarClienteDataBinding cliente)
        {

            try
            {
                var EditarCliente = new Cliente
                {
                    Id = cliente.Id
                };

                EditarCliente.Atualizar(cliente);

            } catch(Exception) { }

            return RedirectToAction("", "Clientes");
        }

        public IActionResult Editar(int clienteId)
        {
            var PegarCliente = new Cliente
            {
                Id = clienteId
            };

            ViewBag.Cliente = PegarCliente.Selecionar();

            return View();
        }

        public RedirectToActionResult Excluir(int clienteId)
        {

            var ExcluirCliente = new Cliente
            {
                Id = clienteId
            };

            ExcluirCliente.Excluir();

            return RedirectToAction("", "Clientes");
        }


        public String Correio()
        {
            CorreioService correioService = new CorreioService();
            return correioService.ConsultaObjeto("PY661612572BR");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
