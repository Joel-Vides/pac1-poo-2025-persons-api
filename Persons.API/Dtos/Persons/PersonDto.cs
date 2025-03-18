using Persons.API.Database.Entities;
using Persons.API.Dtos.Persons;

namespace Persons.API.Dtos.Countries
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string Gender { get; set; }
        public Guid CountryId { get; set; }
        public List<FamilyMemberCreateDto> FamilyGroup { get; set; }
    }
}
