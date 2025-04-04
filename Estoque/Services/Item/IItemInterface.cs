using Estoque.DTO;
using Estoque.Models;

namespace Estoque.Services.Item
{
    public interface IItemInterface
    {
        Task<ItemModel> CriarItem(ItemCriacaoDTO itemCriacaoDTO, IFormFile imagem);
        Task<List<ItemModel>> RetornaItens();
        Task<ItemModel> RetornaItemPeloId(int id);
        Task<ItemModel> EditarItem(ItemModel item, IFormFile? imagem);
        Task<ItemModel> RemoverItem(int id);
        Task<List<ItemModel>> RetornaItensFiltro(string? pesquisar);
    }
}
