using Microsoft.AspNetCore.Mvc;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Statistics;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticsService;

        public StatisticsController(IStatisticService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("counts")]
        public async Task<ActionResult<ResponseDto<StatisticsDto>>> GetCounts()
        {
            var response = await _statisticsService.GetCounts();

            return StatusCode(response.StatusCode, new ResponseDto<StatisticsDto>
            {
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }
    }
}
