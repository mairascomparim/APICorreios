using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnFarmChallenge.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        public String CodigoRastreio { get; set; }
        public DateTime Criado { get; set; } = DateTime.Now;

        public void Salvar()
        {
            var db = new OnFarmChallengeContext();
            db.Pedidos.Add(this);
            db.SaveChanges();
        }

        public List<Pedido> GetPedidoByClientId(int clienteId)
        {
            var db = new OnFarmChallengeContext();
            return db.Pedidos.Where(p => p.ClienteId == clienteId).ToList();
        }

        public void ExcluirById(int pedidoId)
        {
            this.Id = pedidoId;

            var db = new OnFarmChallengeContext();
            db.Pedidos.Remove(this);
            db.SaveChanges();
        }

        public Pedido getPedido()
        {
            var db = new OnFarmChallengeContext();
            return db.Pedidos.First(p => p.Id == this.Id);
        }

        public void Atualizar(String CodigoRastreio)
        {
            var db = new OnFarmChallengeContext();

            Pedido NovoPedido = db.Pedidos.SingleOrDefault(p => p.Id == this.Id);
            NovoPedido.CodigoRastreio = CodigoRastreio;
            db.SaveChanges();
            
        }

    }
}
