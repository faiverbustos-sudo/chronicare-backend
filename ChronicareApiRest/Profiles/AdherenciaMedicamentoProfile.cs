using AutoMapper;
using ChronicareApiRest.DataAccessObject.AdherenciaMedicamento;
using ChronicareApiRest.Entity;

namespace ChronicareApiRest.Profiles;

public class AdherenciaMedicamentoProfile : Profile
{
    public AdherenciaMedicamentoProfile()
    {
        CreateMap<AdherenciaMedicamento, AdherenciaMedicamentoReadDto>();
        CreateMap<AdherenciaMedicamentoCreateDto, AdherenciaMedicamento>();
        CreateMap<AdherenciaMedicamentoUpdateDto, AdherenciaMedicamento>();
    }
}
