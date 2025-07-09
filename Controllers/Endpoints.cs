using MassTransit;
using RabbitStudy.Relatorios;

namespace RabbitStudy.Controllers;

internal static class Endpoints
{
    public static void AddEndpoints(this WebApplication app)
    {
        app.MapPost("solicitar-relatorio/{name}", async (string name, IBus bus) =>
        {
            var solicitacao = new SolicitacaoRelatorio()
            {
                Id = Guid.NewGuid(),
                Nome = name,
                Status = "Pendente",
                ProcessedTime = null,
            };

            List.Relatorios.Add(solicitacao);

            await bus.Publish(solicitacao);

            return Results.Ok(solicitacao);
        });

        app.MapGet("relatorios", () => List.Relatorios.ToList());
    }
    
}