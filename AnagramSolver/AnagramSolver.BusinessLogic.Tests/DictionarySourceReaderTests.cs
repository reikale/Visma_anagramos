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
        _dictionarySourceReader.Setup(x => x.ReturnWordListFromSource()).Returns(new List<Word>
        {
            new Word{Content = "pilka", Type=""},
            new Word{Content = "pilkais", Type=""},
            new Word{Content = "pilkas", Type=""},
            new Word{Content = "pilki", Type=""},
            new Word{Content = "pilko", Type=""},
            new Word{Content = "pilkos", Type=""},
            new Word{Content = "pilku", Type=""},
            new Word{Content = "plika", Type=""},
            new Word{Content = "plikas", Type=""},
            new Word{Content = "paliks", Type=""},
        
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