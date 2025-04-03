using Estoque.Data;
using Estoque.DTO;
using Estoque.Models;

namespace Estoque.Services.Item
{
    public class ItemService : IItemInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly string _sistema;
        public ItemService(ApplicationDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;
        }
        public string GeraCaminhoArquivo(IFormFile imagem)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoImagem = imagem.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";
            var caminhoParaSalvarImagens = _sistema + "\\imagens\\";
            if(!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }
            using(var stream = File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
            {
                imagem.CopyToAsync(stream).Wait();
            }
            return nomeCaminhoImagem;
        }
        public async Task<ItemModel> CriarItem(ItemCriacaoDTO itemCriacaoDTO, IFormFile imagem)
        {
            try
            {
                var nomeCaminhoImagem = GeraCaminhoArquivo(imagem);
                var item = new ItemModel
                {
                    Nome = itemCriacaoDTO.Nome,
                    Descricao = itemCriacaoDTO.Descricao,
                    Quantidade = itemCriacaoDTO.Quantidade,
                    Valor = itemCriacaoDTO.Valor,
                    Imagem = nomeCaminhoImagem
                };
                _context.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
