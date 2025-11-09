using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class ForwardMessageData : MessageDataBase
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    // Received: structured content list (nodes). Leave as object list for flexibility.
    [JsonPropertyName("content")] public List<object>? Content { get; set; }
}

public class ForwardMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Forward;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new ForwardMessageData();
}
