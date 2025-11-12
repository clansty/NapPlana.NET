using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 回复消息数据。
/// </summary>
public class ReplyMessageData : MessageDataBase
{
    /// <summary>
    /// ID。
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
}

/// <summary>
/// 回复消息。
/// </summary>
public class ReplyMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Reply;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new ReplyMessageData();
}
