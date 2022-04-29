using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class AnagramSolver : IAnagramSolver
{
    private IWordRepository _wordRepository;
    private ICache _cache;

    public AnagramSolver(IWordRepository wordDatabaseReader, ICache cache)
    {
        _wordRepository = wordDatabaseReader;
        _cache = cache;
    }

    public List<Words> CheckForAnagram(string userInput, bool shouldUseDataContext)
    {
        var wrappedWord = new Words{Word = userInput};
        var cacheResults = _cache.FindInCache(wrappedWord, shouldUseDataContext);
        if (cacheResults.Count == 0)
        {
            var userWordCode = wrappedWord.GetHashCode();
            var sourceWords = _wordRepository.ReturnWordListFromSource(shouldUseDataContext);
            List<Words> listOfAnagrams = sourceWords.Where(x=>x.GetHashCode() == userWordCode).ToList();
            _cache.CacheWord(wrappedWord, listOfAnagrams, shouldUseDataContext);
            return listOfAnagrams;
        }
        return cacheResults;
    }

    public List<Words> GetAllSourceWords(bool shouldUseDataContext)
    {
        return _wordRepository.ReturnWordListFromSource(shouldUseDataContext).ToList();
    }
}