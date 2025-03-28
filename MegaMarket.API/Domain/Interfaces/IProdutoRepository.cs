using MegaMarket.Domain.Entities;

namespace MegaMarket.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> GetByIdAsync(int id);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}
