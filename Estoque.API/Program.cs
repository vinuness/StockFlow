using Estoque.API.Utilidades;
using Estoque.Application.Service;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Domain.Services;
using Estoque.Infra.Data.Repositories;
using Estoque.Infrastructure.Data;
using Estoque.Infrastructure.Repositories;
using Estoque.Infrastructure.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = builder.Configuration["Jwt:Key"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //adiciona o serviço de autenticação usando o esquema JWT Bearer
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key)), //chave secreta usada para validar a assinatura do token

                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };
    });

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();

builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();

builder.Services.AddScoped<JWTService>();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Obtém o caminho do arquivo de configuração a partir do appsettings.json e expande as variáveis de ambiente
var ConfigPath = Environment.ExpandEnvironmentVariables(builder.Configuration["ConnectionStrings:ConfigPath"]);

var constant = new Constants();
constant.ConfigFilePath = ConfigPath;

builder.Services.AddDbContext<AppDbContext>((options) =>
{
    options.UseMySql(Constants.Connection, ServerVersion.AutoDetect(Constants.Connection));
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50MB
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors((options) =>
{
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
