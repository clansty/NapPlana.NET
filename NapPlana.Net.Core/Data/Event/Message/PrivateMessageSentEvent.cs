using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Message;

public class PrivateMessageSentEvent: MessageSentEvent
{
    [JsonPropertyName("sub_type")]
    public PrivateMessageSubType SubType { get; set; }
    
    /// <summary>
    /// 其实我不知道要怎么判断临时会话，不过这玩意似乎在其他情况的私聊发送里没有出现过
    /// 用就完了，不对再换
    /// </summary>
    [JsonPropertyName("temp_source")]
    public int? TempFlag { get; set; }
}