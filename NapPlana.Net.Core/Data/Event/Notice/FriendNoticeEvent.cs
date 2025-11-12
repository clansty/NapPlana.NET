using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

/// <summary>
/// 好友添加通知事件。
/// </summary>
public class FriendAddNoticeEvent: NoticeEventBase
{
    /// <summary>
    /// 通知类型。
    /// </summary>
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; }= NoticeType.FriendAdd;
    
    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    
}

/// <summary>
/// 好友消息撤回通知事件。
/// </summary>
public class FriendRecallNoticeEvent: NoticeEventBase
{
    /// <summary>
    /// 通知类型。
    /// </summary>
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; }= NoticeType.FriendRecall;
    
    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    
    /// <summary>
    /// 消息ID。
    /// </summary>
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }
}