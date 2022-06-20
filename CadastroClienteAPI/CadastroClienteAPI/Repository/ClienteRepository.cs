using CadastroClienteAPI.Domain;
using CadastroClienteAPI.Domain.Data;
using CadastroClienteAPI.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CadastroClienteAPI.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _db;

        public ClienteRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Cliente> GetClientes()
        {
            var clientes = _db.Clientes.ToList();

            if (clientes.Count == 0)
                return new List<Cliente>();

            List<Endereco> enderecos;
            List<Beneficio> beneficios;

            foreach (var cliente in clientes)
            {
                enderecos = _db.Enderecos.Select(e => e).Where(e => e.Id == cliente.EnderecoId).ToList();
                beneficios = _db.Beneficios.Select(b => b).Where(b => b.ClienteId == cliente.Id).ToList();

                cliente.Endereco = enderecos.FirstOrDefault();
                cliente.Beneficios = beneficios;
            }

            return clientes;
        }

        public Cliente GetCliente(int id)
        {
            var cliente = _db.Clientes.FirstOrDefault(x => x.Id == id);

            if (cliente is null)
                return null;

            var endereco = _db.Enderecos.Select(e => e).Where(e => e.Id == cliente.EnderecoId).ToList();
            var beneficio = _db.Beneficios.Select(b => b).Where(b => b.ClienteId == cliente.Id).ToList();

            cliente.Endereco = endereco.FirstOrDefault();
            cliente.Beneficios = beneficio;

            return cliente;
        }

        public bool CreateCliente(Cliente cliente)
        {
            _db.Clientes.Add(cliente);
            return Save();
        }

        public bool UpdateCliente(Cliente cliente)
        {
            _db.Clientes.Update(cliente);
            return Save();
        }

        public bool DeleteCliente(Cliente cliente)
        {
            _db.Clientes.Remove(cliente);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool ClienteExists(int id)
        {
            return _db.Clientes.Any(x => x.Id == id);
        }

        public bool ClienteExists(string cpf)
        {
            bool value = _db.Clientes.Any(y => y.Email.ToLower().Trim() == cpf.ToLower().Trim());
            return value;
        }
    }
}
