using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BankBlazor.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");


        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri("https://bankblazorapi-byh0frhqe7b2argt.germanywestcentral-01.azurewebsites.net/")
        });

        await builder.Build().RunAsync();
    }
}
