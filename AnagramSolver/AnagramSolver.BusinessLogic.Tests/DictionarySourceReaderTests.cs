using System.Collections.Generic;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AnagramSolver.BusinessLogic.Tests;

[TestFixture]
public class DictionarySourceReaderTests
{
    private Mock<IWordRepository> _dictionarySourceReader;

    [SetUp]
    public void Init()
    {
        _dictionarySourceReader = new Mock<IWordRepository>();
        _dictionarySourceReader.Setup(x => x.ReturnWordListFromSource()).Returns(new List<WordModel>
        {
            new WordModel{Word = "pilka", Category= ""},
            new WordModel{Word = "pilkais", Category=""},
            new WordModel{Word = "pilkas", Category=""},
            new WordModel{Word = "pilki", Category=""},
            new WordModel{Word = "pilko", Category=""},
            new WordModel{Word = "pilkos", Category=""},
            new WordModel{Word = "pilku", Category=""},
            new WordModel{Word = "plika", Category=""},
            new WordModel{Word = "plikas", Category=""},
            new WordModel{Word = "paliks", Category=""},
        
        });
    }
    
    [Test]
    public void ReturnWordListFromSource_ShouldWork_LengthIs()
    {
        //Act
        var result = _dictionarySourceReader.Object.ReturnWordListFromSource().Count;
        //Assert
        result.ShouldBe(10);
    }

}