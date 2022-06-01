namespace MPR.Shared.Logic.Abstractions
{
    public interface ICurrentUserService
    {
        Guid? GetUserId();
    }
}
