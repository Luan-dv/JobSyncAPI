using FluentValidation;
using JobSync.Communication.Requests;

namespace JobSync.Aplication.UseCases.Users.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("nome vazio");
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("email vazio") //trocar depois
            .EmailAddress()
            .EmailAddress().WithMessage("email inválido");
        RuleFor(user => user.PhoneNumber)
            .NotEmpty()
            .WithMessage("telefone vazio") //trocar depois
            .NotNull().WithMessage("telefone nulo")
            .Matches(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$")
            .WithMessage("núimero inválido");

        RuleFor(user => user.Gender)
            .IsInEnum()

            .WithMessage("gênero inválido");



        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson >());
        RuleFor(user => user.Cpf).SetValidator(new CpfValidator<RequestRegisterUserJson>());
    }
}
