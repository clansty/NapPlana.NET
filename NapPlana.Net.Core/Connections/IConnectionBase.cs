using NapPlana.Core.Data;

namespace NapPlana.Core.Connections;

/// <summary>
/// 连接接口
/// </summary>
public interface IConnectionBase
{
    /// <summary>
    /// 连接类型
    /// </summary>
    public BotConnectionType ConnectionType { get; protected set; }
}