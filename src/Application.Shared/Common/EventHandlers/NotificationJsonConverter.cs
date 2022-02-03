using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Shared.Common.EventHandlers;

public class NotificationJsonConverter : JsonConverter<SerializedNotification>
{
    private readonly IEnumerable<Type> _types;

    public NotificationJsonConverter()
    {
        var type = typeof(SerializedNotification);
        _types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
            .ToList();
        Console.WriteLine(string.Join("\n", _types));
    }

    public override SerializedNotification Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Console.WriteLine("SerializedNotification.Read");
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }
        Console.WriteLine("SerializedNotification.Read2");

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {   
            if (!jsonDocument.RootElement.TryGetProperty("notificationType", out var typeProperty))
            {
                Console.WriteLine("SerializedNotification.notificationType not found");
                throw new JsonException();
            }
            var type = _types.FirstOrDefault(x => x.Name == typeProperty.GetString());
            if (type == null)
            {
                throw new JsonException();
            }

            Console.WriteLine($"SerializedNotification.type: {type}");

            var jsonObject = jsonDocument.RootElement.GetRawText();
            Console.WriteLine($"SerializedNotification.jsonObject: {jsonObject}");
            try
            {
                var result = JsonSerializer.Deserialize(jsonObject, type, options);
                Console.WriteLine($"SerializedNotification.result: {result.GetType().FullName}");
                return (SerializedNotification)result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, SerializedNotification value, JsonSerializerOptions options)
    {
        Console.WriteLine("SerializedNotification.Write");
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}