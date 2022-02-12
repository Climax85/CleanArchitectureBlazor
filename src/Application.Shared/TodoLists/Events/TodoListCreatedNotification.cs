using Application.Shared.Common.EventHandlers;
using Application.Shared.TodoLists.Queries.GetTodos;

namespace Application.Shared.TodoLists.Events;

public class TodoListCreatedNotification : SerializedNotification
{
    public TodoListCreatedNotification(TodoListDto item)
    {
        Item = item;
    }

    public TodoListDto Item { get; }
}