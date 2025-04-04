using Estoque.Data;
using Estoque.DTO;
using Estoque.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<ItemModel>> RetornaItens()
        {
            try
            {
                return await _context.Itens.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ItemModel> RetornaItemPeloId(int id)
        {
            try
            {
                return await _context.Itens.FirstOrDefaultAsync(item => item.Id == id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ItemModel> EditarItem(ItemModel item, IFormFile? imagem)
        {
            try
            {
                var itemBanco = await _context.Itens.AsNoTracking().FirstOrDefaultAsync(itemBD => itemBD.Id == item.Id);
                var nomeCaminhoImagem = "";
                if(imagem != null)
                {
                    string caminhoImagemExistente = _sistema + "\\imagens\\" + itemBanco.Imagem;
                    if (File.Exists(caminhoImagemExistente))
                    {
                        File.Delete(caminhoImagemExistente);
                    }
                    nomeCaminhoImagem = GeraCaminhoArquivo(imagem);
                }
                itemBanco.Nome = item.Nome;
                itemBanco.Descricao = item.Descricao;
                itemBanco.Quantidade = item.Quantidade;
                itemBanco.Valor = item.Valor;
                if(nomeCaminhoImagem != "")
                {
                    itemBanco.Imagem = nomeCaminhoImagem;
                }
                else
                {
                    itemBanco.Imagem = itemBanco.Imagem;
                }
                _context.Update(itemBanco);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemModel> RemoverItem(int id)
        {
            try
            {
                var item = await _context.Itens.FirstOrDefaultAsync(itemBanco => itemBanco.Id == id);
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ItemModel>> RetornaItensFiltro(string? pesquisar)
        {
            try
            {
                var itens = await _context.Itens.Where(itensBanco => itensBanco.Nome.Contains(pesquisar)).ToListAsync();
                return itens;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
