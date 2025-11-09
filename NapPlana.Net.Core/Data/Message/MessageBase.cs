using System.Text.Json.Serialization;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data.Message;

[JsonConverter(typeof(MessageBaseConverter))]
public class MessageBase
{
    [JsonPropertyName("type")]
    public virtual MessageDataType MessageType { get; set; }
    
    [JsonPropertyName("data")]
    public virtual MessageDataBase MessageData { get; set; } = new MessageDataBase();
}

/// <summary>
/// 所有Message中Data的基类
/// </summary>
[Serializable]
public class MessageDataBase
{
    
}