using Estoque.Infrastructure.Utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;

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

            var ConfigPath = Environment.ExpandEnvironmentVariables(config["ConnectionStrings:connection"]); //recebo o atributo connection presente no arquivo

            var constant = new Constants();
            constant.ConfigFilePath = ConfigPath;

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(Constants.Connection); //insiro a conexao com o Postgre

            return new AppDbContext(optionsBuilder.Options); //retorno um novo DbContext
        }
    }
}