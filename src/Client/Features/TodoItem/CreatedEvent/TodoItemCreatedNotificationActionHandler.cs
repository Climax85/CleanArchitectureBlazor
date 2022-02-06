using BlazorState;
using MediatR;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class TodoItemCreatedNotificationActionHandler : ActionHandler<TodoItemCreatedNotificationAction>
    {
        private readonly ILogger<TodoItemCreatedNotificationActionHandler> _logger;

        public TodoItemCreatedNotificationActionHandler(IStore store, ILogger<TodoItemCreatedNotificationActionHandler> logger) : base(store)
        {
            _logger = logger;
        }

        public override Task<Unit> Handle(TodoItemCreatedNotificationAction notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UI: CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
            var todoListState = Store.GetState<TodoListState>();

            todoListState.TodoLists[notification.Item.ListId].Items.Add(notification.Item);

            return Unit.Task;
        }
    }
}