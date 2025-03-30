using MegaMarket.Domain.Entities;
using MegaMarket.Domain.Interfaces;
using MegaMarket.Infrastructure;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegaMarket.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Métodos básicos de CRUD (que estavam faltando)
        public async Task<Produto> ObterPorIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task AdicionarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            produto.DataAtualizacao = DateTime.UtcNow;
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        // Métodos de estoque
        public async Task<bool> AtualizarEstoqueAsync(int produtoId, int quantidade)
        {
            var produto = await _context.Produtos.FindAsync(produtoId);
            if (produto == null || produto.QuantidadeEmEstoque < quantidade)
                return false;

            produto.QuantidadeEmEstoque -= quantidade;
            produto.DataAtualizacao = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReporEstoqueAsync(int produtoId, int quantidade)
        {
            var produto = await _context.Produtos.FindAsync(produtoId);
            if (produto == null) return false;

            produto.QuantidadeEmEstoque += quantidade;
            produto.DataAtualizacao = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        // Métodos de consulta
        public async Task<IEnumerable<Produto>> ObterPorNomeAsync(string nome)
        {
            return await _context.Produtos
                .Where(p => p.Nome.Contains(nome))
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterPorFaixaDePrecoAsync(decimal precoMinimo, decimal precoMaximo)
        {
            return await _context.Produtos
                .Where(p => p.Preco >= precoMinimo && p.Preco <= precoMaximo)
                .ToListAsync();
        }

        public async Task<int> ObterQuantidadeTotalEmEstoqueAsync()
        {
            return await _context.Produtos.SumAsync(p => p.QuantidadeEmEstoque);
        }
    }
}