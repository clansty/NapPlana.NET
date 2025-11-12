using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Notice;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Notice;

/// <summary>
/// 通知事件解析器，负责识别并分发 OneBot 通知事件到更细分的解析器（好友、群、notify、离线等）。
/// </summary>
public class NoticeEventParser: RootEventParser
{
    /// <summary>
    /// 解析通知事件 JSON 并根据 <c>notice_type</c> 分发到对应的子解析器。
    /// </summary>
    /// <param name="botEvent">原始 OneBot 通知事件 JSON 字符串</param>
    /// <exception cref="UnSupportFeatureException">反序列化失败或不是通知事件格式</exception>
    public override void ParseEvent(string botEvent)
    {
        var noticeEvent = JsonSerializer.Deserialize<NoticeEventBase>(botEvent);
        if (noticeEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot通知事件格式");
        }
        
        switch (noticeEvent.NoticeType)
        {
            //好友
            case NoticeType.FriendAdd:
            case NoticeType.FriendRecall:
            {
                var friendParser = new FriendNoticeEventParser();
                friendParser.ParseEvent(botEvent);
                break;
            }
            //群
            case NoticeType.GroupRecall:
            case NoticeType.GroupIncrease:
            case NoticeType.GroupDecrease:
            case NoticeType.GroupAdmin:
            case NoticeType.GroupBan:
            case NoticeType.GroupUpload:
            case NoticeType.GroupCard:
            case NoticeType.Essence:
            case NoticeType.GroupMsgEmojiLike:
            {
                var groupParser = new GroupNoticeEventParser();
                groupParser.ParseEvent(botEvent);
                break;
            }
            //notify
            case NoticeType.Notify:
            {
                var notifyParser = new NotifyNoticeEventParser();
                notifyParser.ParseEvent(botEvent);
                break;
            }
            case NoticeType.BotOffline:
            {
                var offlineParser = new BotOfflineNoticeEventParser();
                offlineParser.ParseEvent(botEvent);
                break;
            }
            //请求占位
            case NoticeType.Group:
            case NoticeType.Friend:
            case NoticeType.None:
            default:
                break;
        }
    }
}