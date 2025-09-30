using AutoMapper;
using FluentValidation.Results;
using JobSync.Communication.Requests;
using JobSync.Communication.Responses;
using JobSync.Domain.Repositories;
using JobSync.Domain.Repositories.User;
using JobSync.Domain.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace JobSync.Aplication.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;

    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        //logic to register user
    }


    public async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);
        var emailExtist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        var phoneExtist = await _userReadOnlyRepository.ExistActiveUserWithPhoneNumber(request.PhoneNumber);

        if (emailExtist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMesssages.));
        }
        //continuar lógica
    }
}
