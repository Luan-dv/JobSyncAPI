using JobSync.Domain.Enums;
using System.Xml.Serialization;

namespace JobSync.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    
    Task<bool> ExistActiveUserWithPhoneNumber(string phoneNumber);
}
