using Lenovo.Starview.Manager;
using System;
using UnityEngine;

public abstract class MonManager : MonoBehaviour, IManager
{

    /// <summary>
    /// ��ȡ��Ψһ���¼�������
    /// </summary>
    /// <returns></returns>
    public ManagerEventSystem GetEventSystem()
    {
        return ManagerEventSystem.Instance;
    }


    /// <summary>
    /// ע������ΪMonManager���¼�
    /// </summary>
    /// <typeparam name="MonManager">����</typeparam>
    protected void RegisterEvent<MonManager>()
    {
        GetEventSystem().RegisterEvent<MonManager>(ReceiveEvent);
    }

    /// <summary>
    /// ȡ��ע������ΪMonManager���¼�
    /// </summary>
    /// <typeparam name="MonManager">����</typeparam>
    protected void UnRegisterEvent<MonManager>()
    {
        GetEventSystem().UnRegisterEvent<MonManager>(ReceiveEvent);
    }

    /// <summary>
    /// ��������ΪMonManager���¼�
    /// </summary>
    /// <typeparam name="MonManager">����</typeparam>
    /// <param name="msg">��Ϣ����</param>
    protected void SendEvent<MonManager>(ManagerMsg msg)
    {
        GetEventSystem().SendEvent<MonManager>(msg);
    }

    /// <summary>
    /// ��Ϣ���պ���
    /// </summary>
    /// <param name="msg">��Ϣ����</param>
    protected void ReceiveEvent(ManagerMsg msg)
    {
        ProcessMsg(msg);
    }

    /// <summary>
    /// ��Manager������
    /// </summary>
    public abstract ManagerType managerType { get; }

    /// <summary>
    /// ���ⷢ����Ϣ
    /// </summary>
    /// <param name="msg">��Ϣ����</param>
    public abstract void SendMsg(object msg);

    /// <summary>
    /// ���յ���Ϣ����
    /// </summary>
    /// <param name="msg">������Ϣ����</param>
    protected abstract void ProcessMsg(ManagerMsg msg);

    /// <summary>
    /// Unity���߳�OnDestroy
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (Application.isPlaying)
        {
            OnBeforeDestroy();
        }
    }

    /// <summary>
    /// ����ǰ����
    /// </summary>
    protected abstract void OnBeforeDestroy();
}
