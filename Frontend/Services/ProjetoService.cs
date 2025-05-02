using Frontend.DTOs.Projetos;
using System.Net.Http.Json;

namespace Frontend.Services;

public class ProjetoService : ApiService
{
    public ProjetoService(IHttpClientFactory f) : base(f) { }

    public Task<ProjetoDto?> GetAsync(int id) => GetAsync<ProjetoDto>($"api/projeto/{id}");

    public Task<IEnumerable<ProjetoDto>?> ListByUserAsync(int utilizadorId) =>
        GetAsync<IEnumerable<ProjetoDto>>($"api/projeto/utilizador/{utilizadorId}");

    public async Task<bool> CreateAsync(ProjetoCreateDto dto)
        => (await PostAsync("api/projeto", dto)).IsSuccessStatusCode;

    public Task<bool> UpdateAsync(int id, UpdateProjetoDto dto) =>
        PutAsync($"api/projeto/{id}", dto).ContinueWith(t => t.Result.IsSuccessStatusCode);

    public Task<bool> DeleteAsync(int id) =>
        DeleteAsync($"api/projeto/{id}").ContinueWith(t => t.Result.IsSuccessStatusCode);
}