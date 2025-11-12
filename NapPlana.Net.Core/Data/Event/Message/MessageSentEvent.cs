using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Message;

/// <summary>
/// 消息发送事件。
/// </summary>
public class MessageSentEvent : MessageEventBase
{
    /// <summary>
    /// 事件类型，始终为MessageSent。
    /// </summary>
    [JsonPropertyName("post_type")] 
    public override EventType PostType { get; set; } = EventType.MessageSent;

    /// <summary>
    /// 消息类型。
    /// </summary>
    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; set; }

    /// <summary>
    /// 目标ID。
    /// </summary>
    [JsonPropertyName("target_id")]
    public long TargetId { get; set; }
    
    //MessageSent不需要sender
    
}
