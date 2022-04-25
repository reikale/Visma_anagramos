using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Model
{
    public partial class CachedWord
    {
        public int Id { get; set; }
        public string SearchedWord { get; set; } = null!;
        public int AnagramsId { get; set; }

        public virtual WordModel Anagrams { get; set; } = null!;
    }
}
