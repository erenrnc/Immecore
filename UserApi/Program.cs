using Core.Conn;
using Core.Conn.Impl;
using Core.Db;
using Microsoft.EntityFrameworkCore;
using UserApi;
using UserApi.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Factories
var connString = builder.Configuration.GetValue<string>("DefaultConnection");
builder.Services.AddSingleton<IDatabaseConnectionFactory>(x => new SqlConnectionFactory(connString));
#endregion

builder.Services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(connString));

#region Repositories
builder.Services.AddScoped<IUserRepo, UserRepo>();
#endregion

builder.Services.AddScoped<UserBusiness>();

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
