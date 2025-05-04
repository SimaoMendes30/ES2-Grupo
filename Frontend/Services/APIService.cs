using System.Net.Http.Json;
using System.Reflection;
using System.Web;

namespace Frontend.Services;

public abstract class ApiService
{
    protected readonly HttpClient _http;

    protected ApiService(IHttpClientFactory factory) =>
        _http = factory.CreateClient("Backend");

    protected async Task<T?> GetAsync<T>(string url, object? queryParams = null)
    {
        try
        {
            var fullUrl = queryParams is null ? url : $"{url}{ToQueryString(queryParams)}";
            return await _http.GetFromJsonAsync<T>(fullUrl);
        }
        catch
        {
            return default;
        }
    }

    protected async Task<HttpResponseMessage> PostAsync<T>(string url, T payload)
    {
        try
        {
            return await _http.PostAsJsonAsync(url, payload);
        }
        catch (Exception ex)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
            {
                ReasonPhrase = ex.Message
            };
        }
    }

    protected async Task<HttpResponseMessage> PutAsync<T>(string url, T payload)
    {
        try
        {
            return await _http.PutAsJsonAsync(url, payload);
        }
        catch (Exception ex)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
            {
                ReasonPhrase = ex.Message
            };
        }
    }

    protected async Task<HttpResponseMessage> DeleteRequestAsync(string url)
    {
        try
        {
            return await _http.DeleteAsync(url);
        }
        catch (Exception ex)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
            {
                ReasonPhrase = ex.Message
            };
        }
    }

    // Transforma propriedades públicas não-nulas em query string
    private static string ToQueryString(object obj)
    {
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.GetValue(obj) is not null);

        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj)?.ToString();
            if (!string.IsNullOrWhiteSpace(value))
                query[prop.Name] = value;
        }

        var queryString = query.ToString();
        return string.IsNullOrWhiteSpace(queryString) ? "" : "?" + queryString;
    }
}