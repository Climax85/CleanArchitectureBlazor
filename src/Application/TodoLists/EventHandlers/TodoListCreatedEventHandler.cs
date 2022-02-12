using Application.Shared.Common.Models;
using Application.Shared.TodoLists.Events;
using Application.Shared.TodoLists.Queries.GetTodos;
using AutoMapper;
using CleanArchitecture.Application.Common.Hubs;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoLists.EventHandlers;

public class TodoListCreatedEventHandler : INotificationHandler<DomainEventNotification<TodoListCreatedEvent>>
{
    private readonly ILogger<TodoListCreatedEventHandler> _logger;
    private readonly IMapper _mapper;
    private readonly NotificationHub _notificationHub;

    public TodoListCreatedEventHandler(ILogger<TodoListCreatedEventHandler> logger, NotificationHub notificationHub,
        IMapper mapper)
    {
        _logger = logger;
        _notificationHub = notificationHub;
        _mapper = mapper;
    }

    public async Task Handle(DomainEventNotification<TodoListCreatedEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        await _notificationHub.SendNotification(
            new TodoListCreatedNotification(_mapper.Map<TodoListDto>(domainEvent.Item)));
    }
}