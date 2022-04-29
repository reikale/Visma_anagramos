using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts;

public interface IAnagramSolver
{
    List<Words> CheckForAnagram(string userInput, bool shouldUseDataContext);
    List<Words> GetAllSourceWords(bool shouldUseDataContext);

}