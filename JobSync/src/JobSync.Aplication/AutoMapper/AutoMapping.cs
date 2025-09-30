using AutoMapper;
using JobSync.Communication.Requests;
using JobSync.Domain.Entities;
using System.Reflection.Metadata;

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
            .ForMember(destUser => destUser.Password, config => config.Ignore());
        //fazer para cpf tbm 
    }
}
