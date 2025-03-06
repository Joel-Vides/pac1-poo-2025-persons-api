﻿using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Controllers;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly PersonsDBContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public PersonsService(PersonsDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PaginationDto<List<PersonDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0
            )
        {
            //03/03/25 ->
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<PersonEntity> personQuery = _context.Persons;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                personQuery = personQuery.Where(x => (x.DNI + " " + x.FirstName + " " + x.LastName).Contains(searchTerm));
            }

            int totalRows = await personQuery.CountAsync();

            var personsEntity = await personQuery.OrderBy(x => x.FirstName)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();
            // <-


            //Lo que Teniamos Antes de lo de Arriba
            //var personsEntity = await _context.Persons.ToListAsync();

            var personsDto = _mapper.Map<List<PersonDto>>(personsEntity);

            return new ResponseDto<PaginationDto<List<PersonDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = personsEntity.Count() > 0 ? "Registros Encontrados" : "No se Encontraron Registros", //Operador Ternario
                Data = new PaginationDto<List<PersonDto>>{
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = personsDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math
                    .Ceiling((double)(totalRows / pageSize)),
                }
            };
        }

        public async Task<ResponseDto<PersonDto>> GetOneByIdAsync(Guid id)
        {                                           //person => person.Id
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
                Data = _mapper.Map<PersonDto>(personEntity)
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto)
        {

            var personEntity = _mapper.Map<PersonEntity>(dto);

            var countryEntity = await _context.Countries.FirstOrDefaultAsync(c => c.Id == dto.CountryId);

            if (countryEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El Pais no Existe!"
                };
            }

            _context.Persons.Add(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente",
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
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

            //Validar si Existe el Pais en la Base de Datos de Paises
            var countryEntity = await _context.Countries.FirstOrDefaultAsync(c => c.Id == dto.CountryId);

            if (countryEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El Pais no Existe!"
                };
            }

            //Para Mapeo sin AutoMapper
            //personEntity.FirstName = dto.FirstName;
            //personEntity.LastName = dto.LastName;
            //personEntity.DNI = dto.DNI;
            //personEntity.Gender = dto.Gender;

            _mapper.Map<PersonEditDto, PersonEntity>(dto, personEntity);

            _context.Persons.Update(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Editado Correctamente",
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
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
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
            };

        }

    
    }
}