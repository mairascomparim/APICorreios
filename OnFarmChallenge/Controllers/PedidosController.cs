using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnFarmChallenge.Models;
using OnFarmChallenge.Services;

namespace OnFarmChallenge.Controllers
{
    public class PedidosController : Controller
    {
        public IActionResult Index(int clienteId)
        {
            CorreioService correioService = new CorreioService();

            List<Pedido> Pedidos = new Pedido { ClienteId = clienteId }.GetPedidoByClientId(clienteId);
            Cliente cliente = new Cliente
            {
                Id = clienteId
            };

            cliente = cliente.Selecionar();

            Dictionary<String, String> statusObjetos = new Dictionary<String, String>();

            foreach(Pedido pedido in Pedidos)
            {
                statusObjetos.Add(pedido.CodigoRastreio, correioService.ConsultaObjeto(pedido.CodigoRastreio));
            }
           

            ViewBag.Pedidos = Pedidos;
            ViewBag.StatusObjetos = statusObjetos;
            ViewBag.Cliente = cliente;

            return View();
        }

        public IActionResult Novo(int clienteId)
        {
            ViewBag.ClienteID = clienteId;
            return View();
        }

        public RedirectToActionResult Salvar()
        {

            try
            {
                String CodigoDeRastreio = Request.Form["Rastreio"];
                int ClienteID = int.Parse(Request.Form["ClienteID"]);

                if (CodigoDeRastreio == "")
                {
                    return RedirectToAction("", "Pedidos", new { clienteID = ClienteID });
                }

                Pedido NovoPedido = new Pedido
                {
                    CodigoRastreio = CodigoDeRastreio,
                    ClienteId = ClienteID
                };

                NovoPedido.Salvar();
                return RedirectToAction("", "Pedidos", new { clienteID = ClienteID });
            } catch (Exception) {

                return RedirectToAction("", "Clientes");

            }
        }

        public RedirectToActionResult Excluir(int pedidoId, int clienteId)
        {
            new Pedido().ExcluirById(pedidoId);

            return RedirectToAction("", "Pedidos", new { clienteId = clienteId });
        }

        public IActionResult Editar(int pedidoId, int clienteId)
        {
            Pedido EditarPedido = new Pedido
            {
                Id = pedidoId
            };

            EditarPedido = EditarPedido.getPedido();

            ViewBag.Pedido = EditarPedido;
            ViewBag.ClienteID = clienteId;

            return View();

        }

        public RedirectToActionResult Atualizar()
        {

            try
            {
                String CodigoDeRastreio = Request.Form["Rastreio"];
                int PedidoID = int.Parse(Request.Form["PedidoID"]);
                String ClienteID = Request.Form["ClienteID"];

                if(CodigoDeRastreio == "")
                {
                    return RedirectToAction("", "Pedidos", new { clienteID = ClienteID });
                }

                Pedido NovoPedido = new Pedido
                {
                    Id = PedidoID
                };

                NovoPedido.Atualizar(CodigoDeRastreio);
                return RedirectToAction("", "Pedidos", new { clienteID = ClienteID });
            } catch (Exception) { return RedirectToAction("", "Clientes"); }

            

        }

    }
}
