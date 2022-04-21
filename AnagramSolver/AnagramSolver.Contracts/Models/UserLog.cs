using System.Net;

namespace AnagramSolver.Contracts.Models;

public class UserLog
{
    public int Id { get; set; }
    public string UserIP { get; set; }
    public string SearchString { get; set; }
    public DateTime SearchTime { get; set; }
    public string FoundAnagrams { get; set; }
}