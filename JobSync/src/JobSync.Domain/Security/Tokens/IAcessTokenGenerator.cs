using JobSync.Domain.Entities;

namespace JobSync.Domain.Security.Tokens;
public interface IAcessTokenGenerator
{
    string Generate(User User);
}
