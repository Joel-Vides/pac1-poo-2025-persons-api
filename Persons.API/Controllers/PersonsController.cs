using Microsoft.AspNetCore.Mvc;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService _personsService;

        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<PersonCreateDto>>>> GetList()//este
        {
            var response = await _personsService.GetListAsync();

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PersonDto>>> GetOne(Guid id)
        {
            var response = await _personsService.GetOneByIdAsync(id);

            return StatusCode(Response.StatusCode, response);
        }


        [HttpPost]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> Post([FromBody] PersonCreateDto dto)
        {
            var response = await _personsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new 
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }//Este


        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> Edit([FromBody]PersonEditDto dto, Guid Id)
        {
            var response = await _personsService.EditAsync(dto, Id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> Delete(Guid id)
        {
            var response = await _personsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }

    }

}