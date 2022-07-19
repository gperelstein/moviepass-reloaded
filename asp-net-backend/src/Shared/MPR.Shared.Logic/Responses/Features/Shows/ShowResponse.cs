namespace MPR.Shared.Logic.Responses.Features.Shows
{
    public class ShowResponse
    {
        public Guid Id { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public Guid MovieId { get; set; }
        public Guid CinemaId { get; set; }
        public Guid RoomId { get; set; }
        public Guid Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
