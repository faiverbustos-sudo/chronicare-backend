namespace ChronicareApiRest.Profiles;

using AutoMapper;
using ChronicareApiRest.DataAccessObject.Medico;
using ChronicareApiRest.Entity;

public class MedicoProfile : Profile
{
    public MedicoProfile()
    {
        CreateMap<Medico, MedicoReadDto>();

        CreateMap<MedicoCreateDto, Medico>()
            .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IdMedico, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<MedicoUpdateDto, Medico>();
    }
}
