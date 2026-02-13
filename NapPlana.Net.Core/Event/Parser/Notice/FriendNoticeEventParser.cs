using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Notice;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Notice;

/// <summary>
/// 好友相关通知事件解析器，处理好友添加、好友消息撤回等事件。
/// </summary>
public class FriendNoticeEventParser(IEventHandler handler) : NoticeEventParser(handler)
{
    /// <summary>
    /// 解析好友通知事件并触发对应内部事件，非好友类型将被忽略。
    /// </summary>
    /// <param name="botEvent">原始 OneBot 好友通知事件 JSON 字符串</param>
    /// <exception cref="UnSupportFeatureException">反序列化失败或事件类型不匹配</exception>
    public override void ParseEvent(string botEvent)
    {
        var baseEvent = JsonSerializer.Deserialize<NoticeEventBase>(botEvent);
        if (baseEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot好友通知事件格式");
        }

        switch (baseEvent.NoticeType)
        {
            case NoticeType.FriendAdd:
            {
                var addEvent = JsonSerializer.Deserialize<FriendAddNoticeEvent>(botEvent);
                if (addEvent == null)
                    throw new UnSupportFeatureException("好友添加事件反序列化失败");
                handler.FriendAddNoticeReceived(addEvent);
                break;
            }
            case NoticeType.FriendRecall:
            {
                var recallEvent = JsonSerializer.Deserialize<FriendRecallNoticeEvent>(botEvent);
                if (recallEvent == null)
                    throw new UnSupportFeatureException("好友消息撤回事件反序列化失败");
                handler.FriendRecallNoticeReceived(recallEvent);
                break;
            }
            default:
                // 不是好友相关，忽略
                break;
        }
    }
}
