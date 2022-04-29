using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;
using EF.CodeFirst.Data;
using EF.CodeFirst.Model;
using Words = AnagramSolver.Contracts.Models.Words;

namespace AnagramSolver.BusinessLogic;

public class CacheRepository: ICache
{
    public DataContext _context;
    public DatabaseContext _databaseCotext;
    public CacheRepository(DataContext context, DatabaseContext databaseCotext)
    {
        _context = context;
        _databaseCotext = databaseCotext;
    }
    public void CacheWord(Words searchedWord, List<Words> listOfAnagrams, bool shouldUseDataContext)
    {
        if (shouldUseDataContext)
        {
            foreach (Words word in listOfAnagrams)
            {
                _context.CachedWords.Add(new CachedWord
                {
                    SearchedWord = searchedWord.Word,
                    AnagramsId = word.Id
                });
            }
            _context.SaveChanges();
        }
        else
        {
            foreach (Words word in listOfAnagrams)
            {
                _databaseCotext.CachedWords.Add(new CachedWords
                {
                    SearchedWord = searchedWord.Word,
                    AnagramId = word.Id
                });
            }
            _databaseCotext.SaveChanges();
        }
        
    }

    public List<Words> FindInCache(Words searchedWord, bool shouldUseDataContext)
    {
        List<CachedWord> cacheList;
        if (shouldUseDataContext)
        {
            cacheList =  _context.CachedWords.Where(x => x.SearchedWord == searchedWord.Word).ToList();
        }
        else
        {
            List<CachedWords> cacheListEf =  _databaseCotext.CachedWords.Where(x => x.SearchedWord == searchedWord.Word).ToList();
            cacheList =  cacheListEf.Select(x => new CachedWord {SearchedWord = x.SearchedWord, AnagramsId = x.AnagramId})
                .ToList();
        }
        
        List<int> wordsIds = cacheList.Select(x => x.AnagramsId).ToList();
        List<Words> returnList = _context.Words.Where(x => wordsIds.Contains(x.Id)).ToList();

        return returnList;
    }
}