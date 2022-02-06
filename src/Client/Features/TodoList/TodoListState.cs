using Application.Shared.TodoLists.Queries.GetTodos;
using BlazorState;

namespace Client.Features.TodoList;

internal partial class TodoListState : State<TodoListState>
{
    private Dictionary<int, TodoListDto> _todoLists;

    public TodoListState()
    {
        _todoLists = new Dictionary<int, TodoListDto>();
    }

    public TodoListDto CurrentTodoList { get; private set; }

    public IReadOnlyList<TodoListDto> TodoListsAsList => _todoLists.Values.ToList();
    public IReadOnlyDictionary<int, TodoListDto> TodoLists => _todoLists;

    public sealed override void Initialize() => _todoLists = new Dictionary<int, TodoListDto>();
}