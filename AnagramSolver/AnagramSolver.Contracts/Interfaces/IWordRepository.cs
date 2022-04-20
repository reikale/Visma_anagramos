using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts;

public interface IWordRepository
{
    List<WordModel> ReturnWordListFromSource();
    void CacheWord(WordModel searchedWord, List<WordModel> listOfAnagrams);
    bool CheckForCache(WordModel searchedWord);
    List<WordModel> FindInCache(WordModel searchedWord);
}