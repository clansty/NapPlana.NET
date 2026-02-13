using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NapPlana.Core.API;
using NapPlana.Core.Connections;
using NapPlana.Core.DependencyInjection;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Event.Parser;
using NapPlana.DI.Connection;

namespace NapPlana.DI.Service;

/// <summary>
/// 连接服务 - 负责管理WebSocket连接的生命周期
/// </summary>
/// <remarks>
/// ConnectionService仅管理连接的启动和停止，不参与Bot上下文管理
/// </remarks>
public class ConnectionService : IHostedService
{
    private readonly NapBotOptions _options;
    private readonly IEventHandler _eventHandler;
    private readonly IApiHandler _apiHandler;
    private readonly IEventParser _eventParser;
    private readonly IServiceScopeFactory _scopeFactory;
    private WebSocketDiConnection? _connection;
    private IServiceScope? _scope;

    /// <summary>
    /// 构造函数 - 通过依赖注入获取核心服务
    /// </summary>
    public ConnectionService(
        IOptions<NapBotOptions> options,
        IEventHandler eventHandler,
        IApiHandler apiHandler,
        IEventParser eventParser,
        IServiceScopeFactory scopeFactory)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _eventHandler = eventHandler ?? throw new ArgumentNullException(nameof(eventHandler));
        _apiHandler = apiHandler ?? throw new ArgumentNullException(nameof(apiHandler));
        _eventParser = eventParser ?? throw new ArgumentNullException(nameof(eventParser));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    /// <summary>
    /// 启动服务 - 初始化连接并创建BotContext作用域
    /// </summary>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _eventHandler.LogReceived(Core.Data.LogLevel.Info, "ConnectionService 正在启动...");

            // 创建DI连接实例
            _connection = new WebSocketDiConnection(
                _options.Ip,
                _options.Port,
                _options.Token,
                _eventHandler,
                _apiHandler,
                _eventParser);

            // 初始化连接
            await _connection.InitializeAsync();

            // 创建作用域并注册BotContext
            _scope = _scopeFactory.CreateScope();
            var services = _scope.ServiceProvider;

            // 将连接实例注入到作用域中
            var botContext = ActivatorUtilities.CreateInstance<BotContext>(
                services,
                _connection,
                Options.Create(_options));
            
            foreach (var service in services.GetServices<INapFunctionService>())
            {
                await service.InitializeAsync(botContext);
            }
            _eventHandler.LogReceived(Core.Data.LogLevel.Info, 
                $"ConnectionService 已启动，Bot QQ: {_options.SelfId}");
        }
        catch (Exception ex)
        {
            _eventHandler.LogReceived(Core.Data.LogLevel.Error, 
                $"ConnectionService 启动失败: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 停止服务 - 关闭连接并释放BotContext作用域
    /// </summary>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            _eventHandler.LogReceived(Core.Data.LogLevel.Info, "ConnectionService 正在停止...");

            // 先关闭连接
            if (_connection != null)
            {
                await _connection.ShutdownAsync();
                _connection = null;
            }

            // 释放作用域
            _scope?.Dispose();
            _scope = null;

            _eventHandler.LogReceived(Core.Data.LogLevel.Info, "ConnectionService 已停止");
        }
        catch (Exception ex)
        {
            _eventHandler.LogReceived(Core.Data.LogLevel.Warning, 
                $"ConnectionService 停止时出错: {ex.Message}");
        }
    }
}