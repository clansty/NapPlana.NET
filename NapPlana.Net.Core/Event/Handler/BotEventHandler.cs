using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Data.Event.Meta;
using NapPlana.Core.Data.Event.Notice; // 新增: Notice事件数据

namespace NapPlana.Core.Event.Handler;

public static class BotEventHandler
{
    /// <summary>
    /// 日志通知事件
    /// </summary>
    public static event Action<LogLevel,string>? OnLogReceived;
    
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="logLevel">日志等级</param>
    /// <param name="message">消息</param>
    public static void LogReceived(LogLevel logLevel, string message)
    {
        OnLogReceived?.Invoke(logLevel, message);
    }
    
    #region 元事件
    
    /// <summary>
    /// 机器人 - 上线
    /// </summary>
    public static event Action? OnBotConnected;
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    public static void BotConnected()
    {
        OnBotConnected?.Invoke();
    }
    
    /// <summary>
    /// 机器人 - 心跳
    /// </summary>
    public static event Action<HeartBeatEvent>? OnBotHeartbeat;

    public static void BotHeartbeat(HeartBeatEvent ev)
    {
        OnBotHeartbeat?.Invoke(ev);
    }
    
    /// <summary>
    /// 机器人 - 生命周期事件
    /// </summary>
    public static event Action<LifeCycleEvent>? OnBotLifeCycle;

    public static void BotLifeCycle(LifeCycleEvent ev)
    {
        OnBotLifeCycle?.Invoke(ev);
    }

    #endregion
    

    #region 群聊消息

    /// <summary>
    /// 信息 - 群组
    /// </summary>
    public static event Action<GroupMessageEvent>? OnGroupMessageReceived;
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupMessageReceived(GroupMessageEvent ev)
    {
        OnGroupMessageReceived?.Invoke(ev);
    }

    #endregion

    #region 私信消息

    /// <summary>
    /// 消息 - 私信
    /// </summary>
    public static event Action<PrivateMessageEvent>? OnPrivateMessageReceived;
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void PrivateMessageReceived(PrivateMessageEvent ev)
    {
        OnPrivateMessageReceived?.Invoke(ev);
    }
    
    /// <summary>
    /// 消息 - 私信 - 临时会话
    /// </summary>
    public static event Action<PrivateMessageEvent>? OnPrivateMessageReceivedTemporary;
    public static void PrivateMessageReceivedTemporary(PrivateMessageEvent ev)
    {
        OnPrivateMessageReceivedTemporary?.Invoke(ev);
    }
    
    /// <summary>
    /// 消息 - 私信 - 好友
    /// </summary>
    public static event Action<PrivateMessageEvent>? OnPrivateMessageReceivedFriend;

    public static void PrivateMessageReceivedFriend(PrivateMessageEvent ev)
    {
        OnPrivateMessageReceivedFriend?.Invoke(ev);
    }

    #endregion

    #region 自身发送

    /// <summary>
    /// 消息发送 - 群聊
    /// </summary>
    public static event Action<MessageSentEvent>? OnMessageSentGroup;
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void MessageSentGroup(MessageSentEvent ev)
    {
        OnMessageSentGroup?.Invoke(ev);
    }
    
    /// <summary>
    /// 消息发送 - 私聊
    /// </summary>
    public static event Action<PrivateMessageSentEvent>? OnMessageSentPrivate;
    public static void MessageSentPrivate(PrivateMessageSentEvent ev)
    {
        OnMessageSentPrivate?.Invoke(ev);
    }
    
    /// <summary>
    /// 消息发送 - 私聊 - 临时会话
    /// </summary>
    public static event Action<PrivateMessageSentEvent>? OnMessageSentPrivateTemporary;

    public static void MessageSentTemporary(PrivateMessageSentEvent ev)
    {
        OnMessageSentPrivateTemporary?.Invoke(ev);
    }
    
    /// <summary>
    /// 消息发送 - 私聊 - 好友
    /// </summary>
    public static event Action<PrivateMessageSentEvent>? OnMessageSentPrivateFriend;

    public static void MessageSentPrivateFriend(PrivateMessageSentEvent ev)
    {
        OnMessageSentPrivateFriend?.Invoke(ev);
    }

    #endregion

    #region 通知事件
    // ================== 好友相关 ==================
    /// <summary>
    /// 通知 - 好友添加 (notice.friend_add)
    /// </summary>
    public static event Action<FriendAddNoticeEvent>? OnFriendAddNoticeReceived;

    public static void FriendAddNoticeReceived(FriendAddNoticeEvent ev)
    {
        OnFriendAddNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 好友消息撤回 (notice.friend_recall)
    /// </summary>
    public static event Action<FriendRecallNoticeEvent>? OnFriendRecallNoticeReceived;

    public static void FriendRecallNoticeReceived(FriendRecallNoticeEvent ev)
    {
        OnFriendRecallNoticeReceived?.Invoke(ev);
    }

    // ================== 群管理员 ==================
    /// <summary>
    /// 通知 - 群管理员变动 (总) (notice.group_admin)
    /// </summary>
    public static event Action<GroupAdminNoticeEvent>? OnGroupAdminNoticeReceived;

    public static void GroupAdminNoticeReceived(GroupAdminNoticeEvent ev)
    {
        OnGroupAdminNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群管理员设置 (notice.group_admin.set)
    /// </summary>
    public static event Action<GroupAdminNoticeEvent>? OnGroupAdminSetNoticeReceived;

    public static void GroupAdminSetNoticeReceived(GroupAdminNoticeEvent ev)
    {
        OnGroupAdminSetNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群管理员取消 (notice.group_admin.unset)
    /// </summary>
    public static event Action<GroupAdminNoticeEvent>? OnGroupAdminUnsetNoticeReceived;

    public static void GroupAdminUnsetNoticeReceived(GroupAdminNoticeEvent ev)
    {
        OnGroupAdminUnsetNoticeReceived?.Invoke(ev);
    }

    // ================== 群禁言 ==================
    /// <summary>
    /// 通知 - 群禁言 (总) (notice.group_ban)
    /// </summary>
    public static event Action<GroupBanNoticeEvent>? OnGroupBanNoticeReceived;

    public static void GroupBanNoticeReceived(GroupBanNoticeEvent ev)
    {
        OnGroupBanNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群禁言 - 禁言 (notice.group_ban.ban)
    /// </summary>
    public static event Action<GroupBanNoticeEvent>? OnGroupBanSetNoticeReceived;

    public static void GroupBanSetNoticeReceived(GroupBanNoticeEvent ev)
    {
        OnGroupBanSetNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群禁言 - 解除 (notice.group_ban.lift_ban)
    /// </summary>
    public static event Action<GroupBanNoticeEvent>? OnGroupBanLiftNoticeReceived;

    public static void GroupBanLiftNoticeReceived(GroupBanNoticeEvent ev)
    {
        OnGroupBanLiftNoticeReceived?.Invoke(ev);
    }

    // ================== 群成员名片 ==================
    /// <summary>
    /// 通知 - 群成员名片更新 (notice.group_card)
    /// </summary>
    public static event Action<GroupCardEvent>? OnGroupCardNoticeReceived;

    public static void GroupCardNoticeReceived(GroupCardEvent ev)
    {
        OnGroupCardNoticeReceived?.Invoke(ev);
    }

    // ================== 群成员减少 ==================
    /// <summary>
    /// 通知 - 群成员减少 (总) (notice.group_decrease)
    /// </summary>
    public static event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseNoticeReceived;

    public static void GroupDecreaseNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        OnGroupDecreaseNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群成员减少 - 主动退群 (notice.group_decrease.leave)
    /// </summary>
    public static event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseLeaveNoticeReceived;

    public static void GroupDecreaseLeaveNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        OnGroupDecreaseLeaveNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群成员减少 - 成员被踢 (notice.group_decrease.kick)
    /// </summary>
    public static event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseKickNoticeReceived;

    public static void GroupDecreaseKickNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        OnGroupDecreaseKickNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群成员减少 - 登录号被踢 (notice.group_decrease.kick_me)
    /// </summary>
    public static event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseKickMeNoticeReceived;

    public static void GroupDecreaseKickMeNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        OnGroupDecreaseKickMeNoticeReceived?.Invoke(ev);
    }

    // ================== 群成员增加 ==================
    /// <summary>
    /// 通知 - 群成员增加 (总) (notice.group_increase)
    /// </summary>
    public static event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseNoticeReceived;

    public static void GroupIncreaseNoticeReceived(GroupIncreaseNoticeEvent ev)
    {
        OnGroupIncreaseNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群成员增加 - 管理员同意 (notice.group_increase.approve)
    /// </summary>
    public static event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseApproveNoticeReceived;

    public static void GroupIncreaseApproveNoticeReceived(GroupIncreaseNoticeEvent ev)
    {
        OnGroupIncreaseApproveNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群成员增加 - 管理员邀请 (notice.group_increase.invite)
    /// </summary>
    public static event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseInviteNoticeReceived;

    public static void GroupIncreaseInviteNoticeReceived(GroupIncreaseNoticeEvent ev)
    {
        OnGroupIncreaseInviteNoticeReceived?.Invoke(ev);
    }

    // ================== 群消息撤回 ==================
    /// <summary>
    /// 通知 - 群消息撤回 (notice.group_recall)
    /// </summary>
    public static event Action<GroupRecallNoticeEvent>? OnGroupRecallNoticeReceived;

    public static void GroupRecallNoticeReceived(GroupRecallNoticeEvent ev)
    {
        OnGroupRecallNoticeReceived?.Invoke(ev);
    }

    // ================== 群文件上传 ==================
    /// <summary>
    /// 通知 - 群文件上传 (notice.group_upload)
    /// </summary>
    public static event Action<GroupUploadNoticeEvent>? OnGroupUploadNoticeReceived;

    public static void GroupUploadNoticeReceived(GroupUploadNoticeEvent ev)
    {
        OnGroupUploadNoticeReceived?.Invoke(ev);
    }

    // ================== 群精华消息 ==================
    /// <summary>
    /// 通知 - 群精华消息 (总) (notice.essence)
    /// </summary>
    public static event Action<GroupEssenceNoticeEvent>? OnGroupEssenceNoticeReceived;

    public static void GroupEssenceNoticeReceived(GroupEssenceNoticeEvent ev)
    {
        OnGroupEssenceNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群精华消息增加 (notice.essence.add)
    /// </summary>
    public static event Action<GroupEssenceNoticeEvent>? OnGroupEssenceAddNoticeReceived;

    public static void GroupEssenceAddNoticeReceived(GroupEssenceNoticeEvent ev)
    {
        OnGroupEssenceAddNoticeReceived?.Invoke(ev);
    }
    
    /// <summary>
    /// 通知 - 群精华消息移除 (notice.essence.delete)
    /// </summary>
    public static event Action<GroupEssenceNoticeEvent>? OnGroupEssenceDeleteNoticeReceived;

    public static void GroupEssenceDeleteNoticeReceived(GroupEssenceNoticeEvent ev)
    {
        OnGroupEssenceDeleteNoticeReceived?.Invoke(ev);
    }

    // ================== 群消息表情点赞 ==================
    /// <summary>
    /// 通知 - 群消息表情点赞 (notice.group_msg_emoji_like)
    /// </summary>
    public static event Action<GroupMsgEmojiLikeNoticeEvent>? OnGroupMsgEmojiLikeNoticeReceived;

    public static void GroupMsgEmojiLikeNoticeReceived(GroupMsgEmojiLikeNoticeEvent ev)
    {
        OnGroupMsgEmojiLikeNoticeReceived?.Invoke(ev);
    }
    
    // ================== Notify子类型 ==================
    /// <summary>
    /// 通知 - 戳一戳 (notice.notify.poke) - 好友
    /// </summary>
    public static event Action<FriendPokeNoticeEvent>? OnFriendPokeNoticeReceived;

    public static void FriendPokeNoticeReceived(FriendPokeNoticeEvent ev)
    {
        OnFriendPokeNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 戳一戳 (notice.notify.poke) - 群聊
    /// </summary>
    public static event Action<GroupPokeNoticeEvent>? OnGroupPokeNoticeReceived;

    public static void GroupPokeNoticeReceived(GroupPokeNoticeEvent ev)
    {
        OnGroupPokeNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 输入状态更新 (notice.notify.input_status)
    /// </summary>
    public static event Action<InputStatusNoticeEvent>? OnInputStatusNoticeReceived;

    public static void InputStatusNoticeReceived(InputStatusNoticeEvent ev)
    {
        OnInputStatusNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 群成员头衔变更 (notice.notify.title)
    /// </summary>
    public static event Action<GroupTitleEvent>? OnGroupTitleNoticeReceived;

    public static void GroupTitleNoticeReceived(GroupTitleEvent ev)
    {
        OnGroupTitleNoticeReceived?.Invoke(ev);
    }

    /// <summary>
    /// 通知 - 点赞 (notice.notify.profile_like)
    /// </summary>
    public static event Action<ProfileLikeNoticeEvent>? OnProfileLikeNoticeReceived;

    public static void ProfileLikeNoticeReceived(ProfileLikeNoticeEvent ev)
    {
        OnProfileLikeNoticeReceived?.Invoke(ev);
    }

    #endregion
    
}