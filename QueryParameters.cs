namespace FinalProject
{
    public class QueryParameters
    {
        public string[] Columns { get; set; }
        public List<Filter> Filters { get; set; } = new List<Filter>();
        public List<SortOption> SortOptions { get; set; } = new List<SortOption>();
    }

    public class Filter
    {
        public string PropertyName { get; set; }
        public string Operation { get; set; } // e.g., "Equals", "Contains", "GreaterThan"
        public object Value { get; set; }
    }

    public class SortOption
    {
        public string PropertyName { get; set; }
        public bool IsDescending { get; set; }
    }
}