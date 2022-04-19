using System.Collections.Generic;
using System.Linq;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AnagramSolver.BusinessLogic.Tests;

[TestFixture]
public class AnagramSolverTests
{
    private Mock<IWordRepository> _mockRepository;
    private AnagramSolver _anotherAnagramSolver;

    [SetUp]
    public void Init()
    {
        _mockRepository = new Mock<IWordRepository>();
        _mockRepository.Setup(repo => repo.ReturnWordListFromSource()).Returns(new List<WordModel>
        {
            new WordModel {Word = "pilkas", Category = ""},
            new WordModel {Word = "plikas", Category = ""},
            new WordModel {Word = "paliks", Category = ""},
            new WordModel {Word = "paltas", Category = ""},

        });
        _anotherAnagramSolver = new AnagramSolver(_mockRepository.Object);
    }
    
    [Test]
    public void CheckForAnagram_ShouldWork_AnagramFound()
    {
        //Arrange
        string input = "ailksp";
        //Act
        var resultList = _anotherAnagramSolver.CheckForAnagram(input);
        
        //Assert
        resultList.Any(x => x.Word == "pilkas").ShouldBeTrue();
        resultList.Any(x => x.Word == "plikas").ShouldBeTrue();
        resultList.Any(x => x.Word == "ailksp").ShouldBeFalse();
        resultList.Count().ShouldBe(3);

    }
    
    [TestCase("pilkas", "pilvas")]
    [TestCase("šienas", "sienas")]
    public void CheckForAnagram_ShouldFail_AnagramNotFound(string input, string expected)
    {
        //Act
        var resultList = _anotherAnagramSolver.CheckForAnagram(input);
        var result = resultList.Any(x => x.Word == expected);
        
        //Assert
        result.ShouldBeFalse();
    }
    
    [Test]
    public void CheckForAnagram_ShouldWork_NoDublicates()
    {
        //Arrange
        string input = "pilkas";
        //Act
        var resultList = _anotherAnagramSolver.CheckForAnagram(input);

        //Assert
        resultList.Where(x => x.Word == "pilkas").ToList().Count.ShouldBe(1);
        resultList.Where(x => x.Word == "paliks").ToList().Count.ShouldBe(1);
        resultList.Where(x => x.Word == "palis").ToList().Count.ShouldBe(0);
    }
}