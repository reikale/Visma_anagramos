using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private IAnagramSolver _anagramSolver;
    private DataContext _context;

    public HomeController(IAnagramSolver anagramSolver, DataContext context)
    {
        _anagramSolver = anagramSolver;
        _context = context;
    }
    
    // GET
    //[Route("/{word}")]
    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public IActionResult Index(string word)
    {
        return View("Index", new AnagramResponseViewModel  { UserWord = word, Anagrams = _anagramSolver.CheckForAnagram(word) });
    }
    
    //[Route("/")]
    public IActionResult Empty()
    {
        return View();
    }
    
    public async Task<IActionResult> ViewAll(int? pageNumber)
    { 
        var words= _anagramSolver.GetAllSourceWords();

        if (pageNumber == null) pageNumber = 1;

        int pageSize = 100;
        return View(await PaginatedList<WordModel>.CreateAsync(words, pageNumber ?? 1, pageSize));
    }
    public async Task<IActionResult> Search(string? word)
    {
        var words = _context.Words.FromSqlRaw("SELECT * FROM dbo.Words WHERE Word LIKE '%'+@UserInput+'%'", new SqlParameter("@UserInput", word)).ToList();

        //return foundWords;
        return View("Search", new WordSearchModel{Words = words}); 
    }
}