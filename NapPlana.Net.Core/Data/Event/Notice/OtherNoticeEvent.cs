using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

/// <summary>
/// 戳一戳通知事件。
/// </summary>
public class PokeNoticeEvent : NoticeEventBase
{
    /// <summary>
    /// 通知类型。
    /// </summary>
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;

    /// <summary>
    /// 子类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.Poke;

    /// <summary>
    /// 目标ID。
    /// </summary>
    [JsonPropertyName("target_id")]
    public long TargetId { get; set; }

    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}

/// <summary>
/// 好友戳一戳通知事件。
/// </summary>
public class FriendPokeNoticeEvent : PokeNoticeEvent
{
    /// <summary>
    /// 原始信息。
    /// </summary>
    [JsonPropertyName("raw_info")]
    public object? RawInfo { get; set; }

    /// <summary>
    /// 发送者ID。
    /// </summary>
    [JsonPropertyName("sender_id")]
    public long SenderId { get; set; }
}

/// <summary>
/// 群戳一戳通知事件。
/// </summary>
public class GroupPokeNoticeEvent : PokeNoticeEvent
{
    /// <summary>
    /// 群ID。
    /// </summary>
    [JsonPropertyName("group_id")]
    public long GroupId { get; set; }

    /// <summary>
    /// 原始信息。
    /// </summary>
    [JsonPropertyName("raw_info")]
    public object? RawInfo { get; set; }
}

/// <summary>
/// 个人资料点赞通知事件。
/// </summary>
public class ProfileLikeNoticeEvent : NoticeEventBase
{
    /// <summary>
    /// 通知类型。
    /// </summary>
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;

    /// <summary>
    /// 子类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.ProfileLike;

    /// <summary>
    /// 操作者ID。
    /// </summary>
    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }

    /// <summary>
    /// 操作者昵称。
    /// </summary>
    [JsonPropertyName("operator_nick")]
    public string OperatorNick { get; set; } = string.Empty;

    /// <summary>
    /// 点赞次数。
    /// </summary>
    [JsonPropertyName("times")]
    public int Times { get; set; }
    
}

/// <summary>
/// 输入状态通知事件。
/// </summary>
public class InputStatusNoticeEvent : NoticeEventBase
{
    /// <summary>
    /// 通知类型。
    /// </summary>
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;

    /// <summary>
    /// 子类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.InputStatus;

    /// <summary>
    /// 状态文本。
    /// </summary>
    [JsonPropertyName("status_text")]
    public string StatusText { get; set; } = string.Empty;

    /// <summary>
    /// 输入事件类型。
    /// </summary>
    [JsonPropertyName("event_type")]
    public int InputEventType { get; set; }

    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// 群ID。
    /// </summary>
    [JsonPropertyName("group_id")]
    public long? GroupId { get; set; }
}

/// <summary>
/// 机器人离线事件。
/// </summary>
public class BotOfflineEvent : NoticeEventBase
{
    /// <summary>
    /// 通知类型。
    /// </summary>
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.BotOffline;

    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// 标签。
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// 消息。
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
