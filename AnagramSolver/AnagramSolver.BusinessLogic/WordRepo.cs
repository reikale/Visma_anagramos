using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;
public class WordRepo : IWordRepository
{
    private readonly DictionaryController _dictionaryController;
    private List<Word> _listOfSourceObjects;
    private const int _WORD_CONTENT = 0;
    private const int _WORD_TYPE = 1;

    public WordRepo(DictionaryController dictionaryController)
    {
        _dictionaryController = dictionaryController;
        _listOfSourceObjects = new List<Word>();
    }

    public List<Word> ReturnWordListFromSource(string filePath)
    {
        var data = ReadDataFromFile(filePath);
        
        foreach (string line in data)
        {
            string[] lineContent = line.Split("\t");
            string wordContent = lineContent[_WORD_CONTENT];
            string wordType = lineContent[_WORD_TYPE];
            
            if (!CheckIfWordAlreadyExistsInList(wordContent))
            {
                CreateNewObjectAndAddToList(wordContent, wordType);
            }
        }
        return _listOfSourceObjects.Distinct().ToList();
    }
    public IEnumerable<string> ReadDataFromFile(string filePath)
    {
        return File.ReadLines(filePath);
    }
    public bool CheckIfWordAlreadyExistsInList(string wordContent)
    {
        bool objectsExists = _listOfSourceObjects.Any(x => x.Content == wordContent);
        return objectsExists;
    }
    public void CreateNewObjectAndAddToList(string wordContent, string wordType)
    {
        Word wordObject = new Word{Content = wordContent, Type = wordType};
        _listOfSourceObjects.Add(wordObject);
        Console.WriteLine(wordObject.Content);
    }
    public Dictionary<string, List<Word>> ReturnWordDictionary(IEnumerable<Word> wordList)
    {
        Dictionary<string, List<Word>> wordDictionary = new Dictionary<string, List<Word>>();
        foreach (Word word in wordList)
        {
            string wordCode = _dictionaryController.CalculateWordCode(word.Content);
            if (wordDictionary.ContainsKey(wordCode))
            {
                wordDictionary[wordCode].Add(word);
            }
            else
            {
                wordDictionary.Add(wordCode, new List<Word>{word});
            }
        }
        return wordDictionary;
    }
}