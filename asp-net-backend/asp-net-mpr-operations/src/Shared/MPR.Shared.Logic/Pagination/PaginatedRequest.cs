using MPR.Shared.Logic.Query;

namespace MPR.Shared.Logic.Pagination
{
    public class PaginatedRequest<T> : ListQueryRequest<PaginatedResult<T>>, IPaginatedRequest
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
