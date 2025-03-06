using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persons.API.Database;
using Persons.API.Constants;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Services.Interfaces;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Coutries;
using Microsoft.Extensions.Configuration;


namespace Persons.API.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly PersonsDBContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public CountriesService(PersonsDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PaginationDto<List<CountryDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0
            )
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<CountryEntity> countryQuery = _context.Countries;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                countryQuery = countryQuery.Where(x => (x.Name + " " + x.AlphaCode3).Contains(searchTerm));
            }

            int totalRows = await countryQuery.CountAsync();

            var countriesEntities = await countryQuery.OrderBy(x => x.Name)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var countriesDto = _mapper.Map<List<CountryDto>>(countriesEntities);

            return new ResponseDto<PaginationDto<List<CountryDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = countriesEntities.Count() > 0 ? "Registros Encontrados" : "No se Encontraron Registros", //Operador Ternario
                Data = new PaginationDto<List<CountryDto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = countriesDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math
                    .Ceiling((double)(totalRows / pageSize)),
                }
            };
        }
        public async Task<ResponseDto<CountryDto>> GetOneByIdAsync(Guid id)
        {
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

            var personInCountry = await _context.Persons.CountAsync(p => p.CountryId == id);

            if (personInCountry > 0) 
            {
                return new ResponseDto<CountryActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El Pais Tiene Datos Relacionados!"
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