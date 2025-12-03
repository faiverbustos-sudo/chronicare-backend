using AutoMapper;
using ChronicareApiRest.DataAccessObject.Medicamento;
using ChronicareApiRest.Entity;

namespace ChronicareApiRest.Profiles;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<Medicamento, MedicamentoReadDto>();
    }
}
