using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Request;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Request;

/// <summary>
/// 请求事件解析器，处理好友添加请求和加群请求事件。
/// </summary>
public class RequestEventParser(IEventHandler handler) : RootEventParser(handler)
{
    /// <summary>
    /// 解析请求事件并触发对应内部事件。
    /// </summary>
    /// <param name="botEvent">原始 OneBot 请求事件 JSON 字符串</param>
    /// <exception cref="UnSupportFeatureException">反序列化失败或事件类型不匹配</exception>
    public override void ParseEvent(string botEvent)
    {
        var baseEvent = JsonSerializer.Deserialize<RequestEventBase>(botEvent);
        if (baseEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot请求事件格式");
        }

        switch (baseEvent.RequestType)
        {
            case RequestType.Friend:
            {
                var friendRequest = JsonSerializer.Deserialize<FriendRequestEvent>(botEvent);
                if (friendRequest == null)
                    throw new UnSupportFeatureException("好友添加请求事件反序列化失败");
                handler.FriendRequestReceived(friendRequest);
                break;
            }
            case RequestType.Group:
            {
                var groupRequest = JsonSerializer.Deserialize<GroupRequestEvent>(botEvent);
                if (groupRequest == null)
                    throw new UnSupportFeatureException("加群请求事件反序列化失败");
                handler.GroupRequestReceived(groupRequest);
                break;
            }
            default:
                // 不是已知的请求类型，忽略
                break;
        }
    }
}
