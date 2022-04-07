using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class DictionaryActions : IAnagramSolver
{
    private HashSet<Word> _sourceWords;
    private DictionarySourceReader _dictionarySourceReader;

    public DictionaryActions(DictionarySourceReader dictionarySourceReader)
    {
        _dictionarySourceReader = dictionarySourceReader;
    }

    public void DictionaryStartup()
    {

        var allWordObjects = _dictionarySourceReader.ReturnWordListFromSource();
        _sourceWords = ReturnWordDictionary(allWordObjects);
    }

    public List<Word> CheckForAnagram(string userInput)
    {
        var wrappedWord = new Word{Content = userInput};
        var userWordCode = wrappedWord.GetHashCode();
        var listOfAnagrams = _sourceWords.Where(x=>x.GetHashCode() == userWordCode).ToList();
        return listOfAnagrams;
    }
    
    public HashSet<Word> ReturnWordDictionary(IEnumerable<Word> wordList)
    {
        HashSet<Word> wordDictionary = new HashSet<Word>(wordList);
        return wordDictionary;
    }
}