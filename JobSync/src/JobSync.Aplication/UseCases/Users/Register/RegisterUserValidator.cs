using FluentValidation;
using JobSync.Communication.Requests;
using JobSync.Exception;

namespace JobSync.Aplication.UseCases.Users.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .EmailAddress().WithMessage(ResourceErrorMessages.EMAIL_INVALID);
        RuleFor(user => user.PhoneNumber)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.TELEFONE_EMPTY) //trocar depois
            .NotNull().WithMessage(ResourceErrorMessages.NULL_PHONE)
            .Matches(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$")
            .WithMessage(ResourceErrorMessages.NUMBER_INVALID);

        RuleFor(user => user.Gender)
            .IsInEnum()

            .WithMessage(ResourceErrorMessages.GENDER_INVALID);



        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson >());
        RuleFor(user => user.Cpf).SetValidator(new CpfValidator<RequestRegisterUserJson>());
    }
}
