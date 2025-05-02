using Blazored.LocalStorage;

namespace Frontend.Services;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService _storage;
    public AuthHeaderHandler(ILocalStorageService storage) => _storage = storage;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var jwt = await _storage.GetItemAsync<string>("auth_token");
        if (!string.IsNullOrWhiteSpace(jwt))
            request.Headers.Authorization = new("Bearer", jwt);

        return await base.SendAsync(request, cancellationToken);
    }
}