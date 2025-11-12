using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event;

/// <summary>
/// OneBot 顶层事件基类，包含所有事件的通用字段。
/// </summary>
public class OneBotEvent
{
    /// <summary>
    /// 事件时间戳（秒）。
    /// </summary>
    [JsonPropertyName("time")]
    public long TimeStamp { get; set; }
    
    /// <summary>
    /// 机器人自身账号 ID。
    /// </summary>
    [JsonPropertyName("self_id")]
    public long SelfId { get; set; }
    
    /// <summary>
    /// 事件类型（post_type）。
    /// </summary>
    [JsonPropertyName("post_type")]
    public virtual EventType PostType { get; set; } = EventType.None;
}