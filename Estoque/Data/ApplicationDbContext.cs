using Estoque.Models;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Data
{
    //Classe responsavel pela conexão entre a aplicação e o SqlServer
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ItemModel> Itens  { get; set; }
    }
}
