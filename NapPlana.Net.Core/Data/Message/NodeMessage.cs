using System.Text.Json.Serialization;

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
    [JsonPropertyName("content")] public List<MessageDataBase>? Content { get; set; }
}

/// <summary>
/// 合并转发消息。
/// </summary>
public class NodeMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Node;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new NodeMessageData();
}
