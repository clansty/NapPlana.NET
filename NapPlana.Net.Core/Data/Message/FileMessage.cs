using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class FileMessageData : MessageDataBase
{
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("file")] public string File { get; set; } = string.Empty; // "empty" when sending placeholder
    [JsonPropertyName("path")] public string? Path { get; set; }
    [JsonPropertyName("url")] public string? Url { get; set; }
    [JsonPropertyName("file_id")] public string? FileId { get; set; }
    [JsonPropertyName("file_size")] public string? FileSize { get; set; }
    [JsonPropertyName("file_unique")] public string? FileUnique { get; set; }
}

public class FileMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.File;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new FileMessageData();
}
