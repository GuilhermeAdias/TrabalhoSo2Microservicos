using BeneficioClienteFunctions.Domain;
using Microsoft.EntityFrameworkCore;

namespace CadastroClienteAPI.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Beneficio> Beneficios { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
