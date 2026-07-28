using Estoque.Domain.Entities;
using Estoque.Domain.Interfaces.IRepositories;
using System.Threading;

public class BackgroundWorkerService : BackgroundService
{

    readonly ILogger<BackgroundWorkerService> _logger;
    private readonly IServiceProvider _service;

    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IServiceProvider service)
    {
        _logger = logger;
        _service = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);

            try
            {
                using var scope = _service.CreateScope();

                var repository = scope.ServiceProvider
                    .GetRequiredService<ICarrinhoRepository>();

                await repository.VerificarCarrinhos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar carrinhos.");
            }
        }
    }

}