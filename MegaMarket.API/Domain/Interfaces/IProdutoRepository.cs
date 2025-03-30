using MegaMarket.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MegaMarket.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        // Métodos básicos de CRUD
        Task<Produto> ObterPorIdAsync(int id);
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(int id);

        // Métodos específicos para controle de estoque
        Task<bool> AtualizarEstoqueAsync(int produtoId, int quantidade);
        Task<bool> ReporEstoqueAsync(int produtoId, int quantidade);

        // Métodos para consultas específicas
        Task<IEnumerable<Produto>> ObterPorNomeAsync(string nome);
        Task<IEnumerable<Produto>> ObterPorFaixaDePrecoAsync(decimal precoMinimo, decimal precoMaximo);
        Task<int> ObterQuantidadeTotalEmEstoqueAsync();
    }
}