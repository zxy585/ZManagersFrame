using Lenovo.Starview.Manager;
using System;
using UnityEngine;

public abstract class MonManager : MonoBehaviour, IManager
{

    /// <summary>
    /// 获取到唯一的事件管理器
    /// </summary>
    /// <returns></returns>
    public ManagerEventSystem GetEventSystem()
    {
        return ManagerEventSystem.Instance;
    }


    /// <summary>
    /// 注册类型为MonManager的事件
    /// </summary>
    /// <typeparam name="MonManager">类型</typeparam>
    protected void RegisterEvent<MonManager>()
    {
        GetEventSystem().RegisterEvent<MonManager>(ReceiveEvent);
    }

    /// <summary>
    /// 取消注册类型为MonManager的事件
    /// </summary>
    /// <typeparam name="MonManager">类型</typeparam>
    protected void UnRegisterEvent<MonManager>()
    {
        GetEventSystem().UnRegisterEvent<MonManager>(ReceiveEvent);
    }

    /// <summary>
    /// 发送类型为MonManager的事件
    /// </summary>
    /// <typeparam name="MonManager">类型</typeparam>
    /// <param name="msg">消息参数</param>
    protected void SendEvent<MonManager>(ManagerMsg msg)
    {
        GetEventSystem().SendEvent<MonManager>(msg);
    }

    /// <summary>
    /// 消息接收函数
    /// </summary>
    /// <param name="msg">消息参数</param>
    protected void ReceiveEvent(ManagerMsg msg)
    {
        ProcessMsg(msg);
    }

    /// <summary>
    /// 该Manager的类型
    /// </summary>
    public abstract ManagerType managerType { get; }

    /// <summary>
    /// 对外发送消息
    /// </summary>
    /// <param name="msg">消息参数</param>
    public abstract void SendMsg(object msg);

    /// <summary>
    /// 接收到消息处理
    /// </summary>
    /// <param name="msg">接收消息参数</param>
    protected abstract void ProcessMsg(ManagerMsg msg);

    /// <summary>
    /// Unity主线程OnDestroy
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (Application.isPlaying)
        {
            OnBeforeDestroy();
        }
    }

    /// <summary>
    /// 销毁前处理
    /// </summary>
    protected abstract void OnBeforeDestroy();
}
