using Estoque.DTO;
using Estoque.Models;
using Estoque.Services.Item;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Estoque.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemInterface _itemInterface;

        public ItemController(IItemInterface itemInterface)
        {
            _itemInterface = itemInterface;
        }
        public async Task<IActionResult> Index()
        {
            var itens = await _itemInterface.RetornaItens();
            return View(itens);
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        public async Task<IActionResult> EditarItem(int id)
        {
            var item = await _itemInterface.RetornaItemPeloId(id);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ItemCriacaoDTO itemCriacaoDTO, IFormFile imagem)
        {
            if (ModelState.IsValid)
            {
                var item = await _itemInterface.CriarItem(itemCriacaoDTO, imagem);
                return RedirectToAction("Index");
            }
            else
            {
                return View(itemCriacaoDTO);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditarItem(ItemModel itemModel, IFormFile imagem)
        {
            if (ModelState.IsValid)
            {
                var item = await _itemInterface.EditarItem(itemModel, imagem);
                return RedirectToAction("Index");
            }
            else
            {
                return View(itemModel);
            }
        }
    }
}
