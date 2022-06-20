using Microsoft.EntityFrameworkCore;

namespace CadastroClienteAPI.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Beneficio> Beneficios { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .Property(p => p.Nome)
                .HasMaxLength(80);
        }
    }
}
