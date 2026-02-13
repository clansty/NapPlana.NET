using Microsoft.Extensions.Logging;
using NapPlana.Core.Data.API;
using NapPlana.Core.Event.Handler;
using NapPlana.DI.Service;

namespace NapPlana.Net.DiExample.Service;

public class PokeBackService(ILogger<PokeBackService> logger): INapFunctionService
{
    public async Task InitializeAsync(BotContext context)
    {
        context.EventHandler.OnGroupPokeNoticeReceived += async (sender) =>
        {
            logger.LogInformation("收到戳一戳事件: {EventData}", sender.GroupId);
            if (sender.UserId == context.SelfId)
            {
                return;
            }
            await context.SendPokeAsync(new PokeMessageSend()
            {
                GroupId = sender.GroupId.ToString(),
                UserId = sender.UserId.ToString(),
            });
        };
        await Task.CompletedTask;
    }
}