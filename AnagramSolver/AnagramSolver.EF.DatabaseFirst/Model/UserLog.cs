using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Model
{
    public partial class UserLog
    {
        public int Id { get; set; }
        public string UserIp { get; set; } = null!;
        public string SearchString { get; set; } = null!;
        public DateTime SearchTime { get; set; }
        public string? FoundAnagrams { get; set; }
    }
}
