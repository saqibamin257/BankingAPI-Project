using Microsoft.EntityFrameworkCore;
using System;
using TestBankingAPI.Model;
using TestBankingAPI.Services.BankTransactions;
using TestBankingAPI.Services.CustomerBankAccount;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
#region DB Connection String
var connectionString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
           options.UseSqlServer(connectionString));
#endregion

#region Services
builder.Services.AddScoped<ICustomerBankAccountService, CustomerBankAccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
#endregion







// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
