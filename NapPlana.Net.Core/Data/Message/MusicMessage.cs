using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

/// <summary>
/// 音乐消息数据。
/// </summary>
public class MusicMessageData : MessageDataBase
{
    /// <summary>
    /// 类型。
    /// </summary>
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
    /// <summary>
    /// ID。
    /// </summary>
    [JsonPropertyName("id")] public string? Id { get; set; }
    /// <summary>
    /// URL。
    /// </summary>
    [JsonPropertyName("url")] public string? Url { get; set; }
    /// <summary>
    /// 音频。
    /// </summary>
    [JsonPropertyName("audio")] public string? Audio { get; set; }
    /// <summary>
    /// 标题。
    /// </summary>
    [JsonPropertyName("title")] public string? Title { get; set; }
    /// <summary>
    /// 图片。
    /// </summary>
    [JsonPropertyName("image")] public string? Image { get; set; }
    /// <summary>
    /// 歌手。
    /// </summary>
    [JsonPropertyName("singer")] public string? Singer { get; set; }
}

/// <summary>
/// 音乐消息。
/// </summary>
public class MusicMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Music;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get;set; } = new MusicMessageData();
}
