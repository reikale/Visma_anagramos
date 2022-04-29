using System.Net;
using EF.CodeFirst.Data;
using EF.CodeFirst.Model;
using Words = EF.CodeFirst.Model.Words;

namespace AnagramSolver.BusinessLogic;

public class DnsRepository
{
    private string curentIP;
    private int searchLimit;
    public DnsRepository()
    {
        curentIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[1].ToString();
    }

    public bool LogUserSearch(DatabaseContext context, string word, List<Words> anagrams)
    {
        // get the users search limit
        if (!context.UsersInfo.Select(x => x.IP).Contains(curentIP))
        {
            context.UsersInfo.Add(new UserInfo {IP = curentIP});
            context.SaveChanges();
        }
        searchLimit = context.UsersInfo.Where(x => x.IP == curentIP).Select(x => x.SearchLimit).SingleOrDefault();
        // if this IP searched 5 times already, show error page
        int logCount = context.UserLogs.Where(x => x.UserIP == curentIP).ToList().Count;
        if (logCount < searchLimit)
        {
            context.UserLogs.Add(new EF.CodeFirst.Model.UserLog
            {
                UserIP = curentIP,
                SearchString = word,
                SearchTime = DateTime.Now,
                FoundAnagrams = string.Join( ", ", anagrams.Select(x => x.Word))
            });
            context.SaveChanges();
            return true;
        }

        return false;
    }

    public void AlterSearchLimit(DatabaseContext context, int amount)
    {
        if (searchLimit + amount >= 0)
        {
            var currentUser = context.UsersInfo.Where(x => x.IP == curentIP).FirstOrDefault();
            currentUser.SearchLimit += amount;
            context.SaveChanges();
            Console.WriteLine(context.UsersInfo.Where(x => x.IP == curentIP).Select(x => x.SearchLimit).FirstOrDefault());
        }
    }
    
}