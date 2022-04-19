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
        var userWordCode = wrappedWord.GetHashCode();
        var sourceWords = ReturnWordDictionary();
        var listOfAnagrams = sourceWords.Where(x=>x.GetHashCode() == userWordCode).ToList();
        return listOfAnagrams;
    }

    private HashSet<WordModel> ReturnWordDictionary()
    {
        var allWordObjects = _wordRepository.ReturnWordListFromSource();
        HashSet<WordModel> wordDictionary = new HashSet<WordModel>(allWordObjects);
        return wordDictionary;
    }

    public List<WordModel> GetAllSourceWords()
    {
        return ReturnWordDictionary().ToList();
    }
}