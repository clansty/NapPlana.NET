using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.API;

/// <summary>
/// 处理好友添加请求。
/// </summary>
public class FriendAddRequestAction
{
    /// <summary>
    /// 请求标识（从事件中获取）。
    /// </summary>
    [JsonPropertyName("flag")]
    public string Flag { get; set; } = string.Empty;
    
    /// <summary>
    /// 是否同意。
    /// </summary>
    [JsonPropertyName("approve")]
    public bool Approve { get; set; }
    
    /// <summary>
    /// 同意后的备注名（仅同意时有效）。
    /// </summary>
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
}

/// <summary>
/// 处理群添加请求。
/// </summary>
public class GroupAddRequestAction
{
    /// <summary>
    /// 请求标识（从事件中获取）。
    /// </summary>
    [JsonPropertyName("flag")]
    public string Flag { get; set; } = string.Empty;
    
    /// <summary>
    /// 子类型（add/invite）。
    /// </summary>
    [JsonPropertyName("sub_type")]
    public string SubType { get; set; } = string.Empty;
    
    /// <summary>
    /// 是否同意。
    /// </summary>
    [JsonPropertyName("approve")]
    public bool Approve { get; set; }
    
    /// <summary>
    /// 拒绝理由（仅拒绝时有效）。
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}
