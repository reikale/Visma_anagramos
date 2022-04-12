using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts;

public interface IAnagramSolver
{
    List<Word> CheckForAnagram(string userInput);
    List<Word> GetAllSourceWords();

}