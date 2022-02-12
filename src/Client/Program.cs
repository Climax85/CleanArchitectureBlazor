using System.Reflection;
using Application.Shared.Common.EventHandlers;
using Application.Shared.Common.Mappings;
using Application.Shared.TodoItems.Events;
using Application.Shared.TodoLists.Events;
using AutoMapper;
using BlazorState;
using BlazorUI.Services;
using Client.Features.TodoList;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor.Services;

namespace Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // TODO: MappingProfile nutzen
        builder.Services.AddAutoMapper(expression => expression.CreateProfile("Test", profileExpression =>
        {
            profileExpression.CreateMap<TodoItemCreatedNotification, TodoListState.TodoItemCreatedNotificationAction>();
            profileExpression.CreateMap<TodoListCreatedNotification, TodoListState.TodoListCreatedNotificationAction>();
        }));

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddTransient(factory =>
            new TodoListsClient("https://localhost:5001", factory.GetRequiredService<HttpClient>()));
        builder.Services.AddTransient(factory =>
            new TodoItemsClient("https://localhost:5001", factory.GetRequiredService<HttpClient>()));

        builder.Services.AddMudServices();

        builder.Services.AddBlazorState
        (
            aOptions => aOptions.Assemblies = new[] { Assembly.GetExecutingAssembly() }
        );

        WebAssemblyHost app = builder.Build();

        HubConnection hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:5001/notifications")
            .AddJsonProtocol(o => o.PayloadSerializerOptions.Converters.Add(new NotificationJsonConverter()))
            .Build();

        hubConnection.On<SerializedNotification>("Notification", async notification =>
        {
            IMediator mediator = app.Services.GetRequiredService<IMediator>();
            IMapper mapper = app.Services.GetRequiredService<IMapper>();

            foreach (Type type in GetMatchingActionTypes(notification))
            {
                try
                {
                    var matchingAction = mapper.Map(notification, notification.GetType(), type);
                    await mediator.Send(matchingAction).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        });

        await hubConnection.StartAsync();

        await app.RunAsync();
    }

    public static ICollection<Type> GetMatchingActionTypes(SerializedNotification notification)
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type
                .GetInterfaces()
                .Where(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                .Any(y => y.GetGenericArguments()[0] == notification.GetType()))
            .ToList();
    }
}