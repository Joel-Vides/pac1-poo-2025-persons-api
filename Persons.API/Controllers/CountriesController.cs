using Microsoft.AspNetCore.Mvc;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Dtos.Coutries;
using Persons.API.Services;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CountriesCreateDto>>>> GetList(
            string searchTerm = "", int page = 1, int pageSize = 0
            )
        {
            var response = await _countriesService.GetListAsync(searchTerm, page, pageSize);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<CountryDto>>> GetOne(Guid id)
        {
            var response = await _countriesService.GetOneByIdAsync(id);

            return StatusCode(Response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<CountryActionResponseDto>>> Post([FromBody] CountriesCreateDto dto)
        {
            var response = await _countriesService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<CountryActionResponseDto>>> Edit([FromBody] CountryEditDto dto, Guid Id)
        {
            var response = await _countriesService.EditAsync(dto, Id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<CountryActionResponseDto>>> Delete(Guid id)
        {
            var response = await _countriesService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }

    }
}