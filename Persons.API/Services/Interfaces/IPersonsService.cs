using Persons.API.Controllers;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;

namespace Persons.API.Services.Interfaces
{
    public interface IPersonsService
    {
        Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonsCreateDto dto);
        Task CreateAsync(PersonCreateDto dto);//este
        Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<PersonActionResponseDto>> EditAsync(PersonEditDto dto, Guid id);
        Task<ResponseDto<List<PersonDto>>> GetListAsync();
        Task<ResponseDto<PersonDto>> GetOneByIdAsync(Guid id);
    }
}