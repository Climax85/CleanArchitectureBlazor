using MediatR;

namespace Application.Shared.Common.EventHandlers;

public abstract class SerializedNotification : INotification
{
    public string NotificationType
    {
        get
        {
            return this.GetType().Name;
        }
        set{}
    }
}