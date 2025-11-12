using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 骰子消息数据。
/// </summary>
public class DiceMessageData : MessageDataBase
{
    /// <summary>
    /// 结果。
    /// </summary>
    [JsonPropertyName("result")] public string? Result { get; set; }
}

/// <summary>
/// 随机骰子消息。
/// </summary>
public class DiceMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Dice;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new DiceMessageData();
}
