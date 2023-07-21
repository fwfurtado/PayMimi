using System.Text.RegularExpressions;
using AutoMapper;
using PayMimi.Domain.Services;
using PayMimi.Web.Requests;

namespace PayMimi.Web.Converters;

public class CustomerConverters : Profile
{
    public CustomerConverters()
    {
        CreateMap<CustomerRegistrationIntentRequest, RegistrationIntentCommand>()
            .ForMember(command => command.SocialNumber,
                opt =>
                    opt.MapFrom(request => Regex.Replace(request.SocialNumber, @"\D", "")));
    }
}