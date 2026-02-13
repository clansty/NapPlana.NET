using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Notice;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Notice;

/// <summary>
/// 机器人离线通知事件解析器，处理机器人离线的告警型通知。
/// </summary>
public class BotOfflineNoticeEventParser(IEventHandler handler) : NoticeEventParser(handler)
{
    /// <summary>
    /// 解析机器人离线事件并记录警告日志。
    /// </summary>
    /// <param name="botEvent">原始 OneBot 机器人离线通知事件 JSON 字符串</param>
    /// <exception cref="UnSupportFeatureException">反序列化失败或格式不匹配</exception>
    public override void ParseEvent(string botEvent)
    {
        var offlineEvent = JsonSerializer.Deserialize<BotOfflineEvent>(botEvent);
        if (offlineEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是机器人离线通知事件格式");
        }
        handler.LogReceived(LogLevel.Warning, $"机器人离线: 用户 {offlineEvent.UserId} Tag {offlineEvent.Tag} 信息 {offlineEvent.Message}");
    }
}
