using FluentValidation;
using volei_app.Features.Players.Repositories;
using volei_app.Features.Players.Services;
using volei_app.Features.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços de Controllers ao contêiner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o SignalR
builder.Services.AddSignalR();

// Injeção de dependências para o padrão de repositório e serviço.
// Aqui, registramos as interfaces e suas implementações concretas da feature Players.
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

// Adiciona o FluentValidation para validação, buscando validadores na aplicação inteira.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Configura o CORS para permitir requisições do frontend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

// Mapeia os controllers e o hub do SignalR.
app.MapControllers();
app.MapHub<VoleiHub>("/voleihub");

app.Run();