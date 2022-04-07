using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;
public class DictionarySourceReader : IWordRepository
{
    private readonly string _filePath;
    private AppSettings _appSettings;
    private List<Word> _listOfSourceObjects;
    private const int _WORD_CONTENT = 2;
    private const int _WORD_TYPE = 1;

    public DictionarySourceReader(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _listOfSourceObjects = new List<Word>();
        _filePath = _appSettings.WordsRepoSource.PathToWordsRepo;
    }

    public List<Word> ReturnWordListFromSource()
    {
        var data = ReadDataFromFile();
        
        foreach (string line in data)
        {
            string[] lineContent = line.Split("\t");
            string wordContent = lineContent[_WORD_CONTENT];
            string wordType = lineContent[_WORD_TYPE];
            CreateNewObjectAndAddToList(wordContent, wordType);
        }
        return _listOfSourceObjects.Distinct().ToList();
    }
    
    public IEnumerable<string> ReadDataFromFile()
    {
        return File.ReadLines(_filePath);
    }

    public void CreateNewObjectAndAddToList(string wordContent, string wordType)
    {
        Word wordObject = new Word{Content = wordContent, Type = wordType};
        _listOfSourceObjects.Add(wordObject);
    }
    
}