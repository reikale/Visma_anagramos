using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;
public class DictionarySourceReader : IWordRepository
{
    private readonly AppSettings _appSettings;
    private const int _WORD_CONTENT = 2;
    private const int _WORD_TYPE = 1;

    public DictionarySourceReader()
    {
        _appSettings = new AppSettingsHandler("appsettings.json").GetAppSettings();
    }

    public List<Word> ReturnWordListFromSource()
    {
        var data = ReadDataFromFile();
        List<Word> listOfSourceObjects = new List<Word>();
        foreach (string line in data)
        {
            string[] lineContent = line.Split("\t");
            string wordContent = lineContent[_WORD_CONTENT];
            string wordType = lineContent[_WORD_TYPE];
            listOfSourceObjects.Add(CreateNewWordObject(wordContent, wordType));
        }
        return listOfSourceObjects.Distinct().ToList();
    }

    private IEnumerable<string> ReadDataFromFile()
    {
        return File.ReadLines(_appSettings.WordsRepoSource.PathToWordsRepo);
    }

    private Word CreateNewWordObject(string wordContent, string wordType)
    {
       return new Word{Content = wordContent, Type = wordType};
    }
}