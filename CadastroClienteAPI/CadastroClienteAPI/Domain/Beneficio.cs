namespace CadastroClienteAPI.Domain
{
    public class Beneficio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public string DataInicio { get; set; }
        public string DataTermino { get; set; }
        public int ClienteId { get; set; }
    }
}
