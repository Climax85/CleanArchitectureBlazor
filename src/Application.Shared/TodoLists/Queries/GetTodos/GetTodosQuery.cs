using MediatR;

namespace Application.Shared.TodoLists.Queries.GetTodos;

public class GetTodosQuery : IRequest<TodosVm>
{
}