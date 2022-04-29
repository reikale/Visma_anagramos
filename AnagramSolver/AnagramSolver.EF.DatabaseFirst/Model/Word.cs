
namespace AnagramSolver.EF.DatabaseFirst.Model
{
    public partial class WordModel
    {
        public WordModel()
        {
            CachedWords = new HashSet<CachedWord>();
        }

        public string Category { get; set; } = null!;
        public string Word1 { get; set; } = null!;
        public int Id { get; set; }

        public virtual ICollection<CachedWord> CachedWords { get; set; }
        public override int GetHashCode()
        {
            return string.Concat(Word1.ToLower().OrderBy(c => c).ToArray()).GetHashCode();
        }
    }
}
