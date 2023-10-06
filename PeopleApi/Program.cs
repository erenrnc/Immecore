using AutoMapper;
using Core.Conn;
using Core.Conn.Impl;
using Core.Db;
using Core.Mappers;
using Microsoft.EntityFrameworkCore;
using PeopleApi.Business;
using PeopleApi.Repositories;
using PeopleApi.Repositories.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Factories
var connString = builder.Configuration.GetValue<string>("DefaultConnection");
builder.Services.AddSingleton<IDatabaseConnectionFactory>(x => new SqlConnectionFactory(connString));
#endregion

builder.Services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(connString));

#region Repositories
builder.Services.AddScoped<IPeopleRepo, PeopleRepo>();
builder.Services.AddScoped<ISalaryRepo, SalaryRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
#endregion

#region Business
builder.Services.AddScoped<PeopleBusiness>();
builder.Services.AddScoped<SalaryBusiness>();
builder.Services.AddScoped<DepartmentBusiness>();
#endregion

#region AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options =>
    {
        options.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

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

app.UseCors("AllowOrigin");

app.Run();
