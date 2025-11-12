using NapPlana.Core.API;
using NapPlana.Core.Connections;
using NapPlana.Core.Data;
using NapPlana.Core.Data.API;

namespace NapPlana.Core.Bot;

/// <summary>
/// 机器人主体
/// </summary>
public class NapBot
{
    private ConnectionBase _connection;
    /// <summary>
    /// QQ号
    /// </summary>
    public  long SelfId = 0;
    
    /// <summary>
    /// 创建实例
    /// </summary>
    public NapBot()
    {
        // Default to a dummy connection; should be set properly later
        _connection = new ConnectionBase();
    }

    /// <summary>
    /// 带参创建
    /// </summary>
    /// <param name="connection">连接类型</param>
    /// <param name="selfId">QQ号</param>
    public NapBot(ConnectionBase connection,long selfId)
    {
        _connection = connection;
        SelfId = selfId;
    }

    /// <summary>
    /// 设置连接类型
    /// </summary>
    /// <param name="connection">连接类型</param>
    /// <returns>自身</returns>
    public NapBot SetConnection(ConnectionBase connection)
    {
        _connection = connection;
        return this;
    }

    /// <summary>
    /// 异步启动机器人
    /// </summary>
    /// <returns></returns>
    public Task StartAsync() => _connection.InitializeAsync();
    /// <summary>
    /// 异步终止机器人
    /// </summary>
    /// <returns></returns>
    public Task StopAsync() => _connection.ShutdownAsync();
    
    /// <summary>
    /// 发送群消息
    /// </summary>
    /// <param name="groupMessage">请求</param>
    /// <returns>响应</returns>
    /// <exception cref="ArgumentNullException">传参错误</exception>
    /// <exception cref="InvalidOperationException">远程响应错误</exception>
    /// <exception cref="TimeoutException">访问超时</exception>
    public async Task<GroupMessageSendResponseData> SendGroupMessageAsync(GroupMessageSend groupMessage)
    {
        if (groupMessage is null) throw new ArgumentNullException(nameof(groupMessage));
        
        var echo = Guid.NewGuid().ToString();
        await _connection.SendMessageAsync(ApiActionType.SendGroupMsg, groupMessage, echo);
        
        var timeout = TimeSpan.FromSeconds(15);
        var start = DateTime.UtcNow;

        while (DateTime.UtcNow - start < timeout)
        {
            if (ApiHandler.TryConsume(echo, out var response))
            {
                if (response.RetCode != 0)
                {
                    throw new InvalidOperationException($"send_group_msg failed: {response.RetCode} - {response.Message}");
                }
                var data = response.GetData<GroupMessageSendResponseData>();
                if (data != null)
                {
                    return data;
                }
                throw new InvalidOperationException("Failed to parse send_group_msg response data.");
            }

            await Task.Delay(50);
        }

        throw new TimeoutException("Timed out waiting for send_group_msg response.");
    }
    
    /// <summary>
    /// 发送戳一戳消息
    /// </summary>
    /// <param name="pokeMessage">信息结构</param>
    /// <exception cref="ArgumentNullException">传参错误</exception>
    /// <exception cref="InvalidOperationException">远端响应错误</exception>
    /// <exception cref="TimeoutException">超时</exception>
    public async Task SendPokeAsync(PokeMessageSend pokeMessage)
    {
        if (pokeMessage is null) throw new ArgumentNullException(nameof(pokeMessage));
        
        var echo = Guid.NewGuid().ToString();
        await _connection.SendMessageAsync(ApiActionType.SendPoke, pokeMessage, echo);
        
        var timeout = TimeSpan.FromSeconds(15);
        var start = DateTime.UtcNow;

        while (DateTime.UtcNow - start < timeout)
        {
            if (ApiHandler.TryConsume(echo, out var response))
            {
                if (response.RetCode != 0)
                {
                    throw new InvalidOperationException($"send_poke failed: {response.RetCode} - {response.Message}");
                }
                return;
            }

            await Task.Delay(50);
        }

        throw new TimeoutException("Timed out waiting for send_poke response.");
    }
}