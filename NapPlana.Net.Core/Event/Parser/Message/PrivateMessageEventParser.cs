using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Message;

/// <summary>
/// 私信事件解析器
/// </summary>
public class PrivateMessageEventParser: MessageEventParser
{
    /// <summary>
    /// 私信事件解析器
    /// </summary>
    /// <param name="jsonEventData">数据</param>
    /// <exception cref="UnSupportFeatureException">不是对应事件</exception>
    public override void ParseEvent(string jsonEventData)
    {
        var ev = JsonSerializer.Deserialize<PrivateMessageEvent>(jsonEventData);
        if (ev == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot私聊消息事件格式");
        }
        BotEventHandler.PrivateMessageReceived(ev);
    }
}

