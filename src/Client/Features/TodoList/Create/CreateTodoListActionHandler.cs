using BlazorState;
using BlazorUI.Services;
using MediatR;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class CreateTodoListActionHandler : ActionHandler<CreateTodoListAction>
    {
        private TodoListsClient _client;

        public CreateTodoListActionHandler(IStore aStore, TodoListsClient client) : base(aStore)
        {
            _client = client;
        }

        public override async Task<Unit> Handle(CreateTodoListAction aAction, CancellationToken aCancellationToken)
        {
            await _client.CreateAsync(aAction.CreateTodoListCommand, aCancellationToken);

            return await Unit.Task;
        }
    }
}