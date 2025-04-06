using System.ComponentModel.DataAnnotations;

namespace Estoque.DTO
{
    public class ItemCriacaoDTO
    {
        [Required(ErrorMessage = "Por favor digite um nome")]
        public string Nome { get; set; } = string.Empty;
        public string Imagem { get; set; } = string.Empty;
        [Required(ErrorMessage = "Por favor digite uma descrição")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "Por favor defina uma quantidade")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "Por favor defina um valor")]
        public double Valor { get; set; }
    }
}
