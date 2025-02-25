using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persons.API.Database;
using Persons.API.Constants;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Services.Interfaces;
using Persons.API.Dtos.Coutries;
using Persons.API.Database.Entities;

namespace Persons.API.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly PersonsDBContext _context;
        private readonly IMapper _mapper;

        public CountriesService(PersonsDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CountryDto>>> GetListAsync()
        {
            var countriesEntities = await _context.Countries.ToListAsync();

            var countriesDto = _mapper.Map<List<CountryDto>>(countriesEntities);

            return new ResponseDto<List<CountryDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = countriesEntities.Count() > 0 ? "Registros Encontrados" : "No se Encontraron Registros", //Operador Ternario
                Data = countriesDto
            };
        }

        public async Task<ResponseDto<CountryDto>> GetOneByIdAsync(Guid id)
        {                                           //person => person.Id
            var countryEntity = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (countryEntity is null)
            {
                return new ResponseDto<CountryDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado"
                };

            }
            return new ResponseDto<CountryDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Encontrado",
                Data = _mapper.Map<CountryDto>(countryEntity)
            };
        }

        public async Task<ResponseDto<CountryActionResponseDto>> CreateAsync(CountriesCreateDto dto)
        {

            var countriesEntity = _mapper.Map<CountryEntity>(dto);

            _context.Countries.Add(countriesEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countriesEntity)
            };
        }

        public async Task<ResponseDto<CountryActionResponseDto>> EditAsync(CountryEditDto dto, Guid id)
        {
            var countryEntity = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (countryEntity is null)
            {
                return new ResponseDto<CountryActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado",
                };
            }

            _mapper.Map<CountryEditDto, CountryEntity>(dto, countryEntity);

            _context.Countries.Update(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Editado Correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity)
            };
        }

        public async Task<ResponseDto<CountryActionResponseDto>> DeleteAsync(Guid id)
        {
            var countryEntity = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (countryEntity is null)
            {
                return new ResponseDto<CountryActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado"
                };
            }

            _context.Countries.Remove(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Eliminado Correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity)
            };

        }

    }
}
