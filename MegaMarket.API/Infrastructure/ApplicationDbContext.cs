namespace MegaMarket.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using MegaMarket.Domain.Entities;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; } // Exemplo de entidade
    }
}
