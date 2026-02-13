using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Meta;
using NapPlana.Core.Event.Handler;

namespace NapPlana.Core.Event.Parser.Meta;

/// <summary>
/// 心跳事件解析器，负责解析 OneBot 心跳包并触发内部心跳事件。
/// </summary>
public class HeartBeatEventParser(IEventHandler handler) :MetaEventParser(handler)
{
    /// <summary>
    /// 解析心跳事件 JSON 并分发到事件处理器。
    /// </summary>
    /// <param name="botEvent">原始 OneBot 心跳事件 JSON 字符串</param>
    /// <exception cref="Exception">反序列化失败或不是心跳事件格式</exception>
    public override void ParseEvent(string botEvent)
    {
        var heartBeatEvent = JsonSerializer.Deserialize<HeartBeatEvent>(botEvent);
        if (heartBeatEvent == null)
        {
            throw new Exception("无法解析该事件数据，可能不是OneBot心跳包事件格式");
        }
        
        handler.LogReceived(LogLevel.Debug,$"收到心跳包，时间戳{heartBeatEvent.TimeStamp}");
        handler.BotHeartbeat(heartBeatEvent);
    }
}