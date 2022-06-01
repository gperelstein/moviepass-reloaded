namespace MPR.Shared.Domain.Abstractions
{
    public interface INamedEntity
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
