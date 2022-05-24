using System;

namespace Lenovo.Starview.Manager
{
    /// <summary>
    /// ֧�ֹ���Manager�Ľӿ�
    /// </summary>
    public interface IManager
    {
        ManagerType managerType { get; }
        ManagerEventSystem GetEventSystem();
    }

    /// <summary>
    /// ManagerEventHandle����ӿ�
    /// </summary>
    interface IManagerEventHandle
    {
        IManager manager { get; }
        void RegisterEventHandle();
        void ProcessManagerEventHandle(ManagerMsg managerMsg);
        void UnRegisterEventHandle();
    }

    /// <summary>
    /// ƽ̨��Ϣ��¼
    /// </summary>
    interface IPlatformInfo
    {
        PlatformType platformType { get; }
    }

}



