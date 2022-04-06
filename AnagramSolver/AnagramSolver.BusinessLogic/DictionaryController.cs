using System.Text;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class DictionaryController : IAnagramSolver
{
    private Dictionary<string, List<Word>> _dictionary;
    private WordRepo _wordRepo;

    public void ReceiveReferences(WordRepo wordRepo)
    {
        _wordRepo = wordRepo;
    }
    public void DictionaryStartup(string filePath)
    {
        var allWordObjects = _wordRepo.ReturnWordListFromSource(filePath);
        _dictionary = _wordRepo.ReturnWordDictionary(allWordObjects);
    }
    public List<Word> CheckForAnagram(string userInput)
    {
        var userWordCode = CalculateWordCode(userInput);
        if (_dictionary.Keys.Contains(userWordCode))
        {
            var listOfAnagrams = _dictionary[userWordCode];
            return listOfAnagrams;
        }
        return new List<Word>();
    }

    public string CalculateWordCode(string word)
    {
        var orderedWord = new String(word.ToLower().OrderBy(c => c).ToArray());
        return orderedWord;
    }
    
}