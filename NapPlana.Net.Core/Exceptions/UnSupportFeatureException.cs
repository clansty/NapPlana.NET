namespace NapPlana.Core.Exceptions;

/// <summary>
/// 不受支持报错
/// </summary>
public class UnSupportFeatureException: Exception
{
    /// <summary>
    /// 报错
    /// </summary>
    public UnSupportFeatureException() : base() { }
    /// <summary>
    /// 报错
    /// </summary>
    /// <param name="message">消息</param>
    public UnSupportFeatureException(string message) : base(message) { }
    /// <summary>
    /// 报错
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="inner">内部错误</param>
    public UnSupportFeatureException(string message, Exception inner) : base(message, inner) { }
}