namespace MPR.Cinemas.Logic.Features.Cinemas.Responses
{
    public class CinemaResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
