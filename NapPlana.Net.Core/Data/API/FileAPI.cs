using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.API;

/// <summary>
/// 获取文件信息-请求
/// </summary>
public class GetFileRequest
{
    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonPropertyName("file_id")]
    public string? FileId { get; set; }

    /// <summary>
    /// 文件路径、URL或Base64
    /// </summary>
    [JsonPropertyName("file")]
    public string? File { get; set; }
}

/// <summary>
/// 获取文件信息-响应
/// </summary>
public class GetFileResponseData : ResponseDataBase
{
    /// <summary>
    /// 本地路径
    /// </summary>
    [JsonPropertyName("file")]
    public string? File { get; set; }

    /// <summary>
    /// 下载URL
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    [JsonPropertyName("file_size")]
    public string? FileSize { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    [JsonPropertyName("file_name")]
    public string? FileName { get; set; }

    /// <summary>
    /// Base64编码
    /// </summary>
    [JsonPropertyName("base64")]
    public string? Base64 { get; set; }
}

/// <summary>
/// 获取群文件下载链接-请求
/// </summary>
public class GetGroupFileUrlRequest
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonPropertyName("group_id")]
    public string GroupId { get; set; } = string.Empty;

    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonPropertyName("file_id")]
    public string FileId { get; set; } = string.Empty;
}

/// <summary>
/// 获取私聊文件下载链接-请求
/// </summary>
public class GetPrivateFileUrlRequest
{
    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonPropertyName("file_id")]
    public string FileId { get; set; } = string.Empty;
}

/// <summary>
/// 获取文件下载链接-响应（群聊/私聊通用）
/// </summary>
public class GetFileUrlResponseData : ResponseDataBase
{
    /// <summary>
    /// 文件下载链接
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
