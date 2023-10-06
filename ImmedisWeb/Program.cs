using AutoMapper;
using Core.Conn;
using Core.Conn.Impl;
using Core.Db;
using Core.Mappers;
using FluentValidation;
using ImmedisWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PeopleApi.Business;
using PeopleApi.Repositories;
using PeopleApi.Repositories.Impl;
using Prometheus;
using UserApi;
using UserApi.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Factories
var connString = builder.Configuration.GetValue<string>("DefaultConnection");
builder.Services.AddSingleton<IDatabaseConnectionFactory>(x => new SqlConnectionFactory(connString));
#endregion

builder.Services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(connString));

#region Repositories
builder.Services.AddScoped<IPeopleRepo, PeopleRepo>();
builder.Services.AddScoped<ISalaryRepo, SalaryRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
#endregion

#region Business
builder.Services.AddScoped<PeopleBusiness>();
builder.Services.AddScoped<SalaryBusiness>();
builder.Services.AddScoped<DepartmentBusiness>();
builder.Services.AddScoped<UserBusiness>();
#endregion

#region AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(o =>
     {
         o.LoginPath = new PathString("/Login/Index");
         o.LogoutPath = new PathString("/Login/LogOut");
         o.Cookie.Name = "_auth";
     });

#region Validator
builder.Services.AddScoped<IValidator<LoginModel>, LoginModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginModelValidator>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseHttpMetrics();

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapMetrics();

app.Run();
