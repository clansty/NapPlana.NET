using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Message;

/// <summary>
/// 私信消息发送事件。
/// </summary>
public class PrivateMessageSentEvent: MessageSentEvent
{
    /// <summary>
    /// 子类型。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public PrivateMessageSubType SubType { get; set; }
    
    /// <summary>
    /// 临时会话标识。
    /// </summary>
    [JsonPropertyName("temp_source")]
    public int? TempFlag { get; set; }
}