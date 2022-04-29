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
        _dictionarySourceReader.Setup(x => x.ReturnWordListFromSource(true)).Returns(new List<Words>
        {
            new Words{Word = "pilka", Category= ""},
            new Words{Word = "pilkais", Category=""},
            new Words{Word = "pilkas", Category=""},
            new Words{Word = "pilki", Category=""},
            new Words{Word = "pilko", Category=""},
            new Words{Word = "pilkos", Category=""},
            new Words{Word = "pilku", Category=""},
            new Words{Word = "plika", Category=""},
            new Words{Word = "plikas", Category=""},
            new Words{Word = "paliks", Category=""},
        
        });
    }
    
    [Test]
    public void ReturnWordListFromSource_ShouldWork_LengthIs()
    {
        //Act
        var result = _dictionarySourceReader.Object.ReturnWordListFromSource(true).Count;
        //Assert
        result.ShouldBe(10);
    }

}