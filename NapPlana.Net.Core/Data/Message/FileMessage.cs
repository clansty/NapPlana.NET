using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 文件消息数据。
/// </summary>
public class FileMessageData : MessageDataBase
{
    /// <summary>
    /// 名称。
    /// </summary>
    [JsonPropertyName("name")] public string? Name { get; set; }
    /// <summary>
    /// 文件。
    /// </summary>
    [JsonPropertyName("file")] public string File { get; set; } = string.Empty; // "empty" when sending placeholder
    /// <summary>
    /// 路径。
    /// </summary>
    [JsonPropertyName("path")] public string? Path { get; set; }
    /// <summary>
    /// URL。
    /// </summary>
    [JsonPropertyName("url")] public string? Url { get; set; }
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
/// 文件消息。
/// </summary>
public class FileMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.File;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new FileMessageData();
}
