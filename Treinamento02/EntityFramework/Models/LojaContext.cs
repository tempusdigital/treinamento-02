using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.Models
{
    public partial class LojaContext : DbContext
    {
        public LojaContext()
        {
        }

        public LojaContext(DbContextOptions<LojaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<ClienteTelefone> Clientetelefone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(e => e.CpfOuCnpj)
                    .IsUnique();

                entity.Property(e => e.CpfOuCnpj).HasMaxLength(15);

                entity.Property(e => e.Endereco).HasMaxLength(150);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ClienteTelefone>(entity =>
            {
                entity.ToTable("clientetelefone");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClienteId).HasColumnName("clienteid");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasColumnName("numero")
                    .HasMaxLength(18);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Telefones)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("fk_clientetelefone_cliente");
            });

            modelBuilder.Entity<Numeracao>(entity =>
            {
                entity.Property(p => p.UltimoNumero).ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Produto>();
            
            modelBuilder.Query<ClienteComTelefoneQuery>().ToView("clientecomtelefone");
        }
    }
}
