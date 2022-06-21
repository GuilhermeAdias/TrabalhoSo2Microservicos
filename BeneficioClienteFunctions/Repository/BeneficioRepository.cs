using BeneficioClienteFunctions.Domain;
using CadastroClienteAPI.Domain.Data;
using CadastroClienteAPI.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CadastroClienteAPI.Repository
{
    public class BeneficioRepository : IBeneficioRepository
    {
        private readonly AppDbContext _db;

        public BeneficioRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Beneficio> GetBeneficios()
        {
            var clientes = _db.Beneficios.ToList();

            if (clientes.Count == 0)
                return new List<Beneficio>();

            return clientes;
        }

        public Beneficio GetBeneficio(int id)
        {
            var cliente = _db.Beneficios.FirstOrDefault(x => x.Id == id);

            if (cliente is null)
                return null;

            return cliente;
        }

        public bool CreateBeneficio(Beneficio beneficio)
        {
            _db.Beneficios.Add(beneficio);
            return Save();
        }

        public bool UpdateBeneficio(Beneficio beneficio)
        {
            _db.Beneficios.Update(beneficio);
            return Save();
        }

        public bool DeleteBeneficio(Beneficio beneficio)
        {
            _db.Beneficios.Remove(beneficio);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
