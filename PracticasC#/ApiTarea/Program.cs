using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var usuarios = new Dictionary<int, decimal>
{
    {1, 1500.00m},
    {2, 200.00m}
};

app.MapGet("/api/usuarios/{id}/saldo", (int id) =>
{
    if (usuarios.TryGetValue(id, out var saldo))
    {
        return Results.Ok(new {UsuarioId = id, Saldo = saldo});
    }
    return Results.NotFound(new {Mensaje = "Usuario no encontrado"});
});

app.MapPost("/api/usuarios/{id}/debitar", (int id, decimal CobrarMonto) =>
{
    if (usuarios.TryGetValue(id, out var saldo))
    {
        if (saldo >= CobrarMontoonto)
        {
            usuarios[id] -= CobrarMonto;

            return Results.Ok(new
            {
                UsuarioId = id,
                NuevoSaldo = usuarios[id]
            });
        }

        return Results.BadRequest(new
        {
            Mensaje = "Saldo insuficiente"
        });
    }

    return Results.NotFound(new
    {
        Mensaje = "Usuario no encontrado"
    });
});

app.Run("https://localhost:5001");