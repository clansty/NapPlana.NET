using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Meta;

/// <summary>
/// 元事件基类。
/// </summary>
public class MetaEventBase: OneBotEvent
{
    /// <summary>
    /// 事件类型。
    /// </summary>
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Meta;
    
    /// <summary>
    /// 元事件类型。
    /// </summary>
    [JsonPropertyName("meta_event_type")]
    public virtual MetaEventType MetaEventType { get; set; }
    
}