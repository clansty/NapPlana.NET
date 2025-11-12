using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// @消息数据。
/// </summary>
public class AtMessageData : MessageDataBase
{
    /// <summary>
    /// QQ号或"all"。
    /// </summary>
    [JsonPropertyName("qq")] public string Qq { get; set; } = string.Empty;
}

/// <summary>
/// @消息。
/// </summary>
public class AtMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.At;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new AtMessageData();
}
