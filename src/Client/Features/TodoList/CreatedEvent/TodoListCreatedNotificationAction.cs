using Application.Shared.Common.Mappings;
using Application.Shared.TodoLists.Events;
using Application.Shared.TodoLists.Queries.GetTodos;
using AutoMapper;
using BlazorState;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class TodoListCreatedNotificationAction : IAction, IMapFrom<TodoListCreatedNotification>
    {
        public TodoListCreatedNotificationAction(TodoListDto item)
        {
            Item = item;
        }

        public TodoListCreatedNotificationAction()
        {
        }

        public TodoListDto Item { get; private set; }

        public void Mapping(Profile profile)
        {
            profile
                .CreateMap<TodoListCreatedNotification, TodoListCreatedNotificationAction>();
        }
    }
}