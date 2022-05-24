using DesignPattern.SingleTon;
using Lenovo.Starview.Manager;
using System;
using System.Collections.Generic;
using Tool;

/// <summary>
/// ���ڴ���ManagerEvent���͵��¼�������
/// </summary>
public class ManagerEventSystem : Singleton<ManagerEventSystem>, ITypeEventSystem<ManagerMsg>
{
    public ManagerEventSystem()
    {

    }

    /// <summary>
    /// �¼�����
    /// </summary>
    private Dictionary<Type, IManagerEventRegister> mTypeEventDict = DictionaryPool<Type, IManagerEventRegister>.Get();

    /// <summary>
    /// ע���¼�
    /// </summary>
    /// <param name="onReceive"></param>
    /// <typeparam name="T"></typeparam>
    public IDisposable Register<IManager>(Action<ManagerMsg> onReceive)
    {
        return RegisterEvent<IManager>(onReceive);
    }

    /// <summary>
    /// ע���¼�
    /// </summary>
    /// <param name="onReceive"></param>
    /// <typeparam name="T"></typeparam>
    public void UnRegister<IManager>(Action<ManagerMsg> onReceive)
    {
        UnRegisterEvent<IManager>(onReceive);
    }

    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="t"></param>
    /// <typeparam name="T"></typeparam>
    public void Send<IManager>(ManagerMsg t)
    {
        SendEvent<IManager>(t);
    }


    public IDisposable RegisterEvent<IManager>(Action<ManagerMsg> onReceive)
    {
        var type = typeof(IManager);

        IManagerEventRegister registerations = null;

        if (mTypeEventDict.TryGetValue(type, out registerations))
        {
            var reg = registerations as ManagerEventRegister;
            reg.OnReceives += onReceive;
        }
        else
        {
            var reg = new ManagerEventRegister();
            reg.OnReceives += onReceive;
            mTypeEventDict.Add(type, reg);
        }

        return new ManagerEventUnregister<IManager> { OnReceive = onReceive };
    }

    public void UnRegisterEvent<IManager>(Action<ManagerMsg> onReceive)
    {
        var type = typeof(IManager);

        IManagerEventRegister registerations = null;

        if (mTypeEventDict.TryGetValue(type, out registerations))
        {
            var reg = registerations as ManagerEventRegister;
            reg.OnReceives -= onReceive;
        }
    }

    public void SendEvent<IManager>(ManagerMsg e)
    {
        var type = typeof(IManager);

        IManagerEventRegister registerations = null;

        if (mTypeEventDict.TryGetValue(type, out registerations))
        {
            var reg = registerations as ManagerEventRegister;
            reg.OnReceives(e);
        }
    }

    public void OnReceiveEvent(ManagerMsg e)
    {

    }

    public void Clear()
    {
        foreach (var keyValuePair in mTypeEventDict)
        {
            keyValuePair.Value.Dispose();
        }

        mTypeEventDict.Clear();
    }

    public void Dispose()
    {

    }
}

/// <summary>
/// �ͷ�
/// </summary>
public class ManagerEventUnregister<IManager> : IDisposable
{
    public Action<ManagerMsg> OnReceive;

    public void Dispose()
    {
        ManagerEventSystem.Instance.UnRegister<IManager>(OnReceive);
    }
}

/// <summary>
/// �ӿ� ֻ����洢���ֵ���
/// </summary>
interface IManagerEventRegister : IDisposable
{

}

/// <summary>
/// ���ע��
/// </summary>
class ManagerEventRegister : IManagerEventRegister
{

    public Action<ManagerMsg> OnReceives = obj => { };

    public void Dispose()
    {
        OnReceives = null;
    }
}

