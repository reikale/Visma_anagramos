using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts;

public interface IWordRepository
{

    IEnumerable<string> ReadDataFromFile(string filePath);
    List<Word> ReturnWordListFromSource(string filePath);
}