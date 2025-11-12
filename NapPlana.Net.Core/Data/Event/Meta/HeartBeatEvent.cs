using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Meta;

/// <summary>
/// 心跳状态。
/// </summary>
public class HeartbeatStatus
{
    /// <summary>
    /// 是否在线。
    /// </summary>
    [JsonPropertyName("online")]
    public bool? Online { get; set; }  // 是否在线，可为 null

    /// <summary>
    /// 状态是否良好。
    /// </summary>
    [JsonPropertyName("good")]
    public bool Good { get; set; }     // 状态是否良好
}

/// <summary>
/// 心跳事件。
/// </summary>
public class HeartBeatEvent: MetaEventBase
{
    [JsonPropertyName("meta_event_type")]
    public override MetaEventType MetaEventType { get; set; }= MetaEventType.Heartbeat;
    
    /// <summary>
    /// 状态。
    /// </summary>
    [JsonPropertyName("status")]
    public HeartbeatStatus? Status { get; set; }
    
    /// <summary>
    /// 间隔。
    /// </summary>
    [JsonPropertyName("interval")]
    public int Interval { get; set; }
}