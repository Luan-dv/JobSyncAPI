using AutoMapper;
using JobSync.Communication.Requests;
using JobSync.Domain.Entities;

namespace JobSync.Aplication.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
      
    }
    private void RequestToEntity()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(destUser => destUser.Password, config => config.Ignore())
            .ForMember(destUser => destUser.Cpf, config => config.Ignore());


    }
}
