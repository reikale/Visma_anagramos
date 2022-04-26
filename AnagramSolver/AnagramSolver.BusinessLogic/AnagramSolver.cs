using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class AnagramSolver : IAnagramSolver
{
    private IWordRepository _wordRepository;

    public AnagramSolver(IWordRepository wordDatabaseReader)
    {
        _wordRepository = wordDatabaseReader;
    }

    public List<WordModel> CheckForAnagram(string userInput)
    {
        var wrappedWord = new WordModel{Word = userInput};
        var cacheResults = _wordRepository.FindInCache(wrappedWord);
        if (cacheResults.Count == 0)
        {
            var userWordCode = wrappedWord.GetHashCode();
            var sourceWords = _wordRepository.ReturnWordListFromSource();
            List<WordModel> listOfAnagrams = sourceWords.Where(x=>x.GetHashCode() == userWordCode).ToList();
            _wordRepository.CacheWord(wrappedWord, listOfAnagrams);
            return listOfAnagrams;
        }
        return cacheResults;
    }

    public List<WordModel> GetAllSourceWords()
    {
        return _wordRepository.ReturnWordListFromSource().ToList();
    }
}