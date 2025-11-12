using System.Text.Json.Serialization;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 消息基类。
/// </summary>
[JsonConverter(typeof(MessageBaseConverter))]
public class MessageBase
{
    /// <summary>
    /// 消息类型。
    /// </summary>
    [JsonPropertyName("type")]
    public virtual MessageDataType MessageType { get; set; }
    
    /// <summary>
    /// 消息数据。
    /// </summary>
    [JsonPropertyName("data")]
    public virtual MessageDataBase MessageData { get; set; } = new MessageDataBase();
}

/// <summary>
/// 所有消息数据基类。
/// </summary>
[Serializable]
public class MessageDataBase
{
    
}