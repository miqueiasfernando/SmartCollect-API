using Fiap.Api.Coletas.Models;
using Microsoft.EntityFrameworkCore;
namespace Fiap.Api.Coletas.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ColetasModel> Coletas { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ColetasModel>()
                .ToTable("tb_coletas")
                .HasKey(a => a.Id);
            modelBuilder.Entity<ColetasModel>()
                .Property(a => a.DataColeta)
                .IsRequired()
                .HasColumnName("data_coleta");
            modelBuilder.Entity<ColetasModel>()
                .Property(a => a.HoraColeta)
                .IsRequired()
                .HasColumnName("hora_coleta");
            modelBuilder.Entity<ColetasModel>()
                .Property(a => a.TipoResiduo)
                .HasColumnName("tipo_residuo");
            modelBuilder.Entity<ColetasModel>()
                .Property(a => a.Endereco)
                .HasColumnName("endereco");

        }
    }
}