using NapPlana.Core.Data;

namespace NapPlana.Core.DependencyInjection;

/// <summary>
/// NapBot配置选项
/// </summary>
public class NapBotOptions
{
    /// <summary>
    /// 配置节名称
    /// </summary>
    public const string SectionName = "NapBot";

    /// <summary>
    /// 机器人QQ号
    /// </summary>
    public long SelfId { get; set; }

    /// <summary>
    /// 连接类型
    /// </summary>
    public BotConnectionType ConnectionType { get; set; } = BotConnectionType.WebSocketClient;

    /// <summary>
    /// NapCat服务器IP
    /// </summary>
    public string Ip { get; set; } = "127.0.0.1";

    /// <summary>
    /// NapCat服务器端口
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 访问令牌(可选)
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// API超时时间(秒)
    /// </summary>
    public int ApiTimeout { get; set; } = 15;
}

