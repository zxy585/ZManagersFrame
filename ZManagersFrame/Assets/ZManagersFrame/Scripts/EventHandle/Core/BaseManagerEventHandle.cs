using Lenovo.Starview.Manager;
using UnityEngine;

/// <summary>
/// ManagerEventHandle处理基类，所有ManagerEventHandle的父类
/// </summary>
public abstract class BaseManagerEventHandle : IManagerEventHandle, IPlatformInfo
{
    /// <summary>
    /// 该事件处理类型的管理类的实例
    /// </summary>
    public abstract IManager manager { get; }

    /// <summary>
    /// 设备平台类型
    /// </summary>
    public abstract PlatformType platformType { get; }

    /// <summary>
    /// 注册事件处理
    /// </summary>
    public abstract void RegisterEventHandle();

    /// <summary>
    /// 执行事件处理
    /// </summary>
    /// <param name="managerMsg"></param>
    public abstract void ProcessManagerEventHandle(ManagerMsg managerMsg);

    /// <summary>
    /// 取消注册事件处理
    /// </summary>
    public abstract void UnRegisterEventHandle();

}


