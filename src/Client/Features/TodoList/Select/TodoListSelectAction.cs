using BlazorState;

namespace Client.Features.TodoList;

internal partial class TodoListState
{
    public class TodoListSelectAction : IAction
    {
        public int Id { get; private set; }

        public TodoListSelectAction(int id)
        {
            Id = id;
        }

        private TodoListSelectAction()
        {
        }
    }
}