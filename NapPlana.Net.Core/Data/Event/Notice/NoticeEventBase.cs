using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

/// <summary>
/// 通知事件基类。
/// </summary>
public class NoticeEventBase: OneBotEvent
{
    /// <summary>
    /// 事件类型。
    /// </summary>
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Notice;
    
    /// <summary>
    /// 通知类型。
    /// </summary>
    [JsonPropertyName("notice_type")]
    public virtual NoticeType NoticeType { get; set; }
}