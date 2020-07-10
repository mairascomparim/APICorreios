using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnFarmChallenge.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        public String Cep { get; set; }
        [Required]
        public String Endereco { get; set; }
        [Required]
        public String Telefone { get; set; }
        [Required]
        public DateTime Criado { get; set; } = DateTime.Now;
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public void Salvar()
        {
            var db = new OnFarmChallengeContext();
            db.Clientes.Add(this);
            db.SaveChanges();
        }

        public List<Cliente> Todos()
        {
            var db = new OnFarmChallengeContext();
            return db.Clientes.ToList();
        }

        public void Excluir()
        {
            var db = new OnFarmChallengeContext();
            db.Clientes.Remove(this);
            db.SaveChanges();
        }

        public Cliente Selecionar()
        {
            var db = new OnFarmChallengeContext();
            return db.Clientes.SingleOrDefault(e => e.Id == this.Id);
        }

        public void Atualizar(OnFarmChallenge.Models.DTO.EditarClienteDataBinding cliente)
        {
            var db = new OnFarmChallengeContext();
            Cliente TempCliente = db.Clientes.SingleOrDefault(e => e.Id == cliente.Id);
            TempCliente.Nome = cliente.Nome;
            TempCliente.Endereco = cliente.Endereco;
            TempCliente.Cep = cliente.Cep;
            TempCliente.Telefone = cliente.Telefone;
            db.SaveChanges();
        }
    }
}
