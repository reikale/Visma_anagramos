using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Model
{
    public partial class Word
    {
        public Word()
        {
            CachedWords = new HashSet<CachedWord>();
        }

        public string Category { get; set; } = null!;
        public string Word1 { get; set; } = null!;
        public int Id { get; set; }

        public virtual ICollection<CachedWord> CachedWords { get; set; }
    }
}
