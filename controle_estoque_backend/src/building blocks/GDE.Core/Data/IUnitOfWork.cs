namespace GDE.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
