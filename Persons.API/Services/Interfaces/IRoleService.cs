using Persons.API.Dtos.Common;
using Persons.API.Dtos.Security.Roles;

namespace Persons.API.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseDto<RoleActionResponseDto>> CreateAsync(RoleCreateDto dto);
        Task<ResponseDto<PaginationDto<List<RoleDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 10     
        );
    }
}
