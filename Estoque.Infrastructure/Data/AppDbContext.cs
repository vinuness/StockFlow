using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Pedidos;
using Estoque.Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Produto tem SKU unico
            var produto = modelBuilder.Entity<Produto>();
            produto.HasIndex(p => p.SKU).IsUnique();

            modelBuilder.Entity<ImagemModel>()
                .HasOne(i => i.Produto)
                .WithMany(P => P.Imagens)
                .HasForeignKey(i => i.ProdutoId);

            //Cliente tem email e Cpf unicos
            var cliente = modelBuilder.Entity<Cliente>();
            cliente.HasIndex(c => c.Email).IsUnique();
            cliente.HasIndex(c => c.CPF).IsUnique();
            cliente.HasMany(c => c.Enderecos)
                .WithMany(c => c.Clientes);

            
        }

        //Entidades no banco

        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ImagemModel> Imagens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}
