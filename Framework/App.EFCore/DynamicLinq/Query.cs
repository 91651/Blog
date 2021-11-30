namespace App.EFCore.DynamicLinq
{
    public class Query
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public IEnumerable<Sort> Sort { get; set; } = new List<Sort>();
        public IEnumerable<Filter> Filter { get; set; } = new List<Filter>();
    }
}
