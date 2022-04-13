using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appsettingsConfig = builder.Configuration.GetSection("App");
builder.Services.Configure<AppSettings>(appsettingsConfig);
builder.Services.AddRazorPages();
builder.Services.AddTransient<IWordRepository, AnagramSolver.BusinessLogic.DictionarySourceReader>();
builder.Services.AddTransient<IAnagramSolver, AnagramSolver.BusinessLogic.AnagramSolver>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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

app.MapRazorPages();

app.Run();