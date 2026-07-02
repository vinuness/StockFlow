using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Estoque.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build(); //pego o diretorio atual e coloco o arquivo .json

            var connection = config.GetConnectionString("connection"); //recebo o atributo connection presente no arquivo

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection)); //insiro a conexao com o MySQL

            return new AppDbContext(optionsBuilder.Options); //retorno um novo DbContext
        }
    }
}