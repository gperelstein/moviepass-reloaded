namespace MPR.Shared.Domain.Abstractions
{
    public interface ISortable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
