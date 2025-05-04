using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using Frontend.DTOs.User;
using MudBlazor;

namespace Frontend.Services
{
    public sealed class AuthService : ApiService
    {
        private readonly ILocalStorageService _storage;
        private readonly ISnackbar _snackbar;

        public AuthService(IHttpClientFactory factory, ILocalStorageService storage, ISnackbar snackbar)
            : base(factory)
        {
            _storage = storage;
            _snackbar = snackbar;
        }

        public async Task<bool> LoginAsync(UserLoginDto dto)
        {
            try
            {
                var rsp = await PostAsync("api/utilizadores/login", dto);
                if (!rsp.IsSuccessStatusCode)
                {
                    _snackbar.Add("Falha ao autenticar. Verifica as credenciais.", Severity.Error);
                    return false;
                }

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var token = await rsp.Content.ReadFromJsonAsync<UserTokenDto>(options);

                if (token is null)
                {
                    _snackbar.Add("Erro a processar o token.", Severity.Error);
                    return false;
                }

                await _storage.SetItemAsync("auth_token", token.Token);
                await _storage.SetItemAsync("token_exp", token.Expiration);

                if (token.UserId.HasValue)
                {
                    await _storage.SetItemAsync("user_id", token.UserId.Value);
                    _snackbar.Add($"Login com sucesso! user_id = {token.UserId}", Severity.Success);
                }
                else
                {
                    _snackbar.Add("Aviso: Token recebido mas sem UserId!", Severity.Warning);
                }

                return true;
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Erro no login: {ex.Message}", Severity.Error);
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _storage.RemoveItemAsync("auth_token");
            await _storage.RemoveItemAsync("token_exp");
            await _storage.RemoveItemAsync("user_id");
            _snackbar.Add("Sessão terminada com sucesso.", Severity.Info);
        }

        public async Task<int?> GetUserIdAsync() =>
            await _storage.GetItemAsync<int?>("user_id");

        public async Task<DateTime?> GetTokenExpirationAsync() =>
            await _storage.GetItemAsync<DateTime?>("token_exp");
    }
}
