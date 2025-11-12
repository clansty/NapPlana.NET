using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 文本消息数据。
/// </summary>
public class TextMessageData: MessageDataBase
{
    /// <summary>
    /// 文本。
    /// </summary>
    [JsonPropertyName("text")]
    public String Text { get; set; } = String.Empty;
}

/// <summary>
/// 文本消息。
/// </summary>
public class TextMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Text;
    /// <summary>
    /// 消息数据。
    /// </summary>
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new TextMessageData();
}