using System.Reflection;
using BlazorState;
using BlazorUI.Services;
using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient(factory =>
    new TodoListsClient("https://localhost:5001", factory.GetRequiredService<HttpClient>()));
builder.Services.AddTransient(factory =>
    new TodoItemsClient("https://localhost:5001", factory.GetRequiredService<HttpClient>()));

builder.Services.AddMudServices();

builder.Services.AddBlazorState
(
    aOptions => aOptions.Assemblies = new[] { typeof(Program).GetTypeInfo().Assembly }
);

await builder.Build().RunAsync();