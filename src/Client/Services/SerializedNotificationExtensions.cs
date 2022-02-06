using Application.Shared.Common.EventHandlers;
using Application.Shared.Common.Mappings;

namespace Client.Services;

public static class SerializedNotificationExtensions
{
    public static ICollection<Type> GetMatchingActionTypes(this SerializedNotification notification)
    {
        return typeof(Program).Assembly.GetTypes()
            .Where(type => type
                .GetInterfaces()
                .Where(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                .Any(y => y.GetGenericArguments()[0] == notification.GetType()))
            .ToList();
    }
}