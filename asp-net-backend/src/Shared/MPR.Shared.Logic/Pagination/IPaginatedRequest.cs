namespace MPR.Shared.Logic.Pagination
{
    public interface IPaginatedRequest
    {
        int? PageNumber { get; set; }
        int? PageSize { get; set; }
    }
}
