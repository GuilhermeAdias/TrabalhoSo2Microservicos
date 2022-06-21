using BeneficioClienteFunctions.Domain;
using System.Collections.Generic;

namespace CadastroClienteAPI.Repository.Interfaces
{
    public interface IBeneficioRepository
    {
        List<Beneficio> GetBeneficios();
        Beneficio GetBeneficio(int id);
        bool CreateBeneficio(Beneficio beneficio);
        bool UpdateBeneficio(Beneficio beneficio);
        bool DeleteBeneficio(Beneficio beneficio);
        bool Save();
    }
}
