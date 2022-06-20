using System.Collections.Generic;

namespace CadastroClienteAPI.Domain
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public List<Beneficio> Beneficios { get; set; }
    }
}
