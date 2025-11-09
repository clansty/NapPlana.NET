using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class AtMessageData : MessageDataBase
{
    // QQ number or "all" for @全体
    [JsonPropertyName("qq")] public string Qq { get; set; } = string.Empty;
}

public class AtMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.At;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new AtMessageData();
}
