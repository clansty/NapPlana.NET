using System.Collections.Concurrent;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Action;
using NapPlana.Core.Event.Handler;

namespace NapPlana.Core.API;

/// <summary>
/// 从WS接收到的信息进入这里等待消费
/// 为了统一，Http也需要在缓冲区等待
/// </summary>
public static class ApiHandler
{
    private static ConcurrentDictionary<string,ActionResponse> _responseDict = new();
    
    /// <summary>
    /// 将WS收到的消息加入缓冲区
    /// </summary>
    /// <param name="response">WS响应内容</param>
    public static async Task AddResponseAsync(ActionResponse response)
    {
        if (!_responseDict.TryAdd(response.Echo, response))
        {
            BotEventHandler.LogReceived(LogLevel.Warning, $"响应重复加入队列: {response.Echo}");
        }
        await Task.CompletedTask;
    }
    /// <summary>
    /// 尝试从缓冲区取出响应
    /// </summary>
    /// <param name="echo">标识字段</param>
    /// <param name="response">相应内容</param>
    /// <returns>是否取出成功</returns>
    public static bool TryConsume(string echo, out ActionResponse response)
    {
        return _responseDict.TryRemove(echo, out response);
    }
    
}