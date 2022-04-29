namespace EF.CodeFirst.Model;

public class UserInfo
{
    public int Id { get; set; }
    public string IP { get; set; }
    public int SearchLimit { get; set; } = 5;
}