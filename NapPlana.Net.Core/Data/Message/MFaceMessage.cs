using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 表情消息数据。
/// </summary>
public class MFaceMessageData : MessageDataBase
{
    /// <summary>
    /// 表情ID。
    /// </summary>
    [JsonPropertyName("emoji_id")] public string EmojiId { get; set; } = string.Empty;
    /// <summary>
    /// 表情包ID。
    /// </summary>
    [JsonPropertyName("emoji_package_id")] public string EmojiPackageId { get; set; } = string.Empty;
    /// <summary>
    /// 键。
    /// </summary>
    [JsonPropertyName("key")] public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 摘要。
    /// </summary>
    [JsonPropertyName("summary")] public string? Summary { get; set; }
    /// <summary>
    /// 名称。
    /// </summary>
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    /// <summary>
    /// ID。
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
}

/// <summary>
/// 表情消息。
/// </summary>
public class MFaceMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.MFace;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new MFaceMessageData();
}
