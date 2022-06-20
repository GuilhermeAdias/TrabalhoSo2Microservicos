using CadastroClienteAPI.Domain;
using System.Collections.Generic;

namespace CadastroClienteAPI.Repository.Interfaces
{
    public interface IClienteRepository
    {
        List<Cliente> GetClientes();

        Cliente GetCliente(int id);

        bool ClienteExists(int id);

        bool ClienteExists(string email);

        bool CreateCliente(Cliente cliente);

        bool UpdateCliente(Cliente cliente);

        bool DeleteCliente(Cliente cliente);

        bool Save();
    }
}
