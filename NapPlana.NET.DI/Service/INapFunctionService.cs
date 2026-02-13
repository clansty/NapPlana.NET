namespace NapPlana.DI.Service;

public interface INapFunctionService
{
    /// <summary>
    /// 初始化机器人功能服务，将会在连接建立后使用
    /// </summary>
    /// <param name="context">上下文</param>
    /// <returns></returns>
    Task InitializeAsync(BotContext context);
}