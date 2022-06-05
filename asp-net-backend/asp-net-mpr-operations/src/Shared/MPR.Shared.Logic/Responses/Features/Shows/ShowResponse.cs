using MPR.Shared.Logic.Responses.Features.Cinemas;
using MPR.Shared.Logic.Responses.Features.Movies;

namespace MPR.Shared.Logic.Responses.Features.Shows
{
    public class ShowResponse
    {
        public Guid Id { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public Guid MovieId { get; set; }
        public MovieResponse Movie { get; set; }
        public Guid RoomId { get; set; }
        public RoomResponse Room { get; set; }
        public Guid Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
