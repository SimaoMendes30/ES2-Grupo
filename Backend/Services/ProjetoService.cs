namespace Backend.Services;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using AutoMapper;
using Backend.DTOs.Projeto;
using Backend.Models;
public sealed class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository  _repo;
    private readonly IMembroRepository   _membroRepo;
    private readonly IUtilizadorRepository _userRepo;
    private readonly IMapper             _mapper;
    private readonly ILogger<ProjetoService> _logger;

    public ProjetoService(IProjetoRepository repo, IMembroRepository membroRepo, IUtilizadorRepository userRepo,
                          IMapper mapper, ILogger<ProjetoService> logger)
    {
        _repo       = repo;
        _membroRepo = membroRepo;
        _userRepo   = userRepo;
        _mapper     = mapper;
        _logger     = logger;
    }

    public async Task<ProjetoDto> GetByIdAsync(int id) =>
        _mapper.Map<ProjetoDto>(await _repo.GetByIdAsync(id));

    public async Task<IEnumerable<ProjetoDto>> GetByUtilizadorAsync(int utilizadorId) =>
        _mapper.Map<IEnumerable<ProjetoDto>>(await _repo.GetByUtilizadorIdAsync(utilizadorId));

    public async Task<ProjetoDto> CreateAsync(ProjetoCreateDto dto)
    {
        var entity = _mapper.Map<Projeto>(dto);
        await _repo.AddAsync(entity);
        return _mapper.Map<ProjetoDto>(entity);
    }

    public async Task UpdateAsync(int id, UpdateProjetoDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Projeto não encontrado");

        _mapper.Map(dto, entity); // mapeia apenas campos presentes
        await _repo.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

    public async Task ConvidarUtilizadorAsync(int projetoId, string username)
    {
        var utilizador = await _userRepo.GetByUsernameAsync(username) ??
                         throw new InvalidOperationException("Utilizador não encontrado");

        var membro = new Membro
        {
            IdProjeto     = projetoId,
            IdUtilizador  = utilizador.IdUtilizador,
            EstadoConvite = "Pendente",
            DataConvite   = DateOnly.FromDateTime(DateTime.UtcNow)
        };
        await _membroRepo.AddAsync(membro);
    }

    public async Task RemoverMembroAsync(int membroId) => await _membroRepo.DeleteAsync(membroId);
}