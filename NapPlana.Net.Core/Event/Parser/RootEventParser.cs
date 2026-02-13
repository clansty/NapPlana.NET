using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Event.Parser.Meta;
using NapPlana.Core.Event.Parser.Notice;
using NapPlana.Core.Exceptions;
using NapPlana.Core.Event.Parser.Message;

namespace NapPlana.Core.Event.Parser;

/// <summary>
/// 基础事件解析器：识别 OneBot 顶层 <c>post_type</c> 并分发到具体子解析器（Meta / Message / MessageSent / Notice）。
/// </summary>
public class RootEventParser(IEventHandler handler): IEventParser
{
    /// <summary>
    /// 解析顶层事件 JSON，判断 <c>post_type</c> 并路由到对应解析器；不支持或 None 类型直接返回。
    /// </summary>
    /// <param name="jsonEventData">原始 OneBot 事件 JSON 字符串</param>
    /// <exception cref="UnSupportFeatureException">反序列化失败或非 OneBot 顶层事件格式</exception>
    public virtual void ParseEvent(string jsonEventData)
    {
        var oneBotEvent = JsonSerializer.Deserialize<OneBotEvent>(jsonEventData);
        if (oneBotEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot事件格式");
        }

        switch (oneBotEvent.PostType)
        {
            case EventType.None:
                return;
            case EventType.Meta:
                // 处理Meta事件
                var metaEventParser = new MetaEventParser(handler);
                metaEventParser.ParseEvent(jsonEventData);
                break;
            case EventType.Message:
                // 处理Message事件
                var messageParser = new MessageEventParser(handler);
                messageParser.ParseEvent(jsonEventData);
                break;
            case EventType.MessageSent:
                // 处理MessageSent事件
                var messageSentParser = new MessageEventParser(handler);
                messageSentParser.ParseEvent(jsonEventData);
                break;
            case EventType.Notice:
                // 处理Notice事件
                var noticeEventParser = new NoticeEventParser(handler);
                noticeEventParser.ParseEvent(jsonEventData);
                break;
            case EventType.Request:
                // 处理Request事件
                break;
        }
    }
}