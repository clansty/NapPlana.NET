namespace NapPlana.Core.Event.Parser;

/// <summary>
/// 事件解析器接口，定义统一的事件 JSON 解析入口。
/// </summary>
public interface IEventParser
{
    /// <summary>
    /// 解析并处理事件。
    /// </summary>
    /// <param name="jsonEventData">原始 OneBot 事件 JSON 字符串</param>
    void ParseEvent(string jsonEventData);
}