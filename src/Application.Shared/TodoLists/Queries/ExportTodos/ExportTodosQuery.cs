using MediatR;

namespace Application.Shared.TodoLists.Queries.ExportTodos;

public class ExportTodosQuery : IRequest<ExportTodosVm>
{
    public int ListId { get; set; }
}