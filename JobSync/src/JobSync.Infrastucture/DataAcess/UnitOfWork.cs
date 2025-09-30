using JobSync.Domain.Repositories;

namespace JobSync.Infrastucture.DataAcess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly JobSyncDbContext _dbContext;
    public UnitOfWork(JobSyncDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
