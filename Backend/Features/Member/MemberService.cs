using AutoMapper;
using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Member;
using Backend.Features.Member.Interfaces;
using Backend.Domain.Patterns.Factories;

namespace Backend.Features.Member;

public sealed class MemberService : IMemberService
{
    private readonly IMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<MemberService> _logger;

    public MemberService(IMemberRepository repo, IMapper mapper, ILogger<MemberService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<MemberDetailsDto> CreateAsync(MemberCreateDto dto)
    {
        var entity = MemberFactory.Create(
            idProjeto: dto.IdProjeto,
            idUtilizador: dto.IdUtilizador,
            estadoConvite: dto.EstadoConvite,
            estadoAtividade: dto.EstadoAtividade,
            dataConvite: dto.DataConvite,
            dataEstado: dto.DataEstado
        );

        await _repo.AddAsync(entity);
        return _mapper.Map<MemberDetailsDto>(entity);
    }

    public async Task<IEnumerable<MemberDetailsDto>> FilteredListAsync(MemberFilterDto filter)
    {
        var list = await _repo.FilteredListAsync(filter);
        return _mapper.Map<IEnumerable<MemberDetailsDto>>(list);
    }

    public async Task<PagedResult<MemberDetailsDto>> FilteredPagedAsync(MemberFilterDto filter, int page, int pageSize)
    {
        var result = await _repo.FilteredPagedAsync(filter, page, pageSize);
        return new PagedResult<MemberDetailsDto>
        {
            Items = _mapper.Map<IEnumerable<MemberDetailsDto>>(result.Items),
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
    }

    public async Task<MemberDetailsExtendedDto> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Membro {id} não encontrado");

        return _mapper.Map<MemberDetailsExtendedDto>(entity);
    }

    public async System.Threading.Tasks.Task UpdateAsync(int id, MemberUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Membro {id} não encontrado");

        _mapper.Map(dto, entity);
        await _repo.UpdateAsync(entity);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }
}