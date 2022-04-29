using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.DatabaseFirst.Model;
using EF.CodeFirst.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appsettingsConfig = builder.Configuration.GetSection("App");
builder.Services.Configure<AppSettings>(appsettingsConfig);
builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddTransient<TextFileRepository>();
builder.Services.AddTransient<DnsRepository>();
builder.Services.AddTransient<AnagramRepository>();
builder.Services.AddTransient<IAnagramSolver, AnagramSolver.BusinessLogic.AnagramSolver>();

// To use SQL database:
builder.Services.AddTransient<IWordRepository, AnagramRepository>();
builder.Services.AddTransient<ICache, CacheRepository>();
// To use txt file as database:
//builder.Services.AddTransient<IWordRepository, TextFileRepository>();

builder.Services.AddCors();

// DB First :
builder.Services.AddDbContext<AnagramSolverContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("WebAppContext");
    options.UseSqlServer(connectionString);
});
// Previous connection string:
builder.Services.AddDbContext<DataContext>(options =>
{
  string connectionString = builder.Configuration.GetConnectionString("WebAppContext");
  options.UseSqlServer(connectionString);
});
// Code First:
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DatabaseContext");
    options.UseSqlServer(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=CodeFirst}/{action=Empty}");
app.MapControllerRoute(
    name: "index",
    pattern: "{controller=CodeFirst}/{action=Index}/{word}");
app.MapControllerRoute(
    name: "search",
    pattern: "{controller=CodeFirst}/{action=Search}/{word}");

app.MapRazorPages();

app.Run();