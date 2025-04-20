using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Estoque.DTO;
using Estoque.Models;
using Estoque.Services.Item;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        public IActionResult CadastrarItem()
        {
            return View();
        }
        public async Task<IActionResult> EditarItem(int id)
        {
            var item = await _itemInterface.RetornaItemPeloId(id);
            return View(item);
        }
        public async Task<IActionResult> RemoverItem(int id)
        {
            var item = await _itemInterface.RemoverItem(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DetalhesItem(int id)
        {
            var item = await _itemInterface.RetornaItemPeloId(id);
            return View(item);
        }
        public async Task<IActionResult> Exportar()
        {
            var dados = await _itemInterface.RetornaDados();
            using(XLWorkbook workBook = new XLWorkbook())
            {
                workBook.AddWorksheet(dados, "Listagem de Estoque");
                using(MemoryStream ms = new MemoryStream())
                {
                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Listagem.xls");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> CadastrarItem(ItemCriacaoDTO itemCriacaoDTO, IFormFile imagem)
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
