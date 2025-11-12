using System.Text.Json.Serialization;
using NapPlana.Core.Data.Event.Notice;

namespace NapPlana.Core.Data.Event.Request;

/// <summary>
/// 好友请求事件。
/// </summary>
public class FriendRequestEvent: NoticeEventBase
{
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Request;
    
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; } = NoticeType.Friend;
    
    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    
    /// <summary>
    /// 评论。
    /// </summary>
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    
    /// <summary>
    /// 标志。
    /// </summary>
    [JsonPropertyName("flag")]
    public string Flag { get; set; } = string.Empty;
}

/// <summary>
/// 群请求事件。
/// </summary>
public class GroupRequestEvent: NoticeEventBase
{
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Request;
    
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; } = NoticeType.Group;
    
    /// <summary>
    /// 子类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public string SubType { get; set; } = "";
    
    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    
    /// <summary>
    /// 评论。
    /// </summary>
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    
    /// <summary>
    /// 标志。
    /// </summary>
    [JsonPropertyName("flag")]
    public string Flag { get; set; } = string.Empty;
}