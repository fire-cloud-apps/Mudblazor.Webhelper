using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudBlazorTemplates.WebHelper;
using MudBlazorTemplates.WebHelper.Helper;
using MudBlazorTemplates.WebHelper.Service;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IHttpService, HttpService>()
    .AddScoped<ILocalStorageService, LocalStorageService>();

// configure http client
builder.Services.AddScoped(sp => new HttpClient
{
    //BaseAddress = new Uri("https://gorest.co.in/")
    BaseAddress = new Uri(builder.Configuration["AuthUrl"])
});
//  "AuthUrl": "https://localhost:65501/",
//  "AuthUrl": "https://s2arj6z657.execute-api.ap-south-1.amazonaws.com/Prod",
// "AuthUrl": "https://fcauthentication.azurewebsites.net",

Console.WriteLine($"API URL : {builder.Configuration["AuthUrl"]}");

builder.Services.AddMudServices();
var host = builder.Build();

var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
await authenticationService.Initialize();

await host.RunAsync();

//await builder.Build().RunAsync();

