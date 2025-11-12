using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Meta;

/// <summary>
/// 生命周期事件。
/// </summary>
public class LifeCycleEvent: MetaEventBase
{
    /// <summary>
    /// 元事件类型。
    /// </summary>
    [JsonPropertyName("meta_event_type")]
    public override MetaEventType MetaEventType { get; set; }= MetaEventType.Lifecycle;
    
    /// <summary>
    /// 子类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public LifeCycleSubType SubType { get; set; }
}