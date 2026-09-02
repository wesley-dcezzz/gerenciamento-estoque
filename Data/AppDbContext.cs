using Microsoft.EntityFrameworkCore;
using controleEstoque.Models;
using controleEstoque.Entities;

namespace controleEstoque.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Representa a tabela de produtos no banco de dados
        public DbSet<ProdutoEntity> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura a precisão do campo Preço (18 dígitos no total, sendo 2 casas decimais)
            modelBuilder.Entity<ProdutoEntity>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)");
        }
    }
}
