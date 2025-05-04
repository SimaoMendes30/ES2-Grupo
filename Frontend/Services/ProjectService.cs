using Frontend.DTOs.Common;
using System.Net.Http.Json;
using Frontend.DTOs.Project;

namespace Frontend.Services;

public class ProjectService : ApiService
{
    public ProjectService(IHttpClientFactory f) : base(f) { }

    public async Task<IEnumerable<ProjectDetailsDto>> GetAllAsync(ProjectFilterDto? filter = null)
    {
        var result = await GetAsync<IEnumerable<ProjectDetailsDto>>("api/projetos", filter);
        return result ?? throw new Exception("Erro ao obter a lista de projetos.");
    }

    public async Task<PagedResult<ProjectDetailsDto>> GetPagedAsync(ProjectFilterDto? filter = null, int page = 1, int pageSize = 10)
    {
        var query = new
        {
            page,
            pageSize,
            filter?.Nome,
            filter?.NomeCliente,
            filter?.DataCriacaoAte,
            filter?.DataCriacaoDe,
            filter?.PrecoHoraMax,
            filter?.PrecoHoraMin,
            filter?.Responsavel,
            filter?.IsDeleted,
            filter?.IdProjeto
        };

        var result = await GetAsync<PagedResult<ProjectDetailsDto>>("api/projetos/paged", query);
        return result ?? throw new Exception("Erro ao obter projetos paginados.");
    }

    public async Task<ProjectDetailsExtendedDto> GetByIdAsync(int id)
    {
        var result = await GetAsync<ProjectDetailsExtendedDto>($"api/projetos/{id}");
        return result ?? throw new Exception($"Projeto com ID {id} não encontrado.");
    }

    public async Task<ProjectDetailsDto> CreateAsync(ProjectCreateDto dto)
    {
        var response = await PostAsync("api/projetos", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao criar o projeto.");

        var data = await response.Content.ReadFromJsonAsync<ProjectDetailsDto>();
        return data ?? throw new Exception("Resposta inválida ao criar o projeto.");
    }

    public async Task<bool> UpdateAsync(int id, ProjectUpdateDto dto)
    {
        var response = await PutAsync($"api/projetos/{id}", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao atualizar o projeto com ID {id}.");
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await DeleteRequestAsync($"api/projetos/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao eliminar o projeto com ID {id}.");
        return true;
    }
    
    public async Task<PagedResult<ProjectDetailsDto>> GetByUserPagedAsync(
        int userId,
        int page = 1,
        int pageSize = 10,
        string? orderBy = null,
        bool descending = false,
        ProjectFilterDto? filtros = null)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["page"] = page.ToString(),
            ["pageSize"] = pageSize.ToString(),
            ["orderBy"] = orderBy,
            ["descending"] = descending.ToString().ToLower()
        };

        if (filtros != null)
        {
            queryParams["nome"] = filtros.Nome;
            queryParams["nomeCliente"] = filtros.NomeCliente;
            queryParams["dataCriacaoDe"] = filtros.DataCriacaoDe?.ToString("o"); // ISO 8601
            queryParams["dataCriacaoAte"] = filtros.DataCriacaoAte?.ToString("o");
            queryParams["precoHoraMin"] = filtros.PrecoHoraMin?.ToString();
            queryParams["precoHoraMax"] = filtros.PrecoHoraMax?.ToString();
            queryParams["isDeleted"] = filtros.IsDeleted?.ToString().ToLower();
        }

        var queryString = string.Join("&",
            queryParams
                .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
                .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"));

        var url = $"api/projetos/utilizador/{userId}/paged?{queryString}";

        var result = await GetAsync<PagedResult<ProjectDetailsDto>>(url);
        return result ?? throw new Exception("Erro ao obter projetos do utilizador.");
    }
}
