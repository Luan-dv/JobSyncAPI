namespace JobSync.Domain.Entities;
public class User
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public long Cpf { get; set; }
    
    public string Password { get; set; } = string.Empty;

    public Guid UserIdentifier { get; set; }
    
}
