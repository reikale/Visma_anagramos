using System.Data;
using AnagramSolver.Contracts;
using DiffEngine;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
//app.MapGet("/", () => "");
//app.MapGet("/Home/Index.cshtml", () => "Home");
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();