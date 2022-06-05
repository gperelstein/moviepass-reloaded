using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses.Features.Cinemas;

namespace MPR.Cinemas.Logic.Features.Cinemas.Extensions
{
    public static class RoomsExtensions
    {
        public static RoomResponse ToResponse(this Room room) => new RoomResponse
        {
            Id = room.Id,
            Capacity = room.Capacity,
            TicketValue = room.TicketValue,
            Cinema = room.Cinema.ToResponse(),
            Owner = room.Owner,
            CreatedAt = room.CreatedAt,
            LastUpdatedAt = room.LastUpdatedAt,
            LastUpdatedBy = room.LastUpdatedBy,
        };
    }
}
