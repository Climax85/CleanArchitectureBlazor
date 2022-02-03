using Application.Shared.Common.EventHandlers;
using Application.Shared.TodoLists.Queries.GetTodos;

namespace Application.Shared.TodoItems.Events;

public class TodoItemCreatedNotification : SerializedNotification
{
    public TodoItemCreatedNotification(TodoItemDto item)
    {
        Item = item;
    }

    public TodoItemDto Item { get; }
}