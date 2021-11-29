namespace App.EFCore.DynamicLinq
{
    public class Query
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public IEnumerable<Sort> Sort { get; set; } = new List<Sort>();
        public IEnumerable<Filter> Filter { get; set; } = new List<Filter>();
    }
}
