using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts;

public interface IWordRepository
{
    List<Words> ReturnWordListFromSource(bool shouldUseDataContext);
}