using AutoMapper;
using ChronicareApiRest.DataAccessObject.Tarea;
using ChronicareApiRest.Entity;

namespace ChronicareApiRest.Profiles;

public class TareaProfile : Profile
{
    public TareaProfile()
    {
        CreateMap<Tarea, TareaReadDto>();
    }
}
