using JobSync.Domain.Entities;
using JobSync.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Reflection;

namespace JobSync.Infrastucture.DataAcess.Repositories;
internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
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

    public async Task<bool> ExistActiveUserWithGender(string gender)
    {
        return await _dbContext.Users.AnyAsync(user => user.Gender.Equals(gender));
    }

    public async Task<bool> ExistActiveUserWithPhoneNumber(string phoneNumber)
    {
        return await _dbContext.Users.AnyAsync(user => user.PhoneNumber.Equals(phoneNumber));
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

}
