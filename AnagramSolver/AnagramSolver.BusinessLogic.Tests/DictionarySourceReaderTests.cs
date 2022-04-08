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
        _dictionarySourceReader = new DictionarySourceReader(appSettings);
    }
    
    [Test]
    public void ReturnWordListFromSource_ShouldWork_LengthIsMoreThan()
    {
        //Arrange
        //Act
        var result = _dictionarySourceReader.ReturnWordListFromSource().Count;
        //Assert
        result.ShouldBeGreaterThan(1);
    }
    
    [Test]
    public void ReturnWordListFromSource_ShouldWork_ObjectIsOfType()
    {
        //Arrange
        //Act
        var result = _dictionarySourceReader.ReturnWordListFromSource()[0];
        //Assert
        result.ShouldBeOfType(typeof(Word));
    }
    
}