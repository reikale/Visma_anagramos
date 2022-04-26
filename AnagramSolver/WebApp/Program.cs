using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.DatabaseFirst;
using AnagramSolver.EF.DatabaseFirst.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appsettingsConfig = builder.Configuration.GetSection("App");
builder.Services.Configure<AppSettings>(appsettingsConfig);
builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddTransient<TextFileRepository>();
builder.Services.AddTransient<IAnagramSolver, AnagramSolver.BusinessLogic.AnagramSolver>();

// To use SQL database:
builder.Services.AddTransient<IWordRepository, DatabaseRepository>();
// To use txt file as database:
//builder.Services.AddTransient<IWordRepository, DictionarySourceReader>();

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
   pattern: "{controller=Home}/{action=Empty}");
app.MapControllerRoute(
    name: "index",
    pattern: "{controller=Home}/{action=Index}/{word}");
app.MapControllerRoute(
    name: "index",
    pattern: "{controller=Home}/{action=Search}/{word}");

app.MapRazorPages();

app.Run();