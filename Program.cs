using GameStore.api.Dtos;
using GameStore.api.Endpoints;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGameEndpoints();
app.Run(); 