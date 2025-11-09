using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Message;

public class MessageEventParser: RootEventParser
{
    public override void ParseEvent(string jsonEventData)
    {
        // 先做基础反序列化，判断是否是消息或消息发送事件
        var baseEvent = JsonSerializer.Deserialize<MessageEventBase>(jsonEventData);
        if (baseEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot消息事件格式");
        }

        if (baseEvent.PostType == EventType.MessageSent)
        {
            var sentParser = new MessageSentEventParser();
            sentParser.ParseEvent(jsonEventData);
            return;
        }

        if (baseEvent.PostType != EventType.Message)
        {
            return;
        }
        
        using var doc = JsonDocument.Parse(jsonEventData);
        if (!doc.RootElement.TryGetProperty("message_type", out var messageTypeProp))
        {
            return;
        }
        var messageType = messageTypeProp.GetString();
        switch (messageType)
        {
            case "private":
                var privateParser = new PrivateMessageEventParser();
                privateParser.ParseEvent(jsonEventData);
                break;
            case "group":
                var groupParser = new GroupMessageEventParser();
                groupParser.ParseEvent(jsonEventData);
                break;
        }
    }
}
