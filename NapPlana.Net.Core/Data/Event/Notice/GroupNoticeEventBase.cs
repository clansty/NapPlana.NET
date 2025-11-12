using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

/// <summary>
/// 群通知事件基类。
/// </summary>
public class GroupNoticeEventBase: NoticeEventBase
{
    /// <summary>
    /// 群ID。
    /// </summary>
    [JsonPropertyName("group_id")]
    public long GroupId { get; set; }
    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}