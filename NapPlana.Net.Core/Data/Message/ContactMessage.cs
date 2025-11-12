using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 联系人消息数据。
/// </summary>
public class ContactMessageData : MessageDataBase
{
    /// <summary>
    /// 类型。
    /// </summary>
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
    /// <summary>
    /// ID。
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
}

/// <summary>
/// 联系人消息。
/// </summary>
public class ContactMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Contact;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new ContactMessageData();
}
