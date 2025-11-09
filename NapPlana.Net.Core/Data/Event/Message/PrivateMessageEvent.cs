using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Message;

public class FriendSender
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = string.Empty;

    [JsonPropertyName("sex")]
    public SexType Sex { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }
}

public class PrivateMessageEvent : MessageEventBase
{
    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; set; } = MessageType.Private;

    [JsonPropertyName("sub_type")]
    public PrivateMessageSubType SubType { get; set; }

    [JsonPropertyName("sender")]
    public FriendSender Sender { get; set; } = new();
    
    /// <summary>
    /// 其实我不知道要怎么判断临时会话，不过这玩意似乎在其他情况的私聊发送里没有出现过
    /// 用就完了，不对再换
    /// </summary>
    [JsonPropertyName("temp_source")]
    public int? TempFlag { get; set; }
}
