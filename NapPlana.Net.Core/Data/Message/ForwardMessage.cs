using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 转发消息数据。
/// </summary>
public class ForwardMessageData : MessageDataBase
{
    /// <summary>
    /// ID。
    /// </summary>
    [JsonPropertyName("id")] public string? Id { get; set; }
    /// <summary>
    /// 内容。
    /// </summary>
    [JsonPropertyName("content")] public List<object>? Content { get; set; }
}

/// <summary>
/// 转发消息。
/// </summary>
public class ForwardMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Forward;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new ForwardMessageData();
}
