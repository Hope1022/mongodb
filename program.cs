using pokedex.Services;
using pokedex.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

string mongoConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection");
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(mongoConnectionString));
builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<IPokemonService, PokemonService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
