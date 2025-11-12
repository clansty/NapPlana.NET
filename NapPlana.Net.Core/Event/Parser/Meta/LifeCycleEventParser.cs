using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Meta;
using NapPlana.Core.Event.Handler;

namespace NapPlana.Core.Event.Parser.Meta;

/// <summary>
/// 生命周期事件解析器，处理机器人连接/启用/停用等生命周期相关 Meta 事件。
/// </summary>
public class LifeCycleEventParser: MetaEventParser
{
    /// <summary>
    /// 解析生命周期事件并分发到事件处理器，依据 <c>sub_type</c> 触发对应内部事件与日志。
    /// </summary>
    /// <param name="botEvent">原始 OneBot 生命周期事件 JSON 字符串</param>
    /// <exception cref="Exception">反序列化失败或非生命周期事件格式</exception>
    public override void ParseEvent(string botEvent)
    {
        var lifeCycleEvent = JsonSerializer.Deserialize<LifeCycleEvent>(botEvent);
        if (lifeCycleEvent == null)
        {
            throw new Exception("无法解析该事件数据，可能不是OneBot生命周期事件格式");
        }
        BotEventHandler.BotLifeCycle(lifeCycleEvent);
        switch (lifeCycleEvent.SubType)
        {
            case LifeCycleSubType.Connect:
                BotEventHandler.LogReceived(LogLevel.Info,$"机器人已连接到NapCat服务器");
                BotEventHandler.BotConnected();
                break;
            case LifeCycleSubType.Disable:
                BotEventHandler.LogReceived(LogLevel.Warning,$"机器人已与NapCat服务器断开连接");
                break;
            case LifeCycleSubType.Enable:
                BotEventHandler.LogReceived(LogLevel.Debug,$"机器人已启用");
                break;
        }
    }
}