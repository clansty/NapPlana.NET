using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// JSON消息数据。
/// </summary>
public class JsonMessageData : MessageDataBase
{
    /// <summary>
    /// 数据内容。
    /// </summary>
    [JsonPropertyName("data")] public string DataContent { get; set; } = string.Empty;
}

/// <summary>
/// JSON消息。
/// </summary>
public class JsonMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Json;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new JsonMessageData();
}
