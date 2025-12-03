using AutoMapper;
using ChronicareApiRest.DataAccessObject.Registro;
using ChronicareApiRest.Entity;

namespace ChronicareApiRest.Profiles;

/// <summary>
/// Perfil de AutoMapper para la entidad Registro.
/// </summary>
public class RegistroProfile : Profile
{
    public RegistroProfile()
    {
        CreateMap<Registro, RegistroReadDto>();
        CreateMap<RegistroCreateDto, Registro>();
        CreateMap<RegistroUpdateDto, Registro>();
    }
}
