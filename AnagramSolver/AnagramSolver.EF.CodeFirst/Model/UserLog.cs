using System.ComponentModel.DataAnnotations;

namespace EF.CodeFirst.Model;

public class UserLog
{
    [Key]
    public int Id { get; set; }
    public string UserIP { get; set; }
    public string SearchString { get; set; }
    public DateTime SearchTime { get; set; }
    public string FoundAnagrams { get; set; }
}