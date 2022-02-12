namespace CleanArchitecture.Domain.Events;

public class TodoListCreatedEvent : DomainEvent
{
    public TodoListCreatedEvent(TodoList item)
    {
        Item = item;
    }

    public TodoList Item { get; }
}