using Persons.API.Dtos.Common;
using Persons.API.Dtos.Statistics;

namespace Persons.API.Services.Interfaces
{
    public interface IStatisticService
    {
        Task<ResponseDto<StatisticsDto>> GetCounts();
    }
}
