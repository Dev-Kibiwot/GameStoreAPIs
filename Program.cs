using GameStore.api.Data;
using GameStore.api.Dtos;
using GameStore.api.Endpoints;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);
var app = builder.Build();
app.MapGameEndpoints();
app.Run();  