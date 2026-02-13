using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Message;

/// <summary>
/// 检查并解析OneBot群消息事件
/// </summary>
public class GroupMessageEventParser(IEventHandler handler) : MessageEventParser(handler)
{
    /// <summary>
    /// 解析事件是否为Group消息事件
    /// </summary>
    /// <param name="jsonEventData">数据</param>
    /// <exception cref="UnSupportFeatureException">不是Group消息事件</exception>
    public override void ParseEvent(string jsonEventData)
    {
        var ev = JsonSerializer.Deserialize<GroupMessageEvent>(jsonEventData);
        if (ev == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot群消息事件格式");
        }
        handler.GroupMessageReceived(ev);
    }
}
