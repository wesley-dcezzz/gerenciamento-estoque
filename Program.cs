using controleEstoque.Data;
using controleEstoque.Models;
using controleEstoque.Services;
using controleEstoque.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 1. Registra o contexto do banco de dados (exemplo usando MySQL/Pomelo)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

//Adiciona validação automatica
builder.Services.AddFluentValidationAutoValidation();

//Injeção de dependência para o Service no controller
builder.Services.AddScoped<gerenciamentoService>();

//Registro explícito: Em vez de escanear o assembly todo, indique a classe exata:
builder.Services.AddScoped<IValidator<requestModel>, requestModelValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("PermitirTudo");

app.MapControllers();

app.Run();