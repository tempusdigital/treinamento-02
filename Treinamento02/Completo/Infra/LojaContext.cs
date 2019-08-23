using Microsoft.EntityFrameworkCore;

namespace Completo.Infra
{
    public class Teste
    {
        public int Id { get; set; }
    }

    public class LojaContext : DbContext
    {
        public LojaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Teste>();
        }
    }
}
