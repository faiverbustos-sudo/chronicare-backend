using AutoMapper;
using ChronicareApiRest.DataAccessObject.Alerta;
using ChronicareApiRest.Entity;

namespace ChronicareApiRest.Profiles;

public class AlertaProfile : Profile
{
    public AlertaProfile()
    {
        CreateMap<Alerta, AlertaReadDto>();
    }
}
