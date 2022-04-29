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
    public List<Words> ReturnWordListFromSource(bool shouldUseDataContext)
    {
        var data = ReadDataFromFile();
        List<Words> listOfSourceObjects = new List<Words>();
        foreach (string line in data)
        {
            string[] lineContent = line.Split("\t");
            string wordContent = lineContent[_WORD_CONTENT];
            string wordType = lineContent[_WORD_TYPE];
            listOfSourceObjects.Add(CreateNewWordObject(wordContent, wordType));
        }
        return listOfSourceObjects.Distinct().ToList();
    }

    private Words CreateNewWordObject(string wordContent, string wordType)
    {
        return new Words{Word = wordContent, Category = wordType};
    }
}