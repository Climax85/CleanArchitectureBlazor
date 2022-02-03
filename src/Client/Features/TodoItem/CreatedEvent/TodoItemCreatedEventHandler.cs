using BlazorState;
using MediatR;

namespace Client.Features.TodoItem;
public partial class TodoItemState
{
    public class TodoItemCreatedNotificationActionHandler : ActionHandler<TodoItemCreatedNotificationAction>
    {
        private readonly ILogger<TodoItemCreatedNotificationActionHandler> _logger;

        public TodoItemCreatedNotificationActionHandler(IStore store, ILogger<TodoItemCreatedNotificationActionHandler> logger) : base(store)
        {
            _logger = logger;
        }

        public override Task<Unit> Handle(TodoItemState.TodoItemCreatedNotificationAction notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UI: CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
            Console.WriteLine($"UI: CleanArchitecture Domain Event: {notification.GetType().Name}");
            var todoItemState = Store.GetState<TodoItemState>();

            todoItemState.TodoItems.Add(notification.Item);

            return Unit.Task;
        }
    }
}