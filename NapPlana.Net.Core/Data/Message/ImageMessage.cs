using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 图片消息数据。
/// </summary>
public class ImageMessageData : MessageDataBase
{
    /// <summary>
    /// 名称。
    /// </summary>
    [JsonPropertyName("name")] public string? Name { get; set; }
    /// <summary>
    /// 摘要。
    /// </summary>
    [JsonPropertyName("summary")] public string? Summary { get; set; }
    /// <summary>
    /// 文件。
    /// </summary>
    [JsonPropertyName("file")] public string File { get; set; } = string.Empty;
    /// <summary>
    /// 子类型。
    /// </summary>
    [JsonPropertyName("sub_type")] public string? SubType { get; set; }

    /// <summary>
    /// 文件ID。
    /// </summary>
    [JsonPropertyName("file_id")] public string? FileId { get; set; }
    /// <summary>
    /// URL。
    /// </summary>
    [JsonPropertyName("url")] public string? Url { get; set; }
    /// <summary>
    /// 路径。
    /// </summary>
    [JsonPropertyName("path")] public string? Path { get; set; }
    /// <summary>
    /// 文件大小。
    /// </summary>
    [JsonPropertyName("file_size")] public string? FileSize { get; set; }
    /// <summary>
    /// 文件唯一标识。
    /// </summary>
    [JsonPropertyName("file_unique")] public string? FileUnique { get; set; }
}

/// <summary>
/// 图片消息。
/// </summary>
public class ImageMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Image;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new ImageMessageData();
}
