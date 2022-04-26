using System.ComponentModel.DataAnnotations;

namespace EF.CodeFirst.Model;

public class CachedWords
{
    [Key]
    public int Id { get; set; }
    public string SearchedWord { get; set; }
    public int AnagramId { get; set; }
}