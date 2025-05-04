using Frontend.DTOs.Common;
using System.Net.Http.Json;
using Frontend.DTOs.Member;

namespace Frontend.Services;

public class MemberService : ApiService
{
    public MemberService(IHttpClientFactory f) : base(f) { }

    public async Task<IEnumerable<MemberDetailsDto>> GetAllAsync(MemberFilterDto? filter = null)
    {
        var result = await GetAsync<IEnumerable<MemberDetailsDto>>("api/membros", filter);
        return result ?? throw new Exception("Erro ao obter a lista de membros.");
    }

    public async Task<PagedResult<MemberDetailsDto>> GetPagedAsync(MemberFilterDto? filter = null, int page = 1, int pageSize = 10)
    {
        var query = new
        {
            page,
            pageSize,
            filter?.IdMembro,
            filter?.IdUtilizador,
            filter?.IdProjeto,
            filter?.DataConviteAte,
            filter?.DataConviteDe,
            filter?.DataEstadoAte,
            filter?.DataEstadoDe,
            filter?.EstadoConvite,
            filter?.EstadoAtividade
        };
        
        var result = await GetAsync<PagedResult<MemberDetailsDto>>("api/membros/paged", query);
    
        if (result == null)
        {
            throw new Exception("Erro ao obter membros paginados.");
        }

        return result;
    }


    public async Task<MemberDetailsExtendedDto> GetByIdAsync(int id)
    {
        var result = await GetAsync<MemberDetailsExtendedDto>($"api/membros/{id}");
        return result ?? throw new Exception($"Membro com ID {id} não encontrado.");
    }

    public async Task<MemberDetailsDto> CreateAsync(MemberCreateDto dto)
    {
        var response = await PostAsync("api/membros", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao criar o membro.");

        var data = await response.Content.ReadFromJsonAsync<MemberDetailsDto>();
        return data ?? throw new Exception("Resposta inválida ao criar o membro.");
    }

    public async Task<bool> UpdateAsync(int id, MemberUpdateDto dto)
    {
        var response = await PutAsync($"api/membros/{id}", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao atualizar o membro com ID {id}.");
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await DeleteRequestAsync($"api/membros/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao eliminar o membro com ID {id}.");
        return true;
    }
}
