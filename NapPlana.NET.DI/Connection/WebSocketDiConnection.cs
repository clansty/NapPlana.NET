using NapPlana.Core.Connections.WebSocket;
using NapPlana.Core.Data;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Event.Parser;
using NapPlana.Core.API;
using NapPlana.Core.Connections.Plugins;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using LogLevel = NapPlana.Core.Data.LogLevel;

namespace NapPlana.DI.Connection;

/// <summary>
/// DI架构下的WebSocket连接基类
/// 通过依赖注入管理事件处理、API处理和事件解析
/// </summary>
public class WebSocketDiConnection : WebsocketClientConnection
{
    private readonly IEventHandler _eventHandler;
    private readonly IApiHandler _apiHandler;
    private readonly IEventParser _eventParser;
    private WebSocketClient? _client;

    /// <summary>
    /// 通过依赖注入获取核心服务
    /// </summary>
    /// <param name="ip">NapCat服务器IP</param>
    /// <param name="port">NapCat服务器端口</param>
    /// <param name="token">访问令牌（可选）</param>
    /// <param name="eventHandler">事件处理器</param>
    /// <param name="apiHandler">API响应处理器</param>
    /// <param name="eventParser">事件解析器</param>
    public WebSocketDiConnection(
        string ip,
        int port,
        string? token,
        IEventHandler eventHandler,
        IApiHandler apiHandler,
        IEventParser eventParser) : base(ip, port, token)
    {
        _eventHandler = eventHandler ?? throw new ArgumentNullException(nameof(eventHandler));
        _apiHandler = apiHandler ?? throw new ArgumentNullException(nameof(apiHandler));
        _eventParser = eventParser ?? throw new ArgumentNullException(nameof(eventParser));
        
        ConnectionType = BotConnectionType.WebSocketClient;
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

        try
        {
            _eventHandler.LogReceived(LogLevel.Info, $"正在连接到 WebSocket 服务器 {Ip}:{Port}...");
            
            _client = new WebSocketClient();
            
            // 配置客户端
            await _client.SetupAsync(new TouchSocketConfig()
                .ConfigurePlugins(plugins =>
                {
                    // 添加认证插件（如果有token）
                    if (!string.IsNullOrWhiteSpace(Token))
                    {
                        plugins.Add(new WebSocketAuthPlugin(Token));
                    }
                    
                    // 添加DI版本的消息接收插件
                    plugins.Add(new OnWebsocketMessageReceived(
                        _eventParser, 
                        _eventHandler, 
                        _apiHandler));
                })
                .SetRemoteIPHost($"ws://{Ip}:{Port}"));
            
            // 设置连接回调
            _client.Connected += (sender, e) =>
            {
                _eventHandler.LogReceived(LogLevel.Info, "WebSocket 连接已建立");
                return EasyTask.CompletedTask;
            };
            
            await _client.ConnectAsync(CancellationToken.None);
        }
        catch (Exception ex)
        {
            _eventHandler.LogReceived(LogLevel.Error, $"WebSocket 连接失败: {ex.Message}");
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
            _eventHandler.LogReceived(LogLevel.Info, "正在关闭 WebSocket 连接...");
            await _client.CloseAsync("shutdown");
            _eventHandler.LogReceived(LogLevel.Info, "WebSocket 连接已关闭");
        }
        catch (Exception ex)
        {
            _eventHandler.LogReceived(LogLevel.Warning, $"关闭 WebSocket 连接时出错: {ex.Message}");
        }
        finally
        {
            _client.Dispose();
            _client = null;
        }
    }
}

