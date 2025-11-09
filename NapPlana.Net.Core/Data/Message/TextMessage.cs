using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class TextMessageData: MessageDataBase
{
    [JsonPropertyName("text")]
    public String Text { get; set; } = String.Empty;
}

public class TextMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Text;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new TextMessageData();
}