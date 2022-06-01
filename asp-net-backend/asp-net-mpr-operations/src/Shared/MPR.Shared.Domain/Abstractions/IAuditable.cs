namespace MPR.Shared.Domain.Abstractions
{
    public interface IAuditable
    {
        Guid Owner { get; set; }
        Guid LastUpdatedBy { get; set; }
        DateTime LastUpdatedAt { get; set; }
    }
}
