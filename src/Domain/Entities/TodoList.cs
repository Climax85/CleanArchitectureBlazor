namespace CleanArchitecture.Domain.Entities;

public class TodoList : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}
