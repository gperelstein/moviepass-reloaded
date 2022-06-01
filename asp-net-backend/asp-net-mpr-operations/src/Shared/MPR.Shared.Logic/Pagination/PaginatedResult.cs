namespace MPR.Shared.Logic.Pagination
{
    public class PaginatedResult<TResult>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public IEnumerable<TResult> Items { get; set; }
    }
}
