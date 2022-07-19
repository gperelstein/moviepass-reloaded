namespace MPR.Shared.Logic.Pagination
{
    public static class PaginationHelper
    {
        public static int GetRowsPerPage(IPaginatedRequest request, int maxPageSize)
        {
            return !request.PageSize.HasValue
                                 || request.PageSize.Value <= 0
                                 || request.PageSize.Value > maxPageSize
                               ? maxPageSize
                               : request.PageSize.Value;
        }

        public static int GetPageNumber(IPaginatedRequest request)
        {
            return !request.PageNumber.HasValue
                            || request.PageNumber.Value <= 0
                            ? 1
                            : request.PageNumber.Value;
        }

        public static int GetSkipRows(int rowsPerPage, int pageNumber)
        {
            return rowsPerPage * (pageNumber - 1);
        }
    }
}
