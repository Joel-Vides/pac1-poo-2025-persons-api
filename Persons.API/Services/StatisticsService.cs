using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Statistics;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class StatisticsService : IStatisticService
    {

        private readonly PersonsDBContext _context;

        public StatisticsService(PersonsDBContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<StatisticsDto>> GetCounts()
        {
            var statistics = new StatisticsDto();

            statistics.CountriesCount = await _context.Countries
                .CountAsync();

            statistics.PersonsCount = await _context.Persons
                .CountAsync();

            return new ResponseDto<StatisticsDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Datos obtenidos correctamente",
                Data = statistics
            };
        }
    }

}
    

