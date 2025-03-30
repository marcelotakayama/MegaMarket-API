namespace MegaMarket.API.DTOs
{
    public class ProdutoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
    }

    public class ProdutoResponseDto : ProdutoDto
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}