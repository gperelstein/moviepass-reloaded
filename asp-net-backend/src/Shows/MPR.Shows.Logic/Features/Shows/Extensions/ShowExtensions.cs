using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses.Features.Cinemas;
using MPR.Shared.Logic.Responses.Features.Movies;
using MPR.Shared.Logic.Responses.Features.Shows;

namespace MPR.Shows.Logic.Features.Shows.Extensions
{
    public static class ShowExtensions
    {
        public static ShowResponse ToResponse(this Show show) => new()
        {
            Id = show.Id,
            StartAt = show.StartAt,
            EndAt = show.EndAt,
            MovieId = show.MovieId,
            CinemaId = show.CinemaId,
            RoomId = show.RoomId,
            CreatedAt = show.CreatedAt,
            LastUpdatedAt = show.LastUpdatedAt,
            LastUpdatedBy = show.LastUpdatedBy,
            Owner = show.Owner
        };

        public static ShowDetailedResponse ToDetailedResponse(this Show show, MovieResponse movie, RoomResponse room) => new()
        {
            Id = show.Id,
            StartAt = show.StartAt,
            EndAt = show.EndAt,
            MovieId = show.MovieId,
            Movie = movie,
            CinemaId = show.CinemaId,
            RoomId = show.RoomId,
            Room = room,
            CreatedAt = show.CreatedAt,
            LastUpdatedAt = show.LastUpdatedAt,
            LastUpdatedBy = show.LastUpdatedBy,
            Owner = show.Owner
        };
    }
}
