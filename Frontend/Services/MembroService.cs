using Frontend.DTOs.Membros;
using System.Net.Http.Json;

namespace Frontend.Services;

public class MembroService : ApiService
{
    public MembroService(IHttpClientFactory f) : base(f) { }

    public Task<IEnumerable<MembroDto>?> PorProjetoAsync(int projetoId) =>
        GetAsync<IEnumerable<MembroDto>>($"api/membro/projeto/{projetoId}");

    public async Task<bool> AddAsync(CreateMembroDto dto) =>
        (await PostAsync("api/membro", dto)).IsSuccessStatusCode;

    public Task<bool> DeleteAsync(int id) =>
        DeleteAsync($"api/membro/{id}").ContinueWith(t => t.Result.IsSuccessStatusCode);
}