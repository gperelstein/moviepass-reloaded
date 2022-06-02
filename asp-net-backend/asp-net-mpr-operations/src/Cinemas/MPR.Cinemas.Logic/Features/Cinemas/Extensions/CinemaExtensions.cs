using MPR.Cinemas.Logic.Features.Cinemas.Responses;
using MPR.Shared.Domain.Models;

namespace MPR.Cinemas.Logic.Features.Cinemas.Extensions
{
    public static class CinemaExtensions
    {
        public static CinemaResponse ToResponse(this Cinema cinema) => new CinemaResponse
        {
            Id = cinema.Id,
            Address = cinema.Address,
            Name = cinema.Name,
            CreatedAt = cinema.CreatedAt,
            LastUpdatedAt = cinema.LastUpdatedAt,
            LastUpdatedBy = cinema.LastUpdatedBy,
            Owner = cinema.Owner,
        };

        public static CinemaDetailedResponse ToDetailedResponse(this Cinema cinema) => new CinemaDetailedResponse
        {
            Id = cinema.Id,
            Address = cinema.Address,
            Name = cinema.Name,
            CreatedAt = cinema.CreatedAt,
            LastUpdatedAt = cinema.LastUpdatedAt,
            LastUpdatedBy = cinema.LastUpdatedBy,
            Owner = cinema.Owner,
            Rooms = cinema.Rooms.Select(x => x.ToResponse()).ToList()
        };
    }
}
