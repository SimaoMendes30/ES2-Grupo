using Blazored.LocalStorage;
using System.Net.Http.Json;
using Frontend.DTOs.Utilizadores;              
using System.Net.Http;

namespace Frontend.Services;

public sealed class AuthService : ApiService
{
    private readonly ILocalStorageService _storage;

    public AuthService(IHttpClientFactory factory,
        ILocalStorageService storage) : base(factory)
        => _storage = storage;

    /// <summary>Autentica o utilizador e guarda o token no localStorage.</summary>
    public async Task<bool> LoginAsync(LoginDto dto)
    {
        HttpResponseMessage rsp = await PostAsync("api/utilizador/login", dto);
        if (!rsp.IsSuccessStatusCode) return false;

        var token = await rsp.Content.ReadFromJsonAsync<UtilizadorTokenDto>();
        if (token is null) return false;

        await _storage.SetItemAsync("auth_token", token.Token);
        await _storage.SetItemAsync<DateTime>("token_exp", token.Expiration);
        return true;
    }

    public async Task LogoutAsync()
    {
        await _storage.RemoveItemAsync("auth_token");
        await _storage.RemoveItemAsync("token_exp");
    }
    
    public async Task<int?> GetUserIdAsync() =>
        await _storage.GetItemAsync<int?>("user_id");
    
    public async Task<DateTime?> GetTokenExpirationAsync() =>
        await _storage.GetItemAsync<DateTime?>("token_exp");


}