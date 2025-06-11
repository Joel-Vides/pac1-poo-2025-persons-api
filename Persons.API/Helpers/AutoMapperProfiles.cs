using AutoMapper;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Countries;
using Persons.API.Dtos.Coutries;
using Persons.API.Dtos.Persons;
using Persons.API.Dtos.Security.Roles;

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

            CreateMap<CountryEntity, CountryDto>();
            CreateMap<CountryEntity, CountryActionResponseDto>();
            CreateMap<CountriesCreateDto, CountryEntity>();
            CreateMap<CountryEditDto, CountryEntity>();
            
            //Pasarse Info el Uno a el Otro
            CreateMap<FamilyMemberCreateDto, FamilyMemberEntity>().ReverseMap();

            CreateMap<RoleEntity, RoleDto>();
            CreateMap<RoleEntity, RoleActionResponseDto>();
            CreateMap<RoleCreateDto, RoleEntity>();
            CreateMap<RoleEditDto, RoleEntity>();
        }
    }
}
