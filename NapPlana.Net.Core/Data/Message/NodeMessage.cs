using System.Text.Json.Serialization;
using NapPlana.Core.Data.API;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 合并转发消息数据。
/// </summary>
public class NodeMessageData : MessageDataBase
{
    /// <summary>
    /// ID。
    /// </summary>
    [JsonPropertyName("id")] public string? Id { get; set; }
    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    /// <summary>
    /// 昵称。
    /// </summary>
    [JsonPropertyName("nickname")] public string? Nickname { get; set; }
    /// <summary>
    /// 内容。
    /// </summary>
    [JsonConverter(typeof(MessageListConverter))]
    [JsonPropertyName("content")] public List<MessageBase>? Content { get; set; }
    /// <summary>
    /// 转发卡片标题行的内容（xxx 的聊天记录）（嵌套转发时使用）。
    /// </summary>
    [JsonPropertyName("source")] public string? Source { get; set; }
    /// <summary>
    /// 转发卡片预览文本列表（嵌套转发时使用）。
    /// </summary>
    [JsonPropertyName("news")] public List<ForwardNewsItem>? News { get; set; }
    /// <summary>
    /// 转发卡片摘要（点击查看 x 条聊天记录）（嵌套转发时使用）。
    /// </summary>
    [JsonPropertyName("summary")] public string? Summary { get; set; }
    /// <summary>
    /// 转发卡片在消息列表中的外显文本（[聊天记录]）（嵌套转发时使用）。
    /// </summary>
    [JsonPropertyName("prompt")] public string? Prompt { get; set; }
}

/// <summary>
/// 合并转发消息。
/// </summary>
public class NodeMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Node;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new NodeMessageData();
}
