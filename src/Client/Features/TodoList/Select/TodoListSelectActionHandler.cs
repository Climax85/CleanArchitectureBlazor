using BlazorState;
using MediatR;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class TodoListSelectActionHandler : ActionHandler<TodoListSelectAction>
    {
        public TodoListSelectActionHandler(IStore aStore) : base(aStore)
        {
        }

        public override Task<Unit> Handle(TodoListSelectAction aAction, CancellationToken aCancellationToken)
        {
            var todoListState = Store.GetState<TodoListState>();
            todoListState.CurrentTodoList = todoListState.TodoLists[aAction.Id];

            return Unit.Task;
        }
    }
}