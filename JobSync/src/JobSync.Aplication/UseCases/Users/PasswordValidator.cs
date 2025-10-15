using FluentValidation;
using FluentValidation.Validators;
using JobSync.Exception;
using System.Text.RegularExpressions;

namespace JobSync.Aplication.UseCases.Users;
public partial class PasswordValidator<T> : PropertyValidator<T, string> 
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";
    public override string Name => "PasswordValidator";


    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.EMAIL_REQUIRED);
            return false;
        }
        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.EMAIL_REQUIRED);
            return false;
        }

        if (UpperCaseLetter().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.AZ_MAIUSCULO); 
            return false;

        }
        if (LowerCaseLetter().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.AZ_MINUSCULO); 
            return false;

        }
        if (Numbers().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.NUMERO_ZERO_A_NOVE); 
            return false;

        }
        if (SpecialSymbols().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.CARACTER_ESPECIAL); 
            return false;
        }

        return true;

    }


    private static Regex UpperCaseLetter() => new Regex(@"[A-Z]+");
    private static Regex LowerCaseLetter() => new Regex(@"[a-z]+");
    private static Regex Numbers() => new Regex(@"[0-9]+");
    private static Regex SpecialSymbols() => new Regex(@"[\!\?\*\.]+");



}

