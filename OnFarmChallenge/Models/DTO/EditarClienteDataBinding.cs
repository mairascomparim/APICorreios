using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnFarmChallenge.Models.DTO
{
    public class EditarClienteDataBinding
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Telefone { get; set; }
        public String Cep { get; set; }
        public String Endereco { get; set; }
    }
}
