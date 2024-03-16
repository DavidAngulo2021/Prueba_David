using API_PROYECTO.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DB_PdavidContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));


var reglarCors = "ReglaCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: reglarCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors(reglarCors);

app.UseAuthorization();

app.MapControllers();

app.Run();