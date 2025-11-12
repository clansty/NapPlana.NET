using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 视频消息数据。
/// </summary>
public class VideoMessageData : MessageDataBase
{
    /// <summary>
    /// 文件。
    /// </summary>
    [JsonPropertyName("file")] public string File { get; set; } = string.Empty;
    /// <summary>
    /// 名称。
    /// </summary>
    [JsonPropertyName("name")] public string? Name { get; set; }
    /// <summary>
    /// 缩略图。
    /// </summary>
    [JsonPropertyName("thumb")] public string? Thumb { get; set; }
    /// <summary>
    /// URL。
    /// </summary>
    [JsonPropertyName("url")] public string? Url { get; set; }
    /// <summary>
    /// 路径。
    /// </summary>
    [JsonPropertyName("path")] public string? Path { get; set; }
    /// <summary>
    /// 文件ID。
    /// </summary>
    [JsonPropertyName("file_id")] public string? FileId { get; set; }
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
/// 视频消息。
/// </summary>
public class VideoMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Video;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new VideoMessageData();
}
