using AnagramSolver.Contracts.Models;
using NUnit.Framework;

namespace AnagramSolver.Contracts.Tests;

public class WordTests
{
    [TestCase("pilkas", "plikas")]
    [TestCase("pilkas", "pilkas")]
    public void GetHashCode_ShouldWord_CodesAreEqual(string content1, string content2)
    {
        //Arrange
        var word1 = new Word {Content = content1};
        var word2 = new Word {Content = content2};
        //Act
        var wordCode1 = word1.GetHashCode();
        var wordCode2 = word2.GetHashCode();
        //Assert
        Assert.That(wordCode1, Is.EqualTo(wordCode2));

    }
}