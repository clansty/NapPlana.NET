using System.Text.Json;
using System.Text.Json.Serialization;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Message;

namespace NapPlana.Core.Utilities;

/// <summary>
/// 用于Json解析消息段
/// </summary>
public class MessageBaseConverter : JsonConverter<MessageBase>
{
    public override MessageBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject for MessageBase");
        }
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        if (!root.TryGetProperty("type", out var typeProp))
        {
            return new MessageBase();
        }
        var typeStr = typeProp.GetString() ?? string.Empty;
        var dataElem = root.TryGetProperty("data", out var d) ? d : default;

        MessageDataType msgType;
        if (!Enum.TryParse<MessageDataType>(typeStr, true, out msgType))
        {
            msgType = MessageDataType.None;
        }

        var (messageObj, dataType) = ResolveTypes(msgType);

        if (dataElem.ValueKind != JsonValueKind.Undefined && dataType != typeof(MessageDataBase))
        {
            try
            {
                var dataObj = (MessageDataBase?)JsonSerializer.Deserialize(dataElem.GetRawText(), dataType, options);
                if (dataObj != null)
                {
                    messageObj.MessageData = dataObj;
                }
            }
            catch
            {
                // leave default data object if deserialization fails
            }
        }
        messageObj.MessageType = msgType;
        return messageObj;
    }

    public override void Write(Utf8JsonWriter writer, MessageBase value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("type", value.MessageType.ToString().ToLowerInvariant());
        writer.WritePropertyName("data");
        JsonSerializer.Serialize(writer, value.MessageData, value.MessageData.GetType(), options);
        writer.WriteEndObject();
    }

    private static (MessageBase messageObj, Type dataType) ResolveTypes(MessageDataType type)
    {
        switch (type)
        {
            case MessageDataType.Text: return (new TextMessage(), typeof(TextMessageData));
            case MessageDataType.Image: return (new ImageMessage(), typeof(ImageMessageData));
            case MessageDataType.At: return (new AtMessage(), typeof(AtMessageData));
            case MessageDataType.Record: return (new RecordMessage(), typeof(RecordMessageData));
            case MessageDataType.Video: return (new VideoMessage(), typeof(VideoMessageData));
            case MessageDataType.Rps: return (new RpsMessage(), typeof(RpsMessageData));
            case MessageDataType.Contact: return (new ContactMessage(), typeof(ContactMessageData));
            case MessageDataType.Dice: return (new DiceMessage(), typeof(DiceMessageData));
            case MessageDataType.Music: return (new MusicMessage(), typeof(MusicMessageData));
            case MessageDataType.Reply: return (new ReplyMessage(), typeof(ReplyMessageData));
            case MessageDataType.Forward: return (new ForwardMessage(), typeof(ForwardMessageData));
            case MessageDataType.Node: return (new NodeMessage(), typeof(NodeMessageData));
            case MessageDataType.Json: return (new JsonMessage(), typeof(JsonMessageData));
            case MessageDataType.MFace: return (new MFaceMessage(), typeof(MFaceMessageData));
            case MessageDataType.File: return (new FileMessage(), typeof(FileMessageData));
            default: return (new MessageBase(), typeof(MessageDataBase));
        }
    }
}
