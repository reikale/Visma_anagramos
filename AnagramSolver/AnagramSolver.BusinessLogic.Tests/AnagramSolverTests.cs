using System.Linq;
using AnagramSolver.Contracts;
using NUnit.Framework;

namespace AnagramSolver.BusinessLogic.Tests;

[TestFixture]
public class AnagramSolverTests
{
    private AnagramSolver _anagramSolver;
    private IWordRepository _dictionarySourceReader;
    
    [SetUp]
    public void Init()
    {
        var appSettingsHandler = new AppSettingsHandler("appsettings.json");
        var appSettings = appSettingsHandler.GetAppSettings();
        _dictionarySourceReader = new DictionarySourceReader(appSettings);
        _anagramSolver = new AnagramSolver(_dictionarySourceReader);
    }
    
    [TestCase("pilkas", "plikas")]
    [TestCase("veidas", "dievas")]
    [TestCase("adeisv", "dievas")]
    public void CheckForAnagram_ShouldWork_AnagramFound(string input, string expected)
    {
        //Act
        var resultList = _anagramSolver.CheckForAnagram(input);
        var result = resultList.Any(x => x.Content == expected);
        
        //Assert
        Assert.That(result);
    }
    
    [TestCase("pilkas", "pilvas")]
    [TestCase("šienas", "sienas")]
    public void CheckForAnagram_ShouldFail_AnagramNotFound(string input, string expected)
    {
        //Act
        var resultList = _anagramSolver.CheckForAnagram(input);
        var result = resultList.Any(x => x.Content == expected);
        
        //Assert
        Assert.False(result);
    }
    
    [TestCase("pilkas", "plikas")]
    [TestCase("veidas", "dievas")]
    public void CheckForAnagram_ShouldWork_NoDublicates(string input, string expected)
    {
        //Act
        var resultList = _anagramSolver.CheckForAnagram(input);
        var result = resultList.Where(x => x.Content == expected).ToList().Count;
        
        //Assert
        Assert.That(result, Is.EqualTo(1));
    }
}