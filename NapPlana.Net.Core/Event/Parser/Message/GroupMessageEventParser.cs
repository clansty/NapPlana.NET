using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Message;

public class GroupMessageEventParser: MessageEventParser
{
    public override void ParseEvent(string jsonEventData)
    {
        var ev = JsonSerializer.Deserialize<GroupMessageEvent>(jsonEventData);
        if (ev == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot群消息事件格式");
        }
        BotEventHandler.GroupMessageReceived(ev);
    }
}
