using BlazorState;
using BlazorUI.Services;
using MediatR;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class CreateTodoItemActionHandler : ActionHandler<CreateTodoItemAction>
    {
        private TodoItemsClient _client;

        public CreateTodoItemActionHandler(IStore aStore, TodoItemsClient client) : base(aStore)
        {
            _client = client;
        }

        public override async Task<Unit> Handle(CreateTodoItemAction aAction, CancellationToken aCancellationToken)
        {
            await _client.CreateAsync(aAction.CreateTodoItemCommand, aCancellationToken);

            return await Unit.Task;
        }
    }
}