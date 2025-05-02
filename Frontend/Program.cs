using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using MudBlazor.Services;
using Frontend;
using Frontend.Components.Projetos;
using Frontend.Services;
using MudBlazor;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

// Blazored.LocalStorage
        builder.Services.AddBlazoredLocalStorage();

// JWT handler
        builder.Services.AddScoped<AuthHeaderHandler>();

// HttpClient “Backend”
        builder.Services
            .AddHttpClient("Backend", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5000/"); // URL da sua API
            })
            .AddHttpMessageHandler<AuthHeaderHandler>();

// HttpClient padrão = “Backend”
        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("Backend"));

// Registo de Services
        builder.Services.AddScoped<ApiService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<UtilizadorService>();
        builder.Services.AddScoped<ProjetoService>();
        builder.Services.AddScoped<TarefaService>();
        builder.Services.AddScoped<MembroService>();

// MudBlazor
        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.VisibleStateDuration = 3000;
            config.SnackbarConfiguration.ShowCloseIcon = true;
        });

        await builder.Build().RunAsync();
    }
}