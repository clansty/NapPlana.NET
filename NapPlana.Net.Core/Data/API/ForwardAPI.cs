using System.Text.Json.Serialization;
using NapPlana.Core.Data.Message;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data.API;

/// <summary>
/// 合并转发消息预览项。
/// </summary>
public class ForwardNewsItem
{
    /// <summary>
    /// 预览文本。
    /// </summary>
    [JsonPropertyName("text")] public string Text { get; set; } = string.Empty;
}

/// <summary>
/// 合并转发消息发送基类
/// </summary>
public abstract class ForwardMessageSendBase
{
    /// <summary>
    /// 消息节点列表
    /// </summary>
    [JsonPropertyName("messages")]
    [JsonConverter(typeof(MessageListConverter))]
    public List<MessageBase> Messages { get; set; } = new();

    /// <summary>
    /// 转发卡片标题行的内容（xxx 的聊天记录）（可选）
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>
    /// 转发卡片预览文本列表（可选）
    /// </summary>
    [JsonPropertyName("news")]
    public List<ForwardNewsItem>? News { get; set; }

    /// <summary>
    /// 转发卡片摘要（点击查看 x 条聊天记录）（可选）
    /// </summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    /// <summary>
    /// 转发卡片在消息列表中的外显文本（[聊天记录]）（可选）
    /// </summary>
    [JsonPropertyName("prompt")]
    public string? Prompt { get; set; }
}

/// <summary>
/// 发送群合并转发消息
/// </summary>
public class GroupForwardMessageSend : ForwardMessageSendBase
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonPropertyName("group_id")]
    public string GroupId { get; set; } = string.Empty;
}

/// <summary>
/// 发送私聊合并转发消息
/// </summary>
public class PrivateForwardMessageSend : ForwardMessageSendBase
{
    /// <summary>
    /// 目标 QQ 号
    /// </summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;
}

/// <summary>
/// 发送合并转发消息-响应
/// </summary>
public class ForwardMessageSendResponseData : ResponseDataBase
{
    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; } = 0;

    /// <summary>
    /// 转发消息ID（可选）
    /// </summary>
    [JsonPropertyName("forward_id")]
    public string? ForwardId { get; set; }
}
