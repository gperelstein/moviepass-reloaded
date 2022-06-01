using MPR.Shared.Configuration.Configuration;

namespace MPR.Shared.Configuration
{
    public class AppOptions
    {
        public const string AppConfiguration = "AppConfiguration";
        public PaginationOptions Pagination { get; set; }
    }
}
