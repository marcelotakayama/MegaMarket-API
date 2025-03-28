namespace MegaMarket.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using MegaMarket.Domain.Entities;
    using MegaMarket.Domain.Interfaces;

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> GetByIdAsync(int id) => await _context.Produtos.FindAsync(id);
        public async Task<IEnumerable<Produto>> GetAllAsync() => await _context.Produtos.ToListAsync();
        public async Task AddAsync(Produto produto) => await _context.Produtos.AddAsync(produto);
        public async Task UpdateAsync(Produto produto) => _context.Produtos.Update(produto);
        public async Task DeleteAsync(int id) => _context.Produtos.Remove(new Produto { Id = id });
    }
}
