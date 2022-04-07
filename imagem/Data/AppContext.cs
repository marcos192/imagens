using imagem.Models;
using Microsoft.EntityFrameworkCore;

namespace imagem.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
