using Frontend.DTOs.Common;
using System.Net.Http.Json;
using Frontend.DTOs.Task;

namespace Frontend.Services;

public class TaskService : ApiService
{
    public TaskService(IHttpClientFactory f) : base(f) { }

    public async Task<IEnumerable<TaskDetailsDto>> GetAllAsync(TaskFilterDto? filter = null)
    {
        var result = await GetAsync<IEnumerable<TaskDetailsDto>>("api/tarefas", filter);
        return result ?? throw new Exception("Erro ao obter a lista de tarefas.");
    }

    public async Task<PagedResult<TaskDetailsDto>> GetPagedAsync(TaskFilterDto? filter = null, int page = 1, int pageSize = 10)
    {
        var query = new
        {
            page,
            pageSize,
            filter?.IsDeleted,
            filter?.IdTarefa,
            filter?.PrecoHoraMax,
            filter?.PrecoHoraMin,
            filter?.Responsavel,
            filter?.Titulo,
            filter?.DataInicioAte,
            filter?.DataInicioDe,
            filter?.DataFimAte,
            filter?.DataFimDe,
        };

        var result = await GetAsync<PagedResult<TaskDetailsDto>>("api/tarefas/paged", query);
        return result ?? throw new Exception("Erro ao obter tarefas paginadas.");
    }

    public async Task<TaskDetailsExtendedDto> GetByIdAsync(int id)
    {
        var result = await GetAsync<TaskDetailsExtendedDto>($"api/tarefas/{id}");
        return result ?? throw new Exception($"Tarefa com ID {id} não encontrada.");
    }

    public async Task<TaskDetailsDto> CreateAsync(TaskCreateDto dto)
    {
        var response = await PostAsync("api/tarefas", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao criar a tarefa.");

        var data = await response.Content.ReadFromJsonAsync<TaskDetailsDto>();
        return data ?? throw new Exception("Resposta inválida ao criar a tarefa.");
    }

    public async Task<bool> UpdateAsync(int id, TaskUpdateDto dto)
    {
        var response = await PutAsync($"api/tarefas/{id}", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao atualizar a tarefa com ID {id}.");
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await DeleteRequestAsync($"api/tarefas/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao eliminar a tarefa com ID {id}.");
        return true;
    }
    
    public async Task<PagedResult<TaskDetailsDto>> GetByUserPagedAsync(
        int userId,
        int page = 1,
        int pageSize = 10,
        string? orderBy = null,
        bool descending = false,
        TaskFilterDto? filtros = null)
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
            queryParams["titulo"] = filtros.Titulo;
            queryParams["responsavel"] = filtros.Responsavel?.ToString();
            queryParams["dataInicioDe"] = filtros.DataInicioDe?.ToString("o");
            queryParams["dataInicioAte"] = filtros.DataInicioAte?.ToString("o");
            queryParams["dataFimDe"] = filtros.DataFimDe?.ToString("o");
            queryParams["dataFimAte"] = filtros.DataFimAte?.ToString("o");
            queryParams["estado"] = filtros.Estado;
            queryParams["precoHoraMin"] = filtros.PrecoHoraMin?.ToString();
            queryParams["precoHoraMax"] = filtros.PrecoHoraMax?.ToString();
            queryParams["isDeleted"] = filtros.IsDeleted?.ToString().ToLower();
        }

        var queryString = string.Join("&",
            queryParams
                .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
                .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"));

        var url = $"api/tarefas/utilizador/{userId}/paged?{queryString}";

        var result = await GetAsync<PagedResult<TaskDetailsDto>>(url);
        return result ?? throw new Exception("Erro ao obter tarefas do utilizador.");
    }
    
    public async Task<IEnumerable<TaskDetailsDto>> GetByProjetoIdAsync(int projetoId)
    {
        var result = await GetAsync<IEnumerable<TaskDetailsDto>>($"api/tarefas/projeto/{projetoId}");
        return result ?? throw new Exception("Erro ao obter tarefas do projeto.");
    }
}
