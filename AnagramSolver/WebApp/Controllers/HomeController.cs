using System.Diagnostics.CodeAnalysis;
using System.Net;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private IAnagramSolver _anagramSolver;
    private DataContext _context;
    private const bool _USE_DATA_CONTEXT = true;

    public HomeController(IAnagramSolver anagramSolver, DataContext context)
    {
        _anagramSolver = anagramSolver;
        _context = context;
    }
    
    //[Route("/{word}")]
    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public IActionResult Index(string word)
    {
        List<Words> anagrams = _anagramSolver.CheckForAnagram(word, _USE_DATA_CONTEXT);
        
        string hostName = Dns.GetHostName();
        string curentIP = Dns.GetHostByName(hostName).AddressList[1].ToString();
        _context.UserLogs.Add(new UserLog
        {
            UserIP = curentIP,
            SearchString = word,
            SearchTime = DateTime.Now,
            FoundAnagrams = string.Join( ", ", anagrams.Select(x => x.Word))
        });
        _context.SaveChanges();

        return View("Index", new AnagramResponseViewModel  { UserWord = word, Anagrams = anagrams });
    }
    
    //[Route("/")]
    public IActionResult Empty()
    {
        return View();
    }
    
    public async Task<IActionResult> ViewAll(int? pageNumber)
    { 
        var words= _anagramSolver.GetAllSourceWords(true);
        if (pageNumber == null) pageNumber = 1;
        int pageSize = 100;
        return View(await PaginatedList<Words>.CreateAsync(words, pageNumber ?? 1, pageSize));
    }
    public async Task<IActionResult> Search(string? word)
    {
        var words = _context.Words.Where(x => x.Word.Contains(word)).ToList();
        return View("Search", new WordSearchModel{Words = words}); 
    }
    public IActionResult DeleteData()
    {
        _context.Database.ExecuteSqlRaw("EXEC [dbo].[DeleteDataFromTable] @Table = CachedWords");
        return View();
    }

    public IActionResult ShowSearches()
    {
        var entries = _context.UserLogs.ToList();
        return View(new UserLogViewModel{Entries = entries});
    }
}