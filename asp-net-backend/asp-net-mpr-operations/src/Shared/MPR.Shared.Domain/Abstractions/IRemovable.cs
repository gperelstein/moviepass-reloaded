namespace MPR.Shared.Domain.Abstractions
{
    public interface IRemovable
    {
        bool MarkedAsDeleted { get; set; }
        Guid? DeletedBy { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
