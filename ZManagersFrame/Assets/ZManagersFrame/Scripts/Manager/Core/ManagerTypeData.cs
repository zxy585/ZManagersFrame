/// <summary>
/// 统一Manager消息发送格式
/// </summary>
public class ManagerMsg
{
    public ManagerType managerType;
    public object msgData;
}

/// <summary>
/// 平台类型定义
/// </summary>
public enum PlatformType
{
    None,
    UnityEditor,
    Android,
    IOS,
    ARCamera,
    Hololens
}

/// <summary>
/// Manager类型定义
/// </summary>
public enum ManagerType
{
    Test,//测试
    GameLoop,//主线流程
    UI,//UI 
    Network,//网络
    Audio,//音频管理
    Video,//视频管理
    InputModel,//输入模块
    Devices,//设备
}

