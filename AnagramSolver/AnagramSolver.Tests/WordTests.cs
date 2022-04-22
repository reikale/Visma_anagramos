using AnagramSolver.Contracts.Models;
using NUnit.Framework;
using Shouldly;

namespace AnagramSolver.Contracts.Tests;

public class WordTests
{
    [TestCase("pilkas", "plikas")]
    [TestCase("pilkas", "pilkas")]
    public void GetHashCode_ShouldWord_CodesAreEqual(string content1, string content2)
    {
        //Arrange
        var word1 = new WordModel {Word = content1};
        var word2 = new WordModel {Word = content2};
        //Act
        var wordCode1 = word1.GetHashCode();
        var wordCode2 = word2.GetHashCode();
        //Assert
        wordCode1.ShouldBe(wordCode2);
    }
}