using Lenovo.Starview.Manager;
using UnityEngine;

/// <summary>
/// ManagerEventHandle������࣬����ManagerEventHandle�ĸ���
/// </summary>
public abstract class BaseManagerEventHandle : IManagerEventHandle, IPlatformInfo
{
    /// <summary>
    /// ���¼��������͵Ĺ������ʵ��
    /// </summary>
    public abstract IManager manager { get; }

    /// <summary>
    /// �豸ƽ̨����
    /// </summary>
    public abstract PlatformType platformType { get; }

    /// <summary>
    /// ע���¼�����
    /// </summary>
    public abstract void RegisterEventHandle();

    /// <summary>
    /// ִ���¼�����
    /// </summary>
    /// <param name="managerMsg"></param>
    public abstract void ProcessManagerEventHandle(ManagerMsg managerMsg);

    /// <summary>
    /// ȡ��ע���¼�����
    /// </summary>
    public abstract void UnRegisterEventHandle();

}


