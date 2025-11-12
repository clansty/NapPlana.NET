using System.Text.Json.Serialization;
using NapPlana.Core.Data.Message;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data.Event.Message;

/// <summary>
/// 消息事件基础类，包含公共消息字段。
/// </summary>
public class MessageEventBase : OneBotEvent
{
    /// <summary>
    /// 顶层事件类型，固定为 Message。
    /// </summary>
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Message;

    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }

    /// <summary>
    /// 发送者ID
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// 解析后的消息链
    /// </summary>
    [JsonPropertyName("message")]
    [JsonConverter(typeof(MessageListConverter))]
    public List<MessageBase> Messages { get; set; } = null!;
    
    /// <summary>
    /// 原始消息格式描述，固定为array
    /// </summary>
    [JsonPropertyName("message_format")]
    public string MessageFormat { get; set; } = "array";

    /// <summary>
    /// 未解析的原始消息字符串
    /// </summary>
    [JsonPropertyName("raw_message")]
    public string RawMessage { get; set; } = string.Empty;
}