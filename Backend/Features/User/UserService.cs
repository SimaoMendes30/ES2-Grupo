using AutoMapper;
using Backend.Models;
using Backend.Domain.DTOs.User;
using Backend.Domain.DTOs.Common;
using Backend.Features.User.Interfaces;
using Backend.Domain.Patterns.Builders.Interfaces;

namespace Backend.Features.User;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;
    private readonly IJwtTokenBuilder _tokenBuilder;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository repo,
        IMapper mapper,
        IJwtTokenBuilder tokenBuilder,
        ILogger<UserService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _tokenBuilder = tokenBuilder;
        _logger = logger;
    }

    public async Task<UserTokenDto> GenerateTokenAsync(UserLoginDto loginDto)
    {
        var user = await _repo.GetByUsernameAsync(loginDto.Username)
                   ?? throw new UnauthorizedAccessException("Credenciais inválidas");

        if (user.Password != loginDto.Password)
            throw new UnauthorizedAccessException("Credenciais inválidas");
        
        if(user.IsDeleted == true)
            throw new UnauthorizedAccessException("Utilizador eliminado");
        
        return _tokenBuilder.BuildToken(user);
    }

    public async Task<UserDetailsDto> CreateAsync(UserCreateDto dto)
    {
        var entity = _mapper.Map<UserEntity>(dto);
        await _repo.AddAsync(entity);
        return _mapper.Map<UserDetailsDto>(entity);
    }


    public async Task<IEnumerable<UserDetailsDto>> FilteredListAsync(UserFilterDto filter)
    {
        var list = await _repo.FilteredListAsync(filter);
        return _mapper.Map<IEnumerable<UserDetailsDto>>(list);
    }

    public async Task<PagedResult<UserDetailsDto>> FilteredPagedAsync(UserFilterDto filter, int page, int pageSize)
    {
        var result = await _repo.FilteredPagedAsync(filter, page, pageSize);
        return new PagedResult<UserDetailsDto>
        {
            Items = _mapper.Map<IEnumerable<UserDetailsDto>>(result.Items),
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
    }

    public async Task<UserDetailsExtendedDto> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Utilizador {id} não encontrado");

        return _mapper.Map<UserDetailsExtendedDto>(entity);
    }

    public async System.Threading.Tasks.Task UpdateAsync(int id, UserUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Utilizador {id} não encontrado");

        _mapper.Map(dto, entity);
        await _repo.UpdateAsync(entity);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }
}