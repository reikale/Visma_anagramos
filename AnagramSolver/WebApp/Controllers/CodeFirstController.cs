using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;
using EF.CodeFirst.Data;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;
using Words = AnagramSolver.Contracts.Models.Words;

namespace WebApp.Controllers;

public class CodeFirstController : Controller
{
    private IAnagramSolver _anagramSolver;
    private DatabaseContext _context;
    private DnsRepository _dns;
    private AnagramRepository _anagramRepository;
    private readonly AppSettings _appSettings;
    public const int _WORD_CONTENT = 2;
    public const int _WORD_TYPE = 1;

    public CodeFirstController(IAnagramSolver anagramSolver, DatabaseContext context, DnsRepository dns, AnagramRepository anagramRepository)
    {
        _anagramSolver = anagramSolver;
        _context = context;
        _dns = dns;
        _appSettings = new AppSettingsHandler("appsettings.json").GetAppSettings();
        _anagramRepository = anagramRepository;
    }

    public ViewResult Index(string word)
    { 
        List<Words> anagrams = _anagramSolver.CheckForAnagram(word, false);
       List<EF.CodeFirst.Model.Words> anagramList = anagrams.Select(x => new EF.CodeFirst.Model.Words {Id = x.Id, Word = x.Word, Category = x.Category}).ToList();
       bool isSearchCountWithinLimit = _dns.LogUserSearch(_context, word, anagramList);
       if(isSearchCountWithinLimit) return View("Index", new CodeFirstIndexViewModel{UserWord = word, Anagrams = anagramList});
       return View("Error", new ErrorViewModel{Message = "You have exceeded the limit of searches"});
    }

    public IActionResult Empty()
    {
        if (_context.Words.ToList().Count == 0)
        {
            var data = System.IO.File.ReadLines(_appSettings.WordsRepoSource.PathToWordsRepo);
            List<Words> words = new List<Words>();
            foreach (string line in data)
            {
                string[] lineContent = line.Split("\t");
                string wordContent = lineContent[_WORD_CONTENT];
                string wordType = lineContent[_WORD_TYPE];
                _context.Words.Add(new EF.CodeFirst.Model.Words
                {
                    Word = wordContent,
                    Category = wordType
                });
            }
        }
        _context.SaveChanges();
        return View();
    }

    public async Task<IActionResult> ViewAll(int? pageNumber)
    {
        var words= _anagramSolver.GetAllSourceWords(false);
        List<EF.CodeFirst.Model.Words> wordList = words.Select(x => new EF.CodeFirst.Model.Words {Id = x.Id, Word = x.Word, Category = x.Category}).ToList();

        if (pageNumber == null) pageNumber = 1;

        int pageSize = 100;
        return View(await PaginatedList<EF.CodeFirst.Model.Words>.CreateAsync(wordList, pageNumber ?? 1, pageSize));
    }

    public async Task<IActionResult> Search(string? word)
    {
        var words = _context.Words.Where(x => x.Word.Contains(word)).ToList();
        List<EF.CodeFirst.Model.Words> wordList = words.Select(x => new EF.CodeFirst.Model.Words {Id = x.Id, Word = x.Word, Category = x.Category}).ToList();
        return View("Search", new CodeFirstSearchViewModel{Words = wordList}); 
    }

    public IActionResult ShowSearches()
    {
        var entries = _context.UserLogs.ToList();
        return View(new CodeFirstShowSearchesViewModel {Entries = entries});
    }

    public IActionResult AddWord()
    {
        return View();
    }
    public IActionResult EditWord()
    {
        return View();
    }
    public IActionResult RemoveWord()
    {
        return View();
    }

    public IActionResult Save(DictionaryModification form)
    {
        if (ModelState.IsValid)
        {
            switch (form.ModificationType)
            {
                case "Add":
                    _anagramRepository.AddToDictionary(new EF.CodeFirst.Model.Words{Word = form.Word, Category = form.Category});
                    _dns.AlterSearchLimit(_context, 5);
                    break;
                case "Edit":
                    if (_anagramRepository.EditWordInDictionary(form.Word, form.Category))
                    {
                        _dns.AlterSearchLimit(_context, 1); 
                    }
                    else
                    {
                        return View("Error", new ErrorViewModel{Message = "The word user entered does not exist in dictionary"});
                    }
                    break;
                case "Remove":
                    _anagramRepository.RemoveFromDictionary(form.Word);
                    _dns.AlterSearchLimit(_context, -1);
                    break;
            }
            return View();
        }

        return RedirectToAction("Error", new {message="Invalid input. Try again"});
    }

    public IActionResult Error(string? message)
    {
        return View(new ErrorViewModel{Message = message});
    }
}