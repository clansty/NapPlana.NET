using System.Text.Json;
using System.Text.Json.Serialization;
using NapPlana.Core.Connections.Plugins;
using NapPlana.Core.Data;
using NapPlana.Core.Data.API;
using NapPlana.Core.Event.Handler;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using LogLevel = NapPlana.Core.Data.LogLevel;

namespace NapPlana.Core.Connections.WebSocket;

/// <summary>
/// WebSocket客户端连接
/// </summary>
public class WebsocketClientConnection: ConnectionBase
{
    private WebSocketClient? _client;
    
    /// <summary>
    /// 初始化
    /// </summary>
    public WebsocketClientConnection()
    {
        ConnectionType = BotConnectionType.WebSocketClient;
    }
    /// <summary>
    /// 带参初始化
    /// </summary>
    /// <param name="ip">IP</param>
    /// <param name="port">端口</param>
    /// <param name="token">可空的令牌</param>
    public WebsocketClientConnection(string ip, int port, string? token = null)
        : this()
    {
        this.Ip = ip;
        this.Port = port;
        this.Token = token;
    }

    /// <summary>
    /// 初始化连接
    /// </summary>
    public override async Task InitializeAsync()
    {
        if (_client != null)
        {
            return;
        }
        _client = new WebSocketClient();
        await _client.SetupAsync(new TouchSocketConfig()
            .ConfigurePlugins(a=>
            {
                //配置请求头
                a.Add(new WebSocketAuthPlugin(Token));
                //消息接收
                a.Add<WebSocketMessageReceiverPlugin>();
            })
            .SetRemoteIPHost($"ws://{Ip}:{Port}"));
        //setup callbacks
        _client.Connected += (sender, e) =>
        {
            BotEventHandler.LogReceived(LogLevel.Info,"机器人连接至napcat...等待后续操作");
            return EasyTask.CompletedTask;
        };
        try
        {
            await _client.ConnectAsync(CancellationToken.None);
        }
        catch (Exception ex)
        {
            BotEventHandler.LogReceived(LogLevel.Error, $"连接失败: {ex.Message}");
            throw;
        }
    }
    
    /// <summary>
    /// 关闭连接
    /// </summary>
    public override async Task ShutdownAsync()
    {
        if (_client == null)
        {
            return;
        }
        try
        {
            await _client.CloseAsync("shutdown");
            BotEventHandler.LogReceived(LogLevel.Info, "WebSocket客户端已关闭");
        }
        catch (Exception ex)
        {
            BotEventHandler.LogReceived(LogLevel.Error, $"关闭WebSocket失败: {ex.Message}");
        }
        finally
        {
            _client.Dispose();
            _client = null;
        }
    }
    
    /// <summary>
    /// 发送原始消息
    /// </summary>
    /// <param name="message">消息json</param>
    public override async Task SendMessageAsync(string message)
    {
        if (_client == null)
        {
            BotEventHandler.LogReceived(LogLevel.Error, "无法发送消息，WebSocket未初始化");
            return;
        }
        try
        {
            await _client.SendAsync(message);
            BotEventHandler.LogReceived(LogLevel.Debug, $"已发送消息: {message}");
        }
        catch (Exception ex)
        {
            BotEventHandler.LogReceived(LogLevel.Error, $"发送消息失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="actionType">操作</param>
    /// <param name="message">消息内容</param>
    /// <param name="echo">标识符</param>
    public override async Task SendMessageAsync(ApiActionType actionType,object message,string echo)
    {
        if (_client == null)
        {
            BotEventHandler.LogReceived(LogLevel.Error, "无法发送消息，WebSocket未初始化");
            return;
        }

        if (string.IsNullOrEmpty(echo))
        {
            BotEventHandler.LogReceived(LogLevel.Error, "无法发送消息，WebSocket模式下Echo字段不可为空");
            return;
        }
        //构造GlobalRequest
        var req = new WsGlobalRequest()
        {
            Action = actionType,
            Params = message,
            Echo = echo
        };
        var mess = JsonSerializer.Serialize(req);
        
        try
        {
            await _client.SendAsync(mess);
            BotEventHandler.LogReceived(LogLevel.Debug, $"已发送消息: {message}");
        }
        catch (Exception ex)
        {
            BotEventHandler.LogReceived(LogLevel.Error, $"发送消息失败: {ex.Message}");
        }
    }
}