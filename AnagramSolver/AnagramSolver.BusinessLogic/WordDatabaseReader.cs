using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class WordDatabaseReader : IWordRepository
{
    public DataContext _context;
    public WordDatabaseReader(DataContext context)
    {
        _context = context;
    }

    public List<WordModel> ReturnWordListFromSource()
    {
        return _context.Words.ToList();
    }

    public bool CheckForCache(WordModel searchedWord)
    {
        var result =  _context.CachedWords.Where(x => x.SearchedWord == searchedWord.Word).FirstOrDefault();
        return result != null;
    }
    public void CacheWord(WordModel searchedWord, List<WordModel> listOfAnagrams)
    {
        foreach (WordModel word in listOfAnagrams)
        {
            _context.CachedWords.Add(new CachedWord
            {
                SearchedWord = searchedWord.Word,
                AnagramsId = word.Id
            });
        }
        _context.SaveChanges();
    }

    public List<WordModel> FindInCache(WordModel searchedWord)
    {
        
        var cacheList =  _context.CachedWords.Where(x => x.SearchedWord == searchedWord.Word).ToList();
        List<WordModel> returnList = new List<WordModel>();
        foreach (var cache in cacheList)
        {
            returnList.Add(_context.Words.Where(x => x.Id == cache.AnagramsId).FirstOrDefault());
        }
        return returnList;
    }
}