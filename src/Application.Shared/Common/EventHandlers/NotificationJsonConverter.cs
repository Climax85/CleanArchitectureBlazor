﻿using System.Text.Json;
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
    }

    public override SerializedNotification Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {   
            if (!jsonDocument.RootElement.TryGetProperty("notificationType", out var typeProperty))
            {
                throw new JsonException();
            }
            var type = _types.FirstOrDefault(x => x.Name == typeProperty.GetString());
            if (type == null)
            {
                throw new JsonException();
            }

            var jsonObject = jsonDocument.RootElement.GetRawText();
            try
            {
                var result = JsonSerializer.Deserialize(jsonObject, type, options);
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
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}