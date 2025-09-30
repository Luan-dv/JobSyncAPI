namespace JobSync.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
