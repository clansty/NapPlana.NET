using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Message;

/// <summary>
/// 私信消息发送者信息。
/// </summary>
public class FriendSender
{
    /// <summary>
    /// 发送者用户 ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// 昵称。
    /// </summary>
    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// 性别。
    /// </summary>
    [JsonPropertyName("sex")]
    public SexType Sex { get; set; }

    /// <summary>
    /// 年龄。
    /// </summary>
    [JsonPropertyName("age")]
    public int Age { get; set; }
}

/// <summary>
/// 私信消息事件。
/// </summary>
public class PrivateMessageEvent : MessageEventBase
{
    /// <summary>
    /// 消息类型，固定为 Private。
    /// </summary>
    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; set; } = MessageType.Private;

    /// <summary>
    /// 私信子类型（好友 / 群临时会话 等）。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public PrivateMessageSubType SubType { get; set; }

    /// <summary>
    /// 发送者信息。
    /// </summary>
    [JsonPropertyName("sender")]
    public FriendSender Sender { get; set; } = new();
    
    /// <summary>
    /// 临时会话标识（如存在）。
    /// </summary>
    [JsonPropertyName("temp_source")]
    public int? TempFlag { get; set; }
}
