using Microsoft.EntityFrameworkCore;
using MegaMarket.Domain.Entities;

namespace MegaMarket.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                // Configura a precisão para o campo Preco
                entity.Property(p => p.Preco)
                      .HasPrecision(18, 2); // 18 dígitos no total, 2 casas decimais

                // Configuração opcional para o Nome
                entity.Property(p => p.Nome)
                      .IsRequired()
                      .HasMaxLength(100);

                // Configuração para QuantidadeEmEstoque
                entity.Property(p => p.QuantidadeEmEstoque)
                      .IsRequired()
                      .HasDefaultValue(0); // Valor padrão 0
            });

            // Adicione aqui outras configurações de entidades
        }
    }
}