using BlazorState;
using MediatR;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class TodoListCreatedNotificationActionHandler : ActionHandler<TodoListCreatedNotificationAction>
    {
        private readonly ILogger<TodoListCreatedNotificationActionHandler> _logger;

        public TodoListCreatedNotificationActionHandler(IStore store, ILogger<TodoListCreatedNotificationActionHandler> logger) : base(store)
        {
            _logger = logger;
        }

        public override Task<Unit> Handle(TodoListCreatedNotificationAction notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UI: CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
            var todoListState = Store.GetState<TodoListState>();

            todoListState._todoLists.Add(notification.Item.Id, notification.Item);

            return Unit.Task;
        }
    }
}