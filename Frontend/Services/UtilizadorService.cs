using Frontend.DTOs.Utilizadores;
using System.Net.Http.Json;

namespace Frontend.Services;

public class UtilizadorService : ApiService
{
    public UtilizadorService(IHttpClientFactory f) : base(f) { }

    public Task<UtilizadorDto?>     GetAsync(int id)     => GetAsync<UtilizadorDto>($"api/utilizador/{id}");
    public Task<IEnumerable<UtilizadorDto>?> ListAsync() => GetAsync<IEnumerable<UtilizadorDto>>("api/utilizador");

    public async Task<bool> RegisterAsync(UtilizadorCreateDto dto)
        => (await PostAsync("api/utilizador/criar", dto)).IsSuccessStatusCode;

    public Task<bool> UpdateAsync(int id, UtilizadorUpdateDto dto) =>
        PutAsync($"api/utilizador/{id}", dto).ContinueWith(t => t.Result.IsSuccessStatusCode);

    public Task<bool> UpdatePasswordAsync(int id, UtilizadorUpdatePasswordDto dto) =>
        PutAsync($"api/utilizador/{id}/password", dto).ContinueWith(t => t.Result.IsSuccessStatusCode);
}