using Estoque.DelegateAuth;
using Estoque.Interfaces;
using Estoque.Models;
using Estoque.Services;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JwtHandler>();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7238");
}).AddHttpMessageHandler<JwtHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) //adiciona a autenticação baseada em cookies
.AddCookie(options =>
{
    options.LoginPath = "/Login";
});

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IEstoqueService, EstoqueService>();

builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IHomeService, HomeService>(); 

var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) => // Middleware para ler o token JWT do cookie e criar um ClaimsPrincipal
{
    var token = context.Request.Cookies["jwt"];

    if (!string.IsNullOrWhiteSpace(token))
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(
            ClaimTypes.NameIdentifier,
            jwt.Claims.First(c => c.Type == "nameid").Value));

        identity.AddClaim(new Claim(
            ClaimTypes.Name,
            jwt.Claims.First(c => c.Type == "unique_name").Value));

        identity.AddClaim(new Claim(
            ClaimTypes.Email,
            jwt.Claims.First(c => c.Type == "email").Value));

        identity.AddClaim(new Claim(
            ClaimTypes.Role,
            jwt.Claims.First(c => c.Type == "role").Value));

        context.User = new ClaimsPrincipal(identity);
    }

    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
