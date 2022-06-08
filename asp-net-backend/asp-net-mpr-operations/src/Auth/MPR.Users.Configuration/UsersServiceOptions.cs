using MPR.Users.Configuration.Configurations;

namespace MPR.Users.Configuration
{
    public class UsersServiceOptions
    {
        public const string AppConfiguration = "AppConfiguration";
        public QueuesOptions Queues { get; set; }
    }
}