using Application.Shared.TodoItems.Commands.CreateTodoItem;
using BlazorState;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class CreateTodoItemAction : IAction
    {
        public CreateTodoItemCommand CreateTodoItemCommand { get; set; }
    }
}