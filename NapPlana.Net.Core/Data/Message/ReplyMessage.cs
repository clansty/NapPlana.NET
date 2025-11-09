using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class ReplyMessageData : MessageDataBase
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
}

public class ReplyMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Reply;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new ReplyMessageData();
}
