using Frontend.DTOs.Common;
using System.Net.Http.Json;
using Frontend.DTOs.User;

namespace Frontend.Services;

public class UserService : ApiService
{
    public UserService(IHttpClientFactory f) : base(f) { }

    public async Task<IEnumerable<UserDetailsDto>> GetAllAsync(UserFilterDto? filter = null)
    {
        var result = await GetAsync<IEnumerable<UserDetailsDto>>("api/utilizadores", filter);
        return result ?? throw new Exception("Erro ao obter a lista de utilizadores.");
    }

    public async Task<PagedResult<UserDetailsDto>> GetPagedAsync(UserFilterDto? filter = null, int page = 1, int pageSize = 10)
    {
        var query = new
        {
            page,
            pageSize,
            filter?.Nome,
            filter?.Username,
            filter?.SuperUser,
            filter?.IsDeleted
        };

        var result = await GetAsync<PagedResult<UserDetailsDto>>("api/utilizadores/paged", query);
        return result ?? throw new Exception("Erro ao obter utilizadores paginados.");
    }

    public async Task<UserDetailsExtendedDto> GetByIdAsync(int id)
    {
        var result = await GetAsync<UserDetailsExtendedDto>($"api/utilizadores/{id}");
        return result ?? throw new Exception($"Utilizador com ID {id} não encontrado.");
    }

    public async Task<UserDetailsDto> CreateAsync(UserCreateDto dto)
    {
        var response = await PostAsync("api/utilizadores", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao criar o utilizador.");

        var data = await response.Content.ReadFromJsonAsync<UserDetailsDto>();
        return data ?? throw new Exception("Resposta inválida ao criar o utilizador.");
    }

    public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
    {
        var response = await PutAsync($"api/utilizadores/{id}", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao atualizar o utilizador com ID {id}.");
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await DeleteRequestAsync($"api/utilizadores/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao eliminar o utilizador com ID {id}.");
        return true;
    }
    
    public async Task<UserTokenDto> LoginAsync(UserLoginDto dto)
    {
        var response = await PostAsync("api/utilizadores/login", dto);
    
        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao fazer login: {erro}");
        }

        var token = await response.Content.ReadFromJsonAsync<UserTokenDto>();
        return token ?? throw new Exception("Resposta inválida ao autenticar.");
    }
}
