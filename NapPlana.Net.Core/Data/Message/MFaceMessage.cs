using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class MFaceMessageData : MessageDataBase
{
    [JsonPropertyName("emoji_id")] public string EmojiId { get; set; } = string.Empty;
    [JsonPropertyName("emoji_package_id")] public string EmojiPackageId { get; set; } = string.Empty;
    [JsonPropertyName("key")] public string Key { get; set; } = string.Empty;
    [JsonPropertyName("summary")] public string? Summary { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
}

public class MFaceMessage : MessageBase
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.MFace;
    [JsonPropertyName("data")] public override MessageDataBase MessageData { get; set;} = new MFaceMessageData();
}
