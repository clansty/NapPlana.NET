using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NapPlana.Core.API;
using NapPlana.Core.DependencyInjection;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Event.Parser;
using NapPlana.DI.Service;
using EventHandler = NapPlana.Core.Event.Handler.EventHandler;

namespace NapPlana.DI.Extensions;

/// <summary>
/// NapBot依赖注入扩展方法
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加NapBot DI服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    /// <returns>服务集合</returns>
    private static IServiceCollection AddNapBotRequirements(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 绑定配置 - 使用OptionsConfigurationServiceCollectionExtensions
        var napBotSection = configuration.GetSection(NapBotOptions.SectionName);
        services.Configure<NapBotOptions>(napBotSection);

        // 注册核心服务为Singleton
        services.AddSingleton<IEventHandler, EventHandler>();
        services.AddSingleton<IApiHandler, ApiHandler>();
        services.AddSingleton<IEventParser, RootEventParser>();

        // 注册BotContext为Scoped
        services.AddScoped<BotContext>();

        return services;
    }

    /// <summary>
    /// 添加NapBot DI服务（使用委托配置）
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configureOptions">配置委托</param>
    /// <returns>服务集合</returns>
    private static IServiceCollection AddNapBotRequirements(
        this IServiceCollection services,
        Action<NapBotOptions> configureOptions)
    {
        // 配置选项
        services.Configure(configureOptions);

        // 注册核心服务
        services.AddSingleton<IEventHandler, EventHandler>();
        services.AddSingleton<IApiHandler, ApiHandler>();
        services.AddSingleton<IEventParser, RootEventParser>();

        // 注册BotContext为Scoped
        services.AddScoped<BotContext>();

        return services;
    }

    /// <summary>
    /// 添加NapBot DI服务并注册ConnectionService为HostedService
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddNapBot(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 先添加NapBot服务
        services.AddNapBotRequirements(configuration);

        // 注册ConnectionService为HostedService
        services.AddHostedService<ConnectionService>();

        return services;
    }

    /// <summary>
    /// 添加NapBot DI服务并注册ConnectionService为HostedService
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configureOptions">配置委托</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddNapBot(
        this IServiceCollection services,
        Action<NapBotOptions> configureOptions)
    {
        // 先添加NapBot服务
        services.AddNapBotRequirements(configureOptions);

        // 注册ConnectionService为HostedService
        services.AddHostedService<ConnectionService>();

        return services;
    }
}

