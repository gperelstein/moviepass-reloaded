namespace MPR.Cinemas.Logic.Features.Cinemas.Responses
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public decimal TicketValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
