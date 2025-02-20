using AutoMapper;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Persons;

namespace Persons.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {//origen, destino
            CreateMap<PersonEntity, PersonDto>();
            CreateMap<PersonEntity, PersonActionResponseDto>();
            CreateMap<PersonCreateDto, PersonEntity>();
            CreateMap<PersonEditDto, PersonEntity>();

        }
    }
}
