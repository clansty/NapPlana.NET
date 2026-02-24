using System.Text.Json.Serialization;
using NapPlana.Core.Data.API;
using NapPlana.Core.Data.Event;
using NapPlana.Core.Bot.BotInstance;

namespace NapPlana.Core.Data.Event.Request;

/// <summary>
/// 请求事件基类。
/// </summary>
public class RequestEventBase : OneBotEvent
{
    /// <summary>
    /// 事件类型。
    /// </summary>
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Request;
    
    /// <summary>
    /// 请求类型。
    /// </summary>
    [JsonPropertyName("request_type")]
    public virtual RequestType RequestType { get; set; }
}

/// <summary>
/// 好友请求事件。
/// </summary>
public class FriendRequestEvent : RequestEventBase
{
    /// <summary>
    /// 请求类型。
    /// </summary>
    [JsonPropertyName("request_type")]
    public override RequestType RequestType { get; set; } = RequestType.Friend;
    
    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    
    /// <summary>
    /// 验证信息。
    /// </summary>
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    
    /// <summary>
    /// 请求标识（处理请求时使用）。
    /// </summary>
    [JsonPropertyName("flag")]
    public string Flag { get; set; } = string.Empty;

    /// <summary>
    /// 同意好友请求。
    /// </summary>
    /// <param name="bot">机器人实例。</param>
    /// <param name="remark">同意后的备注名（可选）。</param>
    public async Task AcceptAsync(INapBot bot, string? remark = null)
    {
        await bot.SetFriendAddRequestAsync(new FriendAddRequestAction
        {
            Flag = Flag,
            Approve = true,
            Remark = remark
        });
    }

    /// <summary>
    /// 拒绝好友请求。
    /// </summary>
    /// <param name="bot">机器人实例。</param>
    public async Task RejectAsync(INapBot bot)
    {
        await bot.SetFriendAddRequestAsync(new FriendAddRequestAction
        {
            Flag = Flag,
            Approve = false
        });
    }
}

/// <summary>
/// 群请求事件。
/// </summary>
public class GroupRequestEvent : RequestEventBase
{
    /// <summary>
    /// 请求类型。
    /// </summary>
    [JsonPropertyName("request_type")]
    public override RequestType RequestType { get; set; } = RequestType.Group;
    
    /// <summary>
    /// 子类型（add=加群申请，invite=被邀请入群）。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public string SubType { get; set; } = string.Empty;
    
    /// <summary>
    /// 群号。
    /// </summary>
    [JsonPropertyName("group_id")]
    public long GroupId { get; set; }
    
    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    
    /// <summary>
    /// 验证信息。
    /// </summary>
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    
    /// <summary>
    /// 请求标识（处理请求时使用）。
    /// </summary>
    [JsonPropertyName("flag")]
    public string Flag { get; set; } = string.Empty;

    /// <summary>
    /// 同意群请求。
    /// </summary>
    /// <param name="bot">机器人实例。</param>
    public async Task AcceptAsync(INapBot bot)
    {
        await bot.SetGroupAddRequestAsync(new GroupAddRequestAction
        {
            Flag = Flag,
            SubType = SubType,
            Approve = true
        });
    }

    /// <summary>
    /// 拒绝群请求。
    /// </summary>
    /// <param name="bot">机器人实例。</param>
    /// <param name="reason">拒绝理由（可选）。</param>
    public async Task RejectAsync(INapBot bot, string? reason = null)
    {
        await bot.SetGroupAddRequestAsync(new GroupAddRequestAction
        {
            Flag = Flag,
            SubType = SubType,
            Approve = false,
            Reason = reason
        });
    }
}