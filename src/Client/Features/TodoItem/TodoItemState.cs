using Application.Shared.TodoLists.Queries.GetTodos;
using BlazorState;

namespace Client.Features.TodoItem;

public partial class TodoItemState : State<TodoItemState>
{
    public TodoItemState()
    {
        TodoItems = new List<TodoItemDto>();
    }

    public ICollection<TodoItemDto> TodoItems { get; private set; }
    public override void Initialize() => TodoItems = new List<TodoItemDto>();
}