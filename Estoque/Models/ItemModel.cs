namespace Estoque.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Imagem { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public double Valor { get; set; }

    }
}
