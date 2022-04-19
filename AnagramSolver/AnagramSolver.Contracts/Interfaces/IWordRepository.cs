using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts;

public interface IWordRepository
{
    List<WordModel> ReturnWordListFromSource();
}