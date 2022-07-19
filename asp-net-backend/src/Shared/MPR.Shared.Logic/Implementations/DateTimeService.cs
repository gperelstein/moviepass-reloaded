using MPR.Shared.Logic.Abstractions;

namespace MPR.Shared.Logic.Implementations
{
    internal class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
