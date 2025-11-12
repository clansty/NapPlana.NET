using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Meta;

namespace NapPlana.Core.Event.Parser.Meta;

/// <summary>
/// 元事件解析器，负责识别并分发生命周期与心跳等 Meta 事件。
/// </summary>
public class MetaEventParser: RootEventParser
{
    /// <summary>
    /// 解析 Meta 类型事件并分发到具体的生命周期/心跳解析器。
    /// </summary>
    /// <param name="botEvent">原始 OneBot 元事件 JSON 字符串</param>
    /// <exception cref="Exception">反序列化失败或非元事件格式</exception>
    public override void ParseEvent(string botEvent)
    {

        var metaEvent = JsonSerializer.Deserialize<MetaEventBase>(botEvent);
        if (metaEvent == null)
        {
            throw new Exception("无法解析该事件数据，可能不是OneBot元事件格式");
        }
        switch (metaEvent.MetaEventType)
        {
            case MetaEventType.Lifecycle:
                var lifeCycleParser = new LifeCycleEventParser();
                lifeCycleParser.ParseEvent(botEvent);
                break;
            case MetaEventType.Heartbeat:
                var heartbeatParser = new HeartBeatEventParser();
                heartbeatParser.ParseEvent(botEvent);
                break;
        }
    }
}