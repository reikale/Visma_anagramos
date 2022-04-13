using System.Diagnostics.CodeAnalysis;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private IAnagramSolver _anagramSolver;
    
    public HomeController(IAnagramSolver anagramSolver)
    {

        _anagramSolver = anagramSolver;
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
        return View(await PaginatedList<Word>.CreateAsync(words, pageNumber ?? 1, pageSize));
       
    }
}