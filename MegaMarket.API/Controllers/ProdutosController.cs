using MegaMarket.API.DTOs;
using MegaMarket.Domain.Entities;
using MegaMarket.Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace MegaMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoResponseDto>>> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();
            return Ok(produtos.Select(p => new ProdutoResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
                QuantidadeEmEstoque = p.QuantidadeEmEstoque,
                DataCriacao = p.DataCriacao,
                DataAtualizacao = p.DataAtualizacao
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoResponseDto>> ObterPorId(int id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            if (produto == null) return NotFound();

            return Ok(new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque,
                DataCriacao = produto.DataCriacao,
                DataAtualizacao = produto.DataAtualizacao
            });
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoResponseDto>> Criar(ProdutoDto produtoDto)
        {
            var produto = new Produto
            {
                Nome = produtoDto.Nome,
                Descricao = produtoDto.Descricao,
                Preco = produtoDto.Preco,
                QuantidadeEmEstoque = produtoDto.QuantidadeEmEstoque
            };

            await _produtoRepository.AdicionarAsync(produto);

            return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque,
                DataCriacao = produto.DataCriacao
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ProdutoDto produtoDto)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            if (produto == null) return NotFound();

            produto.Nome = produtoDto.Nome;
            produto.Descricao = produtoDto.Descricao;
            produto.Preco = produtoDto.Preco;
            produto.QuantidadeEmEstoque = produtoDto.QuantidadeEmEstoque;

            await _produtoRepository.AtualizarAsync(produto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _produtoRepository.RemoverAsync(id);
            return NoContent();
        }
    }
}