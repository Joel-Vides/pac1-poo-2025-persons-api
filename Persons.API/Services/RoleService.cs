using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Security.Roles;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly PersonsDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public RoleService(RoleManager<RoleEntity> roleManager, PersonsDBContext context, IConfiguration configuration, IMapper mapper)
        {
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            PAGE_SIZE = _configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = _configuration.GetValue<int>("PageSizeLimit");
        }

        public PersonsDBContext Context { get; }
        public IConfiguration Configuration { get; }

        public async Task<ResponseDto<PaginationDto<List<RoleDto>>>> GetListAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;

            int startIdenx = (page - 1) * pageSize;

            IQueryable<RoleEntity> rolesQuery = _context.Roles;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                rolesQuery = rolesQuery.Where(r => (r.Name + " " + r.Description).Contains(searchTerm));
            }

            int totalRows = await rolesQuery.CountAsync();

            var roles = await rolesQuery.OrderBy(r => r.Name)
                .Skip(startIdenx)
                .Take(pageSize)
                .ToListAsync();

            var rolesDto = _mapper.Map<List<RoleDto>>(roles);

            return new ResponseDto<PaginationDto<List<RoleDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registros Obtenidos Correctamente",
                Data = new PaginationDto<List<RoleDto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = rolesDto,
                    HasNextPage = startIdenx + pageSize > PAGE_SIZE_LIMIT && page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };

        }

        public async Task<ResponseDto<RoleActionResponseDto>> CreateAsync(RoleCreateDto dto)
        {
            var role = _mapper.Map<RoleEntity>(dto);

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return new ResponseDto<RoleActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description)),
                };
            }

            return new ResponseDto<RoleActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente",
                Data = _mapper.Map<RoleActionResponseDto>(role)
            };
        }
    }
}