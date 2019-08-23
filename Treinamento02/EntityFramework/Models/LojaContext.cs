using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Models
{
    public class LojaContext : DbContext
    {
        public LojaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(opts =>
            {
                opts.HasIndex(p => p.CpfOuCnpj).IsUnique();
            });
        }
    }
}
