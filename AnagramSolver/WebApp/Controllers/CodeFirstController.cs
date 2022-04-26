using System.Net;
using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Model;
using EF.CodeFirst.Data;
using EF.CodeFirst.Model;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;
using UserLog = EF.CodeFirst.Model.UserLog;
namespace WebApp.Controllers;

public class CodeFirstController : Controller
{
    private IAnagramSolver _anagramSolver;
    private DatabaseContext _context;

    public CodeFirstController(IAnagramSolver anagramSolver, DatabaseContext context)
    {
        _anagramSolver = anagramSolver;
        _context = context;
    }

    public ViewResult Index(string word)
    {
       List<AnagramSolver.Contracts.Models.WordModel> anagrams = _anagramSolver.CheckForAnagram(word);
       List<Words> anagramList = anagrams.Select(x => new Words {Id = x.Id, Word = x.Word, Category = x.Category}).ToList();
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
       return View("Index", new CodeFirstIndexViewModel{UserWord = word, Anagrams = anagramList});
    }

    public IActionResult Empty()
    {
        if (_context.Words.ToList().Count == 0)
        {
            var words= _anagramSolver.GetAllSourceWords();
            words.ForEach(x => _context.Words.Add(new Words
            {
                Word = x.Word,
                Category = x.Category
            }));
        }
        _context.SaveChanges();
        return View();
    }

    public async Task<IActionResult> ViewAll(int? pageNumber)
    {
        var words= _anagramSolver.GetAllSourceWords();
        List<Words> wordList = words.Select(x => new Words {Id = x.Id, Word = x.Word, Category = x.Category}).ToList();

        if (pageNumber == null) pageNumber = 1;

        int pageSize = 100;
        return View(await PaginatedList<Words>.CreateAsync(wordList, pageNumber ?? 1, pageSize));
    }

    public async Task<IActionResult> Search(string? word)
    {
        var words = _context.Words.Where(x => x.Word.Contains(word)).ToList();
        List<Words> wordList = words.Select(x => new Words {Id = x.Id, Word = x.Word, Category = x.Category}).ToList();
        return View("Search", new CodeFirstSearchViewModel{Words = wordList}); 
    }

    public IActionResult ShowSearches()
    {
        var entries = _context.UserLogs.ToList();
        return View(new CodeFirstShowSearchesViewModel {Entries = entries});
    }
}