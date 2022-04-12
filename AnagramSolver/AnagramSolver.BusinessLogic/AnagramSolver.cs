using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;
using NUnit.Framework.Internal.Execution;

namespace AnagramSolver.BusinessLogic;

public class AnagramSolver : IAnagramSolver
{
    private IWordRepository _dictionarySourceReader;

    public AnagramSolver(IWordRepository dictionarySourceReader)
    {
        _dictionarySourceReader = dictionarySourceReader;
    }

    public List<Word> CheckForAnagram(string userInput)
    {
        var wrappedWord = new Word{Content = userInput};
        var userWordCode = wrappedWord.GetHashCode();
        var sourceWords = ReturnWordDictionary();
        var listOfAnagrams = sourceWords.Where(x=>x.GetHashCode() == userWordCode).ToList();
        return listOfAnagrams;
    }
    
    private HashSet<Word> ReturnWordDictionary()
    {
        var allWordObjects = _dictionarySourceReader.ReturnWordListFromSource();
        HashSet<Word> wordDictionary = new HashSet<Word>(allWordObjects);
        return wordDictionary;
    }

    public List<Word> GetAllSourceWords()
    {
        return ReturnWordDictionary().ToList();
    }
}