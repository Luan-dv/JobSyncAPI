using JobSync.Communication.Enums;

namespace JobSync.Communication.Requests;
public class RequestRegisterUserJson
{

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public long Cpf { get; set; }
    public GenderChoice Gender { get; set; }
    public string Password { get; set; } = string.Empty;
}
