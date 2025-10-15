using System;
using AutoMapper;
using FluentValidation.Results;
using JobSync.Communication.Requests;
using JobSync.Communication.Responses;
using JobSync.Domain.Repositories;
using JobSync.Domain.Repositories.User;
using JobSync.Domain.Security.Cryptography;
using JobSync.Domain.Security.Tokens;
using JobSync.Exception;
using JobSync.Exception.ExceptionBase;

namespace JobSync.Aplication.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAcessTokenGenerator _tokenGenerator;

    public RegisterUserUseCase(IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IAcessTokenGenerator tokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;

    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
       
        
        await Validate(request);
        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _tokenGenerator.Generate(user)
        };
        
        
    }
    

    public async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);
        var emailExtist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        var phoneExtist = await _userReadOnlyRepository.ExistActiveUserWithPhoneNumber(request.PhoneNumber);
       
        if (emailExtist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_DOES_NOT_EXIST));
        }
        if (phoneExtist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.PHONE_DOES_NOT_EXIST));
        }
        
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationExceptions(errorMessages);
        }
    }
}
