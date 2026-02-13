using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Notice;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Notice;

/// <summary>
/// 群相关通知事件解析器，集中处理群成员增减、管理员、禁言、文件上传、名片、精华、消息表情等多种群事件。
/// </summary>
public class GroupNoticeEventParser(IEventHandler handler) : NoticeEventParser(handler)
{
    /// <summary>
    /// 解析群通知事件并触发对应的内部事件，未匹配的类型将被忽略。
    /// </summary>
    /// <param name="botEvent">原始 OneBot 群通知事件 JSON 字符串</param>
    /// <exception cref="UnSupportFeatureException">反序列化失败或事件类型不匹配</exception>
    public override void ParseEvent(string botEvent)
    {
        var baseEvent = JsonSerializer.Deserialize<NoticeEventBase>(botEvent);
        if (baseEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot群通知事件格式");
        }
        //密集堆积一下吧，写文件多了改命名空间会炸的
        switch (baseEvent.NoticeType)
        {
            case NoticeType.GroupRecall:
            {
                var recallEvent = JsonSerializer.Deserialize<GroupRecallNoticeEvent>(botEvent);
                if (recallEvent == null) throw new UnSupportFeatureException("群消息撤回事件反序列化失败");
                handler.GroupRecallNoticeReceived(recallEvent);
                break;
            }
            case NoticeType.GroupIncrease:
            {
                var increaseEvent = JsonSerializer.Deserialize<GroupIncreaseNoticeEvent>(botEvent);
                if (increaseEvent == null) throw new UnSupportFeatureException("群成员增加事件反序列化失败");
                handler.GroupIncreaseNoticeReceived(increaseEvent);
                switch (increaseEvent.IncreaseType)
                {
                    case GroupIncreaseType.Approve:
                        handler.GroupIncreaseApproveNoticeReceived(increaseEvent);
                        break;
                    case GroupIncreaseType.Invite:
                        handler.GroupIncreaseInviteNoticeReceived(increaseEvent);
                        break;
                }
                break;
            }
            case NoticeType.GroupDecrease:
            {
                var decreaseEvent = JsonSerializer.Deserialize<GroupDecreaseNoticeEvent>(botEvent);
                if (decreaseEvent == null) throw new UnSupportFeatureException("群成员减少事件反序列化失败");
                handler.GroupDecreaseNoticeReceived(decreaseEvent);
                switch (decreaseEvent.DecreaseType)
                {
                    case GroupDecreaseType.Leave:
                        handler.GroupDecreaseLeaveNoticeReceived(decreaseEvent);
                        break;
                    case GroupDecreaseType.Kick:
                        handler.GroupDecreaseKickNoticeReceived(decreaseEvent);
                        break;
                    case GroupDecreaseType.KickMe:
                        handler.GroupDecreaseKickMeNoticeReceived(decreaseEvent);
                        break;
                }
                break;
            }
            case NoticeType.GroupAdmin:
            {
                var adminEvent = JsonSerializer.Deserialize<GroupAdminNoticeEvent>(botEvent);
                if (adminEvent == null) throw new UnSupportFeatureException("群管理员变更事件反序列化失败");
                handler.GroupAdminNoticeReceived(adminEvent);
                switch (adminEvent.AdminType)
                {
                    case GroupManagerType.Set:
                        handler.GroupAdminSetNoticeReceived(adminEvent);
                        break;
                    case GroupManagerType.Unset:
                        handler.GroupAdminUnsetNoticeReceived(adminEvent);
                        break;
                }
                break;
            }
            case NoticeType.GroupBan:
            {
                var banEvent = JsonSerializer.Deserialize<GroupBanNoticeEvent>(botEvent);
                if (banEvent == null) throw new UnSupportFeatureException("群禁言事件反序列化失败");
                handler.GroupBanNoticeReceived(banEvent);
                switch (banEvent.BanType)
                {
                    case GroupBanType.Ban:
                        handler.GroupBanSetNoticeReceived(banEvent);
                        break;
                    case GroupBanType.LiftBan:
                        handler.GroupBanLiftNoticeReceived(banEvent);
                        break;
                }
                break;
            }
            case NoticeType.GroupUpload:
            {
                var uploadEvent = JsonSerializer.Deserialize<GroupUploadNoticeEvent>(botEvent);
                if (uploadEvent == null) throw new UnSupportFeatureException("群文件上传事件反序列化失败");
                handler.LogReceived(LogLevel.Info, $"群{uploadEvent.GroupId} 文件上传: {uploadEvent.File.FileName} 大小 {uploadEvent.File.Size} 字节");
                break;
            }
            case NoticeType.GroupCard:
            {
                var cardEvent = JsonSerializer.Deserialize<GroupCardEvent>(botEvent);
                if (cardEvent == null) throw new UnSupportFeatureException("群名片变更事件反序列化失败");
                handler.GroupCardNoticeReceived(cardEvent);
                break;
            }
            case NoticeType.Essence:
            {
                var essenceEvent = JsonSerializer.Deserialize<GroupEssenceNoticeEvent>(botEvent);
                if (essenceEvent == null) throw new UnSupportFeatureException("群精华消息事件反序列化失败");
                handler.GroupEssenceNoticeReceived(essenceEvent);
                switch (essenceEvent.EssenceType)
                {
                    case GroupEssenceType.Add:
                        handler.GroupEssenceAddNoticeReceived(essenceEvent);
                        break;
                    case GroupEssenceType.Delete:
                        handler.GroupEssenceDeleteNoticeReceived(essenceEvent);
                        break;
                }
                break;
            }
            case NoticeType.GroupMsgEmojiLike:
            {
                var likeEvent = JsonSerializer.Deserialize<GroupMsgEmojiLikeNoticeEvent>(botEvent);
                if (likeEvent == null) throw new UnSupportFeatureException("群消息表情回应事件反序列化失败");
                handler.GroupMsgEmojiLikeNoticeReceived(likeEvent);
                break;
            }
            default:
                break; // 非群类型忽略
        }
    }
}
