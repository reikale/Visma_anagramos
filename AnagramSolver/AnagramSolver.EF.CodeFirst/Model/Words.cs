using System.ComponentModel.DataAnnotations;

namespace EF.CodeFirst.Model;

public class Words
{
    [Key]
    public int Id { get; set; }
    public string Word { get; set; }
    public string Category { get; set; }
    
}