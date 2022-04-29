using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using EF.CodeFirst.Data;
using Words = AnagramSolver.Contracts.Models.Words;

namespace AnagramSolver.BusinessLogic;

public class AnagramRepository : IWordRepository
{
    public DataContext _context;
    public DatabaseContext _databaseCotext;
    public AnagramRepository(DataContext context, DatabaseContext databaseContext)
    {
        _context = context;
        _databaseCotext = databaseContext;
    }

    public List<Words> ReturnWordListFromSource(bool shouldUseDataContext)
    {
        if (shouldUseDataContext)
        {
            return _context.Words.ToList();
        }
        else
        {
            var words =  _databaseCotext.Words.ToList();
            return words.Select(x => new Words {Id = x.Id, Word = x.Word, Category = x.Category}).ToList();
        }
    }

    public void AddToDictionary(EF.CodeFirst.Model.Words word)
    {
        _databaseCotext.Words.Add(word);
        _databaseCotext.SaveChanges();
    }

    public void RemoveFromDictionary(string word)
    {
        var wordToRemove =_databaseCotext.Words.Where(x => x.Word == word).FirstOrDefault();
        _databaseCotext.Words.Remove(wordToRemove);
        _databaseCotext.SaveChanges();
    }

    public bool EditWordInDictionary(string wordToChange, string wordToChangeWith)
    {
        if (wordToChange != null && wordToChange != null)
        {
            try
            {
                _databaseCotext.Words.Where(x => x.Word == wordToChange).FirstOrDefault().Word = wordToChangeWith;
                _databaseCotext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        return false;
    }
}