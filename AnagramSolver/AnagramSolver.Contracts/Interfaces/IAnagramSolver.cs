using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts;

public interface IAnagramSolver
{
    List<WordModel> CheckForAnagram(string userInput);
    List<WordModel> GetAllSourceWords();

}