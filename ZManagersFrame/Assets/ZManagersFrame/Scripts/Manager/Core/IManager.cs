using System;

namespace Lenovo.Starview.Manager
{
    /// <summary>
    /// 支持管理Manager的接口
    /// </summary>
    public interface IManager
    {
        ManagerType managerType { get; }
        ManagerEventSystem GetEventSystem();
    }

    /// <summary>
    /// ManagerEventHandle处理接口
    /// </summary>
    interface IManagerEventHandle
    {
        IManager manager { get; }
        void RegisterEventHandle();
        void ProcessManagerEventHandle(ManagerMsg managerMsg);
        void UnRegisterEventHandle();
    }

    /// <summary>
    /// 平台信息记录
    /// </summary>
    interface IPlatformInfo
    {
        PlatformType platformType { get; }
    }

}



