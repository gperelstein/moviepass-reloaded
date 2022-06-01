namespace MPR.Shared.Logic.Query
{
    public static class SortingType
    {
        public const string Ascending = "ascending";
        public const string Descending = "descending";

        public static List<string> List { get; } = new List<string>
        {
            Ascending,
            Descending
        };
    }
}
