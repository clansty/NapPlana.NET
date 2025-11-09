using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Data.Message;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Message;

public class MessageSentEventParser: MessageEventParser
{
    public override void ParseEvent(string jsonEventData)
    {
        var ev = JsonSerializer.Deserialize<MessageSentEvent>(jsonEventData);
        if (ev == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot消息发送事件格式");
        }
        //不打日志了，从事件接收吧
        switch (ev.MessageType)
        {
            case MessageType.Private:
                var privateMsg = new PrivateMessageSentEventParser();
                privateMsg.ParseEvent(jsonEventData);
                return;
            case MessageType.Group:
                BotEventHandler.MessageSentGroup(ev);
                break;
        }
        
    }
}

public class PrivateMessageSentEventParser : MessageSentEventParser
{
    public override void ParseEvent(string jsonEventData)
    {
        var ev = JsonSerializer.Deserialize<PrivateMessageSentEvent>(jsonEventData);
        if (ev == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot私聊消息发送事件格式");
        }
        BotEventHandler.MessageSentPrivate(ev);
        
        switch (ev.SubType)
        {
            //群临时会话，似乎是这样
            case PrivateMessageSubType.Group:
                if (ev.TempFlag.HasValue)
                {
                    BotEventHandler.MessageSentTemporary(ev);
                }
                break;
            case PrivateMessageSubType.Friend:
                BotEventHandler.MessageSentPrivateFriend(ev);
                break;
        }
    }
}
