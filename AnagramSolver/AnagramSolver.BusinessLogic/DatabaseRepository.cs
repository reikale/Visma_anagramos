using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class DatabaseRepository : IWordRepository
{
    public DataContext _context;
    public DatabaseRepository(DataContext context)
    {
        _context = context;
    }

    public List<WordModel> ReturnWordListFromSource()
    {
        return _context.Words.ToList();
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
        List<int> wordsIds = cacheList.Select(x => x.AnagramsId).ToList();
        List<WordModel> returnList = _context.Words.Where(x => wordsIds.Contains(x.Id)).ToList();

        return returnList;
    }
}