using System.Net.Http.Json;
using Frontend.DTOs;

namespace Frontend.Services;

public class ApiService
{
    protected readonly HttpClient _http;

    public ApiService(IHttpClientFactory factory) => _http = factory.CreateClient("Backend");

    protected async Task<T?> GetAsync<T>(string url) =>
        await _http.GetFromJsonAsync<T>(url);

    protected async Task<HttpResponseMessage> PostAsync<T>(string url, T payload) =>
        await _http.PostAsJsonAsync(url, payload);

    protected Task<HttpResponseMessage> PutAsync<T>(string url, T payload) =>
        _http.PutAsJsonAsync(url, payload);

    public async Task<HttpResponseMessage> DeleteAsync(string url)
    {
        return await _http.DeleteAsync(url);
    }
}