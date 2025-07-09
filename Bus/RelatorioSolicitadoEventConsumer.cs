using MassTransit;
using RabbitStudy.Relatorios;

namespace RabbitStudy.Bus;

internal sealed class RelatorioSolicitadoEventConsumer : IConsumer<RelatorioSolicitadoEvent>
{
    private readonly ILogger<RelatorioSolicitadoEventConsumer> _logger;

    public RelatorioSolicitadoEventConsumer(ILogger<RelatorioSolicitadoEventConsumer> logger)
    {
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<RelatorioSolicitadoEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Processando relatório Id:{Id}, Nome:{Nome}", message.Id, message.Name);
        
        // delay
        await Task.Delay(1000);
        
        // atualizando status
        var relatorio = List.Relatorios.FirstOrDefault(r => r.Id == message.Id);

        if (relatorio != null)
        {
            relatorio.Status = "Completado!";
            relatorio.ProcessedTime = DateTime.Now;
        }
        
        _logger.LogInformation("Relatório processado Id:{Id}, Nome:{Nome}", message.Id, message.Name);
    }
}