using System.Text.Json.Serialization;
using System.Text.Json;
using NapPlana.Core.Data.API;

namespace NapPlana.Core.Data.Action;

/// <summary>
/// NapCat远端响应
/// </summary>
[Serializable]
public class ActionResponse
{
    /// <summary>
    /// 状态
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    /// <summary>
    /// 响应码,0表示成功
    /// </summary>
    [JsonPropertyName("retcode")]
    public int RetCode { get; set; } = 0;
    /// <summary>
    /// 消息
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    /// <summary>
    /// 自定义数据
    /// </summary>
    [JsonPropertyName("data")]
    public object Data { get; set; } = new( );
    /// <summary>
    /// 母鸡
    /// </summary>
    [JsonPropertyName("wording")]
    public string Wording { get; set; } = string.Empty;
    /// <summary>
    /// 标识符，需要指定
    /// </summary>
    [JsonPropertyName("echo")]
    public string Echo { get; set; } = string.Empty;
    
    /// <summary>
    /// 快速获取数据
    /// </summary>
    /// <typeparam name="T">ResponseDataBase及其继承</typeparam>
    /// <returns>数据</returns>
    public T? GetData<T>() where T : ResponseDataBase
    {
        if (Data is T t)
        {
            return t;
        }
        if (Data is JsonElement je)
        {
            try
            {
                return je.Deserialize<T>();
            }
            catch
            {
                // fall through to generic convert
            }
        }
        try
        {
            var json = JsonSerializer.Serialize(Data);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch
        {
            return default;
        }
    }
}