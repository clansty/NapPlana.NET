using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NapPlana.Core.Bot.BotInstance;
using NapPlana.Core.Event.Handler;
using NapPlana.DI.Extensions;
using NapPlana.DI.Service;
using NapPlana.Net.DiExample.Service;
using LogLevel = NapPlana.Core.Data.LogLevel;

var builder = Host.CreateApplicationBuilder(args);

//配置日志
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);

//添加NapBot依赖注入服务
builder.Services.AddNapBot(builder.Configuration);
//你可以在此编写自己的服务并通过这个方式注入，要求服务的类实现INapFunctionService
builder.Services.AddNapFunctionService<PokeBackService>();

//构建Host
var host = builder.Build();

//获取事件处理
var eventHandler = host.Services.GetRequiredService<IEventHandler>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

//设置日志转发，这部分可以不做其实
eventHandler.OnLogReceived += (level, message) =>
{
    var logLevel = level switch
    {
        LogLevel.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
        LogLevel.Info => Microsoft.Extensions.Logging.LogLevel.Information,
        LogLevel.Warning => Microsoft.Extensions.Logging.LogLevel.Warning,
        LogLevel.Error => Microsoft.Extensions.Logging.LogLevel.Error,
        _ => Microsoft.Extensions.Logging.LogLevel.Information
    };
    logger.Log(logLevel, "[NapBot] {Message}", message);
};

//注册事件
eventHandler.OnBotConnected += () =>
{
    logger.LogInformation("=== Bot已连接到NapCat服务器 ===");
};

eventHandler.OnGroupMessageReceived += async (ev) =>
{
    logger.LogInformation("收到群消息: [{GroupId}] {Sender}: {Message}", 
        ev.GroupId, ev.Sender.Nickname, ev.RawMessage);
};

eventHandler.OnPrivateMessageReceived += async (ev) =>
{
    logger.LogInformation("收到私聊消息: [{UserId}] {Sender}: {Message}", 
        ev.UserId, ev.Sender.Nickname, ev.RawMessage);
};

eventHandler.OnBotHeartbeat += (ev) =>
{
    logger.LogDebug("心跳: Status={Status}, Interval={Interval}ms", 
        ev.Status?.Good, ev.Interval);
};

logger.LogInformation("=== NapBot DI示例程序启动 ===");
logger.LogInformation("配置文件: appsettings.json");
logger.LogInformation("Bot将自动连接到NapCat服务器...");
logger.LogInformation("按 Ctrl+C 停止程序");

// 运行Host（会自动启动ConnectionService并管理连接生命周期）
await host.RunAsync();

logger.LogInformation("=== 程序已退出 ===");
