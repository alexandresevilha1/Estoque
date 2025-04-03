using Estoque.DTO;
using Estoque.Services.Item;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemInterface _itemInterface;

        public ItemController(IItemInterface itemInterface)
        {
            _itemInterface = itemInterface;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {
            return View();
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
    }
}
