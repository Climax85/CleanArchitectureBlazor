using Application.Shared.Common.Models;
using Application.Shared.TodoItems.Events;
using Application.Shared.TodoLists.Queries.GetTodos;
using AutoMapper;
using CleanArchitecture.Application.Common.Hubs;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler : INotificationHandler<DomainEventNotification<TodoItemCreatedEvent>>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;
    private readonly IMapper _mapper;
    private readonly NotificationHub _notificationHub;

    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger, NotificationHub notificationHub,
        IMapper mapper)
    {
        _logger = logger;
        _notificationHub = notificationHub;
        _mapper = mapper;
    }

    public async Task Handle(DomainEventNotification<TodoItemCreatedEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        await _notificationHub.SendNotification(
            new TodoItemCreatedNotification(_mapper.Map<TodoItemDto>(domainEvent.Item)));
    }
}
