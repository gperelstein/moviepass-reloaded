namespace MPR.Shared.Logic.Responses.Features.Cinemas
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public decimal TicketValue { get; set; }
        public CinemaResponse Cinema { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
