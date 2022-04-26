using System.Diagnostics.CodeAnalysis;
using System.Net;
using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class DbFirstController : Controller
{
    private IAnagramSolver _anagramSolver;
    private AnagramSolverContext _context;

    public DbFirstController(IAnagramSolver anagramSolver, AnagramSolverContext context)
    {
        _anagramSolver = anagramSolver;
        _context = context;
    }
    
    //[Route("/{word}")]
    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public IActionResult Index(string word)
    {
        List<AnagramSolver.Contracts.Models.WordModel> anagrams = _anagramSolver.CheckForAnagram(word);
        
        // log search to table:
        string hostName = Dns.GetHostName();
        string curentIP = Dns.GetHostByName(hostName).AddressList[1].ToString();
        _context.UserLogs.Add(new UserLog
        {
            UserIp = curentIP,
            SearchString = word,
            SearchTime = DateTime.Now,
            FoundAnagrams = string.Join( ", ", anagrams.Select(x => x.Word))
        });
        _context.SaveChanges();

        return View("Index", new DbFirstIndexViewModel  { UserWord = word, Anagrams = anagrams });
    }
    
    //[Route("/")]
    public IActionResult Empty()
    {
        return View();
    }
    
    public async Task<IActionResult> ViewAll(int? pageNumber)
    { 
        List<AnagramSolver.Contracts.Models.WordModel> words = _anagramSolver.GetAllSourceWords();

        if (pageNumber == null) pageNumber = 1;

        int pageSize = 100;
        return View(await PaginatedList<AnagramSolver.Contracts.Models.WordModel>.CreateAsync(words, pageNumber ?? 1, pageSize));
    }
    public async Task<IActionResult> Search(string? word)
    {
        var words = _context.Words.Where(x => x.Word1.Contains(word)).ToList();
        var finalWords = words.Select(x=> new AnagramSolver.Contracts.Models.WordModel{Word = x.Word1, Category = x.Category, Id = x.Id}).ToList();
        return View("Search", new DbFirstSearchViewModel {Words = finalWords}); 
    }
    public IActionResult DeleteData()
    {
        _context.Database.ExecuteSqlRaw("EXEC [dbo].[DeleteDataFromTable] @Table = CachedWords");
        return View();
    }

    public IActionResult ShowSearches()
    {
        var entries = _context.UserLogs.ToList();
        return View(new DbFirstShowSeachesViewModel{Entries = entries});
    }
}