using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

/// <summary>
/// 群消息撤回通知事件。
/// </summary>
public class GroupRecallNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupRecall;

    /// <summary>
    /// 消息ID。
    /// </summary>
    [JsonPropertyName("message_id")] 
    public long MessageId { get; set; }
    
    /// <summary>
    /// 操作者ID。
    /// </summary>
    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
}

/// <summary>
/// 群成员增加通知事件。
/// </summary>
public class GroupIncreaseNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupIncrease;

    /// <summary>
    /// 操作者ID。
    /// </summary>
    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
    
    /// <summary>
    /// 增加类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public GroupIncreaseType IncreaseType { get; set; }
}

/// <summary>
/// 群成员减少通知事件。
/// </summary>
public class GroupDecreaseNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupDecrease;

    /// <summary>
    /// 操作者ID。
    /// </summary>
    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
    
    /// <summary>
    /// 减少类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public GroupDecreaseType DecreaseType { get; set; }
}

/// <summary>
/// 群管理员变更通知事件。
/// </summary>
public class GroupAdminNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupAdmin;
    
    /// <summary>
    /// 管理员变更类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public GroupManagerType AdminType { get; set; }
}

/// <summary>
/// 群禁言通知事件。
/// </summary>
public class GroupBanNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupBan;
    
    /// <summary>
    /// 操作者ID。
    /// </summary>
    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
    
    /// <summary>
    /// 禁言时长。
    /// </summary>
    [JsonPropertyName("duration")]
    public int Duration { get; set; }
    
    /// <summary>
    /// 禁言类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public GroupBanType BanType { get; set; }
}

/// <summary>
/// 群文件上传数据。
/// </summary>
public class GroupUploadFileData
{
    /// <summary>
    /// 文件ID。
    /// </summary>
    [JsonPropertyName("id")] 
    public string FileId { get; set; } = "";
    
    /// <summary>
    /// 总线ID。
    /// </summary>
    [JsonPropertyName("busid")]
    public long BusId { get; set; }
    
    /// <summary>
    /// 文件名。
    /// </summary>
    [JsonPropertyName("name")]
    public string FileName { get; set; } = "";
    
    /// <summary>
    /// 文件大小。
    /// </summary>
    [JsonPropertyName("size")]
    public long Size { get; set;  }
}

/// <summary>
/// 群文件上传通知事件。
/// </summary>
public class GroupUploadNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupUpload;
    
    /// <summary>
    /// 文件数据。
    /// </summary>
    [JsonPropertyName("file")]
    public GroupUploadFileData File { get; set; } = new GroupUploadFileData();
}

/// <summary>
/// 群名片变更事件。
/// </summary>
public class GroupCardEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupCard;
    
    /// <summary>
    /// 旧名片。
    /// </summary>
    [JsonPropertyName("card_old")]
    public string OldCard { get; set; } = "";
    
    /// <summary>
    /// 新名片。
    /// </summary>
    [JsonPropertyName("card_new")]
    public string NewCard { get; set; } = "";
}

/// <summary>
/// 群名称变更事件。
/// </summary>
public class GroupNameEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;
    
    /// <summary>
    /// 子类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.GroupName;
    
    /// <summary>
    /// 新名称。
    /// </summary>
    [JsonPropertyName("name_new")]
    public string NewName { get; set; } = "";
}

/// <summary>
/// 群头衔变更事件。
/// </summary>
public class GroupTitleEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;
    
    /// <summary>
    /// 子类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.Title;
    
    /// <summary>
    /// 头衔。
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";
}

/// <summary>
/// 群精华消息通知事件。
/// </summary>
public class GroupEssenceNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; } = NoticeType.Essence;
    
    /// <summary>
    /// 消息ID。
    /// </summary>
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }
    
    /// <summary>
    /// 发送者ID。
    /// </summary>
    [JsonPropertyName("sender_id")]
    public long SenderId { get; set; }
    
    /// <summary>
    /// 操作者ID。
    /// </summary>
    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
    
    /// <summary>
    /// 精华类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public GroupEssenceType EssenceType { get; set; }
}

/// <summary>
/// 消息表情点赞。
/// </summary>
public class MsgEmojiLike
{
    /// <summary>
    /// 表情ID。
    /// </summary>
    [JsonPropertyName("emoji_id")]
    public string EmojiId { get; set; } = string.Empty;

    /// <summary>
    /// 数量。
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}

/// <summary>
/// 群消息表情点赞通知事件。
/// </summary>
public class GroupMsgEmojiLikeNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupMsgEmojiLike;

    /// <summary>
    /// 消息ID。
    /// </summary>
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }

    /// <summary>
    /// 点赞列表。
    /// </summary>
    [JsonPropertyName("likes")]
    public List<MsgEmojiLike> Likes { get; set; } = new();
}
