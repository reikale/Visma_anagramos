using System.Diagnostics.CodeAnalysis;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private IAnagramSolver _anagramSolver;
    
    public HomeController(IAnagramSolver anagramSolver)
    {

        _anagramSolver = anagramSolver;
    }
    
    // GET
    [Route("/{word}")]
    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public IActionResult Index(string word)
    {
        ViewData["UserWord"] = word;
        ViewData["ResultList"] = _anagramSolver.CheckForAnagram(word);
        return View("Index");
    }
    [Route("/")]
    public IActionResult Empty()
    {
        return View("Empty");
    }
    [Route("/ViewAll")]
    public async Task<IActionResult> ViewAll(int? pageNumber)
    { 
        var words= _anagramSolver.GetAllSourceWords();
        ViewData["ResultList"] = _anagramSolver.GetAllSourceWords();
        if (pageNumber == null) pageNumber = 1;

        int pageSize = 100;
        return View(await PaginatedList<Word>.CreateAsync(words, pageNumber ?? 1, pageSize));
    }
}