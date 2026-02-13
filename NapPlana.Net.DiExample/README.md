# NapPlana.NET DI 示例项目

本项目展示如何使用 NapPlana.NET.DI 依赖注入架构。

## 📋 项目结构

```
NapPlana.Net.DiExample/
├── Program.cs              # 基础示例：事件监听
├── ProgramAdvanced.cs      # 高级示例：消息处理和发送
├── appsettings.json        # 配置文件
└── README.md              # 本文件
```

## 🚀 快速开始

### 1. 配置NapCat

修改 `appsettings.json`：

```json
{
  "NapBot": {
    "SelfId": 123456789,        // 你的Bot QQ号
    "Ip": "127.0.0.1",          // NapCat服务器IP
    "Port": 3001,               // NapCat WebSocket端口
    "Token": ""                 // 访问令牌（可选）
  }
}
```

### 2. 运行示例

#### 基础示例（仅监听事件）
```bash
dotnet run
```

#### 高级示例（消息处理和发送）
将 `Program.cs` 重命名为 `Program.bak`，将 `ProgramAdvanced.cs` 重命名为 `Program.cs`，然后运行：
```bash
dotnet run
```

## 📚 架构说明

### DI架构设计

NapPlana.NET.DI 采用依赖注入架构，职责分离：

- **ConnectionService**: 管理WebSocket连接生命周期（IHostedService）
- **BotContext**: 提供API调用能力（Scoped生命周期）
- **IEventHandler**: 事件处理器（Singleton）
- **IApiHandler**: API响应处理器（Singleton）
- **IEventParser**: 事件解析器（Singleton）

### 生命周期管理

```
应用启动
  ↓
ConnectionService.StartAsync()
  ↓
创建WebSocket连接
  ↓
Bot上线事件
  ↓
[应用运行中]
  ├─ 接收事件 → 触发事件处理器
  └─ 创建Scope → 获取BotContext → 发送消息
  ↓
应用停止
  ↓
ConnectionService.StopAsync()
  ↓
关闭连接，释放BotContext
```

## 💡 使用示例

### 1. 基础事件监听

```csharp
// 添加NapBot服务
builder.Services.AddNapBot(builder.Configuration);

// 获取事件处理器
var eventHandler = host.Services.GetRequiredService<IEventHandler>();

// 监听群消息
eventHandler.OnGroupMessageReceived += async (ev) =>
{
    Console.WriteLine($"收到群消息: {ev.RawMessage}");
};
```

### 2. 发送消息（需要BotContext）

```csharp
// 在事件处理中创建作用域
eventHandler.OnGroupMessageReceived += async (ev) =>
{
    // 创建作用域获取BotContext
    using var scope = host.Services.CreateScope();
    var botContext = scope.ServiceProvider.GetRequiredService<BotContext>();
    
    // 发送消息
    var reply = new GroupMessageSend
    {
        GroupId = ev.GroupId,
        Message = new MessageChainBuilder()
            .Text("Hello!")
            .Build()
    };
    
    await botContext.SendGroupMessageAsync(reply);
};
```

### 3. 使用自定义服务

```csharp
// 注册服务
builder.Services.AddScoped<MessageHandlerService>();

// 在事件中使用
eventHandler.OnGroupMessageReceived += async (ev) =>
{
    using var scope = host.Services.CreateScope();
    var handler = scope.ServiceProvider.GetRequiredService<MessageHandlerService>();
    await handler.HandleGroupMessageAsync(ev);
};

// 自定义服务（依赖注入BotContext）
public class MessageHandlerService
{
    private readonly BotContext _botContext;
    
    public MessageHandlerService(BotContext botContext)
    {
        _botContext = botContext;
    }
    
    public async Task HandleGroupMessageAsync(GroupMessageEvent ev)
    {
        // 使用 _botContext 发送消息
    }
}
```

## 🎯 功能特性

### 基础示例 (Program.cs)
- ✅ 自动连接管理
- ✅ 事件监听
- ✅ 日志集成
- ✅ 优雅关闭

### 高级示例 (ProgramAdvanced.cs)
- ✅ 群消息回复（ping/pong）
- ✅ 私聊消息处理
- ✅ 戳一戳回应
- ✅ 自定义服务注入
- ✅ 错误处理

## 📖 API说明

### BotContext 可用方法

```csharp
// 发送群消息
Task<GroupMessageSendResponseData> SendGroupMessageAsync(GroupMessageSend groupMessage, int timeoutSeconds = 15);

// 发送私聊消息
Task<PrivateMessageSendResponseData> SendPrivateMessageAsync(PrivateMessageSend privateMessage, int timeoutSeconds = 15);

// 发送戳一戳
Task SendPokeAsync(PokeMessageSend pokeMessage);

// 撤回消息
Task DeleteGroupMessageAsync(GroupMessageDelete deleteGroupMessage);
```

### IEventHandler 主要事件

```csharp
// 机器人连接
event Action? OnBotConnected;

// 群消息
event Action<GroupMessageEvent>? OnGroupMessageReceived;

// 私聊消息
event Action<PrivateMessageEvent>? OnPrivateMessageReceived;

// 戳一戳
event Action<GroupPokeNoticeEvent>? OnGroupPokeNoticeReceived;

// 心跳
event Action<HeartBeatEvent>? OnBotHeartbeat;

// 日志
event Action<LogLevel, string>? OnLogReceived;
```

## ⚠️ 注意事项

1. **BotContext生命周期**: BotContext是Scoped生命周期，必须在Scope内使用
2. **连接管理**: ConnectionService自动管理连接，无需手动调用Start/Stop
3. **配置必填项**: SelfId、Ip、Port必须配置
4. **错误处理**: 发送消息时要捕获异常

## 🔧 常见问题

### Q: 如何在事件外使用BotContext？

A: 创建作用域：
```csharp
using var scope = host.Services.CreateScope();
var botContext = scope.ServiceProvider.GetRequiredService<BotContext>();
```

### Q: 连接失败怎么办？

A: 检查：
1. NapCat是否启动
2. IP和端口是否正确
3. Token是否匹配（如果需要）

### Q: 如何添加更多事件处理？

A: 参考 `IEventHandler` 接口定义，订阅相应事件即可。

## 📦 依赖项

- NapPlana.NET.DI - DI架构核心
- Microsoft.Extensions.Hosting - Host支持
- Microsoft.Extensions.Logging.Console - 控制台日志

## 📄 许可证

与 NapPlana.NET 主项目相同

## 🙏 致谢

基于 [NapPlana.NET](https://github.com/LingNc/NapPlana.NET) 开发

