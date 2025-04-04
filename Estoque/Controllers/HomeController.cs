using System.Diagnostics;
using System.Threading.Tasks;
using Estoque.Models;
using Estoque.Services.Item;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemInterface _itemInterface;
        public HomeController(IItemInterface itemInterface)
        {
            _itemInterface = itemInterface;
        }
        public async Task<IActionResult> Index(string? pesquisar)
        {
            if(pesquisar == null)
            {
                var itens = await _itemInterface.RetornaItens();
                return View(itens);
            }
            else
            {
                var itens = await _itemInterface.RetornaItensFiltro(pesquisar);
                return View(itens);
            }


        }
    }
}
