using NapPlana.Core.Data;
using NapPlana.Core.Data.API;
using NapPlana.Core.Data.Message;

namespace NapPlana.Core.Bot;

/// <summary>
/// 合并转发消息构筑器
/// </summary>
public class ForwardMessageBuilder
{
    /// <summary>
    /// 创建合并转发消息构筑器
    /// </summary>
    public static ForwardMessageBuilder Create() => new ForwardMessageBuilder();

    private List<MessageBase> _messages = new List<MessageBase>();
    private string? _source;
    private List<ForwardNewsItem>? _news;
    private string? _summary;
    private string? _prompt;

    public string? Source => _source;
    public List<ForwardNewsItem>? News => _news;
    public string? Summary => _summary;
    public string? Prompt => _prompt;

    /// <summary>
    /// 构建消息节点列表
    /// </summary>
    public List<MessageBase> Build() => _messages;

    /// <summary>
    /// 设置转发卡片来源名称
    /// </summary>
    public ForwardMessageBuilder SetSource(string source)
    {
        _source = source;
        return this;
    }

    /// <summary>
    /// 设置转发卡片预览文本列表
    /// </summary>
    public ForwardMessageBuilder SetNews(List<ForwardNewsItem> news)
    {
        _news = news;
        return this;
    }

    /// <summary>
    /// 设置转发卡片摘要
    /// </summary>
    public ForwardMessageBuilder SetSummary(string summary)
    {
        _summary = summary;
        return this;
    }

    /// <summary>
    /// 设置转发卡片提示文本
    /// </summary>
    public ForwardMessageBuilder SetPrompt(string prompt)
    {
        _prompt = prompt;
        return this;
    }

    /// <summary>
    /// 添加引用已有消息的节点
    /// </summary>
    /// <param name="messageId">消息ID</param>
    public ForwardMessageBuilder AddNode(string messageId)
    {
        _messages.Add(new NodeMessage
        {
            MessageData = new NodeMessageData { Id = messageId },
            MessageType = MessageDataType.Node
        });
        return this;
    }

    /// <summary>
    /// 添加自定义内容节点
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="nickname">昵称</param>
    /// <param name="contentBuilder">消息内容构建委托</param>
    public ForwardMessageBuilder AddNode(string userId, string nickname, Action<MessageChainBuilder> contentBuilder)
    {
        var builder = MessageChainBuilder.Create();
        contentBuilder(builder);
        _messages.Add(new NodeMessage
        {
            MessageData = new NodeMessageData
            {
                UserId = userId,
                Nickname = nickname,
                Content = builder.Build()
            },
            MessageType = MessageDataType.Node
        });
        return this;
    }

    /// <summary>
    /// 添加嵌套合并转发节点
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="nickname">昵称</param>
    /// <param name="nestedBuilder">嵌套转发构建委托</param>
    public ForwardMessageBuilder AddNode(string userId, string nickname, Action<ForwardMessageBuilder> nestedBuilder)
    {
        var nested = ForwardMessageBuilder.Create();
        nestedBuilder(nested);
        _messages.Add(new NodeMessage
        {
            MessageData = new NodeMessageData
            {
                UserId = userId,
                Nickname = nickname,
                Content = nested.Build(),
                Source = nested._source,
                News = nested._news,
                Summary = nested._summary,
                Prompt = nested._prompt
            },
            MessageType = MessageDataType.Node
        });
        return this;
    }
}
