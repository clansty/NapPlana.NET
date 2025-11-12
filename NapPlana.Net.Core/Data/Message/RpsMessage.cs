using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 石头剪刀布消息数据。
/// </summary>
public class RpsMessageData : MessageDataBase
{
    /// <summary>
    /// 结果。
    /// </summary>
    [JsonPropertyName("result")] public string? Result { get; set; }
}

/// <summary>
/// 石头剪刀布消息。
/// </summary>
public class RpsMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Rps;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new RpsMessageData();
}
