using MassTransit;
using RabbitStudy.Bus;
using RabbitStudy.Relatorios;

namespace RabbitStudy.Controllers;

internal static class Endpoints
{
    public static void AddEndpoints(this WebApplication app)
    {
        app.MapPost("solicitar-relatorio/{name}", async (string name, IPublishBus bus, CancellationToken ct) =>
        {
            var solicitacao = new SolicitacaoRelatorio()
            {
                Id = Guid.NewGuid(),
                Nome = name,
                Status = "Pendente",
                ProcessedTime = null,
            };

            List.Relatorios.Add(solicitacao);
            
            var eventRequest = new RelatorioSolicitadoEvent(solicitacao.Id, solicitacao.Nome);

            await bus.PublishAsync(eventRequest, ct);

            return Results.Ok(solicitacao);
        });

        app.MapGet("relatorios", () => List.Relatorios.ToList());
    }
    
}