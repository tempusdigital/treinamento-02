using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Completo
{
    public partial class TreinamentoCompletoContext : DbContext
    {
        public TreinamentoCompletoContext()
        {
        }

        public TreinamentoCompletoContext(DbContextOptions<TreinamentoCompletoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Teste> Teste { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=TreinamentoCompleto;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}
    }
}
