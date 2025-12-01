using ChronicareApiRest.DataAccessObject.Paciente;
using ChronicareApiRest.Entity;
using AutoMapper;

namespace ChronicareApiRest.Profiles;

/// <summary>
/// Perfil de AutoMapper que define los mapeos entre entidades Paciente y sus DTOs.
/// </summary>
public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<Paciente, PacienteReadDto>();

        CreateMap<PacienteCreateDto, Paciente>()
            .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => "activo"));

        CreateMap<PacienteUpdateDto, Paciente>();
    }
}
