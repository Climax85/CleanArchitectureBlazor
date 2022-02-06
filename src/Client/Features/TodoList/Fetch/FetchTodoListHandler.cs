using Application.Shared.TodoLists.Queries.GetTodos;
using BlazorState;
using BlazorUI.Services;
using MediatR;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class FetchTodoListHandler : ActionHandler<FetchTodoListAction>
    {
        private readonly TodoListsClient _client;

        public FetchTodoListHandler(IStore aStore, TodoListsClient client) : base(aStore)
        {
            _client = client;
        }

        public override async Task<Unit> Handle(FetchTodoListAction aAction, CancellationToken aCancellationToken)
        {
            TodoListState? todoListState = Store.GetState<TodoListState>();

            TodosVm? todosVm = await _client.GetAsync(aCancellationToken);

            todoListState._todoLists = todosVm.Lists.ToDictionary(todoList => todoList.Id);
            todoListState.CurrentTodoList = todosVm.Lists.First();

            return await Unit.Task;
        }
    }
}