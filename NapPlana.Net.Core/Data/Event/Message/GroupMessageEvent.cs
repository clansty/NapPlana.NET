using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Message;

/// <summary>
/// 群消息发送者信息。
/// </summary>
public class GroupSender
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
    /// 群名片（备注）。
    /// </summary>
    [JsonPropertyName("card")]
    public string Card { get; set; } = string.Empty;

    /// <summary>
    /// 群内角色（成员/管理/群主）。
    /// </summary>
    [JsonPropertyName("role")]
    public GroupRole Role { get; set; }

    /// <summary>
    /// 头衔（如群头衔）。
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 群等级（如有）。
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;
}

/// <summary>
/// 群消息事件。
/// </summary>
public class GroupMessageEvent : MessageEventBase
{
    /// <summary>
    /// 消息类型，固定为 Group。
    /// </summary>
    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; set; } = MessageType.Group;

    /// <summary>
    /// 群号。
    /// </summary>
    [JsonPropertyName("group_id")]
    public long GroupId { get; set; }

    /// <summary>
    /// 匿名信息（如匿名消息时存在）。
    /// </summary>
    [JsonPropertyName("anonymous")]
    public object? Anonymous { get; set; }

    /// <summary>
    /// 发送者信息。
    /// </summary>
    [JsonPropertyName("sender")]
    public GroupSender Sender { get; set; } = new();
}
