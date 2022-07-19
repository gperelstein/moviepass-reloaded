using MPR.Shared.Logic.Responses.Features.Cinemas;
using MPR.Shared.Logic.Responses.Features.Movies;

namespace MPR.Shared.Logic.Responses.Features.Shows
{
    public class ShowDetailedResponse : ShowResponse
    {
        public MovieResponse Movie { get; set; }
        public RoomResponse Room { get; set; }
    }
}
