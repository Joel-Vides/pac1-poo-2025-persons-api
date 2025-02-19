namespace Persons.API.Dtos.Persons
{
    public class PersonActionResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static implicit operator PersonActionResponseDto(PersonsCreateDto v)
        {
            throw new NotImplementedException();
        }
    }
}
