using DotNetCapTest.Web;
using DotNetCapTest.Web.Entities;
using DotNetCapTest.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")!);
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddCap(x =>
{
    x.UseEntityFramework<AppDbContext>();

    x.UseKafka(builder.Configuration.GetValue<string>("Kafka:Brokers"));

    x.UseDashboard();
});

builder.Services.AddTransient<TransactionConsumer>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseDatabaseMigration();

app.UseAuthorization();

app.MapControllers();

app.Run();
