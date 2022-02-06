using Application.Shared.TodoLists.Commands.CreateTodoList;
using BlazorState;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class CreateTodoListAction : IAction
    {
        public CreateTodoListCommand CreateTodoListCommand { get; set; }
    }
}