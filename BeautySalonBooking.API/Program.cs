using BeautySalonBooking.Application;
using BeautySalonBooking.Contracts.Auth.Requests.RequestsValidations;
using BeautySalonBooking.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

#region EF Core Migration Command
//Add-Migration Name -Context BeautyDbContext -Project BeautySalonBooking.Infrastructure -StartupProject BeautySalonBooking.Api -OutputDir Persistence\Migrations
//$migration = (Get-Migration | Select-Object -Last 1).id
//$migration
//$path = ".\BeautySalonBooking.Infrastructure\Persistence\SqlScripts\$migration.sql"
//$path
//if (!(Test-Path (Split-Path $path))) { New-Item -ItemType Directory -Path (Split-Path $path) -Force } 
//Script-Migration -Idempotent
//Script-Migration -Idempotent | Out-File $path -Encoding utf8
//Remove-Migration
//Update-Database
#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<BeautyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginInitiateRequestValidation>();
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();