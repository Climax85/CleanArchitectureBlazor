using Application.Shared.Common.Mappings;
using Application.Shared.TodoItems.Events;
using Application.Shared.TodoLists.Queries.GetTodos;
using BlazorState;

namespace Client.Features.TodoItem;

public partial class TodoItemState
{
    public class TodoItemCreatedNotificationAction : IAction, IMapFrom<TodoItemCreatedNotification>
    {
        public TodoItemCreatedNotificationAction(TodoItemDto item)
        {
            Item = item;
        }

        public TodoItemCreatedNotificationAction()
        {
        }

        public TodoItemDto Item { get; }
    }
}