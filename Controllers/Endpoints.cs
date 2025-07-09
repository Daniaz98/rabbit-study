using RabbitStudy.Relatorios;

namespace RabbitStudy.Controllers;

internal static class Endpoints
{
    public static void AddEndpoints(this WebApplication app)
    {
        app.MapPost("solicitar-relatorio/{name}", (string name) =>
        {
            var solicitacao = new SolicitacaoRelatorio()
            {
                Id = Guid.NewGuid(),
                Nome = name,
                Status = "Pendente",
                ProcessedTime = null,
            };

            List.Relatorios.Add(solicitacao);

            return Results.Ok(solicitacao);
        });

        app.MapGet("relatorios", () => List.Relatorios.ToList());
    }
    
}