using Application.Shared.Common.Mappings;
using Application.Shared.TodoItems.Events;
using Application.Shared.TodoLists.Queries.GetTodos;
using AutoMapper;
using BlazorState;

namespace Client.Features.TodoList;

internal partial class TodoListState
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

        public TodoItemDto Item { get; private set; }

        public void Mapping(Profile profile)
        {
            profile
                .CreateMap<TodoItemCreatedNotification, TodoItemCreatedNotificationAction>();
        }
    }
}