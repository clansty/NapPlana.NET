using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class DiceMessageData : MessageDataBase
{
    [JsonPropertyName("result")] public string? Result { get; set; }
}

public class DiceMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Dice;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new DiceMessageData();
}
