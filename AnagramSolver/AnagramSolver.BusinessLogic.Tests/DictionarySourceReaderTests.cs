using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;
using NUnit.Framework;
using Shouldly;

namespace AnagramSolver.BusinessLogic.Tests;

[TestFixture]
public class DictionarySourceReaderTests
{
    private DictionarySourceReader _dictionarySourceReader;

    [SetUp]
    public void Init()
    {
        var appSettingsHandler = new AppSettingsHandler("appsettings.json");
        var appSettings = appSettingsHandler.GetAppSettings();
        appSettings.WordsRepoSource.PathToWordsRepo = "../../../../testZodynas.txt";
        _dictionarySourceReader = new DictionarySourceReader(appSettings);
    }
    
    [Test]
    public void ReturnWordListFromSource_ShouldWork_LengthIs()
    {
        //Act
        var result = _dictionarySourceReader.ReturnWordListFromSource().Count;
        //Assert
        result.ShouldBe(10);
    }

}