using System.Text.Json;
using System.Text.Json.Serialization;
using NapPlana.Core.Data.Message;
using NapPlana.Core.Data.Event.Message;

namespace NapPlana.Core.Utilities;

/// <summary>
/// message字段既可能是CQ Message也可能是消息段数组
/// 统一转成List
/// </summary>
public class MessageListConverter : JsonConverter<List<MessageBase>>
{
    public override List<MessageBase>? Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.StartArray:
                var list = new List<MessageBase>();
                using (var doc = JsonDocument.ParseValue(ref reader))
                {
                    foreach (var elem in doc.RootElement.EnumerateArray())
                    {
                        try
                        {
                            var mb = JsonSerializer.Deserialize<MessageBase>(elem.GetRawText(), options);
                            if (mb != null)
                            {
                                list.Add(mb);
                            }
                        }
                        catch
                        {
                            //ignore
                        }
                    }
                }
                return list;
            case JsonTokenType.String:
                var textVal = reader.GetString() ?? string.Empty;
                return new List<MessageBase>
                {
                    new TextMessage
                    {
                        MessageData = new TextMessageData { Text = textVal }
                    }
                };
            case JsonTokenType.Null:
            default:
                return new List<MessageBase>();
        }
    }

    public override void Write(Utf8JsonWriter writer, List<MessageBase> value, JsonSerializerOptions options)
    {
        //始终写成数组
        writer.WriteStartArray();
        foreach (var segment in value)
        {
            JsonSerializer.Serialize(writer, segment, options);
        }
        writer.WriteEndArray();
    }
}

