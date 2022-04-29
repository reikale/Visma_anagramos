using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts;

public interface ICache
{
    void CacheWord(Words searchedWord, List<Words> listOfAnagrams, bool shouldUseDataContext);
    List<Words> FindInCache(Words searchedWord, bool shouldUseDataContext);
}