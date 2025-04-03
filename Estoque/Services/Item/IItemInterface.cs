using Estoque.DTO;
using Estoque.Models;

namespace Estoque.Services.Item
{
    public interface IItemInterface
    {
        Task<ItemModel> CriarItem(ItemCriacaoDTO itemCriacaoDTO, IFormFile imagem);
    }
}
