using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs.Projetos;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Backend.Services;

public sealed class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository    _repo;
    private readonly IMembroRepository     _membroRepo;
    private readonly IUtilizadorRepository _userRepo;
    private readonly IMapper               _mapper;
    private readonly ILogger<ProjetoService> _log;

    public ProjetoService(IProjetoRepository repo,
                          IMembroRepository membroRepo,
                          IUtilizadorRepository userRepo,
                          IMapper mapper,
                          ILogger<ProjetoService> log)
    {
        _repo       = repo;
        _membroRepo = membroRepo;
        _userRepo   = userRepo;
        _mapper     = mapper;
        _log        = log;
    }

    /* ---------------- single ---------------- */
    public async Task<ProjetoDto> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null || entity.IsDeleted) throw new KeyNotFoundException("Projeto não encontrado");
        return _mapper.Map<ProjetoDto>(entity);
    }

    /* ---------------- listagem ---------------- */
    public async Task<IEnumerable<ProjetoDto>> GetByUtilizadorAsync(int responsavelId)
    {
        var list = await _repo.GetByUtilizadorIdAsync(responsavelId);
        return _mapper.Map<IEnumerable<ProjetoDto>>(list.Where(p => !p.IsDeleted));
    }

    /* ---------------- criar / alterar ---------------- */
    public async Task<ProjetoDto> CreateAsync(ProjetoCreateDto dto)
    {
        var entity = _mapper.Map<Projeto>(dto);
        await _repo.AddAsync(entity);
        return _mapper.Map<ProjetoDto>(entity);
    }

    public async Task UpdateAsync(int id, UpdateProjetoDto dto)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Projeto não encontrado");
        _mapper.Map(dto, entity);
        await _repo.UpdateAsync(entity);
    }

    /* ---------------- soft‑delete ---------------- */
    public async Task DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Projeto não encontrado");
        entity.IsDeleted = true;
        await _repo.UpdateAsync(entity);          // grava a flag em vez de eliminar
    }

    /* ---------------- convites ---------------- */
    public async Task ConvidarUtilizadorAsync(int projetoId, string username)
    {
        var user = await _userRepo.GetByUsernameAsync(username)
                   ?? throw new InvalidOperationException("Utilizador não encontrado");

        var membro = new Membro
        {
            IdProjeto     = projetoId,
            IdUtilizador  = user.IdUtilizador,
            EstadoConvite = "Pendente",
            DataConvite   = DateTime.UtcNow
        };
        await _membroRepo.AddAsync(membro);
    }

    public async Task RemoverMembroAsync(int membroId) => await _membroRepo.DeleteAsync(membroId);
}
