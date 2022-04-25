using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Model;
using CachedWord = AnagramSolver.EF.DatabaseFirst.Model.CachedWord;

namespace AnagramSolver.EF.DatabaseFirst;

public class DBFirstRepository
{
    public AnagramSolverContext _context;
    public DBFirstRepository(AnagramSolverContext context)
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
                SearchedWord = searchedWord.Word1,
                AnagramsId = word.Id
            });
        }
        _context.SaveChanges();
    }

    public List<WordModel> FindInCache(WordModel searchedWord)
    {
        var cacheList =  _context.CachedWords.Where(x => x.SearchedWord == searchedWord.Word1).ToList();
        List<int> wordsIds = cacheList.Select(x => x.AnagramsId).ToList();
        List<WordModel> returnList = _context.Words.Where(x => wordsIds.Contains(x.Id)).ToList();

        return returnList;
    }

}