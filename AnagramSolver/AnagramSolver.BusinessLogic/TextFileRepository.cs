using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;
public class TextFileRepository : IWordRepository
{
    private readonly AppSettings _appSettings;
    public const int _WORD_CONTENT = 2;
    public const int _WORD_TYPE = 1;

    public TextFileRepository()
    {
        _appSettings = new AppSettingsHandler("appsettings.json").GetAppSettings();
    }

    public IEnumerable<string> ReadDataFromFile()
    {
        return File.ReadLines(_appSettings.WordsRepoSource.PathToWordsRepo);
    }
    public List<WordModel> ReturnWordListFromSource()
    {
        var data = ReadDataFromFile();
        List<WordModel> listOfSourceObjects = new List<WordModel>();
        foreach (string line in data)
        {
            string[] lineContent = line.Split("\t");
            string wordContent = lineContent[_WORD_CONTENT];
            string wordType = lineContent[_WORD_TYPE];
            listOfSourceObjects.Add(CreateNewWordObject(wordContent, wordType));
        }
        return listOfSourceObjects.Distinct().ToList();
    }

    public void CacheWord(WordModel searchedWord, List<WordModel> listOfAnagrams)
    {
        Console.WriteLine("Word was cached");
    }

    public List<WordModel> FindInCache(WordModel searchedWord)
    {
        return new List<WordModel>();
    }

    private WordModel CreateNewWordObject(string wordContent, string wordType)
    {
        return new WordModel{Word = wordContent, Category = wordType};
    }
}