using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
