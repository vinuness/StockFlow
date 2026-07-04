using Estoque.API.Utilidades;
using Estoque.Application.Service;
using Estoque.Domain.Interfaces.IRepositories;
using Estoque.Domain.Interfaces.IServices;
using Estoque.Domain.Interfaces.Repositories;
using Estoque.Domain.Services;
using Estoque.Infra.Data.Repositories;
using Estoque.Infrastructure.Data;
using Estoque.Infrastructure.Repositories;
using Estoque.Infrastructure.SwaggerPerso;
using Estoque.Infrastructure.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Adiciona o Swagger personalizado
builder.Services.AddInfrastructureSwagger();

var key = builder.Configuration["Jwt:Key"]; //pego a chave do appsettings.json

builder.Services
    .AddAuthentication(options => //configuro a autenticação para usar JWT Bearer
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => //configuro o JWT Bearer
    {
        options.MapInboundClaims = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)),

            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,

            RoleClaimType = "role"
        };

        options.IncludeErrorDetails = true;
        options.Events = new JwtBearerEvents
        {
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
