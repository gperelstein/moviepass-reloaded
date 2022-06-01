namespace MPR.Shared.Domain.Abstractions
{
    public interface IRemovable
    {
        bool MarkedAsDeleted { get; set; }
    }
}
