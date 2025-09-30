using JobSync.Domain.Entities;
using JobSync.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace JobSync.Infrastucture.DataAcess.Repositories;
internal class UserRepository 
{
    private readonly JobSyncDbContext _dbContext;
    public UserRepository(JobSyncDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(User User)
    {
        await _dbContext.Users.AddAsync(User);
    }
    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }
}
