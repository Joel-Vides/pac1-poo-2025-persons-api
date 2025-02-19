using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Controllers;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly PersonsDBContext _context;

        public PersonsService(PersonsDBContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<PersonDto>>> GetListAsync()
        {
            var personsEntity = await _context.Persons.ToListAsync();

            var personsDto = new List<PersonDto>();

            foreach (var person in personsDto)
            {
                personsDto.Add(new PersonDto
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    DNI = person.DNI,
                    Gender = person.Gender
                });
            }

            return new ResponseDto<List<PersonDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = personsEntity.Count() > 0 ? "Registros Encontrados" : "No se Encontraron Registros", //Operador Ternario
                Data = personsDto
            };
        }

        public async Task<ResponseDto<PersonDto>> GetOneByIdAsync(Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null) 
            {
                return new ResponseDto<PersonDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado"
                };

            }
            return new ResponseDto<PersonDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Encontrado",
                Data = new PersonDto
                {
                    Id = personEntity.Id,
                    FirstName = personEntity.FirstName,
                    LastName = personEntity.LastName,
                    DNI = personEntity.DNI,
                    Gender = personEntity.Gender
                }
            };
        }

        //Es person no persons
        public async Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonsCreateDto dto)
        {
            var personEntity = new PersonEntity
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DNI = dto.DNI,
                Gender = dto.Gender
            };

            _context.Persons.Add(personEntity);
            await _context.SaveChangesAsync();

            var response = new PersonActionResponseDto
            {
                Id = personEntity.Id,
                FirstName = personEntity.FirstName,
                LastName = personEntity.LastName,
            };

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente",
                Data = dto
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> EditAsync(PersonEditDto dto, Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado",
                };
            }

            personEntity.FirstName = dto.FirstName;
            personEntity.LastName = dto.LastName;
            personEntity.DNI = dto.DNI;
            personEntity.Gender = dto.Gender;

            _context.Persons.Update(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Editado Correctamente",
                Data = new PersonActionResponseDto
                {
                    Id = personEntity.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                }
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado"
                };
            }

            _context.Persons.Remove(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Eliminado Correctamente",
                Data = new PersonActionResponseDto
                {
                    Id = personEntity.Id,
                    FirstName = personEntity.FirstName,
                    LastName = personEntity.LastName,
                }
            };

        }

        //Esto
        public Task CreateAsync(PersonCreateDto dto)
        {
            throw new NotImplementedException();
        }



    }
}