using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class WordDatabaseReader : IWordRepository
{
    public DataContext _dataContext;
    public WordDatabaseReader(DataContext datacontext)
    {
        _dataContext = datacontext;
    }

    public List<WordModel> ReturnWordListFromSource()
    {
        return _dataContext.Words.ToList();
    }
}